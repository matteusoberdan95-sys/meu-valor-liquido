# AGENTS

Este repositório é alternado entre **Cursor** e **Codex** (máquinas diferentes). Ao continuar um trabalho, siga esta ordem:

1. `git pull origin main` — sempre antes de codar.
2. `README.md`
3. Este arquivo (`AGENTS.md`)
4. `docs/CODING_CONVENTIONS.md` antes de editar código
5. `docs/sprint-plan.md` — seção **Trilha diferenciação e crescimento orgânico (Sprints 69–78)**
6. `docs/NICHO_REFERENCIA_PLAN.md` — regra de corte do nicho
7. **`docs/BLOG_EDITORIAL_PLAN.md`** — ao escrever ou sugerir artigos (Sprint 70+)

## Sprint ativa

| Campo | Valor |
|-------|--------|
| **Próxima sprint** | Sprint 93 — editorial contínuo lote 6 (merge em andamento) |
| **Em seguida** | Editorial contínuo 2 artigos/mês — ver `docs/BLOG_EDITORIAL_PLAN.md` |
| **Concluída recente** | Sprint 92 · Sprint 91 · Sprint 90 · Sprint 89 · Sprint 88 |
| **Paralelo permitido** | Sprint 51 (AdSense) quando Google aprovar |
| **Bloqueada** | Sprint 51 — aguardar aprovação Google |

### Onde começar a implementação (decisão dos agents)

Leia `docs/sprint-plan.md` § **Trilha diferenciação (Sprints 69–78)**. Resumo:

| Prioridade | Sprint | Quem lidera | Quando |
|------------|--------|-------------|--------|
| **1 — agora** | 51 ou 70 (lote 6) | Monetization / SEO | AdSense quando aprovar; editorial contínuo se necessário |
| **Contínuo** | 70 (lote 5+) | SEO/Content | 2 artigos/mês — calendário em `BLOG_EDITORIAL_PLAN.md` |
| **Se AdSense aprovar** | 51 | Monetization | Intercalar; não substitui trilha 69–78 |

**Não duplicar:** antiga Sprint 32 → Sprint 53; antiga Sprint 33 → Sprint 55; antiga Sprint 34 → Sprint 59.

## Regras de continuidade

- Não mova regra de negócio para `src/WebApp` quando ela pertencer a `src/Modules/Calculators`.
- Não reverta mudanças do usuário sem pedido explícito.
- Ao corrigir calculadoras compartilhadas, revise `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de páginas isoladas.
- Se alterar layout dark, revise `src/WebApp/wwwroot/css/site.css` e preserve o padrão Premium Liquid.
- Novos blocos/cards/CTAs devem reaproveitar padrões existentes; não criar estilo isolado sem necessidade.
- **Nicho:** não adicionar calculadoras ou artigos fora do funil salário/trabalho — ver regra de corte em `NICHO_REFERENCIA_PLAN.md`.
- Artigos Sprint 70+: capa WebP obrigatória em `wwwroot/images/blog/{slug}.webp`, brief em `scripts/generate-blog-images.py` e teste de lote.
- PDF: sem anúncios; usar `CalculatorResultPdfGenerator` + `CalculatorPdfInputSummaryBuilder`.
- Conferência de holerite: lógica em `PayslipValidationService`; UI em `/conferir-holerite`.
- Ao **iniciar** uma sprint: marque no `docs/sprint-plan.md` se necessário.
- Ao **concluir** uma sprint: atualize `docs/sprint-plan.md`, `CHANGELOG.md` e esta tabela "Sprint ativa".

## Commits

- Use Conventional Commits em todos os PCs e agentes: `feat:`, `fix:`, `docs:`, `refactor:`, `test:`, `chore:`.
- Escolha o prefixo pelo tipo principal da mudanca:
  - `feat:` nova funcionalidade ou tela.
  - `fix:` correcao de bug, UX quebrada, responsividade, SEO tecnico ou comportamento incorreto.
  - `docs:` documentacao, planos, handoff e instrucoes.
  - `refactor:` reorganizacao interna sem mudanca de comportamento.
  - `test:` somente testes.
  - `chore:` manutencao sem impacto direto no produto.
- Nao criar commits sem prefixo. Exemplo: `fix: melhora banner de cookies e header`.

## Estado atual importante

- Trilhas **47–92 concluídas** + Sprint 70 lote 5.
- **Trilha ativa:** plano de aprovação AdSense (Sprint 93 em merge) + editorial contínuo.
- UX confiança: `CalculatorFieldTooltipCatalog`, `CalculatorResultWarningBuilder`, `PayslipValidationService`.
- AdSense: **desligado** (`ADS_ENABLED=false`); sem placeholders quando inativo. Script externo só após consentimento de Publicidade; verificação usa meta tag. Ativação real continua bloqueada até aprovação.
- Autoridade editorial: `EditorialAuthorCatalog` + `/autores/matteus-oberdan` + `/politica-editorial` + `/correcoes`; manter autoria visível, schema `Person`, LinkedIn e política ao alterar blog/institucional.
- Conteúdo de calculadoras: `CalculatorEditorialCatalog` cobre as 12 páginas prioritárias; exemplos devem continuar usando `ICalculatorApplicationService`, nunca valores de saída escritos manualmente.
- SEO técnico: `SeoRoutePolicyCatalog` é a fonte das rotas estáticas indexáveis; páginas `noindex` não podem voltar ao sitemap e aliases definitivos devem permanecer em `301`.
- Consentimento: `cookie-consent.js` v2 com categorias Essenciais/Analytics/Personalização/Publicidade; política vigente `2026-07-17`; manter alinhado às páginas legais.
- GSC/CTR: Sprint 85 criou `/calculadoras/vale-transporte-hibrido` e reforcou `/blog/vale-transporte-home-office-hibrido`; medir CTR em 7-14 dias antes de novo title/meta. Priorizar paginas com muitas impressoes, posicao media 1-10 e CTR baixo antes de criar pauta nova.

## Comandos úteis

```powershell
git pull origin main
dotnet test .\MeuValorLiquido.slnx
```

```bash
cd /var/www/meu-valor-liquido
git pull origin main
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```
