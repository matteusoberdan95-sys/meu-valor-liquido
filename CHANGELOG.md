# Changelog

## Unreleased

### Fix - PageSpeed mobile
- Fontes Google e Material Symbols deixaram de bloquear a renderizacao inicial e passam a carregar em idle via `font-loader.js`.
- `local-panel.js` agora e carregado sob demanda apenas em paginas com salvar/comparar simulacoes.
- Imagem decorativa da hero passa a ter preload somente no desktop e deixa de competir com o carregamento inicial mobile.
- Home ganhou CSS critico inline e carregamento adiado do CSS completo para reduzir bloqueio de renderizacao no mobile.

### Fix - Ads.txt AdSense
- Adicionado `/ads.txt` publico com o publisher Google AdSense para resolver o status "ads.txt nao encontrado".

### Fix - Verificacao AdSense
- Adicionado modo `Ads:VerificationEnabled` para renderizar o script de verificacao do AdSense no `<head>` sem ativar slots de anuncios.
- `docker-compose.prod.yml` agora repassa `ADS_VERIFICATION_ENABLED` e `ADS_PUBLISHER_ID` para a WebApp.
- Documentacao de VPS/Deploy separa verificacao AdSense de ativacao de anuncios.

### Fix - PageSpeed e cookie mobile
- Banner de cookies no mobile agora fica compacto, com altura limitada e rolagem interna ao personalizar preferências.
- Logo do header/footer recebeu `width` e `height` explícitos para reduzir CLS.
- Hero da home ganhou preload e `fetchpriority="high"` para melhorar descoberta do LCP.

### Sprint 82 - Conversao interna nos artigos
- Artigos com calculadora relacionada agora exibem um painel contextual de proximo passo com CTA para calculadora, assistente, hub e FAQ.
- Novo `BlogConversionPathCatalog` centraliza rotas de conversao por calculadora sem misturar regra de negocio.
- CSS responsivo e testes `Sprint82BlogConversionTests` validam links internos em todos os artigos relacionados.

### Sprint 70 - Editorial lote 4 (set/2026)
- Artigos longos (~5 min): `experiencia-clt-direitos-e-rescisao`, `home-office-clt-descontos`.
- Cross-links nos hubs `/desligamento` e `/negociar-salario` e capas WebP em `wwwroot/images/blog`.
- `BlogEditorialCatalog.Sprint70Lote4EditorialSlugs` e testes `Sprint70Lote4BlogTests`.

### Sprint 70 - Editorial lote 3 (ago/2026)
- Artigos longos (~5 min): `dissidio-salarial-2026-como-avaliar`, `vale-refeicao-desconto-holerite`.
- Cross-links no hub `/negociar-salario` e capas WebP em `wwwroot/images/blog`.
- `BlogEditorialCatalog.Sprint70Lote3EditorialSlugs` e testes `Sprint70Lote3BlogTests`.

### Sprint 81 - Diagnostico do Holerite
- `/conferir-holerite` agora mostra status geral do holerite: correto, atencao ou diferenca relevante.
- Resultado reorganizado em cards por INSS, IRRF e liquido, com esperado, informado e diferenca.
- Checklist contextual para o usuario conferir pontos com o RH e CTAs para salario liquido e assistente.
- Testes WebApp atualizados para carregamento, diagnostico positivo e divergencia relevante.

### Sprint 80 - Assistente educativo Meu Valor Liquido
- Nova pagina `/assistente` com chat educativo responsivo inspirado no mock Stitch IA 2080.
- Respostas guiadas no front-end para INSS, IRRF, CLT vs PJ, rescisao e holerite, sempre apontando para calculadoras relacionadas.
- Botao flutuante global com pop-up para iniciar o chat sem tirar o usuario do fluxo principal.
- `/assistente` incluido no sitemap, mapa do site, breadcrumbs JSON-LD e navegacao mobile/desktop.

