# AGENTS

Este repositório é alternado entre **Cursor** e **Codex** (máquinas diferentes). Ao continuar um trabalho, siga esta ordem:

1. `git pull origin main` — sempre antes de codar.
2. `README.md`
3. Este arquivo (`AGENTS.md`)
4. `docs/CODING_CONVENTIONS.md` antes de editar código
5. `docs/sprint-plan.md` — seção **Trilha diferenciação e crescimento orgânico (Sprints 69–78)**
6. `docs/NICHO_REFERENCIA_PLAN.md` — regra de corte do nicho

## Sprint ativa

| Campo | Valor |
|-------|--------|
| **Próxima sprint** | **Sprint 72** — Comparador visual de propostas |
| **Em seguida** | Sprint 73 (checklist rescisão) · Sprint 70 lote 2 (mais 2 artigos) |
| **Concluída recente** | Sprint 71 (conferir holerite) · Sprint 70 · Sprint 69 |
| **Paralelo permitido** | Sprint 51 (AdSense) quando Google aprovar |
| **Bloqueada** | Sprint 51 — aguardar aprovação Google |

### Onde começar a implementação (decisão dos agents)

Leia `docs/sprint-plan.md` § **Trilha diferenciação (Sprints 69–78)**. Resumo:

| Prioridade | Sprint | Quem lidera | Quando |
|------------|--------|-------------|--------|
| **1 — agora** | 72 | WebApp/Frontend | Comparador visual de propostas salariais |
| **2** | 73–74 | WebApp + SEO | Checklist rescisão + páginas programáticas |
| **Contínuo** | 70 (lote 2+) | SEO/Content | 2 artigos/mês |
| **Se AdSense aprovar** | 51 | Monetization | Intercalar; não substitui trilha 69–78 |

**Não duplicar:** antiga Sprint 32 → Sprint 53; antiga Sprint 33 → Sprint 55; antiga Sprint 34 → Sprint 59.

## Regras de continuidade

- Não mova regra de negócio para `src/WebApp` quando ela pertencer a `src/Modules/Calculators`.
- Não reverta mudanças do usuário sem pedido explícito.
- Ao corrigir calculadoras compartilhadas, revise `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de páginas isoladas.
- Se alterar layout dark, revise `src/WebApp/wwwroot/css/site.css` e preserve o padrão Premium Liquid.
- **Nicho:** não adicionar calculadoras ou artigos fora do funil salário/trabalho — ver regra de corte em `NICHO_REFERENCIA_PLAN.md`.
- PDF: sem anúncios; usar `CalculatorResultPdfGenerator` + `CalculatorPdfInputSummaryBuilder`.
- Conferência de holerite: lógica em `PayslipValidationService`; UI em `/conferir-holerite`.
- Ao **iniciar** uma sprint: marque no `docs/sprint-plan.md` se necessário.
- Ao **concluir** uma sprint: atualize `docs/sprint-plan.md`, `CHANGELOG.md` e esta tabela "Sprint ativa".

## Estado atual importante

- Trilhas **47–71 concluídas** (hubs, editorial, seguro-desemprego, PDF premium, conferir holerite).
- **Trilha ativa:** diferenciação **Sprints 72–78**; editorial contínuo (Sprint 70 em lotes mensais).
- Deploy de produção na VPS: `/var/www/meu-valor-liquido` (não `~/meu-valor-liquido`).
- Benchmark fiscal: `CalculatorBenchmarkCatalog` (51 cenários); testes em `CalculatorBenchmarkCatalogTests`.
- UX confiança: `CalculatorFieldTooltipCatalog`, `CalculatorResultWarningBuilder`, `PayslipValidationService`.
- AdSense: infra pronta, **desligado** (`ADS_ENABLED=false`) até aprovação.

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
