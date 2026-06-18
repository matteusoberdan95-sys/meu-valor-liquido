# Plano — Referência no nicho e tráfego orgânico

Documento de especificação para as **Sprints 65–67**. Objetivo: posicionar o Meu Valor Líquido como **referência brasileira em salário e trabalho** (não portal genérico), com tráfego natural via SEO, jornadas e profundidade no funil.

**North star desta trilha:** o usuário entende **quanto recebe, quanto desconta e quanto sobra** — com hubs temáticos, conteúdo editorial cirúrgico e novas calculadoras só dentro do nicho.

**Regra de corte:** qualquer feature que não ajude alguém a entender salário, desconto ou verba trabalhista **não entra**.

---

## Pilares do nicho (clusters de SEO)

| Pilar | Temas | Calculadoras âncora |
|-------|-------|---------------------|
| **1 — Holerite mensal** | Líquido, bruto, proposta, INSS, IRRF, VT | `salario-liquido`, `salario-bruto-necessario`, `proposta-salarial`, `inss`, `irrf` |
| **2 — Ciclo de vida CLT** | Férias, 13º, HE, FGTS, rescisão, seguro-desemprego | `ferias`, `decimo-terceiro`, `hora-extra`, `fgts`, `rescisao-clt` |
| **3 — CLT vs autônomo** | PJ, MEI, Simples, custo empresa | `pj-vs-clt`, `simulador-mei`, `custo-funcionario` |
| **4 — O que sobra no bolso** | Crédito, juros, multa, planejamento pós-salário | `financiamento`, `juros-compostos`, `multa-atraso` |

---

## Passo 1 — Hubs temáticos (Sprint 65)

Landing pages que agregam calculadoras, artigos, FAQ e jornada — **não** são portais genéricos; cada hub cobre um momento do funil salarial.

### Hub A — Desligamento

| Campo | Valor |
|-------|--------|
| **Rota** | `/desligamento` |
| **Title SEO** | Desligamento CLT: rescisão, FGTS e próximos passos \| Meu Valor Líquido |
| **Description** | Simule rescisão CLT, FGTS e entenda verbas, descontos e o que conferir no TRCT. Jornada educativa com calculadoras calibradas 2026. |
| **H1** | Saiu da empresa? Entenda sua rescisão |
| **Jornada** | `rescisao-clt` → `fgts` → FAQ multa FGTS → artigo TRCT vs simulação |
| **Calculadoras** | `rescisao-clt`, `fgts`, `decimo-terceiro`, `ferias`, `salario-liquido` |
| **Artigos** | `como-calcular-rescisao-clt`, `rescisao-clt-vs-trct`, `fgts-guia-completo` |
| **FAQ** | Itens de rescisão, FGTS, aviso prévio em `PopularQuestionsCatalog` |
| **CTA secundário** | Seguro-desemprego (placeholder até Sprint 67 se escolhida) |

### Hub B — Negociar salário

| Campo | Valor |
|-------|--------|
| **Rota** | `/negociar-salario` |
| **Title SEO** | Como negociar salário: compare proposta pelo líquido \| Meu Valor Líquido |
| **Description** | Avalie aumento salarial pelo que entra no bolso. Compare bruto atual e proposto com INSS e IRRF 2026. Compartilhe a simulação com RH. |
| **H1** | Negociando salário? Compare pelo líquido |
| **Jornada** | `proposta-salarial` → `salario-liquido` → `salario-bruto-necessario` |
| **Calculadoras** | `proposta-salarial`, `salario-liquido`, `salario-bruto-necessario`, `inss`, `irrf` |
| **Artigos** | `como-avaliar-proposta-salarial`, `o-que-e-salario-liquido`, `como-conferir-holerite` |
| **FAQ** | Proposta, holerite, INSS/IRRF |
| **CTA** | Share/PDF da proposta (já existente) |

### Hub C — Virar PJ

| Campo | Valor |
|-------|--------|
| **Rota** | `/virar-pj` |
| **Title SEO** | CLT ou PJ: vale a pena? Compare líquido e custos \| Meu Valor Líquido |
| **Description** | Compare salário CLT com faturamento PJ ou MEI. Simule Simples, pró-labore e custo oculto dos benefícios CLT. |
| **H1** | Pensando em virar PJ? Compare antes de decidir |
| **Jornada** | `pj-vs-clt` → `simulador-mei` → `custo-funcionario` |
| **Calculadoras** | `pj-vs-clt`, `simulador-mei`, `custo-funcionario`, `salario-liquido` |
| **Artigos** | `pj-ou-clt-qual-melhor`, `mei-faturamento-e-das`, `simples-nacional-pj-guia` (se existir) |
| **FAQ** | PJ ou CLT, MEI, desenquadramento |

