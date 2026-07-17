# Sprint Plan

## Sprint 89 — SEO técnico e controle de indexação (concluída)

**Objetivo:** executar a Sprint 4 do plano de aprovação AdSense, expondo ao Google somente URLs públicas, canônicas e consolidadas.

**Entregas:**
- [x] Política central de rotas indexáveis e páginas `noindex`.
- [x] Matriz de URL com tipo, canonical, sitemap, conteúdo, autoria, fontes, revisão, status e ação.
- [x] Sitemap sem assistente, painel local, newsletter, widget, métricas, correções, filtros ou resultados parametrizados.
- [x] `lastmod` estável e revisão real de 17/07/2026 nas calculadoras prioritárias.
- [x] Slugs duplicados consolidados no sitemap.
- [x] Aliases, caixa alta e barra final normalizados com `301`.
- [x] Calculadora inexistente corrigida de soft 404 para `404` real.
- [x] Página `/Error` com `500`; erros sem canonical.
- [x] Filtros, presets, resultados compartilhados e embeds fora do índice.
- [x] `X-Robots-Tag` e `robots.txt` para API, health check e PDFs.
- [x] Auditoria de breadcrumbs, links e schemas sem avaliações fictícias.

**Fora do escopo:** CMP e consentimento publicitário, alterações de fórmula, validação externa no domínio implantado e ativação do AdSense.

**Definition of Done:** build sem avisos, suíte automatizada verde, matriz em `docs/adsense/URL_INDEXATION_MATRIX.md`, auditoria em `docs/adsense/TECHNICAL_SEO_AUDIT.md` e relatório em `docs/adsense/SPRINT_4_REPORT.md`.

---

## Sprint 88 — Autoria, autoridade e transparência (concluída)

**Objetivo:** executar a Sprint 3 do plano de aprovação AdSense, identificando o responsável editorial e tornando públicos os processos de pesquisa, revisão e correção sem inventar credenciais.

**Entregas:**
- [x] Perfil indexável `/autores/matteus-oberdan` com atuação verificável, LinkedIn, contato, revisão e artigos publicados.
- [x] Avatar profissional consistente em SVG, sem apresentar imagem gerada como fotografia real.
- [x] Assinatura clicável nos artigos e schema `Person` com URL interna e `sameAs` externo.
- [x] Página Sobre ampliada com criação, objetivo, fontes, atualização e canal para erros.
- [x] Política Editorial ampliada com critérios de fontes, proibição de dados inventados, revisão, patrocínio, automação e frequência.
- [x] Página `/correcoes` em `noindex,follow`, fora do sitemap XML até existir histórico real.
- [x] Descoberta do perfil pelo sitemap, mapa do site e footer.
- [x] Testes de autoria, schema, indexação, correções e integridade do avatar.

**Fora do escopo:** inventar formação ou certificações, alterar fórmulas, publicar correções fictícias, ativar AdSense e executar o inventário técnico completo da Sprint 4.

**Definition of Done:** build sem avisos, suíte automatizada verde e relatório em `docs/adsense/SPRINT_3_REPORT.md`.

---

## Sprint 87 — Conteúdo completo das calculadoras prioritárias (concluída)

**Objetivo:** executar a Sprint 2 do plano de aprovação AdSense, transformando as calculadoras prioritárias em páginas editoriais completas sem alterar fórmulas.

**Entregas:**
- [x] Catálogo editorial estático e específico para 12 calculadoras prioritárias.
- [x] Seções de funcionamento, itens incluídos/excluídos, interpretação e erros comuns.
- [x] Exemplos calculados em runtime pelo mesmo `ICalculatorApplicationService` usado pela UI.
- [x] Fontes oficiais, data de revisão, responsável editorial, relacionados e aviso educativo.
- [x] Duas FAQs específicas adicionais por calculadora, também expostas no schema visível.
- [x] Conteúdo longo excluído do modo embed.
- [x] Layout responsivo compartilhado e testes contra conteúdo duplicado/incompleto.

**Calculadoras:** salário líquido, rescisão CLT, férias, décimo terceiro, INSS, IRRF, hora extra, FGTS, PJ vs CLT, MEI, juros compostos e financiamento.

**Fora do escopo:** mudanças de fórmulas, indexação, ativação do AdSense e geração de conteúdo por IA.

**Definition of Done:** build sem avisos, suíte automatizada verde, matriz em `docs/adsense/CONTENT_QUALITY_MATRIX.md` e relatório em `docs/adsense/SPRINT_2_REPORT.md`.

---

## Sprint 86 — AdSense: confiança e remoção de placeholders (concluída)

**Objetivo:** executar a Sprint 1 do plano de aprovação AdSense, removendo sinais não comprovados e qualquer aparência de monetização antes da aprovação.

**Entregas:**
- [x] Removidos `+250k cálculos`, estrelas, avatares e selos de popularidade sem telemetria comprovável.
- [x] Removidos “ML Prime”, `IA 2080` e promessas promocionais não sustentadas.
- [x] `ConfigurableAdSlotProvider` retorna vazio com anúncios desligados e omite slots sem ID real.
- [x] Home, dúvidas e assistente não possuem placeholders estáticos.
- [x] Política de Privacidade, compliance e checklist alinhados ao comportamento real.
- [x] Testes protegem ausência de placeholders e alegações não comprovadas.

**Fora do escopo:** fórmulas, indexação, CMP, ativação do AdSense e conteúdo editorial em massa.

**Definition of Done:** build sem avisos, suíte automatizada verde e relatório em `docs/adsense/SPRINT_1_REPORT.md`.

---

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

## Sprint 30 — Formulário em camadas e INSS/IRRF (concluída)

**Objetivo:** reduzir campos visíveis de uma vez e melhorar clareza fiscal nas calculadoras INSS e IRRF.

**Entregas:**
- [x] Formulário em camadas: salário líquido, férias e 13º
- [x] IRRF com opção de salário bruto (desconta INSS automaticamente)
- [x] Faixa de tabela exibida no resultado de INSS e IRRF
- [x] Correção: meses do 13º fora do accordion de rescisão

---

## Meta: ser referência no nicho (Sprints 31–38)

**North star:** resultado **confiável** (paridade com sites líderes ±R$ 1–5), **UX que o usuário entende** (poucos campos essenciais, detalhes opcionais, resultado resumido primeiro) e **transparência** (metodologia, faixas, avisos contextuais).

**Trilhas paralelas:**
- **Sprints 31–38** — excelência das 17 calculadoras (motor + UX funcional)
- **Sprints 39–46** — redesign dark Premium Liquid (fidelidade visual Stitch)
- **Sprint 20** — AdSense pós-aprovação Google (quando a conta for aprovada)

**Agents por sprint:**

| Agent | Responsabilidade |
|-------|------------------|
| **Backend/Calculators** | Motor, regras CLT/fiscal, calibração, novos campos |
| **WebApp/Frontend** | Formulário em camadas, tooltips, resultado agrupado, mobile |
| **QA/Test** | Benchmark de paridade, regressão, smoke WebApp |
| **SEO/Content** | FAQ, metodologia, artigos cruzados, JSON-LD |

**Definition of Done (todas):** `dotnet test` verde; cenários de benchmark documentados; `CHANGELOG.md` + este arquivo atualizados.

---

## Sprint 31 — Férias e 13º nível referência (concluída)

**Objetivo:** cobrir modos que sites líderes oferecem e que ainda faltavam no motor.

| Agent | Entregas |
|-------|----------|
| Backend | Abono pecuniário, 1ª/2ª parcela do 13º, adiantamento, dias 20/30/automático, férias em dobro |
| Frontend | Accordion com opções de férias e adiantamento do 13º; tooltips nos campos de meses |
| QA | 10 cenários em `Sprint31FeatureTests` |
| SEO | FAQ dedicado em férias e 13º com links cruzados |

---

## Sprint 32 — Holerite completo (salário líquido, bruto necessário, proposta)

> **Status:** escopo **absorvido pela Sprint 53** (trilha pos-Sprint 50). Nao implementar em duplicata — usar Sprint 53 como referencia ativa.

**Objetivo:** paridade total entre as três calculadoras de holerite — mesmo conjunto de descontos, mesmo extrato.

| Agent | Entregas |
|-------|----------|
| Backend | VT, VR/VA e plano de saúde em campos separados; pensão alimentícia (% ou valor); isenção IRRF (faixa 2026); bruto necessário com faixa (“entre R$ X e Y”) |
| Frontend | Camada essencial (bruto) + “Ajustar descontos”; resultado agrupado nas três; proposta com destaque “ganho no bolso” vs “% no bruto” |
| QA | Paridade salário líquido ↔ bruto necessário (ida e volta); proposta com 4 cenários |
| SEO | Atualizar artigos “salário líquido” e “proposta salarial” |

---

