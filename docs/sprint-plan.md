# Sprint Plan

## Status do roadmap

| Sprint | Tema | Status |
|--------|------|--------|
| 0–1 | Arquitetura e bootstrap | Concluído |
| 2 | Core + fluxo calculadora | Concluído |
| 3–4 | Calculadoras MVP + tabelas 2026 | Concluído |
| 5 | Content, SEO, páginas legais | Concluído (base) |
| 6 | Testes e segurança | Concluído (base) |
| 7 | Local production-like | Concluído (base) |
| 8 | PostgreSQL + EF + seeds | Concluído |
| 9 | INSS/IRRF versionáveis | Concluído |
| 10 | UX por calculadora + ads | Concluído |
| 11 | Sitemap dinâmico + conteúdo DB | Concluído |
| 12 | Mailpit SMTP + contato persistido | Concluído |
| 13 | Health, compressão, docs locais | Concluído |

## Sprint 8 - Persistência e infraestrutura real

Objetivo: PostgreSQL, EF Core, migrations, seed e health check.

Definition of Done: `dotnet ef database update` + app lê catálogo/blog do banco.

## Sprint 9 - Calculadoras com tabelas 2026

Objetivo: `IInssCalculator` / `IIrrfCalculator` e `CalculationEngine` testáveis.

Definition of Done: testes de regressão fiscal passando.

## Sprint 10 - UX e financeiras

Objetivo: campos por calculadora (`CalculatorFieldProfile`) e placeholders de anúncio.

Definition of Done: formulários contextuais nas páginas `/calculadoras/{slug}`.

## Sprint 11 - SEO e conteúdo

Objetivo: sitemap dinâmico, conteúdo educativo no banco, blog via EF.

Definition of Done: `/sitemap.xml` lista calculadoras e posts publicados.

## Sprint 12 - Contato e Mailpit

Objetivo: SMTP Mailpit, mensagens de contato persistidas.

Definition of Done: formulário de contato grava no PostgreSQL e envia e-mail local.

## Sprint 13 - Qualidade local

Objetivo: ambiente documentado, testes verdes, compressão e health.

Definition of Done: `dotnet test` verde + `docs/setup-local.md` atualizado.