### Implementação técnica (orientação aos agents)

| Agent | Responsabilidade |
|-------|------------------|
| **WebApp/Frontend** | Razor Pages `Pages/Desligamento/Index.cshtml`, `Pages/NegociarSalario/Index.cshtml`, `Pages/VirarPj/Index.cshtml` (ou estrutura equivalente); layout Stitch; cards de calculadora reutilizando `_CalculadoraHubRowCard` |
| **SEO/Content** | `ViewData` title/description/canonical; breadcrumbs; JSON-LD `WebPage` ou `CollectionPage`; links cruzados nos artigos existentes |
| **QA/Test** | Smoke das 3 rotas, sitemap, metadata, links internos quebrados |
| **Documentation** | Atualizar `MapaDoSite`, sitemap generator |

**Definition of Done:** 3 hubs publicados, indexáveis, com conteúdo editorial original (intro + seções por ferramenta), no sitemap.

---

## Passo 2 — Calendário editorial 6 meses (Sprint 66)

**Meta:** 2 artigos/mês (12 no período), sempre **1 problema do trabalhador + 1 calculadora**. Sem revista genérica.

### Mês 1 — Autoridade holerite

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `tabela-inss-2026-guia` | Tabela INSS 2026: faixas e como conferir no holerite | `inss` | 1 |
| 3–4 | `irrf-2026-reducao-imposto` | IRRF 2026: redução de imposto e quem está isento | `irrf` | 1 |

### Mês 2 — Desligamento (reforço hub `/desligamento`)

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `seguro-desemprego-quem-tem-direito` | Seguro-desemprego: quem tem direito e como estimar | `rescisao-clt` (até calculadora dedicada) | 2 |
| 3–4 | `multa-fgts-40-ou-20` | Multa FGTS 40% ou 20%: quando cada uma se aplica | `fgts` | 2 |

### Mês 3 — Negociação e proposta

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `aumento-salario-quanto-sobra-liquido` | Aumento de salário: quanto sobra no líquido de verdade | `proposta-salarial` | 1 |
| 3–4 | `quanto-preciso-ganhar-para-receber-x` | Quanto preciso ganhar para receber X líquido? | `salario-bruto-necessario` | 1 |

### Mês 4 — PJ / MEI (reforço hub `/virar-pj`)

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `mei-desenquadramento-o-que-fazer` | MEI: o que acontece ao ultrapassar o limite | `simulador-mei` | 3 |
| 3–4 | `pro-labore-pj-quanto-retirar` | Pró-labore na PJ: quanto retirar sem prejudicar o líquido | `pj-vs-clt` | 3 |

### Mês 5 — Sazonal 13º e férias

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `decimo-terceiro-primeira-segunda-parcela` | 13º salário: 1ª e 2ª parcela e descontos | `decimo-terceiro` | 2 |
| 3–4 | `ferias-abono-pecuniario-vale-a-pena` | Abono pecuniário: vale a pena vender 1/3 das férias? | `ferias` | 2 |

### Mês 6 — Planejamento pós-salário

| Semana | Slug sugerido | Título | Calculadora | Pilar |
|--------|---------------|--------|-------------|-------|
| 1–2 | `emprestimo-consignado-desconto-holerite` | Empréstimo consignado: quanto desconta do salário | Sprint 67 (se consignado) ou `salario-liquido` | 4 |
| 3–4 | `reserva-emergencia-quanto-guardar` | Reserva de emergência: quanto guardar com seu salário | `juros-compostos` + `salario-liquido` | 4 |

### Entregas Sprint 66

| Agent | Entregas |
|-------|----------|
| **SEO/Content** | Este calendário versionado; primeiros **4 artigos** (Mês 1 completo + Mês 2 semana 1–2); cross-link nos hubs da Sprint 65 |
| **WebApp/Frontend** | Cards no blog; links nos hubs; breadcrumbs |
| **Product Owner** | Validar slugs sem duplicar artigos já em `BlogArticleSeedData.cs` |
| **QA/Test** | Contagem mínima, links para calculadoras, sitemap |

**Definition of Done:** calendário em `docs/` (este arquivo, seção mantida); ≥4 artigos novos publicados; nenhum artigo sem calculadora relacionada.

**Status (2026-06):** calendário de 6 meses **concluído** — Sprint 66 (4 artigos) + Sprint 68 (7 artigos). Total 11 artigos novos do plano editorial.

---

## Passo 3 — Próxima calculadora (Sprint 67)

