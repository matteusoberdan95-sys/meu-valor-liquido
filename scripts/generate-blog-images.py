"""Gera capas hero do blog (16:9, WebP) com marca d'água da marca Meu Valor Líquido.

Uso:
  pip install Pillow
  python scripts/generate-blog-images.py render              # todas as capas (PIL)
  python scripts/generate-blog-images.py render --slug X     # um post
  python scripts/generate-blog-images.py watermark --input-dir tmp/ai  # pós-processa PNG/JPG da IA
  python scripts/generate-blog-images.py validate            # checa slugs e dimensões
  python scripts/generate-blog-images.py prompt              # prompt para Codex CLI / gerador de imagens
  python scripts/generate-blog-images.py prompt --slug X

Saída: src/WebApp/wwwroot/images/blog/{slug}.webp (1200×675)
"""
from __future__ import annotations

import argparse
import re
import sys
from pathlib import Path

from PIL import Image, ImageDraw, ImageFilter, ImageFont

ROOT = Path(__file__).resolve().parents[1]
OUT_DIR = ROOT / "src" / "WebApp" / "wwwroot" / "images" / "blog"
BRAND_DIR = ROOT / "src" / "WebApp" / "wwwroot" / "images" / "brand"
SEED_FILE = ROOT / "src" / "WebApp" / "Data" / "BlogArticleSeedData.cs"
W, H = 1200, 675

COLORS = {
    "bg": (10, 10, 11),
    "bg2": (18, 18, 20),
    "teal": (89, 219, 199),
    "teal_dark": (0, 168, 150),
    "blue": (59, 130, 246),
    "purple": (168, 85, 247),
    "mint": (20, 184, 166),
    "text": (230, 235, 240),
    "muted": (120, 125, 135),
    "card": (32, 32, 36),
    "card_border": (50, 52, 58),
}

CATEGORY_ACCENT = {
    "Trabalhista": "blue",
    "Fiscal": "purple",
    "Financeiro": "mint",
}

VISUAL_BRIEF: dict[str, str] = {
    "o-que-e-salario-liquido": "holerite dark com destaque bruto vs líquido; setas INSS/IRRF",
    "como-avaliar-proposta-salarial": "duas colunas atual vs proposta; gráfico de ganho no bolso",
    "como-conferir-holerite": "checklist + lupa sobre linhas de desconto no holerite",
    "como-calcular-ferias": "calendário + praia sutil + badge +1/3",
    "como-calcular-rescisao-clt": "documento TRCT estilizado + ícones de verbas",
    "rescisao-clt-vs-trct": "split simulação (tela app) vs documento oficial",
    "como-calcular-inss": "tabela progressiva / degraus com teto previdenciário",
    "entenda-o-irrf": "imposto retido, faixas, dedução de dependentes",
    "tabela-inss-2026-guia": "tabela INSS estilizada, badge 2026",
    "tabela-irrf-2026-guia": "tabela IRRF dark, badge 2026",
    "mei-faturamento-e-das": "MEI + limite R$ 81k + DAS mensal",
    "pj-ou-clt-qual-melhor": "balança ou dois caminhos CLT vs CNPJ",
    "cdb-ou-tesouro-direto-investimentos": "dois cards CDB vs Tesouro Selic; selo liquidez",
    "reserva-emergencia-onde-investir": "escudo + cofre + 6 meses de despesas",
    "como-investir-com-pouco-dinheiro": "moedas pequenas + curva de crescimento + aporte mensal",
    "quanto-cobrar-servicos-pj": "calculadora + nota R$/hora + margem lucro",
    "mei-nota-fiscal-quando-emitir": "documento NFS-e + selo MEI",
    "simples-nacional-pj-guia-iniciantes": "anexos Simples + gráfico alíquota efetiva",
    "guia-decimo-terceiro": "13 estilizado + duas parcelas nov/dez",
    "hora-extra-como-calcular": "relógio + badges +50% / +100%",
    "fgts-guia-completo": "cofre/poupança 8% + multa 40%",
    "desconto-vale-transporte": "cartão transporte + 6%",
    "planejamento-financeiro-com-salario": "gráfico 50-30-20 + carteira",
    "juros-compostos-guia": "curva exponencial crescendo, moedas",
    "financiamento-como-calcular-parcelas": "casa wireframe + parcelas Price/SAC",
}