## Sprint 33 — Rescisão: lacunas legais e confiança

> **Status:** escopo **absorvido pela Sprint 55** (trilha pos-Sprint 50). Nao implementar em duplicata.

**Objetivo:** fechar gaps que ainda separam o resultado de sites especializados em rescisão.

| Agent | Entregas |
|-------|----------|
| Backend | Seguro-desemprego (informativo, demissão sem justa causa); adiantamento de 13º já pago; média salarial com HE/comissão (campo opcional); regra dos 15 dias em férias na rescisão (auditoria) |
| Frontend | Avisos contextuais **no painel de resultado** (não só na explicação); ícones `(i)` nos campos que mais mudam o valor |
| QA | Expandir `CalculatorBenchmarkTests` para 15+ cenários rescisão (incl. experiência, aposentadoria, acordo 484-A) |
| SEO | Seção em `/como-calculamos` só para rescisão; disclaimer TRCT/holerite |

---

## Sprint 34 — PJ×CLT e MEI profundos

> **Status:** escopo **absorvido pela Sprint 59** (trilha pos-Sprint 50). MEI desenquadramento ja corrigido; restante em Sprint 59.

**Objetivo:** comparador que responde “vale a pena ser PJ?” com a mesma profundidade dos concorrentes.

| Agent | Entregas |
|-------|----------|
| Backend | Anexo Simples (I–V) com alíquota sugerida; pró-labore editável (%); benefícios CLT em valor (13º, férias+1/3, FGTS) como “custo oculto”; MEI: limite anual, DAS por atividade, alerta de teto |
| Frontend | Wizard PJ×CLT passo 4 opcional “benefícios”; MEI com faturamento anual acumulado; links cruzados PJ ↔ MEI ↔ custo funcionário |
| QA | 6 cenários PJ×CLT documentados; MEI no limite vs. abaixo do teto |
| SEO | FAQ “PJ ou CLT”; artigo MEI atualizado |

---

## Sprint 35 — Hora extra e FGTS integrados

**Objetivo:** ferramentas trabalhistas complementares alinhadas à rescisão e ao holerite.

| Agent | Entregas |
|-------|----------|
| Backend | HE: quantidades por tipo (50%, 100%, noturna); modo “calcular hora pelo salário” como padrão; DSR explícito no extrato. FGTS: motivos iguais à rescisão; saldo × multa × valor sacável separados |
| Frontend | Formulário por blocos (jornada → horas → resultado); FGTS com resultado em 3 linhas (depósito, multa, saque) |
| QA | HE: Súmula 172 TST em 5 cenários; FGTS: multa 40%/20%/0% por motivo |
| SEO | Links hora extra → rescisão (“média salarial”) |

---

## Sprint 36 — Financeiras avançadas (juros e financiamento)

**Objetivo:** igualar calculadoras financeiras de referência (investimentos e crédito).

| Agent | Entregas |
|-------|----------|
| Backend | Juros: taxa anual **ou** mensal com conversão; tabela mês a mês (últimos 12 + totais). Financiamento: entrada + valor financiado; CET aproximado (informativo) |
| Frontend | Toggle taxa anual/mensal; tabela responsiva pós-cálculo; comparativo Price×SAC já existente mantido |
| QA | Juros: aporte + taxa anual vs. mensal; financiamento: entrada 20% + CET ordem de grandeza |
| SEO | FAQ juros compostos e financiamento |

---

## Sprint 37 — Suite de paridade automatizada

**Objetivo:** **não regredir** — qualquer mudança futura passa por benchmark contra referências externas.

| Agent | Entregas |
|-------|----------|
| Backend | Arquivo `CalculatorBenchmarkCatalog.cs` com cenários nomeados por slug (entrada + valor esperado + tolerância + fonte) |
| QA | Mínimo **5 cenários/slug** nas 10 calculadoras prioritárias; job CI opcional `benchmark` |
| Frontend | Badge “Validado com cenários de referência” em `/como-calculamos` |
| SEO | Página metodologia com tabela de fontes e data da última calibração |

**Calculadoras prioritárias:** rescisão, salário líquido, férias, 13º, INSS, IRRF, PJ×CLT, hora extra, FGTS, financiamento.

---

## Sprint 38 — UX referência (polish transversal)

**Objetivo:** sensação de produto premium em **todas** as calculadoras.

| Agent | Entregas |
|-------|----------|
| Frontend | Accordion “Ajustar detalhes” padronizado nas 17; tooltips `(i)` nos 20 campos de maior impacto; warnings pós-cálculo em rescisão, PJ×CLT e holerite |
| Backend | Normalização de labels via `CalculatorFieldProfile`; mensagens de validação em linguagem simples |
| QA | `GoLiveSmokeTests` + snapshot de formulário das 17 slugs |
| SEO | BreadcrumbList JSON-LD nas calculadoras; revisão title/description por slug |

---

## Meta: Dark Premium Liquid (Sprints 39–46)

**Objetivo:** fidelidade visual **igual aos mocks** do Stitch dark (`stitch_redesing/stitch_meu_valor_l_quido_dark_redesign/`), sem Tailwind CDN — tokens mapeados para `--valora-*` em `site.css`.

**Trilha paralela** às Sprints 31–38 (motor/UX de calculadoras). Os mocks ficam **apenas na máquina local** (pasta `stitch_redesing/` no `.gitignore`).

**Documentação:** [docs/STITCH_DARK_FIDELITY_PLAN.md](STITCH_DARK_FIDELITY_PLAN.md) · [docs/STITCH_DARK_REDESIGN_PROMPT.md](STITCH_DARK_REDESIGN_PROMPT.md)

**Agents por sprint:**

| Agent | Responsabilidade |
|-------|------------------|
| **WebApp/Frontend** | Portar `code.html` → Razor; CSS em `site.css`; comparar com `screen.png` |
| **QA/Test** | `GoLiveSmokeTests`, `BrandAssetsTests`, smoke visual por rota |
| **SEO/Content** | Metadados e JSON-LD inalterados; revisar contraste e legibilidade |

**Definition of Done (cada sprint dark):** comparar **390px** e **1280px** com `screen.png`; `dotnet test` verde; `CHANGELOG.md` + este arquivo + `STITCH_DARK_FIDELITY_PLAN.md` atualizados.

---

## Sprint 39 — Fundação dark Premium Liquid (concluída)

**Objetivo:** trocar o tema claro pelo design system dark e alinhar shell + home aos mocks.

| Agent | Entregas |
|-------|----------|
| Frontend | Tokens `Premium Liquid` em `:root`; Plus Jakarta Sans; header/bottom nav glass; brand mark ícone + wordmark |
| Frontend | Home: hero mobile (`Educação Financeira`, glow) e desktop (split); bento com icon boxes; CTA com glow |
| QA | `BrandAssetsTests` atualizado para brand Stitch; suite completa verde |
| Docs | `STITCH_DARK_FIDELITY_PLAN.md`, sprints 39–46 neste arquivo, README |

**Referência Stitch:** `home_mobile`, `home_desktop_dark_premium`, `premium_liquid/DESIGN.md`

---

## Sprint 40 — Shell compartilhado e home polish (concluída)

**Objetivo:** componentes globais dark em **todas** as páginas; home com paridade total aos mocks.

| Agent | Entregas |
|-------|----------|
| Frontend | Footer dark multi-coluna (Produtos / Legal / redes); form cards, inputs, radio choices, result panel |
| Frontend | Blog cards na home com imagem, badge de categoria e tempo de leitura |
| Frontend | Ad slot horizontal dark (desktop); teaser oculto no mobile; tokens teal unificados |
| QA | 244 testes verdes |

**Referência Stitch:** `home_mobile` (footer, blog), `home_desktop_dark_premium` (ad slot)

---

## Sprint 41 — Central de calculadoras (concluída)

**Objetivo:** hub `/calculadoras` idêntico ao Stitch mobile e desktop.

| Agent | Entregas |
|-------|----------|
| Frontend | Mobile: busca, chips horizontais, cards em linha com badge + chevron, CTA sugerir |
| Frontend | Desktop: hero, sidebar com categorias e contagem, grid 3 colunas, featured + CTA |
| Frontend | `_CalculadoraHubRowCard` (mobile) e `_CalculadoraHubCard` redesenhado (desktop) |
| QA | `GoLiveSmokeTests` hub verde (121 testes WebApp) |

**Referência Stitch:** `central_de_calculadoras_mobile`, `central_de_calculadoras_desktop`

---

## Sprint 42 — Template calculadora (detail) (concluída)

**Objetivo:** layout padrão dark para **todas** as 17 calculadoras via partials compartilhados.

