# Evidências de validação matemática — Sprint 91 (plano AdSense Sprint 6)

**Data de calibração:** 17/07/2026  
**Arredondamento:** `MoneyRounding` — 2 casas, `MidpointRounding.AwayFromZero`  
**Tipo monetário:** `decimal` / `Money` (nunca `double` para valores em R$)  
**Tolerância padrão dos benchmarks:** R$ 0,02  

Os resultados obtidos são validados automaticamente por `CalculatorBenchmarkCatalogTests` e `Sprint91MathValidationTests`.
Este documento é a evidência interna exigida pela Sprint 7 do plano AdSense.

## Tabelas versionadas

| Ano | Vigência | Salário mínimo | Teto INSS | Fonte |
|-----|----------|----------------|-----------|-------|
| 2025 | 01/01/2025 a 31/12/2025 | R$ 1.518,00 | R$ 8.157,41 | Portaria MPS/MF nº 6/2025 + MP 1.294/2025 |
| 2026 | a partir de 01/01/2026 | R$ 1.621,00 | R$ 8.475,55 | Portaria MPS/MF nº 13/2026 + Lei 15.270/2025 |

A tabela de 2025 permanece em `BrTaxTables2025.cs` e **não é sobrescrita** ao atualizar 2026.

## Benchmarks (entrada → esperado)

