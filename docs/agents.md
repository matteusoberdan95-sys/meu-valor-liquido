# Agents

## Convenções obrigatórias

Leia **`docs/CODING_CONVENTIONS.md`** antes de editar código.

Se o agente tiver começado fora do Cursor, leia também **`AGENTS.md`** na raiz para o handoff operacional entre ferramentas.

Resumo para agentes:

- **GlobalUsings por projeto** — não adicione `using` nas classes; exceção: migrations EF.
- **Calculators** — toda lógica fiscal/cálculo no módulo; WebApp só orquestra UI, SEO, PDF e share.
- **Sprints** — atualize `docs/sprint-plan.md` e `CHANGELOG.md` ao concluir entregas.
- **AdSense** — sem anúncios em PDF, e-mail ou widget; ver `docs/ADSENSE_COMPLIANCE.md`.
- **Deploy VPS** — o diretório padrão de produção é `/var/www/meu-valor-liquido`.
- **UI compartilhada das calculadoras CLT** — priorize `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de editar slugs individuais.

## Handoff rápido

Use esta sequência para continuar o trabalho sem perder contexto:

1. `git pull origin main`
2. `README.md`
3. `AGENTS.md` (tabela **Sprint ativa**)
4. `docs/CODING_CONVENTIONS.md`
5. `docs/sprint-plan.md` → seção **Trilha referência no nicho (Sprints 65–67)**
6. `docs/NICHO_REFERENCIA_PLAN.md` — hubs, calendário 6 meses, matriz de calculadoras

### Sprint ativa (2026-06)

| Prioridade | Sprints | O quê |
|------------|---------|-------|
| **Alta — próxima** | **67** | Próxima calculadora (`seguro-desemprego` recomendado) |
| Concluídas | 47–66 | Auditoria, nicho, Stitch v2, hubs, conteúdo Sprint 66 |

**Onde começar:** ver `NICHO_REFERENCIA_PLAN.md` § *Onde os agents começam*. Ordem sugerida **65 → 66 → 67**; conteúdo (66) pode avançar em paralelo aos hubs (65).

**Mapeamento legado:** Sprint 32 → 53; Sprint 33 → 55; Sprint 34 → 59 (não implementar em duplicata).

**Regra de nicho:** não adicionar ferramentas ou artigos fora do funil salário/trabalho — ver `NICHO_REFERENCIA_PLAN.md`.

Pontos já entregues (não reabrir sem motivo):

- Sprints 47–50, **53–59, 60–64**: benchmark, holerite, jornadas, PJ×CLT, fidelidade Stitch v2
- Tema dark Premium Liquid (Stitch 39–46)
- Formulário compartilhado CLT em `Details.cshtml`; testes em `CalculatorFormFieldsTests.cs`

## Product Owner Agent

Define escopo, prioridades, backlog e critérios de aceite. Atua em `docs`, `README.md` e planejamento.

## Architecture Agent

Mantém limites do Modular Monolith, ADRs e dependências. Atua em `src/Core`, `src/Shared`, `src/Modules` e `docs`.

## Backend/Calculators Agent

Implementa contratos, validações e fórmulas no módulo Calculators. Não coloca regra pesada no WebApp.

## WebApp/Frontend Agent

Implementa Razor Pages, layout, rotas, formulários, SEO técnico e UX mobile-first.

## Database Agent

Cuida de PostgreSQL, EF Core, migrations e ownership de dados por módulo.

## Infrastructure Agent

Cuida de Docker Compose, Mailpit, CI e ambiente local.

## QA/Test Agent

Garante testes unitários, regressão fiscal, validação e testes web.

## Security Agent

Revisa OWASP, CSRF, headers, rate limiting, logs e privacidade.

## SEO/Content Agent

Cuida de slugs, metadata, sitemap, robots, blog, FAQs e conteúdo educativo.

## Monetization/AdSense Agent

Prepara placeholders e checklist. Não integra AdSense real no MVP.

## Documentation Agent

Mantém documentação técnica, setup, decisões e guias operacionais.