| Agent | Entregas |
|-------|----------|
| Frontend | Split desktop 7/5: formulário glass + resultado sticky dark |
| Frontend | Header sem gradiente; breadcrumb desktop; lead visível no mobile |
| Frontend | Inputs `#0e0e0f`, foco teal, accordion dark; valor líquido com glow emerald |
| QA | Smoke `GoLiveSmokeTests` detail verde (121 testes WebApp) |

**Referência Stitch:** `calculadora_de_sal_rio_l_quido_mobile`, `calculadora_de_sal_rio_l_quido_desktop`

---

## Sprint 43 — Calculadoras prioritárias dark (concluída)

**Objetivo:** telas complexas com layout próprio no Stitch.

| Agent | Entregas |
|-------|----------|
| Frontend | Rescisão CLT: header trabalhista, seção &quot;Dados do Contrato&quot;, callout FGTS no painel |
| Frontend | PJ×CLT: wizard 3 passos dark com stepper, dicas laterais e resumo antes do submit |
| Frontend | Mobile: hint fiscal INSS/IRRF; badge roxo; accordion destacado em férias/13º |
| QA | Smoke rescisão + PJ×CLT verde (121 testes WebApp) |

**Referência Stitch:** `calculadora_de_rescis_o_desktop`, `comparador_clt_vs_pj_desktop`

---

## Sprint 44 — Conteúdo e ajuda (concluída)

**Objetivo:** FAQ, blog e metodologia no tema dark.

| Agent | Entregas |
|-------|----------|
| Frontend | FAQ dark: sidebar desktop, chips mobile, accordion com badges de categoria |
| Frontend | Blog hub/artigo: cards `#1C1C1F`, TOC no aside, newsletter dark |
| Frontend | Metodologia wide dark com tabelas INSS/IRRF |
| QA | Smoke FAQ, blog, artigo e metodologia verde (121 testes WebApp) |

**Referência Stitch:** `faq_mobile`, `faq_desktop`, `blog_desktop`, `artigo_do_blog_desktop`, `metodologia_desktop`

---

## Sprint 45 — Painel e institucional (concluída)

**Objetivo:** páginas de suporte e conversão no dark.

| Agent | Entregas |
|-------|----------|
| Frontend | Meu painel mobile + desktop (cards de simulação, empty state, cards `#1C1C1F`) |
| Frontend | Sobre, contato, newsletter com formulários dark e aside FAQ |
| Frontend | Privacidade e termos (layout legível, índice lateral desktop sticky) |
| QA | Smoke contato, newsletter, sobre e legal verde (124 testes WebApp) |

**Referência Stitch:** `meu_painel_mobile`, `meu_painel_desktop`, `sobre_n_s_desktop`, `contato_desktop`, `newsletter_desktop`, `privacidade_e_termos_desktop`

---

## Sprint 46 — Polish final e validação visual (concluída)

**Objetivo:** fechar gaps e garantir paridade em **todas** as telas Stitch.

| Agent | Entregas |
|-------|----------|
| Frontend | Página de erro 404/500 dark (`valora-stitch-error`) |
| Frontend | Nav desktop: search pill, item ativo com borda teal |
| Frontend | Varredura final `site.css` (focus rings, tokens dark, scoped CSS limpo) |
| QA | Smoke erro/404/header + suite verde (127 testes WebApp) |
| Docs | `STITCH_DARK_FIDELITY_PLAN.md` com todas as linhas “Concluída”; README atualizado |

**Referência Stitch:** `p_gina_de_erro_desktop` + revisão de todas as pastas

---

## Trilha Fidelidade Stitch v2 (Sprints 60–64)

**Origem:** auditoria jun/2026 — mocks locais vs produção; Sprints 39–46 entregaram fundação, mas fidelidade visual não atinge 100%.

**Objetivo:** paridade pixel-a-pixel com `stitch_redesing/.../screen.png` em **390px** e **1280px**.

**Documentação:** [STITCH_DARK_FIDELITY_PLAN.md](STITCH_DARK_FIDELITY_PLAN.md) (tabela de gaps e % por tela).

| Sprint | Foco | Agents |
|--------|------|--------|
| **60** | Salário líquido (donut, % bruto, cards INSS/IRRF, alíquota efetiva) + home desktop (bento 12 col, metodologia, social proof, subtitles mobile) | Frontend, QA |
| **61** | Shell: footer 4 col + newsletter inline, background `#0A0A0B`, ícones wght 300 — **sem Entrar** | Frontend |
| **62** | Hubs: banner CLT×PJ premium, blog newsletter + dica rápida, FAQ CTA gradiente | Frontend, Content |
| **63** | Rescisão multi-card + PJ×CLT wizard visual (3 passos mock ou doc v2 para 4 passos) | Frontend, Backend |
| **64** | Meu painel (greeting + cards), template C1 nas 14 calcs restantes, checklist visual final | Frontend, QA |

**Definition of Done (cada sprint 60–64):** comparar telas do escopo com `screen.png`; `dotnet test` verde; `CHANGELOG.md` + `AGENTS.md` + `STITCH_DARK_FIDELITY_PLAN.md` atualizados.

### Sprint 60 — Salário líquido + home desktop (concluída)

| Agent | Entregas |
|-------|----------|
| Frontend | `_SalarioLiquidoStitchResult.cshtml`, donut SVG, % do bruto, alíquota efetiva, cards educativos desktop |
| Frontend | Home: bento desktop 12 col (featured + ML Prime), seção Metodologia, social proof, subtitles bento mobile |
| QA | `Sprint60FidelityTests`, `SalarioLiquidoStitchResultBuilderTests` |

**Referência Stitch:** `calculadora_de_sal_rio_l_quido_mobile`, `calculadora_de_sal_rio_l_quido_desktop`, `home_desktop_dark_premium`, `home_mobile`

### Sprint 61 — Shell global sem login (concluída)

| Agent | Entregas |
|-------|----------|
| Frontend | Footer 4 colunas: marca, Calculadoras, Institucional, Newsletter inline (`_FooterStitchNewsletter.cshtml`) |
| Frontend | Background `--valora-background: #0A0A0B`; Material Symbols **wght 300** |
| Produto | **Sem botão Entrar** — mock Stitch ignorado; **Meu painel** cobre a jornada sem auth (AdSense) |
| QA | `Sprint61ShellTests` |

### Sprint 62 — Hubs (calculadoras, blog, FAQ) (concluída)

| Agent | Entregas |
|-------|----------|
| Frontend | Banner premium CLT×PJ na central (`valora-stitch-calc-premium-banner`); placeholder de busca Stitch |
| Frontend | Blog: card dica rápida (`_BlogStitchTipCard`) + newsletter (`_BlogStitchNewsletterSection`) |
| Frontend | FAQ: CTA gradiente “Ainda com dúvidas?” / “Falar com suporte”; lead hero Stitch |
| QA | `Sprint62HubTests` |

**Referência Stitch:** `central_de_calculadoras_desktop`, `blog_desktop`, `faq_mobile`

### Sprint 63 — Rescisão multi-card + PJ×CLT visual (concluída)

| Agent | Entregas |
|-------|----------|
| Frontend | Rescisão: breakdown multi-card (`_RescisaoStitchBreakdown`) + resumo sticky (`_RescisaoStitchSummary`) |
| Frontend | PJ×CLT: hero wizard, callout Factor-R, veredito anual com barras e cards de detalhamento |
| Backend | `RescisaoStitchResultBuilder`, `PjVsCltStitchDisplayBuilder` |
| QA | `Sprint63CalculatorFidelityTests`, builders unit tests |

**Referência Stitch:** `calculadora_de_rescis_o_desktop`, `comparador_clt_vs_pj_desktop`  
**Divergência intencional:** wizard PJ×CLT permanece em **4 passos** (benefícios CLT); mock usa 3.

### Sprint 64 — Meu painel + template C1 + polish (concluída)

| Agent | Entregas |
|-------|----------|
| Frontend | Meu painel bento: greeting, perfil local, cálculos salvos, leituras sugeridas, newsletter |
| Frontend | Template C1 nas 14 calculadoras: modifiers fiscal/layered/financial/trabalhista + botão “Calcular agora” |
| QA | `Sprint64FidelityTests` (14 slugs C1 + painel + 404) |

**Referência Stitch:** `meu_painel_desktop`, `meu_painel_mobile`, template C1 em `STITCH_DARK_REDESIGN_PROMPT.md`  
**Divergência intencional:** sem perfil premium/login do mock; “Leituras sugeridas” no lugar de favoritos com auth.

---

## Trilha referência no nicho e tráfego orgânico (Sprints 65–67)

**Origem:** decisão de produto — ser referência no nicho salário/trabalho (não portal genérico), com tráfego natural e base para AdSense.

**North star:** hubs temáticos no funil salarial + conteúdo editorial cirúrgico + uma calculadora de alto ROI por vez.

**Especificação completa:** `docs/NICHO_REFERENCIA_PLAN.md` (URLs, SEO, calendário 6 meses, matriz de calculadoras).

