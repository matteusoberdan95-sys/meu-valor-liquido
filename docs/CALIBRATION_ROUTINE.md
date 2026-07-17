# Rotina trimestral de calibração (Sprint 52)

Garantir que tabelas fiscais, benchmarks e fontes citadas em `/como-calculamos` permanecem corretas.

## Quando revisar

- **Janeiro, abril, julho e outubro** (ou após publicação de nova portaria/tabela).
- Antes de campanhas sazonais: 13º (nov–dez), férias (dez–fev), IR (mar–abr).

## Checklist técnico

1. **Fontes oficiais**
   - Portaria INSS/IRRF vigente (DOU).
   - Teto MEI e valores DAS (`BrMeiTables2026`).
   - Salário mínimo e faixas (`BrTaxTables2026`).

2. **Benchmarks**
   - Rodar `dotnet test` — `CalculatorBenchmarkCatalogTests` e `Sprint91MathValidationTests` devem permanecer verdes.
   - Atualizar `CalculatorBenchmarkCatalog.CalibrationDate` ao recalibrar cenários.
   - Documentar tolerância e fonte em cada cenário novo.
   - Não sobrescrever `BrTaxTablesYYYY` anteriores; criar novo ano e registrar vigência em `BrTaxTableCatalog`.

3. **Conteúdo**
   - `/como-calculamos` — data de calibração e quantidade de cenários.
   - Artigos do blog que citam valores fixos (MEI, INSS, salário mínimo).
   - FAQs em `/duvidas` com valores anuais.

4. **Produção**
   - Smoke: `SMOKE_BASE_URL=https://seu-dominio dotnet test --filter ProductionSmokeTests`
   - `/health` → `Healthy`
   - Cenário aceite Sprint 47: salário R$ 4.000 + VT + outros → líquido esperado.

## Passos no repositório

```bash
git pull origin main
dotnet test MeuValorLiquido.slnx
# Após ajustar tabelas/benchmarks:
# - src/Modules/Calculators/Tax/*
# - CalculatorBenchmarkCatalog.cs
# - BlogArticleSeedData / PopularQuestionsCatalog se necessário
```

## Registro

```
Trimestre YYYY-Qn | calibrado em YYYY-MM-DD
Tabelas: INSS / IRRF / MEI / ...
Benchmarks alterados: ...
Deploy: sim/não | commit ...
```

## Referências

- `docs/how-to-create-calculator.md` — adicionar benchmark
- `CalculatorBenchmarkCatalog.cs` — cenários nomeados
- `docs/DEPLOY.md` — pós-deploy
