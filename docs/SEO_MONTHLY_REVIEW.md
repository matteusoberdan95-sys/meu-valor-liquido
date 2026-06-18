# Rotina mensal de SEO (Sprint 52)

Complementa `docs/METRICS_ROUTINE.md` (semanal, produto) com revisão de **busca orgânica**.

## Quando revisar

- **Primeira semana de cada mês** (após dados do Search Console consolidarem).
- Ferramentas: [Google Search Console](https://search.google.com/search-console), Analytics (se configurado).

## O que olhar

1. **Impressões altas + CTR baixo** — título ou meta description pouco claros; testar variação educativa (sem clickbait).
2. **Páginas com posição 4–15** — candidatas a artigo de apoio, FAQ ou link interno extra.
3. **Consultas com cauda longa** — criar ou expandir entrada em `/duvidas` ou artigo no blog.
4. **Calculadoras com tráfego mas pouco cálculo** (ver `/metricas-internas`) — reforçar CTA acima da dobra e jornada pós-resultado.

## Checklist mensal

| Passo | Ação |
|-------|------|
| 1 | Exportar top 20 páginas por impressões (28 dias) |
| 2 | Marcar URLs com CTR &lt; 2% e impressões &gt; 500 |
| 3 | Para cada URL: revisar `SeoTitle`, `SeoDescription` e H1 |
| 4 | Adicionar 2–3 links internos relevantes (calculadora ↔ blog ↔ FAQ) |
| 5 | Registrar decisão no backlog (sprint ou artigo) |

## Sinais × ação

| Sinal | Ação sugerida |
|-------|----------------|
| Faixa salarial com impressão alta | Expandir texto editorial único (`SalaryBandEditorialCatalog`) |
| FAQ com muitas impressões | Link para calculadora relacionada no primeiro parágrafo |
| Blog com bounce alto | Incluir bloco "Como validamos" + CTA calculadora |
| Hub `/calculadoras` forte | Destacar calculadora subutilizada no bento |

## O que não fazer

- Não criar páginas duplicadas só para keywords.
- Não prometer consultoria ou resultado oficial.
- Não indexar `/metricas-internas` nem painéis internos.

## Registro rápido

```
Mês YYYY-MM | Search Console 28d
URL com CTR baixo: ...
Ajuste: title / links / artigo
Próxima revisão: ...
```