PROMPT_HEADER = """# Gerar capas hero do blog — Meu Valor Líquido

Gere imagens profissionais 1200×675 (16:9) para os posts listados.
Estilo: tema dark Premium Liquid (#0a0a0b), fintech educacional BR, glassmorphism/3D suave.
Sem texto longo na imagem. Sem stock photo genérico. Sem fundo branco.

Cores de accent por categoria:
- Trabalhista: #3b82f6
- Fiscal: #a855f7
- Financeiro: #14b8a6

Marca primária: teal #59dbc7 / #00a896

Após gerar, salve PNG/JPG em uma pasta e rode:
  python scripts/generate-blog-images.py watermark --input-dir <pasta>

Ou use render local:
  python scripts/generate-blog-images.py render
"""


def load_posts_from_seed() -> list[tuple[str, str, str]]:
    if not SEED_FILE.exists():
        raise FileNotFoundError(f"Seed não encontrado: {SEED_FILE}")
    text = SEED_FILE.read_text(encoding="utf-8")
    pattern = re.compile(
        r'Article\(\s*"([^"]+)",\s*"([^"]+)",\s*"[^"]*",\s*"[^"]*",\s*"([^"]+)"',
        re.MULTILINE,
    )
    posts = [(m.group(1), m.group(2), m.group(3)) for m in pattern.finditer(text)]
    if not posts:
        raise RuntimeError("Nenhum post encontrado em BlogArticleSeedData.cs")
    return posts


def accent(category: str) -> tuple[int, int, int]:
    return COLORS[CATEGORY_ACCENT.get(category, "teal")]


def font(size: int, bold: bool = False) -> ImageFont.FreeTypeFont | ImageFont.ImageFont:
    candidates = [
        "C:/Windows/Fonts/segoeuib.ttf" if bold else "C:/Windows/Fonts/segoeui.ttf",
        "C:/Windows/Fonts/arialbd.ttf" if bold else "C:/Windows/Fonts/arial.ttf",
        "/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf" if bold else "/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf",
    ]
    for path in candidates:
        if Path(path).exists():
            return ImageFont.truetype(path, size)
    return ImageFont.load_default()


def load_logo() -> Image.Image:
    for name in ("logo-horizontal.png", "logo-horizontal.webp"):
        path = BRAND_DIR / name
        if path.exists():
            return Image.open(path).convert("RGBA")
    raise FileNotFoundError(f"Logo não encontrado em {BRAND_DIR}")


def radial_glow(base: Image.Image, cx: int, cy: int, radius: int, color: tuple[int, int, int], alpha: int = 90) -> None:
    glow = Image.new("RGBA", base.size, (0, 0, 0, 0))
    gdraw = ImageDraw.Draw(glow)
    for step in range(radius, 0, -8):
        a = int(alpha * (step / radius) ** 2)
        gdraw.ellipse((cx - step, cy - step, cx + step, cy + step), fill=(*color, a))
    glow = glow.filter(ImageFilter.GaussianBlur(18))
    base.paste(Image.alpha_composite(base.convert("RGBA"), glow).convert("RGB"))


def apply_watermark(img: Image.Image, logo: Image.Image, opacity: float = 0.82) -> Image.Image:
    """Embute gradiente inferior + logo horizontal (canto inferior direito)."""
    base = img.convert("RGBA")
    width, height = base.size
    band_h = max(int(height * 0.24), 120)

    band = Image.new("RGBA", (width, band_h), (0, 0, 0, 0))
    band_draw = ImageDraw.Draw(band)
    for y in range(band_h):
        t = y / max(band_h - 1, 1)
        alpha = int(200 * t)
        band_draw.line([(0, y), (width, y)], fill=(10, 10, 11, alpha))
    base.alpha_composite(band, (0, height - band_h))

    target_w = int(width * 0.20)
    scale = target_w / logo.width
    target_h = max(1, int(logo.height * scale))
    logo_r = logo.resize((target_w, target_h), Image.Resampling.LANCZOS)

    if opacity < 1.0:
        r, g, b, a = logo_r.split()
        a = a.point(lambda p: int(p * opacity))
        logo_r = Image.merge("RGBA", (r, g, b, a))

    pad_x, pad_y = 28, 24
    x = width - target_w - pad_x
    y = height - target_h - pad_y
    base.alpha_composite(logo_r, (x, y))
    return base.convert("RGB")