**Agents:** leiam o plano e **decidam por onde começar** (ver seção “Onde os agents começam”). Ordem sugerida: **65 → 66 → 67**; Sprint 66 (planejamento + artigos) pode avançar em paralelo à 65.

| Sprint | Passo | Tema | Status |
|--------|-------|------|--------|
| **65** | 1 | Hubs temáticos (`/desligamento`, `/negociar-salario`, `/virar-pj`) | **CONCLUÍDA** |
| **66** | 2 | Calendário editorial 6 meses + ≥4 artigos novos | **CONCLUÍDA** |
| **67** | 3 | Próxima calculadora (PO escolhe 1 na matriz do plano) | **CONCLUÍDA** |
| **68** | 2 | Calendário editorial meses 3–6 (7 artigos restantes) | **CONCLUÍDA** |

**Paralelo permitido:** Sprint **51** (AdSense) quando Google aprovar — não bloqueia 65–67.

**Definition of Done (trilha):** `dotnet test` verde; hubs no sitemap; artigos com calculadora relacionada; uma calculadora nova se Sprint 67 concluída; `CHANGELOG.md` + `AGENTS.md` atualizados.

---

### Sprint 65 — Hubs temáticos (Passo 1) — CONCLUÍDA

**Objetivo:** três landing pages que agregam calculadoras, artigos, FAQ e jornada — reforço de SEO e pageviews sem virar portal genérico.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Páginas `/desligamento`, `/negociar-salario`, `/virar-pj` via `ThematicHubLoader` + `_ThematicHubPage` |
| SEO/Content | Title, description, canonical, breadcrumbs, JSON-LD; intro editorial por hub |
| QA/Test | `Sprint65ThematicHubTests`; rotas em `GoLiveSmokeTests` |
| Documentation | Sitemap, `MapaDoSite`, `CHANGELOG.md` |

**Critérios de aceite:** ver `docs/NICHO_REFERENCIA_PLAN.md` § Passo 1 — atendidos.

---

### Sprint 66 — Calendário editorial 6 meses (Passo 2) — CONCLUÍDA

**Objetivo:** calendário versionado em `NICHO_REFERENCIA_PLAN.md` e primeiros 4 artigos (Mês 1 IRRF + Mês 2 desligamento + negociação).

| Agent | Entregas |
|-------|----------|
| SEO/Content | 4 artigos em `BlogArticleSeedData.cs`; `BlogEditorialCatalog.Sprint66EditorialSlugs` |
| WebApp/Frontend | Hubs `/desligamento` e `/negociar-salario` atualizados com novos artigos |
| QA/Test | `Sprint66BlogTests` (editorial, sitemap, hubs) |
| Documentation | `CHANGELOG.md`, `AGENTS.md` |

**Artigos novos:** `irrf-2026-reducao-imposto`, `seguro-desemprego-quem-tem-direito`, `multa-fgts-40-ou-20`, `aumento-salario-quanto-sobra-liquido`.

---

### Sprint 67 — Calculadora seguro-desemprego (Passo 3) — CONCLUÍDA

**Objetivo:** uma calculadora nova dentro do nicho; **PO escolheu `seguro-desemprego`** (matriz em `NICHO_REFERENCIA_PLAN.md`).

| Agent | Entregas |
|-------|----------|
| Product Owner | Escolha `seguro-desemprego` registrada no `CHANGELOG.md` |
| Backend/Calculators | `UnemploymentInsuranceCalculator`, tabela MTE 2026, benchmarks, jornada |
| WebApp/Frontend | `/calculadoras/seguro-desemprego`, field profile, hub `/desligamento` |
| SEO/Content | Artigo e FAQ atualizados com link para calculadora dedicada |
| QA/Test | `Sprint67UnemploymentInsuranceTests`, `Sprint67WebTests` |

**Calculadora:** `seguro-desemprego` — parcelas, carência e elegibilidade (tabela MTE vigente 11/01/2026).

---

### Sprint 68 — Calendário editorial meses 3–6 (Passo 2, continuação) — CONCLUÍDA

**Objetivo:** concluir os 7 artigos restantes do calendário de 6 meses em `NICHO_REFERENCIA_PLAN.md` (meses 3–6).

| Agent | Entregas |
|-------|----------|
| SEO/Content | 7 artigos em `BlogArticleSeedData.cs`; `BlogEditorialCatalog.Sprint68EditorialSlugs` |
| WebApp/Frontend | Hubs `/negociar-salario` e `/virar-pj` atualizados; imagens hero |
| QA/Test | `Sprint68BlogTests` (editorial, sitemap, hubs) |
| Documentation | `CHANGELOG.md`, `AGENTS.md` |

**Artigos novos:** `quanto-preciso-ganhar-para-receber-x`, `mei-desenquadramento-o-que-fazer`, `pro-labore-pj-quanto-retirar`, `decimo-terceiro-primeira-segunda-parcela`, `ferias-abono-pecuniario-vale-a-pena`, `emprestimo-consignado-desconto-holerite`, `reserva-emergencia-quanto-guardar`.

---

## Trilha diferenciação e crescimento orgânico (Sprints 69–78)

**Origem:** decisão de produto pós go-live — reforçar valor percebido (PDF, conferência de holerite, jornadas) e manter ritmo editorial sem expandir para fora do nicho salário/trabalho.

**North star:** o usuário **entende, confere e compartilha** simulações com aparência profissional — não só vê um número.

**Especificação de referência:** conversa de produto (jun/2026) + tiers de diferenciação acordados com PO.

**Agents:** leiam `AGENTS.md` (tabela Sprint ativa) antes de codar. Ordem sugerida: **69 → 70 (paralelo) → 71 → 72 → 73 → 74 → 75–78**.

| Sprint | Tier | Tema | Status |
|--------|------|------|--------|
| **69** | PDF | Relatório PDF premium (logo, extrato, inputs) | **CONCLUÍDA** |
| **70** | Conteúdo | Editorial contínuo (2 artigos/mês) | **CONCLUÍDA** (lote 1) |
| **71** | 1 | Conferir holerite (validação INSS/IRRF 2026) | **CONCLUÍDA** |
| **72** | 1 | Comparador visual de propostas salariais | **CONCLUÍDA** |
| **73** | 1 | Checklist interativo de rescisão | **CONCLUÍDA** |
| **74** | 1 | Páginas programáticas expandidas | **CONCLUÍDA** |
| **75** | 2 | Simulador “E se…” (cenários pré-montados) | **CONCLUÍDA** |
| **76** | 2 | Meu painel: comparar 2 cenários salvos | **CONCLUÍDA** |
| **77** | 2 | Badge “Tabelas 2026” + FAQ expandido | **CONCLUÍDA** |
| **78** | 3 | Widget embed + newsletter semanal | **CONCLUÍDA** |

**Paralelo permitido:** Sprint **51** (AdSense) quando Google aprovar — não bloqueia 69–78.

**Definition of Done (trilha):** `dotnet test` verde; entrega alinhada ao nicho; `CHANGELOG.md` + `AGENTS.md` atualizados ao concluir cada sprint.

**Calendário sugerido (1 dev):** Jul/26 → 69+70 · Ago/26 → 71 · Set/26 → 72+70 · Out/26 → 73+74 · Nov/26 → 75–76 · Dez/26 → 77–78.

---

### Sprint 69 — PDF Premium — CONCLUÍDA

**Objetivo:** relatório PDF com identidade visual Valora, logo, dados informados, extrato agrupado e resumo — substituir layout genérico atual em `CalculatorResultPdfGenerator`.

| Agent | Entregas |
|-------|----------|
| Product Owner | Mock de referência (salário líquido, proposta, rescisão) |
| WebApp/Frontend | `CalculatorPdfReportContext`, `CalculatorPdfInputSummaryBuilder`, redesign QuestPDF |
| Backend/Calculators | Sem mudança de fórmula; inputs decodificados no WebApp |
| QA/Test | `Sprint69PdfTests` + regressão endpoints PDF |
| Security | Sem PII persistida; PDF sem anúncios |
| Documentation | `CHANGELOG.md`, `AGENTS.md` |

**Definition of Done:** logo no PDF; seção “Dados informados”; extrato em tabela (proventos/descontos/informativos); destaque do líquido; disclaimer + URL; `dotnet test` verde.

---

### Sprint 70 — Editorial contínuo — CONCLUÍDA (lote 1 + lote 2 + lote 3 + lote 4 + lote 5)

**Objetivo:** manter **2 artigos/mês** com calculadora relacionada, hub e FAQ — ritmo pós-calendário Sprint 68.

