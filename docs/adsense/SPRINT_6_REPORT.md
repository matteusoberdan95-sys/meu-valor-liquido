# Relatório da Sprint 6 — validação matemática

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-6`  
**Referência no roadmap do repositório:** Sprint 91

## Objetivo

Comprovar que as calculadoras prioritárias têm cenários de referência, rejeitam entradas inválidas, usam `decimal` com arredondamento explícito e mantêm tabelas fiscais versionadas com vigência.

## Entregas

- Recalibração dos benchmarks em `2026-07-17`.
- `CalculatorEdgeCaseCatalog` com 13 cenários de falha esperada (zero, negativo, inválido, datas invertidas, limites).
- `BrTaxTables2025` preservada em paralelo a `BrTaxTables2026`.
- `BrTaxTableCatalog` resolve competência por data sem sobrescrever anos anteriores.
- `MoneyRounding` documenta e aplica 2 casas com `MidpointRounding.AwayFromZero`.
- Evidências em `docs/adsense/MATH_VALIDATION_EVIDENCE.md`.
- `/como-calculamos` lista vigência das tabelas versionadas.

## Critérios de aceite

- [x] Cálculos prioritários cobertos por testes de benchmark.
- [x] Nenhum `double` armazenando valores monetários no core/`Money`.
- [x] Arredondamento definido explicitamente.
- [x] Tabelas com vigência.
- [x] Build sem avisos e suíte verde.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx -c Release
Core: 5 aprovados
Integration: 1 aprovado
Calculators: 269 aprovados, 1 ignorado
Playwright: 11 aprovados
WebApp: 614 aprovados
Total esperado: ~900 aprovados, 1 ignorado, 0 falhas
```

## Próxima sprint recomendada

Sprint 7 do plano AdSense, registrada como Sprint 92: performance e mobile (Core Web Vitals, CSS/JS, viewports 360/390/412).