def draw_grid(draw: ImageDraw.ImageDraw, color: tuple[int, int, int] = (28, 30, 34)) -> None:
    for x in range(0, W, 48):
        draw.line((x, 0, x, H), fill=color, width=1)
    for y in range(0, H, 48):
        draw.line((0, y, W, y), fill=color, width=1)


def rounded_rect(
    draw: ImageDraw.ImageDraw,
    box: tuple[int, int, int, int],
    radius: int,
    fill,
    outline=None,
    width: int = 1,
) -> None:
    draw.rounded_rectangle(box, radius=radius, fill=fill, outline=outline, width=width)


def draw_payslip(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int]) -> None:
    rounded_rect(draw, (x, y, x + 220, y + 280), 16, COLORS["card"], COLORS["card_border"], 2)
    draw.rectangle((x + 24, y + 24, x + 120, y + 36), fill=color)
    for i, w in enumerate([160, 130, 145, 100]):
        draw.rectangle((x + 24, y + 56 + i * 28, x + 24 + w, y + 72 + i * 28), fill=(45, 48, 54))
    rounded_rect(draw, (x + 24, y + 220, x + 196, y + 252), 10, color)


def draw_chart_bars(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int], heights: list[int]) -> None:
    rounded_rect(draw, (x, y, x + 260, y + 200), 16, COLORS["card"], COLORS["card_border"], 2)
    bw, gap = 28, 18
    base_y = y + 170
    for i, h in enumerate(heights):
        bx = x + 24 + i * (bw + gap)
        rounded_rect(draw, (bx, base_y - h, bx + bw, base_y), 6, color)


def draw_table(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int], rows: int = 4, cols: int = 3) -> None:
    rounded_rect(draw, (x, y, x + 280, y + 210), 16, COLORS["card"], COLORS["card_border"], 2)
    cw, rh = 80, 36
    for r in range(rows + 1):
        draw.line((x + 16, y + 20 + r * rh, x + 264, y + 20 + r * rh), fill=COLORS["card_border"])
    for c in range(cols + 1):
        draw.line((x + 16 + c * cw, y + 20, x + 16 + c * cw, y + 20 + rows * rh), fill=COLORS["card_border"])
    draw.rectangle((x + 20, y + 24, x + 16 + cw - 4, y + 20 + rh - 4), fill=color)


def draw_coins(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int], count: int = 3) -> None:
    for i in range(count):
        ox = x + i * 34
        draw.ellipse((ox, y + i * 6, ox + 72, y + 72 + i * 6), fill=color, outline=(255, 255, 255, 40))


def draw_growth_line(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int]) -> None:
    rounded_rect(draw, (x, y, x + 280, y + 180), 16, COLORS["card"], COLORS["card_border"], 2)
    points = [(x + 24, y + 140), (x + 80, y + 110), (x + 140, y + 95), (x + 200, y + 60), (x + 250, y + 40)]
    draw.line(points, fill=color, width=4, joint="curve")
    for px, py in points:
        draw.ellipse((px - 6, py - 6, px + 6, py + 6), fill=color)


def draw_split_cards(draw: ImageDraw.ImageDraw, x: int, y: int, left: tuple[int, int, int], right: tuple[int, int, int]) -> None:
    rounded_rect(draw, (x, y, x + 120, y + 160), 14, COLORS["card"], left, 2)
    rounded_rect(draw, (x + 140, y, x + 260, y + 160), 14, COLORS["card"], right, 2)
    draw.text((x + 36, y + 60), "CLT", fill=left, font=font(28, True))
    draw.text((x + 176, y + 60), "PJ", fill=right, font=font(28, True))


def draw_bus_card(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int]) -> None:
    rounded_rect(draw, (x, y, x + 240, y + 120), 16, COLORS["card"], COLORS["card_border"], 2)
    rounded_rect(draw, (x + 20, y + 36, x + 220, y + 96), 12, color)
    draw.rectangle((x + 36, y + 52, x + 72, y + 68), fill=COLORS["bg"])


def draw_calendar(draw: ImageDraw.ImageDraw, x: int, y: int, color: tuple[int, int, int]) -> None:
    rounded_rect(draw, (x, y, x + 200, y + 200), 16, COLORS["card"], COLORS["card_border"], 2)
    draw.rectangle((x + 16, y + 16, x + 184, y + 52), fill=color)
    for row in range(4):
        for col in range(5):
            cx = x + 28 + col * 30
            cy = y + 68 + row * 28
            draw.ellipse((cx, cy, cx + 14, cy + 14), fill=(50, 54, 60) if (row + col) % 2 else color)