### Sprint 79 - Indexacao inicial e primeiros ganhos SEO
- `/sitemap.xml` agora inclui `lastmod` para URLs estaticas, calculadoras, FAQs, faixas programaticas e artigos.
- Redirect permanente de `/duvidas/o-que-e-irrf` para `/duvidas/irrf-quem-paga-e-como-calcular`, evitando 404 em slug intuitivo.
- Nova rotina `docs/SEARCH_CONSOLE_INDEXATION_PLAYBOOK.md` para inspecao manual de URLs prioritarias no Search Console.
- Sprint futura 80 registrada para avaliar um assistente educativo com IA/FAQ limitado ao nicho salario/trabalho.

### Sprint 70 — Editorial lote 2 (jul/2026)
- Artigos longos (~5 min): `ferias-coletivas-clt-guia-completo`, `pedir-demissao-ou-aguardar-dispensa`.
- Cross-links nos hubs `/desligamento` e `/negociar-salario`.
- `docs/BLOG_EDITORIAL_PLAN.md` — calendário sazonal, backlog e guia para agents.
- Testes `Sprint70Lote2BlogTests` (≥850 palavras, links EEAT).

### Sprint 78 — Widget embed + newsletter semanal
- Landing `/widget` redesenhada (Stitch): seletor de calculadoras, pré-visualização única e snippet copiável (`widget-hub.js`).
- `WeeklyNewsletterTemplateCatalog`, preview em `/newsletter` e template editorial em `docs/NEWSLETTER_WEEKLY_TEMPLATE.md`.
- E-mail de confirmação cita curadoria semanal; embed continua sem anúncios.
- Testes `Sprint78WidgetAndNewsletterTests`.

### Sprint 77 — Badge tabelas 2026 + FAQ expandido
- Badge `INSS/IRRF 2026 · Revisado em [mês]` nas calculadoras template C1 com INSS/IRRF (`TaxTablesBadgeHelper`, `_TaxTablesRevisionBadge`).
- 7 novas páginas `/duvidas/`: conferir holerite, redução IRRF 2026, teto INSS, abono pecuniário, acordo 484-A, plano de saúde e saque FGTS.
- Testes `Sprint77TaxTablesBadgeAndFaqTests`.

### Sprint 76 — Meu painel: comparar cenários
- Comparativo lado a lado de 2 simulações salvas em `/meu-painel` (seleção por checkbox).
- `local-panel.js`: `renderPanelCompare`, diff de valor estimado, barras visuais e `netAmountValue` no storage.
- CSS `.valora-stitch-panel-compare-*` e testes `Sprint76PanelCompareTests`.

### Sprint 75 — Simulador “E se…”
- `WhatIfScenarioCatalog` com 3 presets: pedir demissão, aceitar PJ, vender 1/3 das férias.
- Cards na home e nos hubs temáticos (`/desligamento`, `/virar-pj`, `/negociar-salario`).
- `_WhatIfScenarioCards`, `WhatIfScenarioLinkBuilder` e CSS `.valora-stitch-whatif-*`.
- Testes `WhatIfScenarioCatalogTests` e `Sprint75WhatIfScenarioTests`.

### Sprint 74 — Páginas programáticas expandidas
- `SalaryBandCatalog` ampliado para 46 faixas brutas (2400–19000 preenchendo lacunas).
- Variantes indexáveis com 0, 1 e 2 dependentes: `/salario-liquido/{valor}/1-dependente` e `/clt-pj/{valor}/1-dependente`.
- `ProgrammaticDependentsCatalog`, SEO/breadcrumbs por variante; sitemap com todas as URLs.
- Ângulos editoriais únicos para as novas faixas; conteúdo diferenciado por dependentes.
- Testes `Sprint74ProgrammaticPagesTests`.