| Agent | Entregas |
|-------|----------|
| SEO/Content | 2 artigos long tail por ciclo mensal (~5 min leitura) |
| WebApp/Frontend | `BlogEditorialCatalog`; cross-link nos hubs |
| QA/Test | `Sprint70BlogTests`, `Sprint70Lote2BlogTests`, `Sprint70Lote3BlogTests`, `Sprint70Lote4BlogTests`, `Sprint70Lote5BlogTests` |
| Documentation | `docs/BLOG_EDITORIAL_PLAN.md` |

**Lote 1 (jun/2026):** `acordo-484a-verbas-e-multa-fgts`, `custo-total-clt-para-empregador`.

**Lote 2 (jun/2026, publicação escalonada):** `ferias-coletivas-clt-guia-completo`, `pedir-demissao-ou-aguardar-dispensa`.

**Lote 3 (jun/2026, publicação escalonada):** `dissidio-salarial-2026-como-avaliar`, `vale-refeicao-desconto-holerite`.

**Lote 4 (jun/2026, publicação escalonada):** `experiencia-clt-direitos-e-rescisao`, `home-office-clt-descontos`.

**Lote 5 (jul/2026):** `vale-transporte-home-office-hibrido`, `plano-saude-holerite-coparticipacao`.

**Próximo:** Sprint 51 quando AdSense aprovar ou lote 6 editorial — ver `docs/BLOG_EDITORIAL_PLAN.md`.

**Definition of Done:** 2 artigos publicados; sitemap; links para calculadora + `/como-calculamos` + FAQ; ≥850 palavras.

---

### Sprint 71 — Conferir holerite (Tier 1) — CONCLUÍDA

**Objetivo:** usuário informa valores do holerite → validação educativa contra tabelas 2026.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | `PayslipValidationService` (tolerância ±R$ 1) |
| WebApp/Frontend | Rota `/conferir-holerite` |
| SEO/Content | Links no hub `/negociar-salario` e artigo `como-conferir-holerite` |
| QA/Test | `Sprint71PayslipValidationTests`, `Sprint71ConferirHoleriteTests` |

---

### Sprint 72 — Comparador visual de propostas (Tier 1) — CONCLUÍDA

**Objetivo:** evoluir `proposta-salarial` — lado a lado, % real no bolso, ganho anual, CTA share/PDF.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | `SalaryProposalStitchResultBuilder`, `_PropostaSalarialStitchResults`, CSS `.valora-stitch-proposta-*` |
| SEO/Content | Reforço hub `/negociar-salario` |
| QA/Test | `Sprint72PropostaComparisonTests` |

---

### Sprint 73 — Checklist interativo de rescisão (Tier 1) — CONCLUÍDA

**Objetivo:** checklist pós-demissão no hub `/desligamento` (localStorage).

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | `RescisaoChecklistCatalog`, `_RescisaoChecklist`, `rescisao-checklist.js` |
| SEO/Content | FAQ rich snippets no hub; intro atualizada |
| QA/Test | `Sprint73RescisaoChecklistTests` |

---

### Sprint 74 — Páginas programáticas expandidas (Tier 1) — CONCLUÍDA

**Objetivo:** escalar `SalaryBandCatalog` / `CltPjBandCatalog` e variantes com dependentes.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | 46 faixas; rotas `/1-dependente` e `/2-dependentes`; sitemap expandido |
| SEO/Content | Titles/descriptions e editorial únicos por faixa e variante |
| QA/Test | `Sprint74ProgrammaticPagesTests` |

---

### Sprint 75 — Simulador “E se…” (Tier 2) — CONCLUÍDA

**Objetivo:** cenários pré-montados (pedir demissão, aceitar PJ, vender férias).

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | `WhatIfScenarioCatalog` com presets de `CalculatorInput` |
| WebApp/Frontend | Cards na home e hubs; links com `?r=` |
| QA/Test | `WhatIfScenarioCatalogTests`, `Sprint75WhatIfScenarioTests` |

---

### Sprint 76 — Meu painel: comparar cenários (Tier 2) — CONCLUÍDA

**Objetivo:** comparar 2 simulações salvas no `/meu-painel`.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | UI diff lado a lado; checkboxes na lista; `local-panel.js` + CSS `.valora-stitch-panel-compare-*` |
| QA/Test | `Sprint76PanelCompareTests` (markup + script localStorage) |

---

### Sprint 77 — Badge atualização + FAQ expandido (Tier 2) — CONCLUÍDA

**Objetivo:** badge “INSS/IRRF 2026 · Revisado em [mês]”; novas páginas `/duvidas/`.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | `_TaxTablesRevisionBadge` no template C1 (INSS/IRRF) |
| SEO/Content | 7 FAQs indexáveis em `PopularQuestionsCatalog` |
| QA/Test | `Sprint77TaxTablesBadgeAndFaqTests` |

---

### Sprint 78 — Widget + newsletter (Tier 3) — CONCLUÍDA

**Objetivo:** distribuição (embed) e retenção (newsletter semanal).

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Landing `/widget` Stitch + snippet interativo (`widget-hub.js`) |
| SEO/Content | `WeeklyNewsletterTemplateCatalog`, preview `/newsletter`, `docs/NEWSLETTER_WEEKLY_TEMPLATE.md` |
| Monetization | Embed sem anúncios (regressão mantida) |
| QA/Test | `Sprint78WidgetAndNewsletterTests` |

---

### Sprint 79 — Indexação inicial e primeiros ganhos SEO — CONCLUÍDA

**Objetivo:** transformar a indexação inicial no Search Console em ganho prático de rastreio, corrigindo sinais técnicos e criando rotina operacional.

| Agent | Entregas |
|-------|----------|
| SEO/Content | `docs/SEARCH_CONSOLE_INDEXATION_PLAYBOOK.md` com URLs prioritárias, interpretação de status e checklist |
| WebApp/Frontend | `lastmod` no `/sitemap.xml` para páginas estáticas, calculadoras, FAQs, faixas e artigos |
| WebApp/Frontend | Redirect permanente `/duvidas/o-que-e-irrf` → `/duvidas/irrf-quem-paga-e-como-calcular` |
| QA/Test | Testes para `lastmod` no sitemap e redirect do slug intuitivo de IRRF |

**Critérios de aceite:** sitemap segue processável pelo Google; URLs prioritárias têm rotina de inspeção manual; slug intuitivo de IRRF não gera 404; `dotnet test` verde.

---

### Sprint 80 — Assistente educativo Meu Valor Líquido — CONCLUÍDA

**Objetivo:** publicar um chat educativo responsivo, inspirado no mock Stitch "IA 2080", limitado ao nicho salário/trabalho e sem IA generativa em produção.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Página `/assistente` com layout desktop + mobile, sugestões rápidas, respostas guiadas e links para calculadoras |
| WebApp/Frontend | Botão flutuante global com pop-up perguntando se o usuário quer iniciar o chat |
| SEO/Content | Metadados, breadcrumb JSON-LD, sitemap e mapa do site com `/assistente` |
| Monetization | Espaço publicitário discreto previsto no mock; placeholder removido pela Sprint 86 |
| QA/Test | Smoke da página, launcher global, sitemap e mapa do site |

**Critérios de aceite:** chat deixa claro que é educativo; não coleta PII; aponta para calculadoras; é responsivo; `/assistente` é indexável; `dotnet test` verde.

**Evolução futura:** RAG com conteúdo próprio, limites de uso, logs sem dados sensíveis e bloqueio explícito de aconselhamento jurídico/contábil individual.

---

### Sprint 81 - Diagnostico do Holerite - CONCLUIDA

**Objetivo:** transformar `/conferir-holerite` em uma experiencia de diagnostico, com status geral, leitura por linha e checklist pratico para o usuario conversar com o RH.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Hero orientado a fluxo, CTA "Gerar diagnostico", painel responsivo de status geral e cards por INSS/IRRF/liquido |
| Backend/Calculators | Reuso de `PayslipValidationService`; sem duplicar regra fiscal no WebApp |
| Product/UX | Checklist contextual para RH e proximos passos para salario liquido e assistente |
| QA/Test | Regressao WebApp para carregamento, diagnostico positivo e diferenca relevante |

**Criterios de aceite:** a pagina explica se o holerite parece correto, quando ha diferenca relevante e quais pontos conferir; mantem carater educativo; `dotnet test` verde.

---

### Sprint 82 - Conversao interna nos artigos - CONCLUIDA

**Objetivo:** transformar leitura editorial em acao pratica, levando usuarios dos artigos para calculadoras, assistente, hubs e FAQs sem sair do nicho salario/trabalho.

| Agent | Entregas |
|-------|----------|
| Product/SEO | Mapa contextual de proximo passo por calculadora relacionada |
| WebApp/Frontend | Painel responsivo no template de artigo com CTA primario para calculadora, CTA secundario para assistente e links para hub/FAQ |
| Architecture | `BlogConversionPathCatalog` centraliza rotas editoriais sem regra fiscal no WebApp |
| QA/Test | `Sprint82BlogConversionTests` cobre artigos com calculadora relacionada e rotas contextuais |
| Documentation | `CHANGELOG.md`, `AGENTS.md`, `docs/BLOG_EDITORIAL_PLAN.md` |