def draw_motif(draw: ImageDraw.ImageDraw, slug: str, color: tuple[int, int, int]) -> None:
    ox, oy = 720, 140
    match slug:
        case "o-que-e-salario-liquido" | "como-conferir-holerite" | "como-avaliar-proposta-salarial":
            draw_payslip(draw, ox, oy, color)
            if slug == "como-avaliar-proposta-salarial":
                draw_payslip(draw, ox - 40, oy + 30, COLORS["mint"])
        case "como-calcular-ferias":
            draw_calendar(draw, ox, oy, color)
        case "como-calcular-rescisao-clt" | "rescisao-clt-vs-trct":
            draw_payslip(draw, ox, oy, color)
            draw_payslip(draw, ox + 50, oy + 40, COLORS["muted"])
        case "como-calcular-inss" | "tabela-inss-2026-guia":
            draw_table(draw, ox, oy, color)
            draw_chart_bars(draw, ox + 40, oy + 60, color, [40, 70, 100, 130])
        case "entenda-o-irrf" | "tabela-irrf-2026-guia":
            draw_table(draw, ox, oy, color, rows=5)
            draw.text((ox + 40, oy + 230), "%", fill=color, font=font(48, True))
        case "pj-ou-clt-qual-melhor":
            draw_split_cards(draw, ox, oy + 20, color, COLORS["mint"])
        case "guia-decimo-terceiro":
            draw_coins(draw, ox + 40, oy + 40, color, 4)
            draw.text((ox + 70, oy + 180), "13", fill=color, font=font(64, True))
        case "juros-compostos-guia" | "planejamento-financeiro-com-salario":
            draw_growth_line(draw, ox, oy + 10, color)
        case "hora-extra-como-calcular":
            draw.ellipse((ox + 40, oy + 20, ox + 200, oy + 180), outline=color, width=4)
            draw.line((ox + 120, oy + 100, ox + 120, oy + 55), fill=color, width=4)
            draw.line((ox + 120, oy + 100, ox + 165, oy + 100), fill=color, width=4)
            draw.text((ox + 210, oy + 150), "+50%", fill=color, font=font(36, True))
        case "financiamento-como-calcular-parcelas":
            rounded_rect(draw, (ox, oy + 60, ox + 200, oy + 180), 8, COLORS["card"], color, 2)
            draw.polygon([(ox + 100, oy + 20), (ox + 60, oy + 70), (ox + 140, oy + 70)], fill=color)
        case "desconto-vale-transporte":
            draw_bus_card(draw, ox + 10, oy + 50, color)
        case "fgts-guia-completo":
            rounded_rect(draw, (ox + 30, oy + 40, ox + 230, oy + 200), 20, COLORS["card"], color, 3)
            draw.text((ox + 72, oy + 95), "FGTS", fill=color, font=font(42, True))
            draw.text((ox + 95, oy + 145), "8%", fill=COLORS["text"], font=font(32, True))
        case "mei-faturamento-e-das":
            rounded_rect(draw, (ox, oy + 40, ox + 240, oy + 200), 16, COLORS["card"], color, 2)
            draw.text((ox + 50, oy + 90), "MEI", fill=color, font=font(48, True))
            draw.text((ox + 42, oy + 145), "DAS", fill=COLORS["text"], font=font(28, True))
        case "cdb-ou-tesouro-direto-investimentos":
            rounded_rect(draw, (ox, oy + 30, ox + 115, oy + 190), 14, COLORS["card"], color, 2)
            rounded_rect(draw, (ox + 125, oy + 30, ox + 240, oy + 190), 14, COLORS["card"], COLORS["teal"], 2)
            draw.text((ox + 28, oy + 95), "CDB", fill=color, font=font(32, True))
            draw.text((ox + 148, oy + 85), "Tes.", fill=COLORS["teal"], font=font(28, True))
        case "reserva-emergencia-onde-investir":
            draw.ellipse((ox + 70, oy + 30, ox + 170, oy + 130), outline=color, width=4)
            draw.polygon([(ox + 120, oy + 55), (ox + 95, oy + 95), (ox + 145, oy + 95)], fill=color)
            rounded_rect(draw, (ox + 30, oy + 150, ox + 210, oy + 210), 12, COLORS["card"], color, 2)
            draw.text((ox + 55, oy + 168), "6 meses", fill=COLORS["text"], font=font(22, True))
        case "como-investir-com-pouco-dinheiro":
            draw_growth_line(draw, ox, oy + 10, color)
            draw_coins(draw, ox + 20, oy + 200, color, 2)
            draw.text((ox + 100, oy + 215), "+R$", fill=color, font=font(24, True))
        case "quanto-cobrar-servicos-pj":
            rounded_rect(draw, (ox + 20, oy + 40, ox + 220, oy + 200), 16, COLORS["card"], color, 2)
            draw.text((ox + 48, oy + 75), "R$/hora", fill=color, font=font(28, True))
            draw_chart_bars(draw, ox + 30, oy + 120, color, [35, 55, 75, 95])
        case "mei-nota-fiscal-quando-emitir":
            rounded_rect(draw, (ox + 30, oy + 35, ox + 210, oy + 195), 14, COLORS["card"], color, 2)
            draw.text((ox + 55, oy + 70), "NFS-e", fill=color, font=font(36, True))
            draw.rectangle((ox + 50, oy + 120, ox + 190, oy + 132), fill=(50, 54, 60))
            draw.rectangle((ox + 50, oy + 145, ox + 150, oy + 157), fill=(50, 54, 60))
        case "simples-nacional-pj-guia-iniciantes":
            draw_table(draw, ox, oy + 20, color, rows=3)
            draw.text((ox + 40, oy + 200), "SN", fill=color, font=font(40, True))
            draw_chart_bars(draw, ox + 50, oy + 60, COLORS["teal"], [30, 50, 70, 90])
        case _:
            draw_chart_bars(draw, ox, oy + 20, color, [50, 80, 120, 90, 140])


