# Auditoria técnica de SEO — Sprint 4

**Data:** 17/07/2026  
**Escopo:** rotas públicas, indexação, canonical, sitemap, redirects, status HTTP, links e dados estruturados

## Resumo executivo

A aplicação já possuía canonical absoluto, sitemap dinâmico, breadcrumbs e schemas compatíveis com várias páginas. A auditoria encontrou, porém, cinco riscos relevantes:

1. calculadora inexistente retornava `200 OK` com mensagem de ausência, caracterizando soft 404;
2. páginas utilitárias ou pessoais estavam indexáveis e presentes no sitemap;
3. aliases conhecidos usavam `302`, mesmo representando substituições permanentes;
4. filtros, presets e resultados por query podiam ser indexados como variações do conteúdo principal;
5. `/Error` podia responder `200` quando acessada diretamente.

Todos os itens foram corrigidos nesta sprint.

## Achados e tratamento

### P0 — soft 404 em calculadora inexistente

- **Antes:** `/calculadoras/nao-existe` renderizava página com definição nula e status `200`.
- **Depois:** GET e POST com slug desconhecido retornam `404`.
- A página de erro mantém `noindex,nofollow` e não emite canonical para URL inexistente.

### P0 — páginas sem valor de busca no sitemap

Foram removidas do sitemap XML:

- `/assistente`;
- `/meu-painel`;
- `/newsletter`;
- `/widget`.

`/metricas-internas` e `/correcoes` já permaneciam fora. As páginas continuam acessíveis ao usuário, mas agora usam política explícita de `noindex`.

### P0 — aliases temporários

Aliases de calculadoras, CLT/PJ, painel, widget e dúvida antiga agora retornam `301`. A normalização de caminho também redireciona caixa alta e barra final para uma única URL minúscula e sem barra final.

### P1 — parâmetros e estados derivados

- Buscas e filtros de `/calculadoras`, `/duvidas` e `/blog` recebem `noindex,follow` e canonical do respectivo hub, inclusive quando não há resultados.
- Calculadoras com preset, resultado compartilhado ou outro query string recebem `noindex,follow` e canonical da ferramenta.
- Embeds continuam em `noindex,nofollow`.

### P1 — endpoints operacionais

`/health`, `/api/*` e PDFs de resultado recebem `X-Robots-Tag: noindex, nofollow`. O `robots.txt` também impede o crawl dessas áreas, sem bloquear páginas HTML que precisam ser acessadas para o crawler ler o `noindex`.

### P1 — sitemap e lastmod

- Rotas estáticas indexáveis foram centralizadas em `SeoRoutePolicyCatalog`.
- Slugs duplicados de calculadoras e artigos são consolidados.
- As 12 calculadoras revisadas na Sprint 87 usam `lastmod` real de 17/07/2026.
- Artigos usam data de publicação disponível no modelo.
- Todas as URLs são absolutas, HTTPS, minúsculas, sem query e sem barra final, exceto a raiz.

## Canonical e status

- O layout continua emitindo canonical absoluto para páginas válidas.
- Páginas de erro não emitem canonical.
- Rotas dinâmicas fora dos catálogos retornam `404`.
- `/Error` retorna `500`.
- Não há redirect de conteúdo inexistente para a home.

## Links e dados estruturados

- Links de navegação e relacionados usam elementos `<a href>`.
- Breadcrumbs em JSON-LD correspondem às trilhas visíveis nas páginas que os exibem.
- `Article` corresponde aos artigos e usa autor `Person` visível.
- `FAQPage` só é incluído onde as perguntas e respostas são renderizadas.
- Não foram encontrados schemas de avaliação, estrelas, depoimentos ou contadores fictícios.
- O mapa HTML mantém no corpo as áreas destinadas à indexação; utilitários continuam acessíveis pela navegação normal.

## Evidências automatizadas

`Sprint89TechnicalSeoTests` cobre:

- páginas `noindex` ausentes do sitemap;
- sitemap único, canônico, absoluto e com `lastmod`;
- aliases e normalização com `301`;
- `404` e `500` reais sem canonical;
- proteção de endpoints operacionais;
- filtros e estados por query fora do índice.

## Riscos restantes

- O modelo de artigo possui data de publicação, mas ainda não possui campo separado de última revisão editorial.
- A validação de schema foi feita por estrutura e testes locais; a verificação externa no Rich Results Test depende do domínio implantado.
- O inventário reflete o código e o ambiente de testes. Após deploy, deve ser feito crawl HTTP do domínio para confirmar proxy, HTTPS, host canônico e redirects na infraestrutura.