**Product Owner** escolhe **uma** calculadora antes de codar, usando a matriz abaixo. **Backend/Calculators** não implementa as três em paralelo.

### Candidatas (dentro do nicho)

| Slug | Nome | Volume busca | Fit motor atual | Esforço | Ligação |
|------|------|--------------|-----------------|---------|---------|
| `seguro-desemprego` | Seguro-desemprego | Alto | Médio — regras INSS, parcelas, carência | M | Hub `/desligamento`, jornada rescisão |
| `emprestimo-consignado` | Empréstimo consignado | Alto | Baixo — % sobre líquido/bruto, teto margem | P | Holerite, pilar 4 |
| `simples-nacional-pj` | Simples Nacional PJ | Alto | Médio — estende `pj-vs-clt` | G | Hub `/virar-pj` |

**Legenda esforço:** P = pequeno, M = médio, G = grande.

### Critérios de decisão (pontuar 1–5 cada)

1. **Volume de busca** no nicho salário/trabalho (Search Console ou referência externa).
2. **Reuso de código** (`CalculationEngine`, tabelas INSS/IRRF, jornadas existentes).
3. **Reforço de hub** — acelera tráfego nas Sprints 65–66?
4. **Diferenciação** — concorrentes já dominam com qualidade?
5. **Manutenção anual** — tabela muda todo ano?

**Recomendação inicial (Product Owner pode alterar):**

| Ordem | Calculadora | Motivo |
|-------|-------------|--------|
| 1ª | `seguro-desemprego` | Fecha jornada `/desligamento`; citada na Sprint 54/55; alto volume |
| 2ª | `emprestimo-consignado` | Esforço baixo; liga holerite; artigo Mês 6 do calendário |
| 3ª | `simples-nacional-pj` | Maior esforço; complementa PJ×CLT já profundo (Sprint 59) |

### Entregas Sprint 67 (após escolha)

Seguir `docs/how-to-create-calculator.md`:

1. `CalculatorSeedData.cs`, `CalculationEngine.cs`, `CalculatorFieldProfile.cs`
2. FAQ, seed educativo, jornada atualizada
3. Artigo blog + entrada no hub correspondente
4. Testes unitários + benchmark se aplicável
5. `CalculatorJourneyCatalog` e hub temático atualizados

**Definition of Done:** 1 calculadora nova no catálogo; testes verdes; hub e calendário editorial referenciam a ferramenta.

---

## Onde os agents começam (decisão de implementação)

Os três passos são sprints distintas, mas **podem ter trabalho preparatório em paralelo**:

```
                    ┌─────────────────────────────────────┐
                    │  Product Owner: ler este doc e    │
                    │  registrar escolha Sprint 67      │
                    └─────────────────┬───────────────────┘
                                      │
         ┌────────────────────────────┼────────────────────────────┐
         ▼                            ▼                            ▼
  Sprint 65 (Hubs)            Sprint 66 (Conteúdo)          Sprint 67 (Calc)
  WebApp + SEO                SEO/Content primeiro          Backend após escolha PO
         │                            │                            │
         └────────────────────────────┴────────────────────────────┘
                                      │
                    Ordem sugerida se uma sprint por vez:
                    65 → 66 (4 artigos) → 67
                    Ou: 65 + planejamento 66 em paralelo → 67
```

| Situação | Por onde começar |
|----------|------------------|
| Foco em **tráfego rápido** (páginas indexáveis) | **Sprint 65** — hubs com conteúdo original |
| Foco em **AdSense/conteúdo** (aprovação Google) | **Sprint 66** — artigos Mês 1–2 + revisão institucional |
| Foco em **diferenciação técnica** | **Sprint 67** — após PO escolher na matriz |
| **AdSense aprovado** | Intercalar **Sprint 51** (ativar anúncios) com 65 ou 66 — não bloqueia hubs |

**Agents:** ao iniciar, declare no PR/commit qual sprint e qual passo; não misturar escopo das três sem atualizar este doc.

---

## Métricas de sucesso (sem PII)

- Impressões orgânicas nos clusters: salário líquido, rescisão, PJ/MEI.
- Páginas/sessão nas rotas `/desligamento`, `/negociar-salario`, `/virar-pj`.
- Cliques em jornadas e share/PDF a partir dos hubs.
- Novos artigos indexados com CTR no Search Console.

---

## Referências

- `docs/sprint-plan.md` — Sprints 65–67
- `docs/ROADMAP_MONETIZACAO.md` — princípios de monetização
- `docs/how-to-create-calculator.md` — implementação de calculadora
- `src/Modules/Calculators/CalculatorJourneyCatalog.cs` — jornadas existentes