def render_cover(slug: str, title: str, category: str, logo: Image.Image) -> Image.Image:
    color = accent(category)
    img = Image.new("RGB", (W, H), COLORS["bg"])
    draw = ImageDraw.Draw(img)

    draw_grid(draw)
    radial_glow(img, 900, 340, 280, color, 110)
    radial_glow(img, 760, 500, 180, COLORS["teal_dark"], 60)
    draw = ImageDraw.Draw(img)

    rounded_rect(draw, (0, H - 8, W, H), 0, color)

    cat_label = category.upper()
    draw.text((64, 72), cat_label, fill=color, font=font(22, True))
    draw.line((64, 108, 64 + len(cat_label) * 12, 108), fill=color, width=3)

    title_font = font(52, True)
    words = title.split()
    lines: list[str] = []
    current = ""
    for word in words:
        trial = f"{current} {word}".strip()
        if draw.textlength(trial, font=title_font) <= 560:
            current = trial
        else:
            if current:
                lines.append(current)
            current = word
    if current:
        lines.append(current)
    if not lines:
        lines = [title[:42]]

    y = 140
    for line in lines[:3]:
        draw.text((64, y), line, fill=COLORS["text"], font=title_font)
        y += 62

    draw_motif(draw, slug, color)
    return apply_watermark(img, logo)


def crop_center_16_9(im: Image.Image) -> Image.Image:
    w, h = im.size
    target_ratio = W / H
    current_ratio = w / h
    if current_ratio > target_ratio:
        new_w = int(h * target_ratio)
        left = (w - new_w) // 2
        box = (left, 0, left + new_w, h)
    else:
        new_h = int(w / target_ratio)
        top = (h - new_h) // 2
        box = (0, top, w, top + new_h)
    return im.crop(box).resize((W, H), Image.Resampling.LANCZOS)


def save_webp(img: Image.Image, path: Path) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    if img.mode != "RGB":
        img = img.convert("RGB")
    img.save(path, "WEBP", quality=86, method=6)


def cmd_render(slugs: set[str] | None) -> int:
    logo = load_logo()
    posts = load_posts_from_seed()
    OUT_DIR.mkdir(parents=True, exist_ok=True)
    count = 0
    for slug, title, category in posts:
        if slugs and slug not in slugs:
            continue
        cover = render_cover(slug, title, category, logo)
        path = OUT_DIR / f"{slug}.webp"
        save_webp(cover, path)
        print(f"OK {path.name} ({path.stat().st_size // 1024} KB)")
        count += 1
    print(f"Geradas {count} capas em {OUT_DIR}")
    return 0


