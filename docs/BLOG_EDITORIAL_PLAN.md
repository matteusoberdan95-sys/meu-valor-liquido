# Plano editorial do blog — Meu Valor Líquido

Documento de referência para **agents (Cursor/Codex)** e para a rotina **Sprint 70 — editorial contínuo** (2 artigos/mês).

**Regra de nicho:** só temas de salário, trabalho, impostos na folha e finanças pessoais ligadas ao holerite — ver `docs/NICHO_REFERENCIA_PLAN.md`.

---

## Ritmo e Definition of Done

| Item | Regra |
|------|--------|
| Cadência | **2 artigos/mês** (lotes numerados: lote 1, lote 2…) |
| Tempo de leitura | **~5 minutos** (≥ **850 palavras** no corpo, ideal 900–1.200) |
| Tom | Educativo, sem promessa de consultoria; AdSense-friendly |
| Links obrigatórios | Calculadora relacionada + `/como-calculamos` + ≥1 FAQ `/duvidas/` |
| Seções obrigatórias | `id="dica-pratica"` + `id="como-validamos"` (via `EnrichContent` ou manual) |
| Código | `BlogArticleSeedData.cs`, slug em `BlogEditorialCatalog`, testes `Sprint70LoteNBlogTests` |
| Hub | Cross-link em hub temático quando fizer sentido (`ThematicHubCatalog`) |
| Capa | **Obrigatória**: `scripts/generate-blog-images.py` → `wwwroot/images/blog/{slug}.webp` |

---

## Padrão de estrutura (copiar dos artigos recentes)

1. **Introdução** (2–3 parágrafos) — problema real do leitor.
2. **3–6 seções `<h2>`** — conceito, passo a passo, exemplos, checklist ou tabela.
3. **Exemplo numérico ilustrativo** (quando aplicável) — sempre “estimativa educativa”.
4. **Links internos** — calculadora, hub, 1–3 artigos relacionados, 1–2 FAQs.
5. **Dica prática** — `AppendPracticalSection` em `BlogArticleSeedData.cs`.
6. **Como validamos** — metodologia + disclaimer (automático se não estiver no HTML).

**Categorias:** `Trabalhista` | `Fiscal` | `Financeiro`

**Autor padrão:** `Matteus Oberdan` (já no seed).

## Padrão visual das capas

As capas do blog fazem parte da identidade do site e devem seguir o mesmo padrão em qualquer máquina/agente:

- Formato obrigatório: WebP 1200×675 em `src/WebApp/wwwroot/images/blog/{slug}.webp`.
- Estilo: dark Premium Liquid, fintech educacional BR, sem stock genérico e sem texto longo.
- Todo slug novo precisa de entrada em `VISUAL_BRIEF` no arquivo `scripts/generate-blog-images.py`.
- Gere localmente com:

```powershell
python scripts/generate-blog-images.py render --slug exemplo-slug
```

Se `python` não estiver no PATH, use o Python bundled do Codex Desktop:

```powershell
& 'C:\Users\mober\.cache\codex-runtimes\codex-primary-runtime\dependencies\python\python.exe' scripts/generate-blog-images.py render --slug exemplo-slug
```

Em Cursor/CLI de outra máquina, use o Python local com Pillow instalado. Depois rode ou mantenha coberto por `BrandAssetsTests`, que falha se algum post do seed estiver sem capa.

---

## Publicados — registro por lote

### Sprint 58
- `como-conferir-holerite`
- `como-avaliar-proposta-salarial`
- `rescisao-clt-vs-trct`

### Sprint 66
- `irrf-2026-reducao-imposto`
- `seguro-desemprego-quem-tem-direito`
- `multa-fgts-40-ou-20`
- `aumento-salario-quanto-sobra-liquido`

### Sprint 68 (calendário 6 meses)
- `quanto-preciso-ganhar-para-receber-x`
- `mei-desenquadramento-o-que-fazer`
- `pro-labore-pj-quanto-retirar`
- `decimo-terceiro-primeira-segunda-parcela`
- `ferias-abono-pecuniario-vale-a-pena`
- `emprestimo-consignado-desconto-holerite`
- `reserva-emergencia-quanto-guardar`

