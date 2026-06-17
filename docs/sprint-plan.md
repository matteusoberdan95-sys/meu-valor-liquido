# Sprint Plan

## Sprint 27 — Rescisão CLT: UX e precisão (concluída)

**Objetivo:** aproximar a experiência de calculadoras de referência (datas completas, aviso prévio claro, resultado agrupado) sem quebrar os testes de cálculo existentes.

---

## Sprint 28 — Calibração e expansão das calculadoras (concluída)

**Entregas:**
- [x] 13º proporcional com regra dos 15 dias (calibrado: jan–out/2026 ≈ R$ 1.853,86)
- [x] Motivos extras: experiência (no prazo / antecipado), aposentadoria
- [x] Datas em 13º e férias proporcionais; resultado agrupado em 13º, férias e salário líquido
- [x] Salário líquido com campo separado para outros descontos
- [x] Juros compostos com aporte mensal opcional
- [x] Testes Sprint28 + documentação

---

## Sprint 29 — Financiamento SAC, motivos raros e wizard PJ (concluída)

**Objetivo:** completar lacunas de produto antes do deploy: SAC, benchmark de regressão, motivos raros de rescisão e UX guiada no comparador PJ×CLT.

**Entregas:**
- [x] Financiamento: SAC, comparativo Price x SAC
- [x] Rescisão: falecimento do empregador, término de contrato determinado
- [x] `CalculatorBenchmarkTests` com cenários de referência documentados
- [x] Wizard em 3 passos no comparador PJ×CLT
- [x] Testes e documentação

**Definition of Done:** `dotnet test` verde. Deploy aguarda fim das sprints.

---

## Sprint 21–26 — Fidelidade Stitch (Sprint 26 concluída)

**Entregas:** shell, home, central calculadoras, calculadora detail, comparador CLT×PJ, FAQ hub, blog hub/artigo, metodologia, meu painel e polish desktop (≥992px).

**Próximo:** Sprint 20 — Monetização AdSense pós-aprovação (ou manutenção contínua).

---

## Sprint 20 - Monetização AdSense pós-aprovação (próxima)

**Objetivo:** integrar anúncios reais nos slots existentes após aprovação Google.

**Agents:** Monetization/AdSense, WebApp/Frontend, Security.

**Entregas:**
- Ativar `_AdSenseScript` com `Ads:PublisherId` e slot IDs reais
- `Ads:Enabled=true` apenas em produção via ambiente
- Revisão CSP e cookie consent
- Monitoramento CLS e políticas no painel AdSense

**Definition of Done:** anúncios visíveis nos slots rotulados; `dotnet test` verde; checklist compliance OK.

---

## Sprint 19 - Deploy e go-live (concluída)

**Objetivo:** publicar com nova identidade visual e readiness operacional para AdSense.

**Entregas:**
- `ForwardedHeaders` para reverse proxy HTTPS
- `docker-compose.prod.yml`, `.env.prod.example`, `appsettings.Production.json.example`
- Exemplo nginx: `infra/nginx/meu-valor-liquido.conf.example`
- SMTP produção: TLS + autenticação opcional em `SmtpEmailSender`
- `GoLiveSmokeTests` (rotas, assets, sitemap, health, headers)
- CI: job `docker-build`
- `docs/DEPLOY.md` e `docs/adsense-checklist.md` atualizados

**Definition of Done:** pipeline e testes de go-live verdes; documentação de deploy completa. Deploy real (domínio/HTTPS) é passo operacional manual.

---

## Sprint 18 - Lacunas UI + AdSense UX (concluída)

**Objetivo:** páginas sem mockup Stitch no novo visual; slots e embed alinhados.

**Entregas:**
- Widget, Newsletter, Contato, institucionais, faixas salariais com tokens Valores Públicos
- `_EmbedLayout` com favicon e logo
- `_AdSlot` com label “Espaço publicitário”
- Cookie consent estilizado

**Definition of Done:** páginas públicas consistentes; testes de ads/institucionais verdes.

---

## Sprint 17 - Hubs de conteúdo (concluída)

**Entregas:** CLT x PJ, Dúvidas, Blog, Como calculamos, Meu painel — cards com accent border e breadcrumbs.

---

## Sprint 16 - Núcleo UI (concluída)

**Entregas:** Home hero split, central de calculadoras com chips, painel de resultado branco, badges institucionais.

---

## Sprint 15 - Brand + tokens + shell (concluída)