def cmd_watermark(input_dir: Path, slugs: set[str] | None) -> int:
    logo = load_logo()
    posts = {s: (t, c) for s, t, c in load_posts_from_seed()}
    OUT_DIR.mkdir(parents=True, exist_ok=True)
    extensions = {".png", ".jpg", ".jpeg", ".webp"}
    files = [p for p in input_dir.iterdir() if p.suffix.lower() in extensions]
    if not files:
        print(f"Nenhuma imagem em {input_dir}", file=sys.stderr)
        return 1

    count = 0
    for path in sorted(files):
        slug = path.stem
        if slugs and slug not in slugs:
            continue
        if slug not in posts:
            print(f"SKIP {path.name} (slug não está no seed)", file=sys.stderr)
            continue
        im = Image.open(path).convert("RGB")
        im = crop_center_16_9(im)
        im = apply_watermark(im, logo)
        out = OUT_DIR / f"{slug}.webp"
        save_webp(im, out)
        print(f"OK {out.name} ({out.stat().st_size // 1024} KB)")
        count += 1
    print(f"Processadas {count} imagens → {OUT_DIR}")
    return 0 if count else 1


def cmd_validate() -> int:
    posts = load_posts_from_seed()
    missing: list[str] = []
    bad_size: list[str] = []
    for slug, _title, _category in posts:
        path = OUT_DIR / f"{slug}.webp"
        if not path.exists():
            missing.append(slug)
            continue
        with Image.open(path) as im:
            if im.size != (W, H):
                bad_size.append(f"{slug} ({im.size[0]}×{im.size[1]})")
    if missing:
        print("Faltando:", ", ".join(missing))
    if bad_size:
        print("Dimensão incorreta:", ", ".join(bad_size))
    if not missing and not bad_size:
        print(f"OK — {len(posts)} capas em {W}×{H} em {OUT_DIR}")
        return 0
    return 1


def cmd_prompt(slugs: set[str] | None) -> int:
    posts = load_posts_from_seed()
    lines = [PROMPT_HEADER, "", "## Posts", ""]
    lines.append("| slug | categoria | título | direção visual |")
    lines.append("|------|-----------|--------|----------------|")
    for slug, title, category in posts:
        if slugs and slug not in slugs:
            continue
        brief = VISUAL_BRIEF.get(slug, "composição editorial alinhada ao tema do artigo")
        lines.append(f"| {slug} | {category} | {title} | {brief} |")
    lines.extend(
        [
            "",
            "## Entrega",
            "- Salvar como `{slug}.png` ou `{slug}.jpg` em pasta temporária",
            "- Depois: `python scripts/generate-blog-images.py watermark --input-dir <pasta>`",
            "- Validar: `python scripts/generate-blog-images.py validate`",
            "- Testes: `dotnet test tests/MeuValorLiquido.WebApp.Tests --filter BrandAssetsTests`",
        ]
    )
    print("\n".join(lines))
    return 0


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Capas hero do blog Meu Valor Líquido")
    sub = parser.add_subparsers(dest="command", required=True)

    p_render = sub.add_parser("render", help="Gera capas editoriais dark (Pillow)")
    p_render.add_argument("--slug", action="append", dest="slugs", help="Filtrar por slug (repetível)")

    p_wm = sub.add_parser("watermark", help="Aplica marca d'água em imagens da IA")
    p_wm.add_argument("--input-dir", type=Path, required=True, help="Pasta com {slug}.png/jpg")
    p_wm.add_argument("--slug", action="append", dest="slugs", help="Filtrar por slug")

    sub.add_parser("validate", help="Valida slugs e dimensões 1200×675")

    p_prompt = sub.add_parser("prompt", help="Imprime prompt para gerador de imagens (CLI/IA)")
    p_prompt.add_argument("--slug", action="append", dest="slugs", help="Filtrar por slug")

    return parser.parse_args()


def main() -> int:
    args = parse_args()
    slug_set = set(args.slugs) if getattr(args, "slugs", None) else None
    match args.command:
        case "render":
            return cmd_render(slug_set)
        case "watermark":
            return cmd_watermark(args.input_dir, slug_set)
        case "validate":
            return cmd_validate()
        case "prompt":
            return cmd_prompt(slug_set)
        case _:
            return 1


if __name__ == "__main__":
    sys.exit(main())
