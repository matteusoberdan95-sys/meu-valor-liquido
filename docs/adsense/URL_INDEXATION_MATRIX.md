# Matriz de indexação de URLs

**Revisão:** 17/07/2026  
**Implementação:** `SeoRoutePolicyCatalog`, `SitemapGenerator` e metadados das Razor Pages

Esta matriz define quais grupos de URL podem ser enviados ao Google. Somente rotas canônicas, públicas e com conteúdo próprio entram no sitemap XML.

## URLs indexáveis

| URL ou padrão | Tipo | Indexável | Canonical | Sitemap | Conteúdo aproximado | Autor/fontes | Revisão/lastmod | HTTP esperado | Ação | Prioridade |
|---|---|---:|---|---:|---|---|---|---:|---|---|
| `/` | Home | Sim | `/` | Sim | Hub editorial e de ferramentas | Portal/metodologia | 29/06/2026 | 200 | Manter | P0 |
| `/calculadoras` | Hub | Sim | própria | Sim | Catálogo completo | Portal | 29/06/2026 | 200 | Manter | P0 |
| `/calculadoras/{slug-ativo}` | Ferramenta + editorial | Sim | própria | Sim | 600–1.500 palavras nas prioritárias | Matteus Oberdan/fontes da página | 17/07/2026 nas 12 prioritárias | 200 | Manter somente slugs ativos | P0 |
| `/salario-liquido` | Hub programático | Sim | própria | Sim | Guia e links para faixas | Portal/tabelas fiscais | 29/06/2026 | 200 | Manter | P1 |
| `/salario-liquido/{valor}` e variantes válidas | Página programática | Sim | própria | Sim | 400–900 palavras + resultado | Portal/tabelas fiscais | 29/06/2026 | 200 | Manter catálogo fechado | P1 |
| `/clt-pj` | Hub programático | Sim | própria | Sim | Guia e comparações | Portal/metodologia | 29/06/2026 | 200 | Manter | P1 |
| `/clt-pj/{valor...}` e variantes válidas | Página programática | Sim | própria | Sim | 400–900 palavras + comparação | Portal/metodologia | 29/06/2026 | 200 | Manter catálogo fechado | P1 |
| `/blog` | Hub editorial | Sim | própria | Sim | Lista e categorias | Portal | 29/06/2026 | 200 | Manter sem parâmetros | P0 |
| `/blog/{slug-publicado}` | Artigo | Sim | própria | Sim | Artigo completo | Autor visível/fontes do artigo | Data de publicação | 200 | Manter somente publicados | P0 |
| `/duvidas` | Hub de dúvidas | Sim | própria | Sim | Central temática | Portal | 29/06/2026 | 200 | Manter | P1 |
| `/duvidas/{slug-válido}` | Resposta editorial | Sim | própria | Sim | Resposta, FAQ e relacionados | Portal/fontes relacionadas | 29/06/2026 | 200 | Manter catálogo fechado | P1 |
| `/desligamento`, `/negociar-salario`, `/virar-pj` | Hub temático | Sim | própria | Sim | Jornada, artigos e ferramentas | Portal/fontes relacionadas | 29/06/2026 | 200 | Manter | P1 |
| `/conferir-holerite` | Ferramenta educativa | Sim | própria | Sim | Formulário, método e orientações | Portal/tabelas fiscais | 29/06/2026 | 200 | Manter | P0 |
| `/sobre`, `/autores/matteus-oberdan` | Institucional/autoria | Sim | própria | Sim | Perfil e processo editorial | Matteus Oberdan/LinkedIn | 17/07/2026 | 200 | Manter | P0 |
| `/como-calculamos`, `/politica-editorial` | Metodologia/política | Sim | própria | Sim | Premissas, fontes e revisão | Responsável editorial/fontes oficiais | 17/07/2026 na política | 200 | Manter | P0 |
| `/contato` | Institucional | Sim | própria | Sim | Canal de contato e correção | Portal | 29/06/2026 | 200 | Manter | P0 |
| `/politica-de-privacidade`, `/politica-de-cookies`, `/termos-de-uso`, `/aviso-legal` | Legal | Sim | própria | Sim | Política completa | Portal/normas citadas | 29/06/2026 | 200 | Manter | P0 |
| `/mapa-do-site` | Navegação HTML | Sim | própria | Sim | Links para áreas indexáveis | Portal | 17/07/2026 | 200 | Manter sem utilitários no corpo | P1 |

## URLs fora do índice

| URL ou padrão | Tipo | Robots | Sitemap | HTTP esperado | Motivo e ação |
|---|---|---|---:|---:|---|
| `/assistente` | Conteúdo guiado interativo | `noindex,follow` | Não | 200 | Utilitário; calculadoras e guias são os destinos indexáveis |
| `/meu-painel` | Estado local/pessoal | `noindex,follow` | Não | 200 | Conteúdo depende de `localStorage` |
| `/newsletter` | Formulário/confirmação | `noindex,follow` | Não | 200 | Página transacional, inclusive após POST |
| `/widget` | Gerador de iframe | `noindex,follow` | Não | 200 | Utilitário para publishers |
| `/calculadoras/{slug}?embed=1` | Documento incorporável | `noindex,nofollow` | Não | 200 | Versão duplicada e reduzida da calculadora |
| `/calculadoras/{slug}?…` | Preset/resultado compartilhado | `noindex,follow` | Não | 200 | Estado parametrizado; canonical aponta para a ferramenta |
| `/blog?cat=…` ou qualquer query | Filtro | `noindex,follow` | Não | 200 | Faceta; canonical aponta para `/blog` |
| `/metricas-internas` | Operacional | `noindex,nofollow` | Não | 200 | Métricas agregadas internas |
| `/correcoes` | Processo sem histórico atual | `noindex,follow` | Não | 200 | Indexar somente quando existir histórico útil |
| `/Error` | Erro | `noindex,nofollow` | Não | 500 | Sem canonical |
| `/NotFound` e URLs inexistentes | Erro | `noindex,nofollow` | Não | 404 | Sem canonical; elimina soft 404 |
| `/health`, `/api/*`, `*/resultado.pdf` | Endpoint não HTML | `X-Robots-Tag: noindex, nofollow` | Não | Conforme endpoint | Também bloqueado no `robots.txt` |

## Redirecionamentos e normalização

| Origem | Destino | Status |
|---|---|---:|
| `/calculadora-salario-bruto` | `/calculadoras/salario-bruto-necessario` | 301 |
| `/quanto-preciso-ganhar-para-receber-liquido` | `/calculadoras/salario-bruto-necessario` | 301 |
| `/proposta-salarial`, `/comparar-proposta-salarial` | `/calculadoras/proposta-salarial` | 301 |
| `/clt-vs-pj` | `/clt-pj` | 301 |
| `/painel` | `/meu-painel` | 301 |
| `/incorporar` | `/widget` | 301 |
| `/widget/{slug-válido}` | `/calculadoras/{slug}?embed=1` | 301 |
| `/duvidas/o-que-e-irrf` | `/duvidas/irrf-quem-paga-e-como-calcular` | 301 |
| Caminho com maiúsculas ou barra final | Caminho minúsculo sem barra final | 301 |

Slugs, valores ou variantes fora dos catálogos fechados retornam `404`; não são redirecionados para a home nem para hubs genéricos.
