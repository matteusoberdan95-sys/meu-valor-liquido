"""Gera assets web a partir de src/images-referencia/.

Para capas do blog no estilo dark Valora: python scripts/generate-blog-images.py render
"""
from __future__ import annotations

from pathlib import Path

from PIL import Image, ImageDraw

ROOT = Path(__file__).resolve().parents[1]
REF = ROOT / "src" / "images-referencia"
WWW = ROOT / "src" / "WebApp" / "wwwroot" / "images"

BLOG_MAP = {
    "oque-e-salario-liquido.png": "o-que-e-salario-liquido",
    "calcular-feiras.png": "como-calcular-ferias",
    "calcular-recisao-clt.png": "como-calcular-rescisao-clt",
    "como-calcular-inss.png": "como-calcular-inss",
    "entenda-o-irrf.png": "entenda-o-irrf",
    "pj-ou-clt-qual-melhor.png": "pj-ou-clt-qual-melhor",
    "guia-decimo-terceiro.png": "guia-decimo-terceiro",
    "juros-compostos-guia.png": "juros-compostos-guia",
    "hora-extra-como-calcular.png": "hora-extra-como-calcular",
    "financiamento-como-calcular-parcelas.png": "financiamento-como-calcular-parcelas",
    "tabela-inss-2026-guia.png": "tabela-inss-2026-guia",
    "tabela-irrf-2026-guia.png": "tabela-irrf-2026-guia",
    "desconto-vale-transporte.png": "desconto-vale-transporte",
    "fgts-guia-completo.png": "fgts-guia-completo",
    "planejamento-financeiro-com-salario.png": "planejamento-financeiro-com-salario",
}

LOGO_REGIONS = {
    "logo-horizontal": (304, 126, 1124, 511),
    "logo-stacked": (403, 512, 697, 868),
    "logo-icon": (913, 535, 1120, 813),
}

HOME_HERO_SOURCE = "ChatGPT Image 16 de jun. de 2026, 21_39_55.png"
BRAND_TEAL = (0, 104, 95)


def crop_center_16_9(im: Image.Image, target_w: int, target_h: int) -> Image.Image:
    w, h = im.size
    target_ratio = target_w / target_h
    current_ratio = w / h
    if current_ratio > target_ratio:
        new_w = int(h * target_ratio)
        left = (w - new_w) // 2
        box = (left, 0, left + new_w, h)
    else:
        new_h = int(w / target_ratio)
        top = (h - new_h) // 2
        box = (0, top, w, top + new_h)
    return im.crop(box).resize((target_w, target_h), Image.Resampling.LANCZOS)


def crop_bbox(im: Image.Image, bbox: tuple[int, int, int, int], padding: int = 12) -> Image.Image:
    x0, y0, x1, y1 = bbox
    x0 = max(0, x0 - padding)
    y0 = max(0, y0 - padding)
    x1 = min(im.width - 1, x1 + padding)
    y1 = min(im.height - 1, y1 + padding)
    return im.crop((x0, y0, x1 + 1, y1 + 1))


def save_webp(im: Image.Image, path: Path, quality: int = 86) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    if im.mode not in ("RGB", "RGBA"):
        im = im.convert("RGBA" if "A" in im.getbands() else "RGB")
    im.save(path, "WEBP", quality=quality, method=6)


def process_blog() -> None:
    out_dir = WWW / "blog"
    for filename, slug in BLOG_MAP.items():
        src = REF / filename
        if not src.exists():
            raise FileNotFoundError(src)
        im = Image.open(src).convert("RGB")
        save_webp(crop_center_16_9(im, 1200, 675), out_dir / f"{slug}.webp")


def process_brand() -> None:
    logo = Image.open(REF / "marca-logo.png").convert("RGBA")
    brand_dir = WWW / "brand"
    for name, bbox in LOGO_REGIONS.items():
        save_webp(crop_bbox(logo, bbox), brand_dir / f"{name}.webp")

    icon = crop_bbox(logo, LOGO_REGIONS["logo-icon"], padding=8)
    icon_sizes = [16, 32, 48, 64, 128, 180, 256]
    icons = [icon.resize((s, s), Image.Resampling.LANCZOS) for s in icon_sizes]
    (ROOT / "src" / "WebApp" / "wwwroot" / "favicon.ico").parent.mkdir(parents=True, exist_ok=True)
    icons[0].save(
        ROOT / "src" / "WebApp" / "wwwroot" / "favicon.ico",
        format="ICO",
        sizes=[(s, s) for s in icon_sizes],
    )
    icons[-2].save(ROOT / "src" / "WebApp" / "wwwroot" / "apple-touch-icon.png", "PNG")

    horizontal = crop_bbox(logo, LOGO_REGIONS["logo-horizontal"])
    og = Image.new("RGB", (1200, 630), BRAND_TEAL)
    max_w, max_h = 900, 380
    scale = min(max_w / horizontal.width, max_h / horizontal.height, 1.0)
    sized = horizontal.resize(
        (int(horizontal.width * scale), int(horizontal.height * scale)),
        Image.Resampling.LANCZOS,
    )
    x = (1200 - sized.width) // 2
    y = (630 - sized.height) // 2
    og.paste(sized, (x, y), sized)
    save_webp(og, WWW / "og-default.webp", quality=90)


def process_home_hero() -> None:
    src = REF / HOME_HERO_SOURCE
    if not src.exists():
        raise FileNotFoundError(src)
    im = Image.open(src).convert("RGB")
    save_webp(crop_center_16_9(im, 1200, 675), WWW / "hero" / "home-hero.webp", quality=88)


def main() -> None:
    process_blog()
    process_brand()
    process_home_hero()
    print("Assets gerados em", WWW)


if __name__ == "__main__":
    main()
