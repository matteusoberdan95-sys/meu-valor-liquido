# UI/UX — Design Stitch v2 / Valores Públicos

Redesign em [`stitch_redesign_meu_valor_l_quido/`](../stitch_redesign_meu_valor_l_quido/).

A pasta legada `stitch_meu_valor_l_quido_ui_ux/` (Valora v1) permanece apenas como referência histórica.

## Design system canônico

- **Nome:** Valores Públicos
- **Arquivo:** `stitch_redesign_meu_valor_l_quido/valores_p_blicos/DESIGN.md`
- **Primary:** `#00685F` / `#004E47`
- **Fonte:** Inter + Material Symbols Outlined
- **CSS:** `src/WebApp/wwwroot/css/site.css` (classes `valora-*`)

Descartar `lumina_modern/` (variante azul não usada nas telas).

## Brand assets (Gemini)

| Asset | Caminho WebApp |
|-------|----------------|
| Logo horizontal | `wwwroot/images/brand/logo-horizontal.png` |
| Favicon | `wwwroot/favicon.ico`, `images/icons/icon-32.png` |
| Apple touch | `wwwroot/apple-touch-icon.png` |
| OG image | `wwwroot/images/og-default.png` |
| PWA | `images/icons/icon-192.png`, `icon-512.png` |

Origem: `stitch_redesign_meu_valor_l_quido/icons/`

## Mapeamento Stitch → WebApp

| Stitch | WebApp |
|--------|--------|
| `home_meu_valor_l_quido_*` | `Pages/Index.cshtml` |
| `central_de_calculadoras_*` | `Pages/Calculadoras/Index.cshtml` |
| `calculadora_sal_rio_l_quido_*` | `Pages/Calculadoras/Details.cshtml` |
| `comparador_clt_x_pj_*` | `Pages/CltPj/` |
| `faq_hub_*` | `Pages/Duvidas/` |
| `blog_hub_*` / `artigo_*` | `Pages/Blog/` |
| `metodologia_*` / `como_calculamos_*` | `Pages/ComoCalculamos.cshtml` |
| `meu_painel_*` | `Pages/MeuPainel/Index.cshtml` |

Páginas sem mockup Stitch seguem o mesmo design system: Widget, Newsletter, Contato, institucionais, faixas salariais, embed.

## Componentes WebApp

| Padrão Stitch | Implementação |
|---------------|---------------|
| Cards com borda sutil | `.valora-card` + `--accent-*` |
| Chips de filtro | `.valora-filter-chip` |
| Resultado / extrato | `.valora-result-panel` |
| Valores R$ | `.valora-monetary-display` |
| Anúncios | `_AdSlot.cshtml` → “Espaço publicitário” |
| Logo | `_Layout.cshtml` → `logo-horizontal.png` |