### Sprint 73 — Checklist interativo de rescisão
- Checklist pós-demissão no hub `/desligamento` com progresso em `localStorage` (`mvl-rescisao-checklist-v1`).
- `RescisaoChecklistCatalog`, partial `_RescisaoChecklist`, `rescisao-checklist.js`.
- FAQ rich snippets (`FAQPage`) no hub desligamento via `ThematicHubFaqSchemaBuilder`.
- Política de Cookies atualizada com a nova chave local.
- Testes `Sprint73RescisaoChecklistTests`.

### Sprint 72 — Comparador visual de propostas
- UI lado a lado em `proposta-salarial`: veredito com % real no bolso, cards atual vs proposto (INSS, IRRF, líquido), barras comparativas.
- `SalaryProposalStitchResultBuilder`, partial `_PropostaSalarialStitchResults`; integração em `Details.cshtml`.
- Hub `/negociar-salario` reforçado com link ao comparador visual.
- Testes `Sprint72PropostaComparisonTests`; `CalculatorSharePageTests` atualizado.

### Sprint 71 — Conferir holerite
- `PayslipValidationService` com tolerância de R$ 1,00 em INSS, IRRF e líquido opcional.
- Página `/conferir-holerite` com formulário Stitch e painel de divergências.
- Links no hub `/negociar-salario`, artigo `como-conferir-holerite`, sitemap e mapa do site.
- Testes `Sprint71PayslipValidationTests` e `Sprint71ConferirHoleriteTests`.

### Sprint 70 — Editorial contínuo (lote 1)
- 2 artigos: `acordo-484a-verbas-e-multa-fgts`, `custo-total-clt-para-empregador`.
- `BlogEditorialCatalog.Sprint70EditorialSlugs`; hubs `/desligamento` e `/virar-pj` atualizados.
- Testes `Sprint70BlogTests`.

### Sprint 69 — PDF Premium
- Redesign do relatório PDF (QuestPDF): logo Valora, faixa de marca, seção **Dados informados**, extrato agrupado (proventos/descontos/detalhamento), destaque do líquido, resumo e rodapé com metodologia.
- `CalculatorPdfReportContext`, `CalculatorPdfInputSummaryBuilder`, `SalaryBandPdfContext`.
- Testes `Sprint69PdfTests`.

- 7 artigos: negociação, MEI/PJ, 13º, férias, consignado e reserva de emergência.
- `BlogEditorialCatalog.Sprint68EditorialSlugs`; hubs `/negociar-salario` e `/virar-pj` atualizados.
- Calendário de 6 meses do `NICHO_REFERENCIA_PLAN.md` completo (11 artigos novos nas Sprints 66+68).
- Testes `Sprint68BlogTests`.

### Sprint 67 — Calculadora seguro-desemprego (PO: `seguro-desemprego`)
- Nova calculadora `/calculadoras/seguro-desemprego` com tabela MTE 2026, carência, parcelas e elegibilidade.
- `BrUnemploymentInsuranceTables2026`, `UnemploymentInsuranceCalculator`; 5 benchmarks; jornada e hub `/desligamento` atualizados.
- FAQ, artigo `seguro-desemprego-quem-tem-direito` e rescisão CLT apontam para a calculadora dedicada.
- Testes `Sprint67UnemploymentInsuranceTests` e `Sprint67WebTests`.

### Sprint 66 — Calendário editorial (4 artigos)
- Artigos: `irrf-2026-reducao-imposto`, `seguro-desemprego-quem-tem-direito`, `multa-fgts-40-ou-20`, `aumento-salario-quanto-sobra-liquido`.
- `BlogEditorialCatalog.Sprint66EditorialSlugs`; hubs temáticos atualizados; testes `Sprint66BlogTests`.
- Blog passa a 30 artigos editoriais no seed.

### Sprint 65 — Hubs temáticos
- `ThematicHubCatalog`, `ThematicHubPageBuilder`, `ThematicHubLoader` e partial `_ThematicHubPage`.
- Páginas `/desligamento`, `/negociar-salario`, `/virar-pj` com calculadoras, jornada, artigos, FAQ e SEO.
- Jornada `VirarPj` em `CalculatorJourneyCatalog` (entrada `pj-vs-clt`).
- Sitemap, mapa do site, breadcrumbs JSON-LD e testes `Sprint65ThematicHubTests`.

