# AGENTS

Este repositório é alternado entre **Cursor** e **Codex** (máquinas diferentes). Ao continuar um trabalho, siga esta ordem:

1. `git pull origin main` — sempre antes de codar.
2. `README.md`
3. Este arquivo (`AGENTS.md`)
4. `docs/CODING_CONVENTIONS.md` antes de editar código
5. `docs/sprint-plan.md` — seção **Trilha Fidelidade Stitch v2 (Sprints 60–64)**

## Sprint ativa

| Campo | Valor |
|-------|--------|
| **Próxima sprint** | **Trilha 60–64 concluída** — manutenção / Sprint 51 (AdSense quando aprovar) |
| **Em seguida** | Sprint 51 (AdSense) ou polish pós-checklist manual |
| **Concluída recente** | Sprint 64 (painel + C1) · Sprint 63 (rescisão/PJ×CLT) |
| **Prioridade alta** | Trilha 60–64 (fidelidade visual 100%) |
| **Bloqueada** | Sprint 51 (AdSense — aguardar aprovação Google) |

**Não duplicar:** antiga Sprint 32 → Sprint 53; antiga Sprint 33 → Sprint 55; antiga Sprint 34 → Sprint 59.

## Regras de continuidade

- Não mova regra de negócio para `src/WebApp` quando ela pertencer a `src/Modules/Calculators`.
- Não reverta mudanças do usuário sem pedido explícito.
- Ao corrigir calculadoras compartilhadas, revise `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de páginas isoladas.
- Se alterar layout dark, revise `src/WebApp/wwwroot/css/site.css` e preserve o padrão Premium Liquid.
- Ao **iniciar** uma sprint: marque no `docs/sprint-plan.md` se necessário.
- Ao **concluir** uma sprint: atualize `docs/sprint-plan.md`, `CHANGELOG.md` e esta tabela "Sprint ativa".

## Estado atual importante

- Trilha Stitch 39–46 e trilhas **47–50, 52–59 concluídas**.
- **Trilha Fidelidade v2 (60–64)** em andamento — ver `docs/STITCH_DARK_FIDELITY_PLAN.md`.
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