**Entregas:**
- Ícones Gemini em `wwwroot/` (logo, favicon, OG PNG, apple-touch)
- Tokens Valores Públicos em `site.css`
- Logo no `_Layout`; `SeoMetadataHelper.DefaultOgImagePath` → PNG
- `BrandAssetsTests`

**Definition of Done:** marca visível em todas as páginas; `dotnet test` verde.

---

## Sprint 14 - Institucional + AdSense readiness (concluída)

**Objetivo:** confiança para aprovação AdSense — páginas institucionais completas e infraestrutura de anúncios configurável.

**Entregas:**
- Páginas expandidas: `/sobre`, `/politica-de-privacidade`, `/termos-de-uso`, `/aviso-legal`
- Nova página `/como-calculamos` com metodologia e tabelas @2026
- `AdsOptions` + `ConfigurableAdSlotProvider` (placeholders até `Ads:Enabled=true`)
- Partial `_AdSenseScript`, banner de cookies quando ads ativos, CSP para domínios Google
- Sitemap, mapa do site e footer atualizados

**Definition of Done:** conteúdo institucional indexável; privacidade menciona AdSense/cookies; `dotnet test` verde; sem publisher ID no código.

---

## Sprint 13 - Métricas internas agregadas (concluída)

**Objetivo:** decisões de produto com contadores diários sem PII.

**Entregas:**
- Tabela `aggregated_metrics` (data, evento, dimensão, contagem)
- `IProductMetricsService` com registro em cálculos, PDFs e widget embed
- `POST /api/metrics/collect` para share e painel local (rate limit)
- Painel `/metricas-internas` (noindex) com totais e top calculadoras
- `product-metrics.js` integrado ao share e painel local

**Definition of Done:** sem IP, e-mail ou valores salariais; eventos server-only bloqueados na API; `dotnet test` verde.

---

## Sprint 12 - Performance e Core Web Vitals (concluída)

**Objetivo:** melhorar LCP/CLS e reduzir carga no servidor para readiness AdSense.

**Entregas:**
- `CachedCalculatorCatalogService` e `CachedContentService` (`IMemoryCache`, 10 min)
- Output cache do `sitemap.xml` (1 h) e compressão Brotli/Gzip
- `StaticAssetCacheMiddleware` com `Cache-Control` longo para `/css`, `/js`, `/lib`, `/images`
- jQuery só em páginas com validação; scripts globais com `defer`; preload de `site.css`
- Slots de anúncio com altura fixa e `contain` para reduzir CLS
- Testes de cache, headers e carregamento condicional de scripts

**Definition of Done:** testes verdes; home sem jQuery; assets estáticos com cache imutável.

---

## Sprint 11 - Widget incorporável (concluída)

**Objetivo:** referência legítima em blogs e portais com iframe gratuito e atribuição ao site.

**Entregas:**
- `EmbedWidgetCatalog` com 8 calculadoras incorporáveis
- Hub `/widget` (alias `/incorporar`) com preview, código copiável e regras de uso
- Rotas `/widget/{slug}` → calculadora em modo `?embed=1` sem anúncios nem share
- `_EmbedLayout`, `EmbedFramePolicy` (`frame-ancestors *`) e CSP específica
- Sitemap, mapa do site e CTA na home

**Definition of Done:** sem ads no embed; slugs não listados retornam 404; `dotnet test` verde.

---

## Sprint 10 - Painel local (localStorage) (concluída)

**Objetivo:** retenção sem login — o usuário salva simulações no próprio navegador e volta depois.

**Entregas:**
- `LocalPanelSaveContext` + botão **Salvar no painel** no bloco de compartilhamento
- Página `/meu-painel` (alias `/painel`) com lista, reabrir, remover e limpar tudo
- `local-panel.js` com `localStorage`, badge no menu e aviso de privacidade
- Integração em calculadoras compartilháveis e faixas `/salario-liquido/{valor}`
- Sitemap, mapa do site, CTA na home e link no menu

**Definition of Done:** dados só no cliente; reabrir via link `?r=`; `dotnet test` verde.

---

## Sprint 9 - Dúvidas populares (concluída)

**Objetivo:** capturar cauda longa de busca com respostas educativas e internal linking para calculadoras.

**Entregas:**
- `PopularQuestionsCatalog` com 17 perguntas (slug, categoria, FAQ, links relacionados)
- Hub `/duvidas` e páginas `/duvidas/{slug}`
- JSON-LD FAQPage + breadcrumbs; CTA para calculadora relacionada
- Sitemap, mapa do site e CTA na home
- Testes de hub, detalhe, catálogo e sitemap

