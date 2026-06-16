# Changelog

## Unreleased

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
