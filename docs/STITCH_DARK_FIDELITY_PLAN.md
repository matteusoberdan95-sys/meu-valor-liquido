# Plano de fidelidade — Stitch Dark Premium Liquid

Referência local (gitignored): `stitch_redesing/stitch_meu_valor_l_quido_dark_redesign/`  
Design system: `premium_liquid/DESIGN.md` (dentro da pasta acima)  
Tokens implementados em: `src/WebApp/wwwroot/css/site.css` (`:root` + classes `valora-*`)

## Status geral

As Sprints **39–46** entregaram a fundação dark e cobertura estrutural. Uma **auditoria pixel-a-pixel** (jun/2026) mostrou fidelidade ~60–85% nas telas com mock — insuficiente para “100% fiel”. A trilha **60–64** fecha os gaps restantes com checklist visual 390/1280px.

## Trilha Fidelidade v2 (Sprints 60–64)

| Sprint | Escopo | Status |
|--------|--------|--------|
| **60** | Salário líquido: painel Stitch (donut, % bruto, INSS/IRRF, alíquota efetiva) + home desktop (bento 12 col, metodologia, social proof) | Concluída |
| **61** | Shell global: footer 4 colunas + newsletter inline, tokens `#0A0A0B`, Material Symbols wght 300 — **sem botão Entrar** (divergência intencional: Meu painel) | Concluída |
| **62** | Hubs: central calculadoras (banner premium), blog (newsletter + dica rápida), FAQ CTA suporte | Concluída |
| **63** | Calculadoras-chave: rescisão multi-card + PJ×CLT alinhado ao mock (3 passos visuais ou mock v2) | Concluída |
| **64** | Meu painel + 14 calculadoras template C1 + checklist visual final 390/1280px | Concluída |

**Estimativa:** 5 sprints · 1 dev · ~5–8 semanas (paralelizável 61+62 após 60).

## Sprints 39–46 (fundação — concluída com ressalvas)

| Sprint | Escopo | Status |
|--------|--------|--------|
| **39** | Fundação dark: tokens, shell, brand, home base | Concluída |
| **40** | Shell compartilhado + home polish (footer, forms, blog cards) | Concluída |
| **41** | Central de calculadoras (hub mobile + desktop) | Concluída |
| **42** | Template calculadora detail (17 slugs) | Concluída |
| **43** | Rescisão, PJ×CLT e refinamento mobile fiscal | Concluída |
| **44** | FAQ, blog, artigo, metodologia | Concluída |
| **45** | Meu painel, sobre, contato, newsletter, privacidade | Concluída |
| **46** | Erro, polish final, checklist visual | Concluída* |

\* Checklist manual em [sprint-plan.md](sprint-plan.md) permanece aberto até Sprint 64.

## Mapeamento tela → rota

| Pasta Stitch | Breakpoint | Rota WebApp | Sprint alvo | Status fidelidade |
|--------------|------------|-------------|-------------|-------------------|
| `home_mobile` | mobile | `/` | 60 | ~90% após 60 |
| `home_desktop_dark_premium` | desktop | `/` | 60–61 | ~80% após 60 |
| `central_de_calculadoras_mobile` | mobile | `/calculadoras` | 62 | ~80% |
| `central_de_calculadoras_desktop` | desktop | `/calculadoras` | 62 | ~80% |
| `calculadora_de_sal_rio_l_quido_mobile` | mobile | `/calculadoras/salario-liquido` | 60 | ~85% após 60 |
| `calculadora_de_sal_rio_l_quido_desktop` | desktop | `/calculadoras/salario-liquido` | 60–61 | ~85% após 60 |
| `calculadora_de_rescis_o_desktop` | desktop | `/calculadoras/rescisao-clt` | 63 | ~85% após 63 |
| `comparador_clt_vs_pj_desktop` | desktop | `/calculadoras/pj-vs-clt` | 63 | ~80% após 63 |
| `faq_mobile` | mobile | `/duvidas` | 62 | ~80% |
| `faq_desktop` | desktop | `/duvidas` | 62 | ~80% |
| `blog_desktop` | desktop | `/blog` | 62 | ~70% |
| `artigo_do_blog_desktop` | desktop | `/blog/{slug}` | 62 | ~75% |
| `metodologia_desktop` | desktop | `/como-calculamos` | 64 | ~75% |
| `meu_painel_mobile` | mobile | `/meu-painel` | 64 | ~85% após 64 |
| `meu_painel_desktop` | desktop | `/meu-painel` | 64 | ~85% após 64 |
| `contato_desktop` | desktop | `/contato` | 64 | ~75% |
| `newsletter_desktop` | desktop | `/newsletter` | 61–62 | ~75% |
| `sobre_n_s_desktop` | desktop | `/sobre` | 64 | ~75% |
| `privacidade_e_termos_desktop` | desktop | `/politica-de-privacidade`, `/termos-de-uso` | 64 | ~75% |
| `p_gina_de_erro_desktop` | desktop | `/NotFound`, `/Error` | 64 | ~75% |

### Sem mock Stitch local (derivar do template C1)

As 14 calculadoras restantes, páginas SEO (`/salario-liquido/{valor}`), widget e `/metricas-internas` — Sprint **64** (checklist por slug, sem `screen.png` dedicado).

## Tokens Stitch → CSS

| Stitch | `--valora-*` |
|--------|----------------|
| `background` | `--valora-background` (#0A0A0B) |
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

## Divergências intencionais (produto vs mock Stitch)

| Mock Stitch | Implementação | Motivo |
|-------------|---------------|--------|
| Botão **Entrar** no header | **Meu painel** (sem autenticação) | Site gratuito + AdSense; sem login real |
| Teaser “Relatórios completos” na home | Seção **Nossa Metodologia** | Mock desktop `home_desktop_dark_premium` |
| Wizard PJ×CLT **3 passos** | Wizard **4 passos** (Sprint 59/63) | Funcionalidade ampliada; visual alinhado na Sprint 63 |
| Mock **perfil premium / Entrar** no painel | **Painel local** sem login | Produto gratuito + AdSense |

## Critérios de aceite (Sprints 60–64)

- Comparar com `screen.png` em **390px** (mobile) e **1280px** (desktop) por tela do mapeamento
- Não usar Tailwind CDN — apenas tokens em `site.css`
- `dotnet test MeuValorLiquido.slnx` verde
- Marcar checklist em [sprint-plan.md](sprint-plan.md) item a item ao concluir Sprint 64

## Arquivos principais (v2)

| Área | Arquivo |
|------|---------|
| Resultado salário Stitch | `Pages/Shared/_SalarioLiquidoStitchResult.cshtml` |
| Builder donut / % | `Infrastructure/SalarioLiquidoStitchResultBuilder.cs` |
| Home desktop bento | `Pages/Index.cshtml` |
| Tokens + componentes | `wwwroot/css/site.css` |
| Testes fidelidade | `tests/.../Sprint60FidelityTests.cs` |

## Como usar os mocks localmente

1. Mantenha `stitch_redesing/` na raiz (gitignored).
2. Compare `screen.png` com `http://localhost:8080` em 390px e 1280px.
3. Atualize a coluna **Status fidelidade** neste arquivo ao fechar cada sprint.
