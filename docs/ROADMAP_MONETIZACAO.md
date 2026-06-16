# Roadmap de Monetização — Meu Valor Líquido

Documento de referência para evolução do produto com foco em tráfego orgânico legítimo, retenção, compartilhamento e monetização via Google AdSense **sem violar políticas do Google**.

## Visão

Transformar o site de um conjunto de calculadoras em uma **plataforma brasileira de respostas práticas** sobre salário, trabalho, CLT, PJ, férias, décimo terceiro, rescisão, INSS, IRRF, FGTS, proposta salarial e planejamento financeiro.

**Princípios:**

- Mudanças incrementais; preservar o que já funciona.
- Monólito modular (.NET, Razor Pages, PostgreSQL, EF Core).
- Sem bot, tráfego artificial, auto-refresh abusivo ou incentivo a clique em anúncios.
- Cálculos estimativos e educativos; aviso legal visível.

---

## Auditoria Inicial (Sprint 0)

### Estrutura e arquitetura

| Área | Localização | Status |
|------|-------------|--------|
| Core / contratos | `src/Core` | OK |
| Utilitários | `src/Shared` | OK |
| Módulos de negócio | `src/Modules/*` | OK |
| WebApp (Razor, EF, DI) | `src/WebApp` | OK |
| Testes | `tests/*` | OK (81 testes) |
| Documentação | `docs/` | Em expansão |

**Padrão:** Modular Monolith (ADR-0001). Regras de cálculo no módulo `Calculators`; persistência EF no `WebApp`.

### Calculadoras existentes (15)

| Slug | Nome | Categoria |
|------|------|-----------|
| `salario-liquido` | Salário líquido | Trabalhista |
| `ferias` | Férias | Trabalhista |
| `decimo-terceiro` | Décimo terceiro | Trabalhista |
| `rescisao-clt` | Rescisão CLT | Trabalhista |
| `hora-extra` | Hora extra | Trabalhista |
| `fgts` | FGTS | Trabalhista |
| `custo-funcionario` | Custo de funcionário | Trabalhista |
| `conversor-salario` | Conversor de salário | Trabalhista |
| `inss` | INSS | Fiscal |
| `irrf` | IRRF | Fiscal |
| `simulador-mei` | Simulador MEI | Fiscal |
| `pj-vs-clt` | PJ vs CLT | Financeiro |
| `juros-compostos` | Juros compostos | Financeiro |
| `financiamento` | Financiamento | Financeiro |
| `multa-atraso` | Multa de atraso | Financeiro |

**Motor:** `CalculationEngine.cs` (switch por slug). Oportunidade futura: handlers `ICalculatorHandler` por calculadora.

### SEO e conteúdo

| Item | Status | Observação |
|------|--------|--------------|
| Canonical, title, description | Parcial | Layout global em `_Layout.cshtml` |
| Open Graph / Twitter | Parcial | Sem `og:image` / `twitter:image` |
| `robots.txt` | OK | `wwwroot/robots.txt` |
| `sitemap.xml` dinâmico | Parcial | Falta `/newsletter` e páginas futuras |
| JSON-LD WebSite | OK | Home |
| JSON-LD FAQPage | OK | Calculadoras |
| JSON-LD Article | OK | Blog |
| BreadcrumbList | Pendente | Sprint 1 |
| Blog | OK | 15 artigos editoriais |
| Páginas institucionais | Parcial | Conteúdo ainda enxuto |

### Anúncios (placeholders)

- Slots: `calculator-top`, `calculator-bottom` (`Modules/Ads`, `_AdSlot.cshtml`).
- AdSense real: **não integrado** (conforme política do projeto).
- Regras detalhadas: `docs/ADSENSE_COMPLIANCE.md`.

### Segurança e privacidade

| Item | Status |
|------|--------|
| Secrets no repositório | Apenas defaults locais (`change-me-local`) |
| `appsettings.Production.json` | Não versionado (`.gitignore`) |
| Headers de segurança | OK (`Program.cs`) |
| Rate limiting em formulários | OK |
| PII em logs | Corrigido: e-mail mascarado no SMTP |
| Log de salário/CPF | Não implementado (política: não logar) |

### Testes, cache e telemetria

| Item | Status |
|------|--------|
| Testes unitários calculadoras | Forte (~55 testes) |
| Testes WebApp (SEO, páginas) | Básico |
| Integração com PostgreSQL real | Placeholder (Testcontainers previsto) |
| Cache de leitura | Ausente |
| Eventos/métricas internas | Ausente (Sprint 13) |

---

## Roadmap por Sprints (crescimento)

| Sprint | Tema | Objetivo de monetização |
|--------|------|-------------------------|
| **0** | Auditoria e segurança | Base documentada; compliance AdSense |
| **1** | SEO técnico essencial | Indexação e rich results |
| **2** | Salário bruto necessário | Diferencial + cauda longa |
| **3** | Páginas por faixa salarial | Tráfego programático útil |
| **4** | Resultado compartilhável | Viralidade orgânica (WhatsApp) |
| **5** | PDF do resultado | Valor percebido + retorno |
| **6** | Modo explicação simples | Tempo na página + links internos |
| **7** | Proposta salarial | Ferramenta altamente compartilhável |
| **8** | CLT x PJ avançada | Página âncora do site |
| **9** | Dúvidas populares | Cauda longa + internal linking |
| **10** | Painel local (localStorage) | Retenção sem login |
| **11** | Widget incorporável | Referência legítima |
| **12** | Performance e Core Web Vitals | RPM e aprovação AdSense |
| **13** | Métricas internas agregadas | Decisões de produto |
| **14** | Institucional + AdSense | Aprovação e confiança |

Sprints legadas (0–16 do repo): fundação, 15 calculadoras, blog — **concluídas**.

---

## Métricas de sucesso (sem PII)

- Sessões orgânicas (Search Console).
- Páginas por sessão e tempo na página.
- Calculadoras mais usadas (eventos agregados, Sprint 13).
- Compartilhamentos e PDFs gerados (agregado).
- RPM/eCPM AdSense (pós-aprovação).

**Não coletar:** salário individual identificável, CPF, e-mail em analytics, IP completo em logs de produto.

---

## Riscos e mitigações

| Risco | Mitigação |
|-------|-----------|
| Clique acidental em anúncio | Espaçamento, altura reservada, revisão `ADSENSE_COMPLIANCE.md` |
| Thin content em páginas programáticas | Conteúdo original por faixa (Sprint 3) |
| Páginas institucionais fracas | Sprint 14 + revisão privacidade |
| Regressão fiscal | Testes + tabelas centralizadas em `BrTaxTables2026` |
| `CalculationEngine` monolítico | Refatorar para handlers quando passar de ~20 calculadoras |

---

## Referências

- `docs/SEO_CHECKLIST.md` — checklist técnico de SEO
- `docs/ADSENSE_COMPLIANCE.md` — regras de anúncios
- `docs/adsense-checklist.md` — pré-requisitos para solicitar AdSense
- `docs/sprint-plan.md` — histórico de sprints do repositório
- `docs/agents.md` — papéis dos agentes