### Sprint 70 — lote 1 (jun/2026)
- `acordo-484a-verbas-e-multa-fgts` → hub `/desligamento`
- `custo-total-clt-para-empregador` → hub `/virar-pj`

### Sprint 70 — lote 2 (jun/2026, publicação escalonada) ✅
- `ferias-coletivas-clt-guia-completo` → hub `/negociar-salario`, calc `ferias`
- `pedir-demissao-ou-aguardar-dispensa` → hub `/desligamento`, calc `rescisao-clt`

---

## Calendário sazonal (sugestões por mês)

Use para escolher os **2 artigos do mês**. Priorize o que ainda **não tem slug** em `BlogArticleSeedData.cs`.

| Mês | Contexto Brasil | Temas sugeridos | Calculadora / hub |
|-----|-----------------|-----------------|-------------------|
| **Jan** | Volta às aulas, reajustes | Orçamento pós-férias; consignado no holerite | `salario-liquido` |
| **Fev** | Carnaval, folga | Férias coletivas carnaval; desconto VT | `ferias` |
| **Mar** | Dia da mulher; PIS (memória) | Licenças e afastamentos (se no nicho); holerite mulher | `salario-liquido` |
| **Abr** | IR declaração | Restituição x holerite; dependentes IRRF | `irrf` |
| **Mai** | Dia do trabalhador | Hora extra + DSR; custo empregador | `hora-extra`, `custo-funcionario` |
| **Jun** | Meio do ano, dissídio | Dissídio salarial; avaliar reajuste pelo líquido | `proposta-salarial` |
| **Jul** | Férias coletivas verão | ✅ Férias coletivas; pedir demissão vs dispensa | `ferias`, `rescisao-clt` |
| **Ago** | Volta às aulas comércio | VT e VR no orçamento; salário mínimo | `salario-liquido` |
| **Set** | Primavera, vagas | Proposta em entrevista; bruto necessário | `proposta-salarial` |
| **Out** | Outubro rosa (se editorial) | Plano de saúde na folha; descontos | `salario-liquido` |
| **Nov** | 1ª parcela 13º | 13º 1ª parcela; planejamento dezembro | `decimo-terceiro` |
| **Dez** | 2ª parcela 13º, férias coletivas | 13º 2ª parcela; férias dezembro | `decimo-terceiro`, `ferias` |

---

## Alta no momento — como agents devem decidir

Antes de propor um artigo, consulte (quando disponível):

1. **`/metricas-internas`** — top calculadoras e taxa de cálculo (`docs/METRICS_ROUTINE.md`).
2. **Search Console** — queries com impressões e CTR baixo (`docs/SEO_MONTHLY_REVIEW.md`).
3. **FAQs sem artigo** — slugs em `PopularQuestionsCatalog` sem post dedicado no blog.
4. **Hubs temáticos** — lacunas em `ThematicHubCatalog.BlogSlugs`.
5. **Notícias trabalhistas/fiscais** — apenas se couber no nicho e tiver **calculadora** para CTA (ex.: mudança de tabela INSS/IRRF → artigo + `/como-calculamos`).

### Matriz rápida de prioridade (1–5)

| Critério | Pergunta |
|----------|----------|
| Busca | Há volume no nicho salário/trabalho? |
| Ferramenta | Existe calculadora pronta para CTA? |
| Hub | Reforça `/desligamento`, `/negociar-salario` ou `/virar-pj`? |
| Unicidade | Slug já existe? (não duplicar) |
| Evergreen | Vale daqui 12 meses sem reescrever tudo? |

**Score ≥ 18:** prioridade alta para o próximo lote.

---

## Sprint 70 - lote 3 publicado (jun/2026, publicação escalonada)

- `dissidio-salarial-2026-como-avaliar` -> hub `/negociar-salario`, calc `proposta-salarial`
- `vale-refeicao-desconto-holerite` -> hub `/negociar-salario`, calc `salario-liquido`

