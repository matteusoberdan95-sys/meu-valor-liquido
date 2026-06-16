# Plano: Fidelidade pixel Stitch v2

Objetivo: WebApp **igual** aos mocks em `stitch_redesign_meu_valor_l_quido/`, sem assets Gemini.

## Decisão dos agents (consenso)

| Agent | Papel | Decisão |
|-------|--------|---------|
| **WebApp/Frontend** | Lead | Portar cada `code.html` → Razor + CSS; telas só PNG → implementar olhando `screen.png` |
| **Documentation** | Este arquivo + `ui-ux-stitch.md` | Inventário Stitch→WebApp; critérios de aceite por tela |
| **QA/Test** | Regressão | `GoLiveSmokeTests` + asserts de estrutura HTML (nav, search, bento) por página |
| **SEO/Content** | OG/meta | `og-default.svg` até export Stitch; sem imagens IA |
| **Monetization/AdSense** | Slots | Manter placeholders “Espaço publicitário” nos pontos do mock |
| **Security** | CSP | Evitar URLs `lh3.googleusercontent.com` dos HTMLs Stitch |

### Estratégia técnica (melhor caminho)

1. **Não** copiar Tailwind CDN — mapear classes Stitch → `site.css` usando tokens de `valores_p_blicos/DESIGN.md` (nomes 1:1: `primary-container`, `border-subtle`, etc.).
2. **Fonte de verdade por tela:** `code.html` se existir; senão `screen.png` (desktop + mobile).
3. **Shell global** (todas as páginas mobile): header fixo glass + **bottom nav** (Início / Cálculos / Ajuda / Perfil) — presente em todos os mocks mobile.
4. **Marca:** texto + ícone Material (`water_drop` ou `account_balance_wallet`) — **sem** PNG Gemini; favicon SVG/ICO simples.
5. **Imagens decorativas** dos HTMLs (Google CDN): substituir por blocos CSS ou exportar recortes de `screen.png` para `wwwroot/images/stitch/` (Sprint 22+).
6. **Ordem de implementação** = tráfego + dependências de layout.

## Inventário Stitch → WebApp

| Prioridade | Stitch | WebApp | HTML | Aceite |
|------------|--------|--------|------|--------|
| P0 | Shell (header + bottom nav) | `_Layout.cshtml` | home mobile_1 | Nav fixa; 4 itens; item ativo com pill teal |
| P1 | `home_meu_valor_l_quido_mobile_1` | `Index.cshtml` | sim | Hero, busca, bento 2×2, ad, teaser relatórios |
| P1 | `home_meu_valor_l_quido_desktop` | `Index.cshtml` | PNG | Layout desktop do screen.png |
| P2 | `central_de_calculadoras_*` | `Calculadoras/Index.cshtml` | sim | Bento 12 col, card CLT featured, CTA sugerir |
| P2 | `calculadora_sal_rio_l_quido_*` | `Calculadoras/Details.cshtml` | PNG | Form + painel resultado lado a lado |
| P3 | `comparador_clt_x_pj_cores_atualizadas_mobile` | `CltPj/Comparacao.cshtml` | sim | Inputs CLT/PJ, veredito, PDF/share |
| P3 | `faq_hub_meu_valor_l_quido_mobile` | `Duvidas/Index.cshtml` | sim | Hero teal + busca, grid categorias, accordion |
| P4 | `blog_hub_meu_valor_l_quido_mobile` | `Blog/Index.cshtml` | sim | Destaque 16:9, chips, feed |
| P4 | `artigo_clt_vs_pj_*_mobile` | `Blog/Post.cshtml` | sim | Hero artigo, progress, related |
| P5 | `metodologia_meu_valor_l_quido_mobile` | `ComoCalculamos.cshtml` | sim | Cards regime, tabelas INSS/IRRF |
| P5 | `meu_painel_*` | `MeuPainel/Index.cshtml` | PNG | Lista / empty state do screen |
| P6 | Demais desktop PNGs | várias | PNG | Breakpoints ≥992px |
| P6 | Sem mockup | Widget, Contato, etc. | — | Derivar do shell + tokens |

## Sprints propostas

### Sprint 21 — Shell + remoção Gemini (em andamento)
- Remover `wwwroot/images/brand/`, icons PNG Gemini, `og-default.png`, `apple-touch-icon.png`
- Marca texto + ícone Material; OG `og-default.svg`; favicon legado
- `_BottomNav.cshtml`, header glass Stitch
- `docs/STITCH_FIDELITY_PLAN.md`

### Sprint 22 — Home desktop + Central calculadoras (concluída)

- Home desktop: hero em duas colunas, workspace visual, trust row, bento 4 col, artigos
- `/calculadoras`: bento 12 col, card CLT featured, ad slot, 4 secundários, CTA sugerir, busca `?q=`

### Sprint 23 — Calculadora detail (próxima)
- Layout de `calculadora_sal_rio_l_quido_*` (PNG mobile + desktop)

### Sprint 24 — CLT×PJ + FAQ
- `comparador_clt_x_pj_cores_atualizadas_mobile`
- `faq_hub_mobile`

### Sprint 25 — Blog + Metodologia + Painel
- Blog hub + artigo mobile
- Metodologia + meu painel (PNG)

### Sprint 26 — Desktop polish + QA visual
- Todas as telas desktop PNG
- Checklist manual tela a tela

## Definition of Done (fidelidade)

Para cada tela: comparar lado a lado com `screen.png` ou `code.html` em viewport 390px e 1280px; estrutura DOM e hierarquia visual equivalentes; sem assets Gemini; `dotnet test` verde.
