# Como Criar Uma Calculadora

1. Adicione a definicao no catalogo de calculadoras.
2. Crie ou ajuste o calculo no modulo `src/Modules/Calculators`.
3. Garanta saida com `CalculationResult` e `CalculationLineItem`.
4. Adicione validacoes com FluentValidation quando necessario.
5. Crie testes unitarios para regra, arredondamento e limites.
6. Verifique a pagina em `/calculadoras/{slug}`.

Toda calculadora deve ter extrato, explicacao simples, FAQ e aviso legal.

## Benchmarks de Paridade

Ao criar ou alterar formula de calculadora trabalhista/fiscal:

1. Adicione ou atualize cenarios em `src/Modules/Calculators/CalculatorBenchmarkCatalog.cs`.
2. Registre entrada, bruto esperado, liquido esperado, tolerancia, fonte e data de calibracao.
3. Use fonte oficial quando a regra vier de lei, tabela INSS/IRRF, CLT ou outra norma.
4. Para linhas criticas, adicione `CalculatorBenchmarkLineExpectation` (ex.: `INSS`, `IRRF`, `Outros descontos`).
5. Rode `dotnet test tests/MeuValorLiquido.Calculators.Tests/MeuValorLiquido.Calculators.Tests.csproj --filter CalculatorBenchmarkCatalogTests`.

As 10 calculadoras prioritarias devem manter pelo menos 5 cenarios cada: salario liquido, salario bruto necessario, proposta salarial, ferias, 13o, rescisao CLT, INSS, IRRF, FGTS e hora extra.