**Definition of Done:** conteúdo único por pergunta; links cruzados; `dotnet test` verde.

---

## Sprint 8 - CLT x PJ avançada (concluída)

**Objetivo:** página âncora do site com comparativo tributário útil e tráfego programático.

**Entregas:**
- `CltPjComparisonCalculator` com pró-labore, Simples configurável e solver de faturamento equivalente
- Calculadora `pj-vs-clt` com extrato detalhado CLT/PJ e texto de compartilhamento
- Hub `/clt-pj` e 18 páginas `/clt-pj/{valor}-clt-equivale-a-quanto-pj`
- `CltPjContentBuilder`, sitemap, mapa do site, alias `/clt-vs-pj`
- CTA na home

**Definition of Done:** conteúdo único por faixa CLT; testes verdes; links cruzados com calculadoras.

---

## Sprint 7 - Proposta salarial (concluída)

**Objetivo:** ferramenta compartilhável para negociação salarial com foco no líquido real.

**Entregas:**
- Calculadora `proposta-salarial` (bruto atual x proposto, ganho líquido mensal/anual, % bruto x % líquido)
- Texto de compartilhamento otimizado para WhatsApp/RH
- Aliases `/proposta-salarial` e `/comparar-proposta-salarial`
- Integração com share, PDF, explicação simples e links cruzados
- CTA na home

**Definition of Done:** 17 calculadoras; compartilhamento reproduz comparativo; `dotnet test` verde.

---

## Sprint 6 - Modo explicação simples (concluída)

**Objetivo:** aumentar tempo na página e páginas por sessão com linguagem acessível e links internos.

**Entregas:**
- Abas **Extrato** | **Explicação simples** no painel de resultado
- `CalculatorSimpleExplanationBuilder` com passos por calculadora
- `CalculatorRelatedLinksCatalog` e bloco **Continue explorando**
- Partials `_CalculatorResultPanel` e `_SalaryBandResultPanel`
- `calculator-result-views.js` para alternar visualização

**Definition of Done:** passos legíveis; links cruzados entre calculadoras; testes verdes.

---

## Sprint 5 - PDF do resultado (concluída)

**Objetivo:** valor percebido e retorno ao site via extrato baixável, sem anúncios no PDF.

**Entregas:**
- `CalculatorResultPdfGenerator` (QuestPDF, licença Community)
- Endpoints `GET /calculadoras/{slug}/resultado.pdf?r=` e `GET /salario-liquido/{valor}/resultado.pdf`
- Botão "Baixar PDF" no partial `_CalculatorResultShare`
- Reutilização do token `?r=` do Sprint 4
- Testes de endpoint PDF

**Definition of Done:** PDF com marca do site, disclaimer e link; sem ads; `dotnet test` verde.

---

## Sprint 4 - Resultado compartilhável (concluída)

**Objetivo:** viralidade orgânica via WhatsApp sem armazenar dados no servidor.

**Entregas:**
- `CalculatorInputShareCodec` (token `?r=` na URL)
- `CalculatorShareTextBuilder` e botões WhatsApp / copiar link / copiar texto
- PRG após calcular (link compartilhável reproduz o extrato)
- Partial `_CalculatorResultShare` nas calculadoras e faixas salariais
- Web Share API em dispositivos compatíveis

**Definition of Done:** compartilhamento voluntário; canonical sem query; testes verdes.

---

## Sprint 3 - Páginas por faixa salarial (concluída)

**Objetivo:** tráfego programático útil sem thin content.

**Entregas:**
- Hub `/salario-liquido` com 18 valores brutos indexáveis
- Páginas `/salario-liquido/{valor}` com extrato, contexto editorial e FAQ por faixa
- `SalaryBandCatalog` e `SalaryBandContentBuilder`
- Sitemap, mapa do site, links cruzados com calculadoras
- Prefill `?valor=` na calculadora de salário líquido

**Definition of Done:** conteúdo único por faixa; `dotnet test` verde; URLs no sitemap.

---

## Sprint 2 - Salário bruto necessário (concluída)

**Objetivo:** calculadora inversa de alto valor para SEO e negociação salarial.

**Entregas:**
- `NetSalaryCalculator` e `GrossSalarySolver` (busca binária)
- Calculadora `salario-bruto-necessario` com campos de líquido desejado, dependentes e descontos
- Aliases `/calculadora-salario-bruto` e `/quanto-preciso-ganhar-para-receber-liquido`
- Destaque na home, links cruzados com salário líquido, seed incremental no banco
- Testes unitários (`GrossSalarySolverTests`)