## Sprint 70 - lote 4 publicado (jun/2026, publicação escalonada)

- `experiencia-clt-direitos-e-rescisao` -> hub `/desligamento`, calc `rescisao-clt`
- `home-office-clt-descontos` -> hub `/negociar-salario`, calc `salario-liquido`

## Sprint 70 - lote 5 publicado (jul/2026)

- `vale-transporte-home-office-hibrido` -> hub `/negociar-salario`, calc `vale-transporte-hibrido`
- `plano-saude-holerite-coparticipacao` -> hub `/negociar-salario`, calc `salario-liquido`
- Cadência recente normalizada: artigos publicados em 05/06, 10/06, 15/06, 20/06, 25/06, 30/06 e lote 5 em 05/07.

## Rotina GSC CTR

Antes de abrir pauta nova, exporte `Consultas.csv` e `Páginas.csv` do Google Search Console e procure páginas com:

- muitas impressões nos últimos 7 a 28 dias;
- posição média entre 1 e 10;
- CTR baixo ou zerado;
- consulta claramente dentro do nicho salário/trabalho.

Para esses casos, otimize primeiro a página existente: title, meta description, H1/abertura e um subtítulo natural com os termos reais da consulta. Não troque slug, não crie página duplicada e aguarde 7 a 14 dias antes de novo ajuste.

### Backlog — sazonal / trending

| Gatilho | Artigo |
|---------|--------|
| Nova tabela INSS/IRRF | Atualizar `tabela-inss-2026-guia` ou novo `tabela-inss-{ano}-guia` |
| Pauta “demissão em massa” | `pedir-demissao-ou-aguardar-dispensa` (já publicado) + reforçar hub |
| Pauta MEI limite | `mei-desenquadramento-o-que-fazer` (já existe) — cross-link |
| Black Friday / consumo | Só se ligar a **orçamento com salário líquido** (não puro varejo) |

---

## Checklist do agent (antes do PR)

- [ ] Slug único em `BlogArticleSeedData.GetAll()`
- [ ] ≥ 850 palavras (teste `Sprint70LoteNBlogTests`)
- [ ] `BlogEditorialCatalog.Sprint70LoteNEditorialSlugs` atualizado
- [ ] Hub temático atualizado (se aplicável)
- [ ] `dica-pratica` no switch `AppendPracticalSection`
- [ ] `VISUAL_BRIEF` em `scripts/generate-blog-images.py`
- [ ] `CHANGELOG.md` + `docs/sprint-plan.md` + `AGENTS.md`
- [ ] `dotnet test` verde

---

## Arquivos de referência

| Arquivo | Uso |
|---------|-----|
| `src/WebApp/Data/BlogArticleSeedData.cs` | Conteúdo HTML dos posts |
| `src/WebApp/Infrastructure/BlogEditorialCatalog.cs` | Slugs por sprint/lote |
| `src/WebApp/Infrastructure/ThematicHubCatalog.cs` | Cross-links nos hubs |
| `tests/.../Sprint70Lote2BlogTests.cs` | Modelo de testes do lote |
| `docs/ADSENSE_COMPLIANCE.md` | Tom e políticas de conteúdo |
| `docs/NICHO_REFERENCIA_PLAN.md` | Corte de escopo |

---

- Sprint 82 concluiu conversao interna nos artigos. Proximo ciclo recomendado: Sprint 51 quando AdSense aprovar ou lote 6 editorial.

## Como sugerir novo artigo (template para agents)

Copie e preencha num PR ou issue:

```markdown
### Proposta — [mês/ano]
- **Slug:** `exemplo-slug`
- **Título:** ...
- **Por que agora:** (sazonal / métrica / FAQ gap)
- **Calculadora:** `/calculadoras/...`
- **Hub:** `/desligamento` | `/negociar-salario` | `/virar-pj` | —
- **FAQs relacionadas:** `/duvidas/...`
- **Palavras-chave alvo:** ...
- **Conflito com slug existente:** não / qual
```

Última atualização: **jul/2026** (lote 2 publicado).
