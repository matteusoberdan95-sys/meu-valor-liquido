# Matriz de qualidade — calculadoras prioritárias

**Atualização:** 17/07/2026  
**Implementação:** `CalculatorEditorialCatalog` + `_CalculatorEditorialContent.cshtml`

Todas as páginas abaixo possuem resumo, funcionamento, itens incluídos e excluídos, exemplo produzido pelo motor de domínio, interpretação, erros comuns, fontes, revisão, responsável, FAQs específicas, relacionados e aviso educativo.

| Calculadora | Foco editorial específico | Exemplo do domínio | Fontes principais | Revisão | Status |
|---|---|---|---|---|---|
| Salário líquido | Ordem de INSS, IRRF e descontos do holerite | R$ 3.500, sem dependentes | Portaria MPS/MF 13/2026; Lei 15.270/2025 | 17/07/2026 | Completo |
| Rescisão CLT | Verbas por motivo, datas, aviso e FGTS | Dispensa sem justa causa após 12 meses | CLT; Lei do FGTS; Lei 4.090/1962 | 17/07/2026 | Completo |
| Férias | Avos, terço, abono e pagamento em dobro | Férias integrais sobre R$ 3.000 | CLT | 17/07/2026 | Completo |
| Décimo terceiro | Avos, adiantamento e descontos finais | 12 avos sobre R$ 3.000 | Lei 4.090/1962; tabelas tributárias | 17/07/2026 | Completo |
| INSS | Progressividade e teto contributivo | Base de R$ 3.000 | Portaria MPS/MF 13/2026 | 17/07/2026 | Completo |
| IRRF | Diferença entre base tributável e salário bruto | Base de R$ 6.000 | Lei 15.270/2025; Portaria 13/2026 | 17/07/2026 | Completo |
| Hora extra | Divisor, adicional, turno e DSR | 10 horas sobre salário de R$ 3.000 | CLT | 17/07/2026 | Completo |
| FGTS | Depósitos, saldo e multas por desligamento | R$ 3.000 por 12 meses | Lei 8.036/1990 | 17/07/2026 | Completo |
| PJ vs CLT | Líquido, provisões, tributos e despesas | R$ 5.000 CLT versus R$ 9.000 PJ | CLT; LC 123/2006; Lei do FGTS | 17/07/2026 | Completo |
| Simulador MEI | DAS, atividade e limite de faturamento | Serviços com R$ 5.000 mensais | LC 123/2006 | 17/07/2026 | Completo |
| Juros compostos | Capitalização, aportes, taxa e prazo | R$ 1.000 + R$ 200/mês por 12 meses | Calculadora do Cidadão — BCB | 17/07/2026 | Completo |
| Financiamento | Price, SAC, juros totais e CET | R$ 100.000 em 360 meses | Calculadora do Cidadão — BCB | 17/07/2026 | Completo |

## Garantias técnicas

- O texto é estático e revisável; não é gerado por IA em runtime.
- Os valores de saída dos exemplos não ficam escritos no catálogo.
- Cada exemplo chama `ICalculatorApplicationService.Calculate(slug, input)`.
- Falha no motor impede a renderização do exemplo e quebra os testes da Sprint 87.
- FAQs editoriais são mescladas às FAQs do catálogo e usadas no conteúdo visível e no JSON-LD.
- O modo `embed=1` não recebe o conteúdo longo.

## Lacunas fora desta sprint

- As sete calculadoras não prioritárias continuam com o conteúdo anterior.
- A validação jurídica/contábil externa não foi realizada.
- Mudanças de legislação exigem nova revisão e atualização da data somente após conferência real.