**Criterios de aceite:** todo artigo com calculadora relacionada tem painel de conversao; CTAs internos ficam claros e educativos; mobile nao quebra layout; `dotnet test` verde.

**Proximo recomendado:** Sprint 83 (autoridade editorial para AdSense) antes da revisão final do Google; depois Sprint 70 lote 5 ou Sprint 51 quando AdSense aprovar.

---

### Sprint 83 - Autoridade editorial para AdSense - CONCLUIDA

**Objetivo:** elevar os sinais de confiança, autoria e transparência editorial para melhorar a chance de aprovação e manutenção do Google AdSense.

| Agent | Entregas |
|-------|----------|
| SEO/Content | Página `/politica-editorial` com fontes oficiais, revisão, limites e correções |
| WebApp/Frontend | Card editorial reutilizável com foto, bio, LinkedIn e responsividade |
| Architecture | `EditorialAuthorCatalog` centraliza dados do autor sem duplicar texto em Razor |
| SEO Técnico | JSON-LD `Article` com `Person`, `sameAs`, imagem e publisher |
| QA/Test | `Sprint83AdSenseTrustTests` cobrindo autoria, schema, sitemap e páginas institucionais |

**Criterios de aceite:** autor visível nos artigos; LinkedIn e foto pública disponíveis; Política Editorial indexável; sitemap/mapa/footer linkam a página; `dotnet test` verde.

**Proximo recomendado:** Sprint 70 lote 5 (definir pauta editorial) ou Sprint 51 quando AdSense aprovar.

---

### Sprint 84 - Otimizacao CTR GSC - CONCLUIDA

**Objetivo:** transformar o primeiro sinal forte do Google Search Console em ganho de CTR sem criar pagina duplicada nem mexer pesado durante a revisao do AdSense.

| Agent | Entregas |
|-------|----------|
| SEO/Content | Titulo, resumo e abertura do artigo de vale-transporte hibrido alinhados ao cluster real de consultas |
| SEO/Content | Titles/metas de vale-refeicao, home office CLT, ferias coletivas, acordo 484-A e salario liquido programatico ajustados para consultas com CTR baixo |
| WebApp/SEO | Meta description renderizada com termos de intencao: regime hibrido, dias presenciais, desconto em holerite e salario liquido |
| Interlinking | Artigo de home office aponta para o guia dedicado de vale-transporte proporcional, reforcando a pagina com maior oportunidade |
| QA/Test | `Sprint84GscCtrTests` cobrindo titulo, descricao e termos principais dos clusters GSC |
| Documentation | `CHANGELOG.md`, `AGENTS.md` e `docs/BLOG_EDITORIAL_PLAN.md` atualizados com rotina GSC CTR |

**Criterios de aceite:** pagina `/blog/vale-transporte-home-office-hibrido` mantem conteudo natural, melhora aderencia a consultas com muita impressao e CTR baixo, sem trocar slug ou perder links internos.

**Proximo recomendado:** aguardar 7 a 14 dias de Search Console para medir CTR do artigo; se continuar com posicao 1-10 e CTR baixo, testar novo ajuste de title/meta antes de criar nova pagina.

---

### Sprint 85 - VT hibrido: calculadora, FAQ e medicao GSC - CONCLUIDA

**Objetivo:** transformar a oportunidade de impressao alta e CTR baixo em utilidade direta, sem trocar o slug do artigo que ja aparece no Google.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | Nova calculadora `vale-transporte-hibrido` com custo por dia, dias presenciais, limite educativo de 6% e comparacao com holerite |
| WebApp/UX | Campos, tooltips, iconografia, defaults, links relacionados e promocao de conferir holerite |
| SEO/Content | Artigo `/blog/vale-transporte-home-office-hibrido` aponta para a calculadora especifica e ganhou FAQ de VT hibrido |
| Interlinking | Hub `/negociar-salario`, FAQ de VT e artigo de home office reforcam o cluster interno |
| Measurement | `docs/SEO_MONTHLY_REVIEW.md` registra experimento, janela de 7-14 dias e criterio de decisao |
| QA/Test | Testes de formula, benchmark, render, sitemap, hub e conversao editorial |

**Criterios de aceite:** `/calculadoras/vale-transporte-hibrido` renderiza e calcula; artigo mantem slug e passa a converter para a calculadora propria; sitemap e hub descobrem a nova pagina; proxima revisao GSC ocorre apos 7 a 14 dias.

**Proximo recomendado:** subir na VPS, solicitar/aguardar recrawl natural da pagina e comparar CTR da pagina no Search Console em 7 a 14 dias.

---

## Trilha pos-auditoria: referencia no nicho e monetizacao (Sprints 47-52)

**Origem:** auditoria manual em producao + comparacao com referencias externas de salario liquido/INSS/IRRF 2026.

**North star:** calculadoras confiaveis, fluxo sem friccao, metodologia transparente e paginas prontas para AdSense sem comprometer UX, Core Web Vitals ou politica de anuncios.

**Agents por trilha:**

| Agent | Responsabilidade |
|-------|------------------|
| **Product Owner** | Sequencia de valor, criterios de aceite e corte de escopo |
| **Backend/Calculators** | Motor, tabelas, contratos e paridade de formulas |
| **WebApp/Frontend** | Formularios, radios/selects, tooltips, resultado e mobile |
| **QA/Test** | Smoke de todas as calculadoras, regressao fiscal e benchmark |
| **SEO/Content** | Metodologia, conteudo de suporte, schema e interlinking |
| **Monetization/AdSense** | Slots, politicas, CLS e checklist pos-aprovacao |
| **Security** | CSP, cookies, privacidade e antiforgery |
| **Infrastructure** | Deploy VPS, observabilidade e rollback |
| **Documentation** | `sprint-plan.md`, `CHANGELOG.md` e guias de operacao |

**Definition of Done (todas):** `dotnet test .\MeuValorLiquido.slnx` verde; evidencia de smoke manual/automatizado; documentacao atualizada quando houver entrega de produto.

---

## Sprint 47 - Hotfix de confianca e deploy (concluida)

**Objetivo:** colocar em producao as correcoes encontradas na auditoria antes de ampliar escopo.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | `salario-liquido` desconta `OtherDiscounts` e mostra linha separada no extrato |
| WebApp/Frontend | Radios de ferias, rescisao, hora extra, financiamento, FGTS, MEI e conversor com opcao padrao marcada |
| QA/Test | Regressao de desconto extra + teste de radios padrao em `CalculatorFormFieldsTests` |
| Infrastructure | Deploy na VPS em `/var/www/meu-valor-liquido` com rollback documentado |
| Documentation | `CHANGELOG.md` atualizado com hotfix e resultado dos testes |

**Criterios de aceite:**
- Producao: salario R$ 4.000, VT R$ 240 e outros R$ 100 retorna liquido R$ 3.291,40.
- Todas as 17 calculadoras conseguem submeter o formulario com valores default.
- `dotnet test .\MeuValorLiquido.slnx` verde antes do deploy.

**Validacao em 2026-06-17:**
- `dotnet test .\MeuValorLiquido.slnx --no-restore` verde: 268 testes.
- Producao `/health` retornou `Healthy`.
- Producao `/calculadoras/salario-liquido` retornou R$ 3.291,40 no cenario de aceite.
- Radios padrao confirmados em producao: ferias, rescisao, hora extra, financiamento, FGTS, MEI e conversor de salario.
- Tentativa de SSH direto na VPS bloqueada por credenciais nesta maquina; deploy manual nao foi executado daqui, mas a producao ja esta servindo o commit `8d8acbf`.

---

## Sprint 48 - Suite de paridade fiscal/trabalhista (concluida)

**Objetivo:** transformar a auditoria manual em protecao permanente contra regressao.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | `CalculatorBenchmarkCatalog` com entradas, esperado, tolerancia, fonte e data de calibracao |
| QA/Test | 5 cenarios por slug nas 10 calculadoras prioritarias: salario liquido, bruto necessario, proposta, ferias, 13o, rescisao, INSS, IRRF, FGTS e hora extra |
| SEO/Content | Lista de fontes em `/como-calculamos` com Planalto, DOU e referencias comerciais |
| Documentation | Guia rapido para adicionar benchmark ao alterar formula |

**Criterios de aceite:**
- Benchmarks separados dos testes unitarios triviais.
- Tolerancia documentada por tipo de calculadora.
- Fontes oficiais linkadas quando a regra vier de lei/tabela.