### Sprint 64 — Meu painel + template C1
- Meu painel em layout bento Stitch: greeting, perfil local, cálculos salvos, leituras sugeridas, newsletter.
- Template C1 nas 14 calculadoras restantes: modifiers por categoria + botão “Calcular agora”.
- Centralização em `CalculatorUiHelper.IsTemplateC1Slug` / `GetStitchDetailModifierClass`.
- Testes `Sprint64FidelityTests`.

### Sprint 63 — Rescisão multi-card + PJ×CLT visual
- Rescisão CLT: cards de verbas, descontos e FGTS na coluna principal; resumo sticky com total líquido, PDF e dica.
- PJ×CLT: hero “Wizard de Comparação”, callout Factor-R, veredito com comparativo anual e detalhamento CLT/PJ.
- **Divergência intencional:** wizard permanece em 4 passos (benefícios CLT).
- Testes `Sprint63CalculatorFidelityTests` + builders.

### Sprint 62 — Hubs (calculadoras, blog, FAQ)
- Banner premium **CLT vs PJ** na central de calculadoras (`/calculadoras`).
- Blog: card **dica rápida** + seção newsletter “Mantenha seu valor líquido em dia”.
- FAQ: CTA Stitch “Ainda com dúvidas?” / “Falar com suporte”; lead do hero alinhado ao mock.
- Placeholder de busca na central: “Qual cálculo você precisa fazer hoje?”.
- Testes `Sprint62HubTests`.

### Sprint 61 — Shell global sem login
- Footer Stitch em **4 colunas**: marca, Calculadoras, Institucional, Newsletter inline no rodapé.
- Background base `#0A0A0B` e Material Symbols com **wght 300**.
- **Divergência intencional:** sem botão “Entrar” do mock; **Meu painel** no header (produto gratuito + AdSense).
- Testes `Sprint61ShellTests`.

### Sprint 60 — Fidelidade Stitch v2 (parcial)
- Painel de resultado Stitch para salário líquido: gráfico donut, % do bruto, cards INSS/IRRF, alíquota efetiva, CTA finanças.
- Home desktop: bento 12 colunas (featured + ML Prime), seção Nossa Metodologia, social proof, breadcrumb.
- Home mobile: subtitles descritivos nos cards bento (paridade `home_mobile`).
- Docs: trilha Sprints 60–64 em `STITCH_DARK_FIDELITY_PLAN.md` e `sprint-plan.md`; `AGENTS.md` atualizado.
- Testes `Sprint60FidelityTests` e `SalarioLiquidoStitchResultBuilderTests`.

### Sprint 52 - Observabilidade completa
- Métricas agregadas de erros HTTP 404/500 e falhas de cálculo (sem PII).
- Painel `/metricas-internas` com alertas, priorização sugerida e rankings de erro.
- Middleware `ProductMetricsHttpErrorMiddleware` + registro em `/Error`.
- Smoke de submissão das 17 calculadoras (`CalculatorSubmissionSmokeTests`).
- Smoke opcional de produção via `SMOKE_BASE_URL` (`ProductionSmokeTests`).
- Docs: `SEO_MONTHLY_REVIEW.md`, `CALIBRATION_ROUTINE.md`; `METRICS_ROUTINE` e `DEPLOY` atualizados.
- Testes `Sprint52ObservabilityTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 502 testes (1 ignorado).

### Sprint 59 - PJ x CLT e MEI profundos
- Anexo Simples Nacional (I–V) com alíquota sugerida; pró-labore editável (%).
- Custo oculto CLT no extrato: FGTS, provisão 13º e férias + 1/3.
- Wizard PJ×CLT em 4 passos com etapa opcional de benefícios CLT.
- MEI: campo de faturamento anual acumulado com projeção do teto.
- 6 cenários `pj-vs-clt` em `CalculatorBenchmarkCatalog`.
- Artigo MEI atualizado; FAQ `pj-ou-clt-qual-compensa` reforçado.
- Testes `Sprint59PjMeiTests` e `Sprint59WebTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 460 testes (1 ignorado).