**Definition of Done:** 16 calculadoras; consistência forward/inverse; `dotnet test` verde.

---

## Sprint 1 - SEO técnico essencial (concluída)

**Objetivo:** preparar indexação, rastreamento e apresentação nos buscadores.

**Entregas:**
- `SeoMetadataHelper` centralizado
- `meta robots`, `og:image`, `twitter:image` no layout
- `BreadcrumbList` JSON-LD em calculadoras e blog
- Página `/mapa-do-site`
- Sitemap com `/newsletter` e `/mapa-do-site`
- `/Error` em português com `noindex`
- Imagem OG padrão em `wwwroot/images/og-default.png`

**Definition of Done:** testes SEO verdes; metadados centralizados.

---

## Sprint 0 - Auditoria, organização e segurança (concluída)

**Objetivo:** entender o projeto, corrigir problemas básicos e preparar base para crescimento.

**Entregas:**
- Auditoria documentada em `docs/ROADMAP_MONETIZACAO.md`
- `docs/SEO_CHECKLIST.md` e `docs/ADSENSE_COMPLIANCE.md`
- E-mail mascarado em logs (`SmtpEmailSender`)
- Slots de anúncio com altura/margem para reduzir CLS
- Política de logging em `docs/security-checklist.md`

**Definition of Done:** `dotnet test` verde; documentação Sprint 0; sem PII em logs de e-mail.

---

## Meta de produto

Monetizar rapidamente via **Google AdSense**, priorizando:

1. Site **público** com domínio e HTTPS (pré-requisito de aprovação).
2. Checklist AdSense atendido no código (15 calculadoras, 15 artigos, páginas legais).
3. Expandir catálogo para **15 calculadoras** (tráfego orgânico adicional).
4. Integrar AdSense real **após** aprovação (não no MVP — ver `docs/adsense-checklist.md`).

## Backlog de calculadoras

| Calculadora | Slug | Status |
|-------------|------|--------|
| Salário líquido | `salario-liquido` | Concluída |
| Férias | `ferias` | Concluída |
| Décimo terceiro | `decimo-terceiro` | Concluída |
| Rescisão CLT | `rescisao-clt` | Concluída |
| Hora extra | `hora-extra` | Concluída |
| INSS | `inss` | Concluída |
| IRRF | `irrf` | Concluída |
| PJ vs CLT | `pj-vs-clt` | Concluída |
| Financiamento | `financiamento` | Concluída |
| Juros compostos | `juros-compostos` | Concluída |
| FGTS | `fgts` | Concluída |
| Simulador MEI | `simulador-mei` | Concluída |
| Custo de funcionário (empresa) | `custo-funcionario` | Concluída |
| Multa de atraso | `multa-atraso` | Concluída |
| Conversor salário (mês/dia/hora) | `conversor-salario` | Concluída |

**15 calculadoras no catálogo.**

## Sprint 16 - Correções de cálculo e 5 calculadoras novas (concluída)

**Objetivo:** site pronto para deploy com catálogo completo e fórmulas corrigidas.

**Correções aplicadas:**
- IRRF: isenção total para base tributável até R$ 5.000 (Lei 15.270/2025).
- FGTS na rescisão: saldo estimado sobre todo o tempo de empresa (não mais limitado a 12 meses).
- Hora extra: valor bruto do extrato alinhado ao total com DSR.

**Calculadoras novas:** `fgts`, `simulador-mei`, `custo-funcionario`, `multa-atraso`, `conversor-salario`.

**Definition of Done:** 15 calculadoras; testes de regressão fiscal; `dotnet test` verde.

---

## Sprint 17 - Conteúdo e SEO das novas ferramentas (planejada)

**Objetivo:** artigos de blog, cross-linking e sitemap com 15 slugs.

**Entregas:** artigos para MEI, custo funcionário, multa e conversor; atualização do artigo FGTS.

---

## Sprint 18 - Deploy e go-live (planejada)

**Objetivo:** ambiente produção, domínio, HTTPS, solicitar AdSense.

---

## Sprint 19 - Monetização AdSense pós-aprovação (planejada)

**Objetivo:** integrar anúncios reais nos slots existentes.

---

## Sprints anteriores (referência)

## Status do roadmap

