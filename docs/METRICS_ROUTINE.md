# Rotina semanal de métricas (Sprint 56)

Painel: `/metricas-internas` (noindex, sem PII).

## Quando revisar

- **Toda segunda-feira** (ou após cada deploy relevante).
- Alternar entre **7 dias** (tendência recente) e **30 dias** (visão mensal).

## O que olhar

1. **Ranking de calculadoras** — top 5 por volume de cálculos.
2. **Taxas de engajamento** (sobre o total de cálculos no período):
   - PDF baixado
   - Texto/link compartilhado
   - Simulação salva no painel local
3. **Widget** — views do embed (crescimento orgânico / backlinks).
4. **Lacunas** — calculadora com muitos cálculos e pouco share/PDF pode precisar de CTA ou jornada melhor.

## Como decidir backlog (Sprints 57-58)

| Sinal | Ação sugerida |
|-------|----------------|
| Alta em `salario-liquido` / `proposta-salarial` | Priorizar faixas salariais (Sprint 57) e artigos de proposta (Sprint 58) |
| Alta em `rescisao-clt` com baixo PDF | Reforçar share/PDF no painel de resultado |
| Baixo uso em calculadora fiscal | Revisar SEO e links internos do blog/FAQ |
| Widget views subindo | Expandir CTAs de incorporação (Sprint 57) |

## O que **não** fazer

- Não persistir valores de salário, CPF, e-mail ou IP.
- Não usar estas métricas como promessa comercial ou auditoria fiscal.
- Não comparar com Analytics sem alinhar fuso (painel usa **UTC**).

## Registro rápido (opcional)

Anote em issue ou nota interna:

```
Semana YYYY-MM-DD | período 7d/30d
Top 3: ...
Share %: ... | PDF %: ... | Painel %: ...
Decisão: Sprint X / artigo Y / calculadora Z
```