### Sprint 58 - Conteúdo editorial direcionado
- Novos artigos: `como-conferir-holerite` e `rescisao-clt-vs-trct`.
- Atualização de `como-avaliar-proposta-salarial` e artigos de rescisão com links cruzados.
- Seção **Como validamos** com `id="como-validamos"` e link na sidebar do blog.
- Cada artigo Sprint 58 linka calculadora, `/como-calculamos` e FAQ em `/duvidas`.
- `BlogEditorialCatalog` e testes `Sprint58BlogTests`.

### Sprint 57 - Faixas salariais e widget incorporável
- **31 faixas** em `/salario-liquido/{valor}` (13 novas: 2200–11000 etc.) com texto editorial único por valor.
- `SalaryBandEditorialCatalog` — cenário de mercado e dica de planejamento por faixa; 5ª FAQ dedicada.
- CTA do widget em blog, contato, hub e páginas de faixa (`_WidgetEmbedCta.cshtml`).
- `docs/SEO_CHECKLIST.md` atualizado; sitemap inclui novas faixas.
- Testes `Sprint57SalaryBandTests` e `Sprint57WidgetCtaTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 445 testes (1 ignorado).

### Sprint 56 - Métricas enxutas e decisão por dados
- `/metricas-internas` com seletor de período (7/30 dias), taxas de engajamento (share, PDF, painel) e rankings ampliados.
- Nomes legíveis das calculadoras no ranking; tops de share e painel local.
- Checklist pós-deploy atualizado em `docs/DEPLOY.md` (18 calculadoras, hubs temáticos, amostra editorial).
- Rotina semanal documentada em `docs/METRICS_ROUTINE.md`.
- Smoke `PostDeploy_All_Calculators_Should_Load` em `GoLiveSmokeTests`.
- Testes `Sprint56MetricsTests`.

### Sprint 55 - Rescisão: lacunas legais e confiança
- Seguro-desemprego como linha informativa (sem promessa de valor) na demissão sem justa causa.
- Adiantamento do 13º já pago descontado na rescisão; campo opcional de média salarial (HE/comissão).
- Regra dos 15 dias aplicada às férias proporcionais quando há datas completas (`CountVacationProportionalAvos`).
- Avisos TRCT/holerite reforçados no painel; tooltips nos campos que mais mudam o valor.
- 15 cenários de benchmark em `rescisao-clt` (aposentadoria, acordo 484-A, experiência, média HE etc.).
- Seção **Rescisão CLT** em `/como-calculamos`; FAQ `seguro-desemprego-quando-tem-direito`; jornada saída da empresa atualizada.
- Testes `Sprint55TerminationTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 402 testes (1 ignorado).

