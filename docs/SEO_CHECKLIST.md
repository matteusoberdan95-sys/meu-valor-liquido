# SEO Checklist — Meu Valor Líquido

Checklist técnico com **status atual** (Sprint 0) e critérios por tipo de página. Implementações pendentes estão marcadas para **Sprint 1+**.

> Checklist resumido legado: `docs/seo-checklist.md`

---

## Status geral (Sprint 0)

| Item | Status | Arquivo / nota |
|------|--------|----------------|
| URLs amigáveis | OK | Razor Pages (`/calculadoras/{slug}`, `/blog/{slug}`) |
| Title único | OK | `ViewData["Title"]` + sufixo no `_Layout.cshtml` |
| Meta description única | OK | `ViewData["Description"]` |
| Canonical | Parcial | `ViewData["CanonicalPath"]`; fallback usa `Request.Path` |
| Open Graph básico | OK | `og:title`, `og:description`, `og:url`, `og:locale`, `og:image` |
| `og:image` | OK | `/images/og-default.svg` (substituir por PNG 1200×630 em produção) |
| Twitter Card | OK | `summary_large_image` + `twitter:image` |
| `meta robots` por página | OK | Layout + `noindex` em erro e 404 de conteúdo |
| `robots.txt` | OK | `src/WebApp/wwwroot/robots.txt` |
| `sitemap.xml` dinâmico | OK | `Program.cs`; inclui newsletter e mapa do site |
| H1 único | OK | Revisar em novas páginas |
| Breadcrumbs (HTML) | OK | Calculadoras e blog |
| BreadcrumbList JSON-LD | OK | `_JsonLdBreadcrumb.cshtml` |
| FAQPage JSON-LD | OK | `_JsonLdFaq.cshtml` |
| Article JSON-LD | OK | `_JsonLdArticle.cshtml` |
| WebSite JSON-LD | OK | Home |
| Mobile-first | OK | Design Valora |
| Conteúdo abaixo da calculadora | OK | `DataSeeder` + seed EF |

---

## Por tipo de página

### Home (`/`)

- [x] Title e description
- [x] Canonical
- [x] Open Graph
- [x] WebSite schema
- [ ] `og:image` (Sprint 1)
- [x] Indexável

### Listagem de calculadoras (`/calculadoras`)

- [x] Title e description
- [x] Canonical
- [x] Noindex: **não** (indexável)
- [x] Links para todas as ferramentas

### Calculadora (`/calculadoras/{slug}`)

- [x] Title/description do catálogo (`SeoTitle`, `SeoDescription`)
- [x] Canonical por slug
- [x] FAQPage schema
- [x] Breadcrumbs
- [x] H1 = nome da calculadora
- [x] Conteúdo educativo + FAQ
- [ ] BreadcrumbList schema (Sprint 1)

### Blog listagem (`/blog`)

- [x] Metadata
- [x] Indexável

### Artigo (`/blog/{slug}`)

- [x] Article schema
- [x] Categoria, data, tempo de leitura
- [x] Link para calculadora relacionada

### Institucionais

| Página | Rota | Indexável | Conteúdo |
|--------|------|-----------|----------|
| Sobre | `/sobre` | Sim | Expandido (Sprint 14) |
| Contato | `/contato` | Sim | OK |
| Como calculamos | `/como-calculamos` | Sim | Metodologia (Sprint 14) |
| Privacidade | `/politica-de-privacidade` | Sim | LGPD + AdSense/cookies (Sprint 14) |
| Termos | `/termos-de-uso` | Sim | Expandido (Sprint 14) |
| Aviso legal | `/aviso-legal` | Sim | Expandido (Sprint 14) |
| Newsletter | `/newsletter` | Sim | OK no sitemap |

### Páginas que devem ser `noindex` (Sprint 1)

| Página | Motivo |
|--------|--------|
| `/Error` | Conteúdo técnico, sem valor SEO |
| Páginas de teste/dev | Não expostas em produção |

### Páginas futuras (Sprints 2–9)

| Tipo | Exemplo | Requisitos |
|------|---------|------------|
| Salário bruto necessário | `/calculadora-salario-bruto` | Title, FAQ, links internos |
| Faixa salarial | `/salario-liquido/3000` | Conteúdo único, canonical, sitemap |
| CLT x PJ derivadas | `/clt-pj/5000-clt-equivale-a-quanto-pj` | Sem thin content |
| Dúvidas | `/duvidas/{slug}` | FAQPage, link para calculadora |

---

## Sitemap (`/sitemap.xml`)

**Incluído hoje:**

- `/`, `/calculadoras`, `/sobre`, `/contato`, `/blog`
- `/politica-de-privacidade`, `/termos-de-uso`, `/aviso-legal`
- Todas as calculadoras ativas (EF)
- Posts publicados (EF)

**Lacunas (Sprint 1):**

- `/newsletter`
- Páginas programáticas (Sprints 2–3)
- `/duvidas/*` (Sprint 9)

---

## robots.txt

Arquivo estático em `wwwroot/robots.txt`. Deve referenciar `Sitemap: {baseUrl}/sitemap.xml`.

---

## Boas práticas para novas páginas

1. Definir `ViewData["Title"]`, `ViewData["Description"]`, `ViewData["CanonicalPath"]`.
2. Um único H1 por página.
3. Conteúdo original acima da dobra e abaixo da ferramenta.
4. FAQ com 4+ perguntas quando fizer sentido.
5. Links internos para calculadoras relacionadas.
6. Não indexar páginas duplicadas ou vazias.
7. Atualizar sitemap ao publicar nova rota pública.

---

## Testes automatizados existentes

- `tests/MeuValorLiquido.WebApp.Tests/SeoMetadataTests.cs`
- `tests/MeuValorLiquido.WebApp.Tests/BlogContentTests.cs` (sitemap, artigos)

**Expandir na Sprint 1:** canonical, robots meta, cobertura do sitemap.
