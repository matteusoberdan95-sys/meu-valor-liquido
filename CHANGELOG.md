# Changelog

## Unreleased

### Sprint 45 — Painel e institucional dark
- Meu painel com cards `#1C1C1F`, empty state e sidebar privacidade no desktop
- Contato com formulário glass, aside FAQ e estado de sucesso
- Newsletter centralizada com ícone mail e CTA teal
- Sobre com grid artigo + cards laterais (transparência, monetização, contato)
- Privacidade e termos com layout legal, índice lateral sticky no desktop
- Smoke tests institucionais em `GoLiveSmokeTests` (124 testes WebApp)

### Sprint 44 — Conteúdo e ajuda dark
- FAQ com layout sidebar desktop, chips mobile e badges de categoria no accordion
- Blog hub e artigo com cards dark, TOC no aside e newsletter no tema Premium Liquid
- Metodologia `/como-calculamos` com hero, regimes e tabelas INSS/IRRF dark

### Sprint 43 — Calculadoras prioritárias dark
- Rescisão CLT com layout `valora-stitch-rescisao`, seção de contrato e callout FGTS
- Comparador PJ×CLT com wizard 3 passos, stepper e painel de dicas (`pj-vs-clt-wizard.js`)
- Refino mobile em INSS, IRRF, férias e 13º (badges fiscais e hint de tabelas)

### Sprint 42 — Template calculadora detail dark
- Header sem gradiente teal; breadcrumb desktop e resumo visível no mobile
- Formulário glass (`#121214`), inputs `#0e0e0f`, accordion dark e botão com glow
- Painel ESTIMATIVA ATUAL dark com borda teal e valor líquido emerald
- Template compartilhado em `Details.cshtml` para as 17 calculadoras

### Sprint 41 — Central de calculadoras dark
- Hub mobile com busca, chips, lista horizontal e CTA de sugestão
- Hub desktop com sidebar de categorias, grid 3 colunas e card featured
- Cards `_CalculadoraHubRowCard` e `_CalculadoraHubCard` no padrão Stitch

### Sprint 40 — Shell dark e home polish
- Footer multi-coluna (Produtos / Legal / ícones sociais)
- Blog cards na home com imagem, badge e tempo de leitura
- Form cards, inputs, choices e botões alinhados ao Premium Liquid
- Ad slot dark no desktop; teaser oculto no mobile

### Sprint 39 — Dark Premium Liquid (fundação)
- Tema dark Premium Liquid: tokens, Plus Jakarta Sans, header/bottom nav glass
- Brand mark Stitch (ícone + wordmark); home mobile e desktop alinhada aos mocks
- Bento com icon boxes; resultado líquido com glow emerald
- Sprints 39–46 documentadas; `stitch_redesing/` no `.gitignore`

### Sprint 31 — Férias e 13º nível referência
- Abono pecuniário, férias em dobro, dias 20/30
- 13º com 1ª/2ª parcela e adiantamento já pago
- FAQ dedicado e 10 testes de benchmark

### Sprint 30 — Camadas e faixas INSS/IRRF
- Formulário em camadas em salário líquido, férias e 13º
- IRRF com toggle de salário bruto e faixa de tabela no resultado
- Faixa INSS exibida no extrato

### Sprint 29 — Financiamento SAC e wizard PJ
- Financiamento com SAC e comparativo Price x SAC
- Motivos de rescisão: falecimento do empregador e contrato determinado
- Benchmark de regressão para cenários de referência
- Wizard em 3 passos no comparador PJ×CLT

### Sprint 28 — Calibração e expansão
- 13º na rescisão com regra dos 15 dias (paridade com calculadoras de referência)
- Novos motivos de desligamento: experiência e aposentadoria
- Datas e resultado agrupado em 13º, férias e salário líquido
- Juros compostos com aporte mensal
- Campo de outros descontos no salário líquido

### Sprint 27 — Rescisão CLT UX
- Datas completas de admissão e afastamento no formulário e no motor de cálculo
- Aviso prévio com quatro modalidades (trabalhado, indenizado, não cumprido, dispensado)
- Resultado da rescisão agrupado em Verbas / Descontos / FGTS / Total líquido
- Formulário reorganizado em seções essenciais e detalhes opcionais
- Documentação: README (seção Calculadoras), `docs/sprint-plan.md`

## Sprint 21 (em andamento)
- Plano Stitch; remoção Gemini; shell; home mobile+desktop; central calculadoras bento (Sprint 22).

## Sprint 19
- Deploy readiness: ForwardedHeaders, compose prod, exemplo nginx, SMTP TLS/auth, GoLiveSmokeTests e CI docker-build.

## Sprint 15–18
- Redesign UI/UX v2 (Stitch Valores Públicos): brand assets Gemini, tokens CSS, home, calculadoras, hubs, ad slots e embed.

## Sprint 14
- Institucional + AdSense readiness: páginas expandidas, `/como-calculamos`, `AdsOptions` configurável e banner de cookies.

## Sprint 13
- Métricas internas agregadas: contadores diários por evento/calculadora, API de coleta client-side e painel `/metricas-internas` sem PII.

## Sprint 12
- Performance: cache em memória (catálogo/conteúdo), output cache do sitemap, Brotli, cache de assets, jQuery sob demanda, defer e CLS nos slots.

## Sprint 11
- Widget incorporável: hub `/widget`, iframe para 8 calculadoras, modo embed sem anúncios e headers `frame-ancestors *`.

## Sprint 10
- Painel local (`/meu-painel`): salvar simulações em `localStorage`, reabrir, remover e badge no menu — sem login.

## Sprint 9
- Dúvidas populares: hub `/duvidas`, 17 páginas com FAQ schema, links para calculadoras e perguntas relacionadas.

## Sprint 8
- CLT x PJ avançada: comparativo detalhado, hub `/clt-pj`, páginas programáticas e solver de faturamento equivalente.

## Sprint 7
- Calculadora `proposta-salarial`: comparativo atual x proposto, ganho líquido, % real no bolso, share/PDF e aliases SEO.

## Sprint 6
- Modo explicação simples: abas Extrato/Explicação, passos em linguagem direta e links para calculadoras relacionadas.

## Sprint 5
- PDF do resultado (QuestPDF): endpoints por calculadora e faixa salarial; botão "Baixar PDF"; sem anúncios no arquivo.

## Sprint 4
- Resultado compartilhável (link com token `?r=`, WhatsApp, copiar link/texto, Web Share API).
- `GlobalUsings.cs` em todos os projetos; convenções em `docs/CODING_CONVENTIONS.md`.

## Sprint 3
- Sprint 0: auditoria, `ROADMAP_MONETIZACAO`, `SEO_CHECKLIST`, `ADSENSE_COMPLIANCE`, mascaramento de e-mail em logs, slots de anúncio com margem/CLS.
- PostgreSQL + EF Core com migrations e seed inicial.
- Tabelas INSS/IRRF 2026, `CalculationEngine` e perfis de campos por calculadora.
- Sitemap dinâmico, health check, Mailpit via SMTP, contato persistido.
- UX de calculadoras com placeholders de anúncio e conteúdo educativo do banco.
- Bootstrap do Modular Monolith.
- Core, Shared, módulos iniciais e WebApp Razor Pages.
- 10 calculadoras MVP em formato estimativo.
- Docker Compose com PostgreSQL e Mailpit.
- Documentação inicial, testes e SEO básico.
