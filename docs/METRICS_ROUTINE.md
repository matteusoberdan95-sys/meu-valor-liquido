# Rotina semanal de métricas (Sprints 56 + 52)

Painel: `/metricas-internas` (noindex, sem PII).

Versão **completa** (Sprint 52): inclui erros HTTP 404/500, falhas de cálculo e bloco de **priorização sugerida**.

## Quando revisar

- **Toda segunda-feira** (ou após cada deploy relevante).
- Alternar entre **7 dias** (tendência recente) e **30 dias** (visão mensal).
- **Mensal:** `docs/SEO_MONTHLY_REVIEW.md` · **Trimestral:** `docs/CALIBRATION_ROUTINE.md`.

## O que olhar

1. **Ranking de calculadoras** — top 5 por volume de cálculos.
2. **Taxas de engajamento** (sobre o total de cálculos no período):
   - PDF baixado
   - Texto/link compartilhado
   - Simulação salva no painel local
3. **Widget** — views do embed (crescimento orgânico / backlinks).
4. **Erros 404** — rotas quebradas ou links externos desatualizados.
5. **Erros 500** — qualquer valor &gt; 0 exige checagem de logs e deploy.
6. **Falhas de cálculo** — validação ou motor; correlacionar com calculadora no ranking de falhas.
7. **Priorização sugerida** — alertas automáticos no painel.
8. **Lacunas** — calculadora com muitos cálculos e pouco share/PDF pode precisar de CTA ou jornada melhor.

## Como decidir backlog (Sprints 57-58)

| Sinal | Ação sugerida |
|-------|----------------|
| Alta em `salario-liquido` / `proposta-salarial` | Priorizar faixas salariais (Sprint 57) e artigos de proposta (Sprint 58) |
| Alta em `rescisao-clt` com baixo PDF | Reforçar share/PDF no painel de resultado |
| Baixo uso em calculadora fiscal | Revisar SEO e links internos do blog/FAQ |
| Widget views subindo | Expandir CTAs de incorporação (Sprint 57) |
| **404 em `/blog/...` ou faixa salarial** | Corrigir link interno ou redirecionamento |
| **Falhas de cálculo &gt; 3%** | Reproduzir formulário; rodar `CalculatorSubmissionSmokeTests` |
| **500 &gt; 0** | Logs Serilog + `/health` + rollback se necessário |

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
