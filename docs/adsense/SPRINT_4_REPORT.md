# Relatório da Sprint 4 — SEO técnico e controle de indexação

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-4`  
**Referência no roadmap do repositório:** Sprint 89

## Objetivo

Garantir que mecanismos de busca encontrem somente páginas públicas, úteis, canônicas e com status HTTP correto.

## Entregas

- `SeoRoutePolicyCatalog` centraliza rotas estáticas indexáveis e páginas utilitárias fora do índice.
- Sitemap XML:
  - exclui assistente, painel, newsletter e widget;
  - já mantém métricas e correções fora;
  - não contém filtros, queries, embeds ou endpoints;
  - consolida slugs duplicados;
  - usa `lastmod` editorial de 17/07/2026 nas 12 calculadoras prioritárias.
- `noindex` aplicado a:
  - assistente;
  - painel local;
  - newsletter e confirmação;
  - widget e embeds;
  - métricas internas;
  - correções sem histórico;
  - buscas e filtros de calculadoras, dúvidas e blog;
  - presets e resultados parametrizados das calculadoras.
- `X-Robots-Tag` aplicado a páginas fora do índice e endpoints operacionais.
- `robots.txt` impede crawl de API, health check e PDFs.
- Todos os aliases conhecidos usam `301`.
- Caminhos com caixa alta ou barra final são normalizados com `301`.
- Slug inexistente de calculadora passou de soft 404 para `404` real.
- `/Error` retorna `500`.
- Páginas 404/500 não emitem canonical.

## Inventário e evidências

- Matriz: `docs/adsense/URL_INDEXATION_MATRIX.md`.
- Auditoria: `docs/adsense/TECHNICAL_SEO_AUDIT.md`.
- Testes: `Sprint89TechnicalSeoTests`.

## Dados estruturados

- `Article` permanece limitado a artigos visíveis.
- `Person` aponta para perfil interno e LinkedIn verificável.
- `FAQPage` usa perguntas e respostas presentes na página.
- `BreadcrumbList` acompanha as trilhas renderizadas.
- Não foram adicionadas avaliações, estrelas ou contadores simulados.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release --no-restore
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx -c Release --no-build --no-restore
Core: 5 aprovados
Calculators: 243 aprovados, 1 teste gerador ignorado
Integration: 1 aprovado
Playwright: 11 aprovados
WebApp: 613 aprovados
Total: 873 aprovados, 0 falhas, 1 ignorado
```

Os 26 cenários específicos de `Sprint89TechnicalSeoTests` também passaram isoladamente.

## Critérios de aceite

- [x] Sitemap sem páginas privadas, utilitárias, parametrizadas ou vazias.
- [x] Canonical absoluto nas páginas indexáveis.
- [x] Nenhuma canonical para a home em conteúdo inexistente.
- [x] URLs duplicadas por barra, caixa e aliases usam `301`.
- [x] 404 real para slugs e variantes inexistentes.
- [x] Erro de servidor retorna 500.
- [x] Schema corresponde ao conteúdo visível.
- [x] Links internos usam `<a href>`.
- [x] Datas do sitemap não mudam automaticamente a cada deploy.
- [x] Build sem avisos e suíte completa verde.

## Riscos restantes

- A verificação de redirects, host canônico e HTTPS precisa ser repetida após o deploy atrás do proxy da VPS.
- Artigos ainda não possuem campo separado de última revisão para `lastmod`.
- Rich Results Test e crawl externo dependem do domínio implantado.
- Consentimento, scripts publicitários e políticas correspondentes pertencem à Sprint 5.

## Próxima sprint recomendada

Sprint 5 do plano AdSense, registrada como Sprint 90: privacidade, cookies e preparação segura do AdSense.