**Entregue em 2026-06-17:**
- `CalculatorBenchmarkCatalog` com 50 cenarios fixos, 5 por slug prioritario.
- Cobertura: salario liquido, salario bruto necessario, proposta salarial, ferias, 13o, rescisao CLT, INSS, IRRF, FGTS e hora extra.
- `CalculatorBenchmarkCatalogTests` valida bruto, liquido, linhas criticas, tolerancia, fontes e cobertura minima.
- `/como-calculamos` exibe fontes, quantidade de cenarios e data de calibracao.
- Guia de manutencao atualizado em `docs/how-to-create-calculator.md`.

---

## Sprint 49 - UX de confianca nas calculadoras (concluida)

**Objetivo:** reduzir duvida do usuario e aumentar conversao/tempo de pagina sem poluir a tela.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Tooltips `(i)` nos campos de maior impacto via `CalculatorFieldTooltipCatalog` e partial `_FieldLabel` |
| WebApp/Frontend | Avisos no painel de resultado (`CalculatorResultWarningBuilder`) para rescisao, ferias/13o, holerite, PJ vs CLT e MEI |
| Backend/Calculators | Labels normalizados em `CalculatorFieldProfile` (VT vs outros descontos na rescisao) |
| QA/Test | `Sprint49TrustUxTests` com smoke das 17 calculadoras e avisos contextuais |
| SEO/Content | Microcopy educativa curta nos tooltips, sem texto promocional |

**Criterios de aceite:**
- Usuario entende por que o resultado difere de holerite/TRCT.
- Nenhum tooltip ou aviso quebra layout mobile (bubble com largura maxima e foco por teclado/toque).
- PDF/share continuam sem anuncios e com disclaimers existentes.

**Entregue em 2026-06-17:**
- `CalculatorFieldTooltipCatalog` + `_FieldLabel.cshtml` com icones `info` nos campos de dependentes, descontos, datas, aviso previo, FGTS, taxa e meses.
- `CalculatorResultWarningBuilder` + `_CalculatorResultWarnings.cshtml` no extrato e no comparador PJ x CLT.
- Estilos `valora-field-tip` e `valora-result-warning` em `site.css`.
- `dotnet test .\MeuValorLiquido.slnx` verde: 350 testes.

---

## Sprint 50 - Metodologia, E-E-A-T e conteudo de apoio (concluida)

**Objetivo:** reforcar autoridade para SEO e confianca, especialmente para AdSense.

| Agent | Entregas |
|-------|----------|
| SEO/Content | `/como-calculamos` com metodologia por categoria (trabalhista, fiscal, financeiro), data de calibracao e links para calculadoras |
| SEO/Content | Artigos atualizados (salario liquido, INSS, IRRF, rescisao, ferias, FGTS) + novo artigo MEI |
| Product Owner | Secoes "Como validamos esta estimativa" priorizam intencao de busca e monetizacao sem tom promocional |
| WebApp/Frontend | Badge `Validado com cenarios de referencia` nas 10 calculadoras prioritarias (`_BenchmarkValidationBadge`) |
| QA/Test | `Sprint50EeatTests` para metadata, links internos, badge e schema Article |

**Criterios de aceite:**
- Conteudo deixa claro que e estimativa educativa, nao consultoria oficial.
- Links internos conectam artigo -> calculadora -> metodologia.
- Paginas legais e politicas permanecem acessiveis.

**Entregue em 2026-06-17:**
- `MetodologiaCategoryCatalog`, `CalculatorBenchmarkHelper` e badge sem aparentar selo oficial.
- Seed de blog sincroniza conteudo editorial a cada deploy; 16 artigos no catalogo.
- `dotnet test .\MeuValorLiquido.slnx` verde: 365 testes.

---

## Trilha ativa pos-Sprint 50 — valor do produto sem AdSense (Sprints 53-59)

**Contexto:** conta Google AdSense ainda nao aprovada. Priorizar confianca, profundidade das calculadoras e crescimento organico antes de monetizacao por anuncios.

**North star desta trilha:** o usuario entende **quanto recebe, quanto desconta e quanto sobra** — com extrato, metodologia e jornada clara entre ferramentas.

**Proxima sprint a implementar:** **Sprint 51** (AdSense, quando Google aprovar) ou manutencao continua.

| Prioridade | Sprints | Status |
|------------|---------|--------|
| **Alta** | — | Sprints 53-59 e 52 concluidas |
| **Media** | — | Trilha pos-Sprint 50 concluida |
| **Baixa / bloqueada** | 51 (AdSense) | 51 aguarda aprovacao Google |

**Mapeamento com trilha antiga (Sprints 31-38):** nao duplicar trabalho. Escopo da antiga Sprint 32 → **Sprint 53**; antiga Sprint 33 → **Sprint 55**; antiga Sprint 34 → **Sprint 59**.

**Handoff Cursor ↔ Codex:** ao iniciar ou concluir uma sprint desta trilha, atualizar o status aqui, `CHANGELOG.md` e `AGENTS.md` (secao "Sprint ativa").

---

## Sprint 53 - Holerite completo (CONCLUIDA)

**Objetivo:** paridade total entre `salario-liquido`, `salario-bruto-necessario` e `proposta-salarial` — mesmo conjunto de descontos e extrato coerente.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | VT, VR/VA e plano de saude em campos separados; pensao alimenticia (% ou valor); isencao IRRF (faixa 2026); bruto necessario com faixa ("entre R$ X e Y") |
| WebApp/Frontend | Camada essencial (bruto) + "Ajustar descontos"; resultado agrupado nas tres; proposta com destaque "ganho no bolso" vs "% no bruto" |
| QA/Test | Paridade salario liquido ↔ bruto necessario (ida e volta); proposta com 4 cenarios; atualizar `CalculatorBenchmarkCatalog` |
| SEO/Content | Atualizar artigos "salario liquido" e "proposta salarial" |

**Criterios de aceite:**
- As tres calculadoras aceitam os mesmos descontos opcionais relevantes.
- Extrato separa VT, VR/VA, plano e pensao (nao tudo em "outros descontos").
- `dotnet test .\MeuValorLiquido.slnx` verde.

**Arquivos-chave:** `CalculationEngine.cs`, `NetSalaryCalculator`, `HoleriteExtratoBuilder.cs`, `GrossSalarySolver`, `CalculatorFieldProfile.cs`, `Details.cshtml`, `_HoleriteOptionalFields.cshtml`.

---

## Sprint 54 - Jornadas guiadas entre calculadoras (CONCLUIDA)

**Objetivo:** conectar ferramentas no momento certo para o usuario nao "morrer" no resultado isolado.

| Agent | Entregas |
|-------|----------|
| WebApp/Frontend | Bloco "Proximo passo" contextual no resultado e em `_CalculatorResultPanel` / share |
| Product Owner | 3 jornadas minimas: proposta recebida; saida da empresa; descobrir liquido desejado |
| SEO/Content | Microcopy educativa curta por jornada (sem tom promocional) |
| QA/Test | Testes WebApp por slug com links esperados no HTML pos-calculo |

**Jornadas minimas:**

1. **Proposta recebida** → `proposta-salarial` → `salario-liquido` → `pj-vs-clt`
2. **Saida da empresa** → `rescisao-clt` → `fgts` → FAQ multa FGTS (seguro-desemprego na Sprint 55)
3. **Liquido desejado** → `salario-bruto-necessario` → `salario-liquido` → faixa `/salario-liquido/{valor}`

**Criterios de aceite:**
- Cada jornada tem pelo menos 2 links uteis visiveis apos calcular.
- Links respeitam estado compartilhavel (`?r=`) quando fizer sentido.

**Arquivos-chave:** `CalculatorJourneyCatalog.cs`, `CalculatorJourneyLinkBuilder.cs`, `_CalculatorJourneyNextSteps.cshtml`, `_CalculatorResultPanel.cshtml`.

---

## Sprint 55 - Rescisao: lacunas legais e confianca (CONCLUIDA)

**Objetivo:** fechar gaps que ainda separam o resultado de sites especializados em rescisao.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | Seguro-desemprego (informativo, demissao sem justa causa); adiantamento de 13o ja pago; media salarial com HE/comissao (campo opcional); auditoria regra dos 15 dias em ferias na rescisao |
| WebApp/Frontend | Avisos TRCT/holerite reforçados no painel; tooltips nos campos que mais mudam valor |
| QA/Test | Expandir `CalculatorBenchmarkCatalog` com 15+ cenarios rescisao (experiencia, aposentadoria, acordo 484-A) |
| SEO/Content | Secao em `/como-calculamos` so para rescisao |

**Criterios de aceite:**
- Seguro-desemprego aparece como linha informativa (nao como promessa de valor oficial).
- Benchmarks de rescisao cobrem motivos raros ja suportados no motor.

