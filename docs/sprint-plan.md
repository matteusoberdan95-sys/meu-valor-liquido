# Sprint Plan

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
