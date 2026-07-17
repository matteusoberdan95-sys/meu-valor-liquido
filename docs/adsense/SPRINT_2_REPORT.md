# Relatório da Sprint 2 — conteúdo das calculadoras

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-2`  
**Referência no roadmap do repositório:** Sprint 87

## Objetivo

Transformar as 12 calculadoras prioritárias em páginas editoriais completas, sem modificar fórmulas e sem escrever manualmente os resultados dos exemplos.

## Entregas

- Modelo `CalculatorEditorialContent` com conteúdo estático e revisável.
- Catálogo específico por slug em `CalculatorEditorialCatalog`.
- Seções visíveis:
  - como o cálculo funciona;
  - o que entra e o que não entra;
  - exemplo prático;
  - interpretação do resultado;
  - erros comuns;
  - fontes oficiais;
  - data e responsável pela revisão;
  - calculadoras relacionadas;
  - aviso educativo.
- Dois FAQs específicos adicionais por calculadora, mesclados ao conteúdo e ao JSON-LD.
- Exemplos processados pelo mesmo `ICalculatorApplicationService` chamado pelo formulário.
- Layout compartilhado responsivo em `_CalculatorEditorialContent.cshtml`.
- Conteúdo longo omitido em widgets `embed=1`.
- Matriz de cobertura em `CONTENT_QUALITY_MATRIX.md`.

## Calculadoras cobertas

1. Salário líquido.
2. Rescisão CLT.
3. Férias.
4. Décimo terceiro.
5. INSS.
6. IRRF.
7. Hora extra.
8. FGTS.
9. PJ vs CLT.
10. Simulador MEI.
11. Juros compostos.
12. Financiamento.

## Decisões técnicas

- O conteúdo editorial fica na WebApp, não no módulo de cálculo, preservando os limites da arquitetura.
- O catálogo não depende da atualização dos registros já existentes no PostgreSQL.
- Inputs dos exemplos são fixos e auditáveis; valores de saída vêm do motor em runtime.
- Se o motor rejeitar um exemplo, o bloco não é renderizado e os testes da Sprint 87 falham.
- O texto não é produzido por IA em runtime.

## Validação

```text
dotnet build MeuValorLiquido.slnx --no-restore
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx --no-build --no-restore
Core: 5 aprovados
Calculators: 243 aprovados, 1 teste gerador ignorado
Integration: 1 aprovado
Playwright: 11 aprovados
WebApp: 582 aprovados
Total: 842 aprovados, 0 falhas, 1 ignorado
```

As fontes citadas pertencem a Planalto, Diário Oficial da União e Banco Central. A checagem automatizada por `curl` sofreu resets/TLS nos portais governamentais, mas os documentos principais foram confirmados por leitura web e já coincidem com fontes usadas nos benchmarks do projeto.

## Critérios de aceite

- [x] As 12 páginas prioritárias têm conteúdo específico.
- [x] Todas apresentam fontes e limitações.
- [x] Todas mostram data e responsável pela revisão.
- [x] Todos os exemplos usam o motor de domínio.
- [x] Todas possuem FAQs específicas.
- [x] Nenhum texto promete resultado garantido.
- [x] Conteúdo editorial não aparece no embed.
- [x] Build sem avisos e suíte verde.

## Riscos restantes

- As sete calculadoras fora da lista prioritária continuam no formato editorial anterior.
- Fontes e textos precisam de nova revisão quando legislação ou tabelas mudarem.
- O responsável editorial ainda não possui página interna própria; isso pertence à Sprint 3.
- Não houve validação jurídica ou contábil externa do conteúdo.

## Próxima sprint recomendada

Sprint 3 do plano AdSense, registrada como Sprint 88: autoria, autoridade e transparência.