| Sprint | Tema | Status |
|--------|------|--------|
| **8** | **CLT x PJ avançada** | **Concluído** |
| **7** | **Proposta salarial** | **Concluído** |
| **6** | **Modo explicação simples** | **Concluído** |
| **5** | **PDF do resultado** | **Concluído** |
| **4** | **Resultado compartilhável** | **Concluído** |
| **3** | **Páginas por faixa salarial** | **Concluído** |
| **2** | **Salário bruto necessário** | **Concluído** |
| **1** | **SEO técnico essencial** | **Concluído** |
| **0** | **Auditoria, segurança, docs monetização** | **Concluído** |
| 0–13 | Fundação, calculadoras, infra, docs | Concluído |
| **14** | **UI/UX, SEO avançado, newsletter** | **Concluído** |
| **15** | **Conteúdo editorial (15 artigos)** | **Concluído** |
| **16** | **Correções de cálculo + 5 calculadoras novas** | **Concluído** |
| 17 | Conteúdo/SEO das novas ferramentas | Planejado |
| 18 | Deploy e go-live (AdSense) | Planejado |
| 19 | Monetização AdSense (pós-aprovação) | Planejado |

## Sprint 14 - UI/UX, SEO avançado e newsletter

Objetivo: elevar a experiência visual, SEO técnico e captura de e-mail.

Entregas:
- Design system CSS (cores, tipografia DM Sans, cards, extrato, hero)
- Layout renovado (navbar, footer, trust badges)
- Filtro por categoria nas calculadoras
- Open Graph + Twitter Cards + canonical
- JSON-LD: WebSite (home), FAQPage (calculadoras), Article (blog)
- Página `/newsletter` com persistência e Mailpit
- 5 artigos adicionais no seed do blog
- Testes de SEO e página newsletter

Definition of Done: `dotnet test` verde; páginas com meta tags e schema; newsletter funcional.

## Sprint 15 - Conteúdo editorial (concluída)

Objetivo: expandir blog para 10–20 artigos originais (readiness AdSense).

Entregas:
- 15 artigos editoriais em `BlogArticleSeedData.cs` com conteúdo estruturado (HTML)
- Campos `Category` e `RelatedCalculatorSlug` em `BlogPostEntity`
- Seed idempotente com upgrade de stubs antigos
- Blog: cards com data/categoria/tempo de leitura, breadcrumbs, CTA calculadora, artigos relacionados
- Home: seção "Últimos artigos"
- Testes: contagem mínima, links para calculadoras, 404, sitemap
- Migration `AddBlogPostCategoryAndCalculatorLink`

Definition of Done: `dotnet test` verde; ≥15 artigos no blog; cross-linking blog ↔ calculadoras.

## Sprint 16 - Deploy e go-live (próxima)

**Objetivo:** colocar o site no ar com domínio e HTTPS para solicitar AdSense.

**Agents:** Infrastructure, Security, SEO/Content, Documentation.

**Entregas:**
- Ambiente de staging e produção (CI/CD deploy).
- Domínio apontando para o host; certificado HTTPS.
- `SITE_BASE_URL` em produção (canonical, OG, sitemap).
- Revisão final do `docs/adsense-checklist.md` e `docs/security-checklist.md`.
- `robots.txt` e `sitemap.xml` acessíveis em produção.
- Smoke test: calculadoras, blog, contato, newsletter, páginas legais.

**Definition of Done:** site público estável; checklist AdSense verificado manualmente; `dotnet test` verde no CI.

---

## Sprint 17 - FGTS e conversor de salário

**Objetivo:** duas calculadoras de alto volume de busca, reaproveitando tabelas e padrões existentes.

**Agents:** Backend/Calculators, WebApp/Frontend, QA/Test, SEO/Content.

**Entregas:**

### `fgts` (Trabalhista)
- Depósito mensal (8%), projeção de saldo, multa rescisória (40%/20%).
- Campos: salário, meses trabalhados, tipo de desligamento (opcional).
- `CalculatorSeedData`, `CalculationEngine`, `CalculatorFieldProfile`, ícone UI.
- Testes unitários (depósito, multa, arredondamento).
- Artigo blog existente (`fgts-guia-completo`) com link para a nova calculadora.

### `conversor-salario` (Trabalhista)
- Conversão mensal ↔ diário ↔ hora (jornada configurável, padrão 220h/mês).
- Campos: valor de entrada, tipo (mensal/diário/hora), horas mensais.
- Testes com CLT 44h/semana e jornadas customizadas.

**Definition of Done:** `/calculadoras/fgts` e `/calculadoras/conversor-salario` funcionais com extrato, FAQ, disclaimer e testes.

