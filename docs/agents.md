# Agents

## Convenções obrigatórias

Leia **`docs/CODING_CONVENTIONS.md`** antes de editar código.

Resumo para agentes:

- **GlobalUsings por projeto** — não adicione `using` nas classes; exceção: migrations EF.
- **Calculators** — toda lógica fiscal/cálculo no módulo; WebApp só orquestra UI, SEO, PDF e share.
- **Sprints** — atualize `docs/sprint-plan.md` e `CHANGELOG.md` ao concluir entregas.
- **AdSense** — sem anúncios em PDF, e-mail ou widget; ver `docs/ADSENSE_COMPLIANCE.md`.

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