| Calculadora | Cenário | Bruto esperado | Líquido esperado | Tolerância | Calibrado |
|-------------|---------|----------------|------------------|------------|-----------|
| `salario-liquido` | `salario-minimo-2026` | 1621.00 | 1499.42 | 0.02 | 2026-07-17 |
| `salario-liquido` | `salario-3000-sem-dependentes` | 3000.00 | 2751.40 | 0.02 | 2026-07-17 |
| `salario-liquido` | `salario-4000-com-vt-e-outros` | 4000.00 | 3291.40 | 0.02 | 2026-07-17 |
| `salario-liquido` | `salario-5000-holerite-separado` | 5000.00 | 3848.49 | 0.02 | 2026-07-17 |
| `salario-liquido` | `salario-6000-com-dependente-e-descontos` | 6000.00 | 4626.18 | 0.02 | 2026-07-17 |
| `salario-liquido` | `salario-9000-teto-inss` | 9000.00 | 6717.36 | 0.02 | 2026-07-17 |
| `salario-bruto-necessario` | `liquido-2000` | 2171.08 | 2000.00 | 0.02 | 2026-07-17 |
| `salario-bruto-necessario` | `liquido-3500-com-descontos` | 4191.59 | 3500.00 | 0.02 | 2026-07-17 |
| `salario-bruto-necessario` | `liquido-5000-com-dependente` | 6321.85 | 5000.00 | 0.02 | 2026-07-17 |
| `salario-bruto-necessario` | `liquido-7000-com-dependentes` | 9521.88 | 7000.00 | 0.02 | 2026-07-17 |
| `salario-bruto-necessario` | `liquido-1200` | 1297.30 | 1200.00 | 0.02 | 2026-07-17 |
| `proposta-salarial` | `3000-para-3500` | 3500.00 | 3191.40 | 0.02 | 2026-07-17 |
| `proposta-salarial` | `4000-para-4800-com-vt` | 4800.00 | 4126.49 | 0.02 | 2026-07-17 |
| `proposta-salarial` | `6000-para-7000-com-dependente` | 7000.00 | 5245.17 | 0.02 | 2026-07-17 |
| `proposta-salarial` | `9000-para-10000` | 10000.00 | 7442.36 | 0.02 | 2026-07-17 |
| `proposta-salarial` | `5000-para-4500` | 4500.00 | 4068.49 | 0.02 | 2026-07-17 |
| `ferias` | `integrais-3000` | 4000.00 | 3631.40 | 0.02 | 2026-07-17 |
| `ferias` | `proporcionais-6-avos-3000` | 2000.00 | 1844.32 | 0.02 | 2026-07-17 |
| `ferias` | `integrais-com-abono` | 5000.00 | 4498.49 | 0.02 | 2026-07-17 |
| `ferias` | `dobro-4000` | 10666.67 | 7925.70 | 0.02 | 2026-07-17 |
| `ferias` | `reduzidas-20-dias` | 2222.22 | 2046.54 | 0.02 | 2026-07-17 |
| `decimo-terceiro` | `integral-3000` | 3000.00 | 2751.40 | 0.02 | 2026-07-17 |
| `decimo-terceiro` | `seis-avos-3000` | 1500.00 | 1387.50 | 0.02 | 2026-07-17 |
| `decimo-terceiro` | `integral-5000-com-dependente` | 5000.00 | 4498.49 | 0.02 | 2026-07-17 |
| `decimo-terceiro` | `integral-5000-com-adiantamento` | 5000.00 | 1998.49 | 0.02 | 2026-07-17 |
| `decimo-terceiro` | `tres-avos-salario-minimo` | 405.25 | 374.86 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `pedido-demissao-jan-out` | 3854.17 | 1853.86 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `demissao-sem-justa-causa-12-meses` | 12952.00 | 12590.90 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `acordo-484a-24-meses` | 16400.00 | 15815.72 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `justa-causa-com-ferias-vencidas` | 5000.00 | 4925.00 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `experiencia-antecipada` | 4794.53 | 4670.78 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `aposentadoria-36-meses` | 10500.00 | 6505.72 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `falecimento-empregador` | 10479.47 | 10167.79 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `contrato-prazo-determinado` | 4555.56 | 1879.88 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `experiencia-no-prazo` | 1444.44 | -75.00 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `pedido-demissao-com-aviso` | 5511.11 | 5118.51 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `demissao-24-meses-5000` | 24506.67 | 23756.56 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `demissao-com-adiantamento-13` | 15057.78 | 12613.50 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `demissao-com-media-he` | 15542.40 | 15084.12 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `rescisao-datas-regra-15-dias` | 3222.22 | -175.68 | 0.02 | 2026-07-17 |
| `rescisao-clt` | `acordo-484a-12-meses-saldo` | 10500.00 | 10161.40 | 0.02 | 2026-07-17 |
| `inss` | `salario-minimo` | 1621.00 | 1499.42 | 0.02 | 2026-07-17 |
| `inss` | `faixa-3000` | 3000.00 | 2751.40 | 0.02 | 2026-07-17 |
| `inss` | `faixa-4000` | 4000.00 | 3631.40 | 0.02 | 2026-07-17 |
| `inss` | `teto-8475-55` | 8475.55 | 7487.46 | 0.02 | 2026-07-17 |
| `inss` | `acima-do-teto` | 20000.00 | 19011.91 | 0.02 | 2026-07-17 |
| `irrf` | `base-4000` | 4000.00 | 4000.00 | 0.02 | 2026-07-17 |
| `irrf` | `base-5000` | 5000.00 | 5000.00 | 0.02 | 2026-07-17 |
| `irrf` | `base-6000` | 6000.00 | 5438.48 | 0.02 | 2026-07-17 |
| `irrf` | `base-7350` | 7350.00 | 6237.48 | 0.02 | 2026-07-17 |
| `irrf` | `base-9000-com-2-dependentes` | 9000.00 | 7538.00 | 0.02 | 2026-07-17 |
| `fgts` | `12-meses-demissao` | 2880.00 | 4032.00 | 0.02 | 2026-07-17 |
| `fgts` | `12-meses-acordo` | 2880.00 | 3456.00 | 0.02 | 2026-07-17 |
| `fgts` | `12-meses-pedido-demissao` | 2880.00 | 2880.00 | 0.02 | 2026-07-17 |
| `fgts` | `saldo-informado-demissao` | 19600.00 | 27440.00 | 0.02 | 2026-07-17 |
| `fgts` | `seis-meses-salario-minimo` | 778.08 | 1089.31 | 0.02 | 2026-07-17 |
| `seguro-desemprego` | `salario-minimo-5-parcelas` | 1621.00 | 8105.00 | 0.02 | 2026-07-17 |
| `seguro-desemprego` | `media-2900-5-parcelas` | 2116.66 | 10583.30 | 0.02 | 2026-07-17 |
| `seguro-desemprego` | `teto-4-parcelas` | 2518.65 | 10074.60 | 0.02 | 2026-07-17 |
| `seguro-desemprego` | `piso-3-parcelas` | 1621.00 | 4863.00 | 0.02 | 2026-07-17 |
| `seguro-desemprego` | `pedido-demissao-sem-direito` | 2166.66 | 0.00 | 0.02 | 2026-07-17 |
| `vale-transporte-hibrido` | `oito-dias-custo-menor-que-seis-por-cento` | 128.00 | 128.00 | 0.02 | 2026-07-17 |
| `vale-transporte-hibrido` | `vinte-e-dois-dias-limitado-a-seis-por-cento` | 264.00 | 120.00 | 0.02 | 2026-07-17 |
| `vale-transporte-hibrido` | `dez-dias-holerite-bate` | 200.00 | 200.00 | 0.02 | 2026-07-17 |
| `hora-extra` | `hora-25-10h-50` | 394.23 | 394.23 | 0.02 | 2026-07-17 |
| `hora-extra` | `mensal-3000-10h-50` | 215.03 | 215.03 | 0.02 | 2026-07-17 |
| `hora-extra` | `domingo-3000-8h` | 234.97 | 234.97 | 0.02 | 2026-07-17 |
| `hora-extra` | `noturna-4000-12h` | 394.41 | 394.41 | 0.02 | 2026-07-17 |
| `hora-extra` | `jornada-40h-cct-70` | 225.96 | 225.96 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-5000-anexo-iii` | 17993.96 | 4498.49 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-5000-pj-9000` | 9000.00 | 4498.49 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-3000-anexo-v` | 11005.60 | 2751.40 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-6000-prolabore-35` | 11000.00 | 5058.80 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-4000-despesas-pj` | 8500.00 | 3431.40 | 0.02 | 2026-07-17 |
| `pj-vs-clt` | `clt-8000-anexo-i` | 24307.16 | 6076.79 | 0.02 | 2026-07-17 |

## Casos de borda (devem falhar validação)

| Calculadora | Cenário | Comportamento |
|-------------|---------|---------------|
| `salario-liquido` | `valor-zero` | `Calculators.InvalidInput` |
| `salario-liquido` | `valor-negativo` | `Calculators.InvalidInput` |
| `salario-liquido` | `desconto-negativo` | `Calculators.InvalidInput` |
| `salario-liquido` | `dependentes-negativos` | `Calculators.InvalidInput` |
| `salario-bruto-necessario` | `liquido-zero` | `Calculators.InvalidInput` |
| `proposta-salarial` | `proposta-sem-secundario` | `Calculators.InvalidInput` |
| `ferias` | `meses-invalidos` | `Calculators.InvalidInput` |
| `decimo-terceiro` | `taxa-invalida` | `Calculators.InvalidInput` |
| `rescisao-clt` | `datas-invertidas` | `Calculators.InvalidInput` |
| `inss` | `campo-principal-invalido` | `Calculators.InvalidInput` |
| `irrf` | `base-zero` | `Calculators.InvalidInput` |
| `fgts` | `meses-acima-do-limite` | `Calculators.InvalidInput` |
| `hora-extra` | `jornada-acima-de-44h` | `Calculators.InvalidInput` |

## Cobertura mínima por prioridade

| Slug | Cenários |
|------|----------|
| `salario-liquido` | 6 |
| `salario-bruto-necessario` | 5 |
| `proposta-salarial` | 5 |
| `ferias` | 5 |
| `decimo-terceiro` | 5 |
| `rescisao-clt` | 15 |
| `inss` | 5 |
| `irrf` | 5 |
| `fgts` | 5 |
| `hora-extra` | 5 |

## Como reproduzir

```bash
dotnet test MeuValorLiquido.slnx -c Release --filter "FullyQualifiedName~CalculatorBenchmarkCatalogTests|FullyQualifiedName~Sprint91"
```

