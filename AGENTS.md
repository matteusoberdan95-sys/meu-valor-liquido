# AGENTS

Este repositório é alternado entre **Cursor** e **Codex** (máquinas diferentes). Ao continuar um trabalho, siga esta ordem:

1. `git pull origin main` — sempre antes de codar.
2. `README.md`
3. Este arquivo (`AGENTS.md`)
4. `docs/CODING_CONVENTIONS.md` antes de editar código
5. `docs/sprint-plan.md` — seção **Trilha referência no nicho (Sprints 65–67)**
6. `docs/NICHO_REFERENCIA_PLAN.md` — especificação dos Passos 1–3 (hubs, calendário, calculadora)

## Sprint ativa

| Campo | Valor |
|-------|--------|
| **Próxima sprint** | **Sprint 51** (AdSense) quando Google aprovar |
| **Em seguida** | Conteúdo ou calculadora conforme `NICHO_REFERENCIA_PLAN.md` (calendário 6 meses concluído) |
| **Concluída recente** | Sprint 68 (calendário editorial) · Sprint 67 · Sprint 66 · Sprint 65 |
| **Paralelo permitido** | Sprint 51 (AdSense) quando Google aprovar |
| **Bloqueada** | Sprint 51 — aguardar aprovação Google |

### Onde começar a implementação (decisão dos agents)

Leia `docs/NICHO_REFERENCIA_PLAN.md` § **Onde os agents começam**. Resumo:

| Prioridade | Sprint | Quem lidera | Quando |
|------------|--------|-------------|--------|
| **Padrão** | 65 | WebApp + SEO | Primeira entrega — páginas indexáveis `/desligamento`, `/negociar-salario`, `/virar-pj` |
| **Paralelo** | 66 (planejamento) | SEO/Content | Pode iniciar calendário e artigos enquanto 65 avança |
| **Após escolha PO** | 67 | Backend/Calculators | PO registra 1 calculadora na matriz antes de codar motor |
| **Se AdSense aprovar** | 51 | Monetization | Intercalar com 65/66; não substitui trilha 65–67 |

**Product Owner:** escolha Sprint 67 registrada — calculadora `seguro-desemprego` (recomendação padrão do plano).

**Não duplicar:** antiga Sprint 32 → Sprint 53; antiga Sprint 33 → Sprint 55; antiga Sprint 34 → Sprint 59.

## Regras de continuidade

- Não mova regra de negócio para `src/WebApp` quando ela pertencer a `src/Modules/Calculators`.
- Não reverta mudanças do usuário sem pedido explícito.
- Ao corrigir calculadoras compartilhadas, revise `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de páginas isoladas.
- Se alterar layout dark, revise `src/WebApp/wwwroot/css/site.css` e preserve o padrão Premium Liquid.
- **Nicho:** não adicionar calculadoras ou artigos fora do funil salário/trabalho — ver regra de corte em `NICHO_REFERENCIA_PLAN.md`.
- Ao **iniciar** uma sprint: marque no `docs/sprint-plan.md` se necessário.
- Ao **concluir** uma sprint: atualize `docs/sprint-plan.md`, `CHANGELOG.md` e esta tabela "Sprint ativa".

## Estado atual importante

- Trilha Stitch 39–46 e trilhas **47–50, 52–59, 60–64 concluídas**.
- **Trilha ativa:** referência no nicho e tráfego orgânico (**Sprints 65–67**).
- Deploy de produção na VPS: `/var/www/meu-valor-liquido` (não `~/meu-valor-liquido`).
- Benchmark fiscal: `CalculatorBenchmarkCatalog` (51 cenários); testes em `CalculatorBenchmarkCatalogTests`.
- UX confiança: `CalculatorFieldTooltipCatalog`, `CalculatorResultWarningBuilder`.
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
