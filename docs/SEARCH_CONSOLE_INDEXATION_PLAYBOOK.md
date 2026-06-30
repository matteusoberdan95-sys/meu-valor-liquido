# Search Console Indexation Playbook

Rotina curta para dominio novo ou lote grande de paginas novas. Use junto com `docs/SEO_MONTHLY_REVIEW.md`.

## Quando usar

- Depois de publicar novas paginas ancoras, hubs, calculadoras ou artigos importantes.
- Quando o sitemap estiver `Processado`, mas paginas prioritarias ainda nao aparecerem no indice.
- Depois de corrigir 404, redirect, canonical ou `noindex` acidental.

## Checklist inicial

1. Confirme que `/robots.txt` permite rastreamento e aponta para `/sitemap.xml`.
2. Confirme que `/sitemap.xml` esta `Processado` no Search Console.
3. Nao reenviar sitemap todo dia. Reenvie apenas se o status estiver com erro ou se a estrutura mudar bastante.
4. Inspecione manualmente so as paginas ancoras, nao todas as URLs programaticas.
5. Se a URL existe, esta 200, sem `noindex` e com canonical proprio, clique em `Solicitar indexacao`.

## URLs prioritarias

Use a Inspecao de URL nesta ordem:

```text
https://meuvalorliquido.com/
https://meuvalorliquido.com/calculadoras
https://meuvalorliquido.com/calculadoras/salario-liquido
https://meuvalorliquido.com/calculadoras/inss
https://meuvalorliquido.com/calculadoras/irrf
https://meuvalorliquido.com/calculadoras/rescisao-clt
https://meuvalorliquido.com/calculadoras/ferias
https://meuvalorliquido.com/calculadoras/decimo-terceiro
https://meuvalorliquido.com/como-calculamos
https://meuvalorliquido.com/desligamento
https://meuvalorliquido.com/negociar-salario
https://meuvalorliquido.com/virar-pj
https://meuvalorliquido.com/blog/o-que-e-salario-liquido
https://meuvalorliquido.com/duvidas/irrf-quem-paga-e-como-calcular
https://meuvalorliquido.com/duvidas/quanto-desconta-inss-2026
```

## Interpretacao rapida

| Status no Search Console | Acao |
|--------------------------|------|
| O URL esta no Google | Marcar OK; nao solicitar novamente sem mudanca relevante |
| O URL nao esta no Google | Solicitar indexacao se a pagina passar no teste ao vivo |
| Descoberta, atualmente nao indexada | Solicitar indexacao; reforcar links internos |
| Rastreada, atualmente nao indexada | Melhorar conteudo, title/H1/FAQ e links internos |
| Solicitacao recusada | Conferir 404, canonical, noindex, robots e sitemap |

## Slugs corrigidos

- `/duvidas/o-que-e-irrf` redireciona permanentemente para `/duvidas/irrf-quem-paga-e-como-calcular`.

## Registro sugerido

```text
Data: YYYY-MM-DD
Sitemap: Processado / Erro
URLs OK:
URLs solicitadas:
URLs com problema:
Acao tomada:
Proxima revisao:
```