---

## Sprint 18 - MEI e custo de funcionário

**Objetivo:** ampliar audiência (empreendedores e empregadores).

**Agents:** Backend/Calculators, WebApp/Frontend, QA/Test, SEO/Content, Product Owner.

**Entregas:**

### `simulador-mei` (Fiscal)
- DAS mensal (valores vigentes), limite de faturamento anual, alerta de desenquadramento.
- Comparativo simplificado MEI vs pró-labore (opcional no extrato).
- Campos: faturamento mensal estimado, atividade (comércio/serviços/indústria).

### `custo-funcionario` (Trabalhista)
- Custo total empresa: salário + INSS patronal, FGTS, provisões (13º, férias+1/3), estimativa de encargos.
- Campos: salário bruto, benefícios opcionais (VT, plano).
- Categoria nova ou badge “Empresa” na UI.

**Definition of Done:** duas calculadoras no catálogo; 2 artigos de blog novos com cross-link; testes de regressão fiscal.

---

## Sprint 19 - Multa de atraso e SEO das novas ferramentas

**Objetivo:** completar o catálogo de 15 calculadoras e fortalecer indexação.

**Agents:** Backend/Calculators, SEO/Content, WebApp/Frontend, QA/Test.

**Entregas:**

### `multa-atraso` (Financeiro)
- Multa e juros por atraso (padrão contratual configurável + referência legal quando aplicável).
- Campos: valor principal, dias de atraso, taxa de multa (%), juros ao mês (%).

### SEO e conteúdo
- Atualizar sitemap com os 5 novos slugs.
- Seed de conteúdo educativo (`DataSeeder`) para cada calculadora nova.
- Home e `/calculadoras`: grid com 15 ferramentas.
- Testes WebApp: metadata, FAQ schema, links no sitemap.

**Definition of Done:** 15 calculadoras publicadas; blog com artigos ligados às 5 novas; `dotnet test` verde.

---

## Sprint 20 - Monetização AdSense (pós-aprovação)

**Objetivo:** substituir placeholders por anúncios reais após aprovação do Google.

**Agents:** Monetization/AdSense, WebApp/Frontend, Security, Documentation.

**Pré-requisito:** conta AdSense aprovada (solicitar na Sprint 16).

**Entregas:**
- Script AdSense via configuração (`appsettings` / variável de ambiente), sem hardcode de publisher ID no repo.
- Substituir `PlaceholderAdSlotProvider` por implementação real nos slots `calculator-top` e `calculator-bottom`.
- Política de privacidade atualizada (cookies de publicidade).
- Lazy load de anúncios; sem layout shift agressivo (Core Web Vitals).
- Documentar rollback e ambiente de preview sem anúncios.

**Definition of Done:** anúncios em produção nos slots definidos; Lighthouse mobile aceitável; docs atualizados.

---

## Guia por agent (referência rápida)

| Agent | Responsabilidade nas sprints 16–20 |
|-------|-------------------------------------|
| Product Owner | Priorização, critérios de aceite, não expandir escopo além das 5 calculadoras |
| Architecture | Manter regras no módulo `Calculators`; não duplicar lógica fiscal no WebApp |
| Backend/Calculators | `CalculationEngine`, validações, tabelas, testes unitários |
| WebApp/Frontend | `CalculatorFieldProfile`, `Details.cshtml`, ícones, UX mobile-first |
| Database | Seeds de conteúdo; migrations só se o modelo exigir |
| Infrastructure | Deploy, Docker, CI/CD, variáveis de ambiente |
| QA/Test | Regressão fiscal, testes de página, sitemap |
| Security | HTTPS, headers, privacidade, rate limiting em contato |
| SEO/Content | Slugs, meta, blog, FAQs, cross-linking |
| Monetization/AdSense | Checklist, slots, integração pós-aprovação |
| Documentation | `sprint-plan.md`, `how-to-create-calculator.md`, CHANGELOG |

## Como implementar cada calculadora nova

Seguir `docs/how-to-create-calculator.md`:

1. `CalculatorSeedData.cs` — definição e FAQ.
2. `CalculationEngine.cs` — fórmulas.
3. `CalculatorFieldProfile.cs` — campos do formulário.
4. `CalculatorUiHelper.cs` — ícone Material.
5. `DataSeeder.cs` — texto educativo abaixo do formulário.
6. Testes em `tests/MeuValorLiquido.Calculators.Tests/`.
7. Verificar `/calculadoras/{slug}`.