### Sprint 54 - Jornadas guiadas entre calculadoras
- Bloco **Próximo passo** no painel de resultado com 3 jornadas: proposta recebida, saída da empresa e líquido desejado.
- Links entre calculadoras com estado compartilhável (`?r=`) e parâmetro `jornada` para continuidade.
- Texto de compartilhamento inclui próximos passos sugeridos.
- Testes `Sprint54JourneyTests` e `CalculatorJourneyCatalogTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 383 testes.

### Sprint 53 - Holerite completo
- Paridade de descontos entre `salario-liquido`, `salario-bruto-necessario` e `proposta-salarial`: VT, VR/VA, plano, pensão (% ou valor) e outros.
- Extrato com linhas separadas e IRRF isento visível (Lei 15.270/2025).
- Bruto necessário com faixa estimada (`GrossSalarySolver.SolveRange`).
- Accordion **Ajustar descontos** nas três calculadoras; destaque de ganho no bolso na proposta.
- Novo cenário de benchmark `salario-5000-holerite-separado` (51 cenários no catálogo).
- Artigo `como-avaliar-proposta-salarial` e atualização do artigo de salário líquido.
- Testes `Sprint53HoleriteTests`.
- `dotnet test .\MeuValorLiquido.slnx` verde com 375 testes.

### Planejamento — trilha ativa pos-Sprint 50 (Sprints 53-59)
- Nova trilha em `docs/sprint-plan.md`: prioridade alta (53 holerite, 54 jornadas, 55 rescisao), media (56-58), baixa/bloqueada (51 AdSense, 59, 52).
- `AGENTS.md` e `docs/agents.md` atualizados com sprint ativa e handoff Cursor/Codex.
- Mapeamento: antiga Sprint 32 → 53; 33 → 55; 34 → 59 (evitar duplicata).

### Sprint 50 - Metodologia, E-E-A-T e conteudo de apoio
- `/como-calculamos` com metodologia por categoria, data de calibracao e links para calculadoras.
- Badge `Validado com cenarios de referencia` nas 10 calculadoras prioritarias do benchmark.
- Artigos editoriais atualizados com secao de validacao e links para metodologia; novo artigo MEI.
- Seed de blog sincroniza conteudo a cada deploy.
- Testes `Sprint50EeatTests` para E-E-A-T, badge e schema Article.
- `dotnet test .\MeuValorLiquido.slnx` verde com 365 testes.

### Sprint 49 - UX de confianca nas calculadoras
- Tooltips educativos `(i)` nos campos de maior impacto via `CalculatorFieldTooltipCatalog` e partial `_FieldLabel`.
- Avisos contextuais no painel de resultado para rescisao, ferias/13o, holerite, PJ x CLT e MEI acima do limite.
- Labels da rescisao clarificam descontos do ultimo mes vs verbas rescisorias.
- Testes `Sprint49TrustUxTests` cobrindo tooltips e avisos nas 17 calculadoras.
- `dotnet test .\MeuValorLiquido.slnx` verde com 350 testes.

### Planejamento pos-auditoria
- Adicionada em `docs/sprint-plan.md` a trilha Sprints 47-52 para hotfix/deploy, paridade fiscal, UX de confianca, metodologia E-E-A-T, AdSense e observabilidade.

### Sprint 47 - Hotfix de confianca e deploy
- Sprint 47 marcada como concluida apos validacao local e smoke publico.
- `dotnet test .\MeuValorLiquido.slnx --no-restore` verde com 268 testes.
- Producao validada: `/health` Healthy, salario liquido R$ 4.000 + VT R$ 240 + outros R$ 100 = R$ 3.291,40, e radios padrao confirmados nas calculadoras afetadas.

### Auditoria de calculadoras e fluxo
- Corrigido `salario-liquido`: o campo "Outros descontos" agora entra no calculo e aparece separado no extrato.
- Corrigidos grupos de radio sem opcao padrao marcada em calculadoras como ferias, rescisao, hora extra, financiamento, FGTS, MEI e conversor de salario.
- Testes WebApp passam a usar chaves de Data Protection locais no ambiente `Testing`, evitando falha por acesso ao perfil do usuario.
- Regressoes adicionadas para desconto extra no salario liquido e opcoes padrao dos formularios.

### Sprint 46 — Polish final dark (trilha Stitch concluída)
- Página 404 (`/NotFound`) com layout `valora-stitch-error`, bento de atalhos e re-execução via `StatusCodePages`
- Página 500 (`/Error`) no mesmo padrão visual com ID de requisição
- Header desktop: pill de busca (`valora-nav-search`) e item ativo com borda teal
- Varredura `site.css`: tokens dark consistentes, focus rings, remoção de estilos claro em `_Layout.cshtml.css`
- Smoke tests de erro, 404 e busca no header (127 testes WebApp)

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
