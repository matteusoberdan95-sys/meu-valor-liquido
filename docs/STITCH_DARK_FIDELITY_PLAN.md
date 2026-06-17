# Plano de fidelidade — Stitch Dark Premium Liquid

Referência local (gitignored): `stitch_redesing/stitch_meu_valor_l_quido_dark_redesign/`  
Design system: `premium_liquid/DESIGN.md` (dentro da pasta acima)  
Tokens implementados em: `src/WebApp/wwwroot/css/site.css` (`:root` + classes `valora-*`)  
Sprints: **39–46** em [sprint-plan.md](sprint-plan.md)

## Resumo das sprints

| Sprint | Escopo | Status |
|--------|--------|--------|
| **39** | Fundação dark: tokens, shell, brand, home base | Concluída |
| **40** | Shell compartilhado + home polish (footer, forms, blog cards) | Concluída |
| **41** | Central de calculadoras (hub mobile + desktop) | Concluída |
| **42** | Template calculadora detail (17 slugs) | Concluída |
| **43** | Rescisão, PJ×CLT e refinamento mobile fiscal | Concluída |
| **44** | FAQ, blog, artigo, metodologia | Pendente |
| **45** | Meu painel, sobre, contato, newsletter, privacidade | Pendente |
| **46** | Erro, polish final, checklist visual | Pendente |

## Mapeamento tela → rota

| Pasta Stitch | Breakpoint | Rota WebApp | Sprint | Status |
|--------------|------------|-------------|--------|--------|
| `home_mobile` | mobile | `/` | 39–40 | Concluída |
| `home_desktop_dark_premium` | desktop | `/` | 39–40 | Concluída |
| `central_de_calculadoras_mobile` | mobile | `/calculadoras` | 41 | Concluída |
| `central_de_calculadoras_desktop` | desktop | `/calculadoras` | 41 | Concluída |
| `calculadora_de_sal_rio_l_quido_mobile` | mobile | `/calculadoras/salario-liquido` | 42 | Concluída |
| `calculadora_de_sal_rio_l_quido_desktop` | desktop | `/calculadoras/salario-liquido` | 42 | Concluída |
| `calculadora_de_rescis_o_desktop` | desktop | `/calculadoras/rescisao-clt` | 43 | Concluída |
| `comparador_clt_vs_pj_desktop` | desktop | `/calculadoras/pj-vs-clt` | 43 | Concluída |
| `faq_mobile` | mobile | `/duvidas` | 44 | Pendente |
| `faq_desktop` | desktop | `/duvidas` | 44 | Pendente |
| `blog_desktop` | desktop | `/blog` | 44 | Pendente |
| `artigo_do_blog_desktop` | desktop | `/blog/{slug}` | 44 | Pendente |
| `metodologia_desktop` | desktop | `/como-calculamos` | 44 | Pendente |
| `meu_painel_mobile` | mobile | `/meu-painel` | 45 | Pendente |
| `meu_painel_desktop` | desktop | `/meu-painel` | 45 | Pendente |
| `contato_desktop` | desktop | `/contato` | 45 | Pendente |
| `newsletter_desktop` | desktop | `/newsletter` | 45 | Pendente |
| `sobre_n_s_desktop` | desktop | `/sobre` | 45 | Pendente |
| `privacidade_e_termos_desktop` | desktop | `/privacidade`, `/termos` | 45 | Pendente |
| `p_gina_de_erro_desktop` | desktop | `/Error` | 46 | Pendente |

## Tokens Stitch → CSS

| Stitch | `--valora-*` |
|--------|----------------|
| `background` | `--valora-background` (#131314) |
| `card-low` | `--valora-card-low` (#1C1C1F) |
| `primary` | `--valora-primary` (#59dbc7) |
| `on-primary` | `--valora-on-primary` (#003731) |
| `tertiary` | `--valora-net-result` (#45dfa4) |
| `border-subtle` | `--valora-border-subtle` |
| `text-heading` | `--valora-text-heading` |
| `text-body` | `--valora-text-muted` |
| `badge-labour` | `--valora-trabalhista-blue` |
| `badge-fiscal` | `--valora-fiscal-purple` |
| `badge-financial` | `--valora-financeiro-teal` |
| Glass nav | `--valora-glass` + `backdrop-filter: blur(20px)` |
| Botão primário glow | `--valora-glow-primary` |
| Resultado líquido glow | `--valora-glow-net` |

## Critérios de aceite

- Comparar com `screen.png` em **390px** (mobile) e **1280px** (desktop)
- Não usar Tailwind CDN — apenas tokens em `site.css`
- Manter `GoLiveSmokeTests` verdes
- Cores de resultado líquido sempre em `--valora-net-result` com glow
- Botões primários com `--valora-glow-primary`

## Arquivos principais

| Área | Arquivo |
|------|---------|
| Tokens + componentes | `src/WebApp/wwwroot/css/site.css` |
| Layout | `src/WebApp/Pages/Shared/_Layout.cshtml` |
| Brand | `src/WebApp/Pages/Shared/_BrandMark.cshtml` |
| Bottom nav | `src/WebApp/Pages/Shared/_BottomNav.cshtml` |
| Home | `src/WebApp/Pages/Index.cshtml` |
| Ícones bento | `src/WebApp/Infrastructure/CalculatorUiHelper.cs` |

## Como usar os mocks localmente

1. Mantenha a pasta `stitch_redesing/` na raiz do projeto (não versionada).
2. Abra `code.html` no navegador ou compare `screen.png` lado a lado com `http://localhost:8080`.
3. Ao concluir cada sprint, marque a coluna **Status** neste arquivo como **Concluída**.