**Arquivos-chave:** `CalculationEngine.cs` (rescisao), `CalculatorBenchmarkCatalog.cs`, `ComoCalculamos.cshtml`.

---

## Sprint 56 - Metricas enxutas e decisao por dados (CONCLUIDA)

**Objetivo:** usar o que ja existe em `ProductMetricsService` para priorizar backlog sem dashboard complexo.

| Agent | Entregas |
|-------|----------|
| Product Owner | Rotina semanal: top calculadoras, taxa de calculo, share/PDF/painel |
| WebApp/Frontend | Melhorar `/metricas-internas` com ranking e periodo (7/30 dias) |
| Infrastructure | Checklist pos-deploy documentado em `docs/DEPLOY.md` |
| QA/Test | Smoke pos-deploy das 17 calculadoras (pode reutilizar `GoLiveSmokeTests`) |

**Criterios de aceite:**
- Decisoes de Sprint 57-58 baseadas em dados agregados sem PII.
- Nenhum dado pessoal de simulacao persistido no servidor.

**Nota:** versao completa de observabilidade permanece na **Sprint 52** (baixa prioridade, apos esta).

---

## Sprint 57 - Faixas salariais e widget incorporavel (CONCLUIDA)

**Objetivo:** crescimento organico via SEO programatico util e backlinks legitimos.

| Agent | Entregas |
|-------|----------|
| SEO/Content | Expandir faixas `/salario-liquido/{valor}` com conteudo unico por faixa |
| WebApp/Frontend | CTA do widget em blog, contato e artigos relacionados |
| QA/Test | Sitemap inclui novas faixas; embed continua sem anuncios |
| Documentation | Atualizar `docs/SEO_CHECKLIST.md` com faixas adicionadas |

**Criterios de aceite:**
- Cada faixa nova tem texto editorial unico (nao thin content).
- `/widget` e `/incorporar` linkados a partir de pelo menos 3 paginas publicas.

---

## Sprint 58 - Conteudo editorial direcionado (CONCLUIDA)

**Objetivo:** artigos que respondem intencao de busca e levam a calculadora certa.

| Agent | Entregas |
|-------|----------|
| SEO/Content | Artigos ou atualizacoes: conferir holerite; proposta salarial; rescisao vs TRCT |
| WebApp/Frontend | Secao "Como validamos" e links para calculadora + metodologia em cada artigo novo |
| QA/Test | `BlogContentTests` e schema Article para artigos novos |

**Criterios de aceite:**
- Cada artigo novo linka para calculadora, `/como-calculamos` e pelo menos uma FAQ.
- Tom educativo; sem promessa de consultoria oficial.

---

## Sprint 51 - Monetizacao AdSense (PRIORIDADE BAIXA — aguardando aprovacao Google)

**Status:** **NAO INICIAR** ate aprovacao da conta AdSense. Infraestrutura base ja existe (`AdsOptions`, `_AdSlot`, CSP, cookie consent).

**Objetivo:** preparar e/ou ativar anuncios reais com baixo risco de politica, CLS e experiencia ruim.

| Agent | Entregas |
|-------|----------|
| Monetization/AdSense | Revisao de `docs/ADSENSE_COMPLIANCE.md` e `docs/adsense-checklist.md` contra o estado atual |
| WebApp/Frontend | Slots com dimensoes estaveis nas paginas de maior trafego e sem anuncios em PDF, email ou widget |
| Security | CSP/cookie consent revisados para scripts de publicidade |
| QA/Test | Testes de renderizacao de slots, embed sem anuncios e paginas legais |
| Infrastructure | Configuracao por ambiente: ads off em dev/test, on apenas em producao |

**Criterios de aceite:**
- Sem layout shift agressivo.
- Sem anuncios em superficies proibidas pelo proprio projeto.
- Rollback simples por variavel de ambiente.

---

## Sprint 59 - PJ x CLT e MEI profundos (CONCLUIDA)

**Objetivo:** comparador que responde "vale a pena ser PJ?" com profundidade de referencia.

| Agent | Entregas |
|-------|----------|
| Backend/Calculators | Anexo Simples (I-V) com aliquota sugerida; pro-labore editavel (%); beneficios CLT em valor (13o, ferias+1/3, FGTS) como custo oculto |
| WebApp/Frontend | Wizard PJ x CLT passo opcional "beneficios"; links cruzados PJ ↔ MEI ↔ custo funcionario |
| QA/Test | 6 cenarios PJ x CLT documentados no benchmark |
| SEO/Content | FAQ "PJ ou CLT"; artigo MEI atualizado |

**Criterios de aceite:**
- Comparativo mostra custo oculto CLT de forma educativa.
- MEI ja corrigido para desenquadramento (nao reimplementar).

**Nota:** escopo parcial da antiga Sprint 34. Fazer apos Sprints 53-55.

---

## Sprint 52 - Observabilidade completa (CONCLUIDA)

**Objetivo:** medir quais calculadoras geram valor e priorizar proximas melhorias com dados (versao completa; a versao enxuta e a Sprint 56).

| Agent | Entregas |
|-------|----------|
| Product Owner | Dashboard de priorizacao: top calculadoras, taxa de calculo, PDF/share/painel |
| Infrastructure | Checklist pos-deploy e monitoramento basico de erros 500/404 |
| QA/Test | Smoke automatizado periodico das 17 calculadoras em producao/staging |
| SEO/Content | Revisao mensal de paginas com impressao alta e CTR baixo |
| Documentation | Rotina de calibracao trimestral de tabelas e fontes |

**Criterios de aceite:**
- Decisoes de backlog baseadas em metricas agregadas sem PII.
- Falhas de formulario/calculo viram alerta antes de afetar receita.
- Proximo ciclo de sprints nasce dos dados, nao de achismo.

---

## Ordem recomendada pos-auditoria

**Concluidas:** 47 → 48 → 49 → 50

**Trilha ativa (sem AdSense):**

```
53 (holerite) -> 54 (jornadas) -> 55 (rescisao)
      -> 56 (metricas enxutas) -> 57 (faixas + widget) -> 58 (conteudo)
      -> 51 (AdSense, quando Google aprovar)
      -> 59 (PJ x CLT profundo) -> 52 (observabilidade completa)
```

**Atalho pragmatico:** nao iniciar Sprint 51 enquanto AdSense nao aprovar. Priorizar Sprints 53-55 porque reforcam o valor unico do produto (extrato confiavel e jornada clara).

---

### Checklist visual manual (390px / 1280px)

Comparar cada `screen.png` local em `stitch_redesing/.../` com `http://localhost:8080`.

**Trilha 60–64 (fidelidade v2):**

- [x] Sprint 60: salário líquido mobile/desktop + home mobile/desktop
- [x] Sprint 61: shell global (footer 4 col, newsletter, tokens) — sem Entrar
- [x] Sprint 62: central calculadoras, blog, FAQ
- [x] Sprint 63: rescisão, PJ×CLT
- [x] Sprint 64: meu painel, institucionais, erro, template C1 restante

**Legado 39–46 (revalidar na Sprint 64):**

- [ ] Home mobile e desktop
- [ ] Central de calculadoras mobile e desktop
- [ ] Calculadora detail (salário líquido) mobile e desktop
- [ ] Rescisão CLT e PJ×CLT desktop
- [ ] FAQ, blog, artigo, metodologia
- [ ] Meu painel, sobre, contato, newsletter, privacidade/termos
- [ ] Página de erro 404

---

## Ordem recomendada e dependências

```
31 (férias/13º) → 32 (holerite) → 33 (rescisão refinamento)
                              ↘
34 (PJ/MEI) ← 32              35 (HE/FGTS)
36 (financeiras) — paralelo
37 (benchmark suite) — após 31–33, expandir até 38
38 (UX polish) — último ou intercalado a cada 2 sprints

39 (fundação dark) ✓ → 40 (shell) ✓ → 41 (hub) ✓ → 42 (template calc)
                                              ↘
                                    43 (rescisão/PJ×CLT)
40 → 44 (conteúdo) — paralelo após 40
45 (institucional) — após 40
46 (polish) — após 41–45
```

**Estimativa calculadoras:** 8 sprints (31–38) ≈ 8–12 semanas com 1 dev.  
**Estimativa dark:** 8 sprints (39–46) ≈ 6–10 semanas; Sprints 40 e 44 podem rodar em paralelo após a 39.

**Deploy:** adiar até concluir trilha desejada (mínimo 39–42 para experiência dark nas calculadoras principais).

**O que NÃO entra (evitar scope creep):**
- CCT específica por sindicato (só % editável)
- Integração eSocial / TRCT oficial
- App mobile nativo

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
- `_AdSlot` com label “Espaço publicitário” (comportamento histórico substituído pela Sprint 86)
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
- `AdsOptions` + `ConfigurableAdSlotProvider` (placeholders históricos removidos pela Sprint 86)
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
