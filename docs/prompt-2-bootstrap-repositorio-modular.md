# PROMPT 2 - Bootstrap do Repositório Modular

Use este prompt no Cursor/Codex para evoluir o repositório após o bootstrap inicial.

## Objetivo

Manter o Modular Monolith compilável e evoluir módulos sem quebrar limites entre `Core`, `Shared`, `Modules` e `WebApp`.

## Regras

1. Módulos não acessam banco/tabelas de outros módulos.
2. Comunicação entre módulos via contratos, eventos ou application services.
3. WebApp orquestra UX; regra de cálculo fica em `Calculators`.
4. Core pequeno; Shared sem regra de negócio pesada.
5. Sem secrets no repositório.

## Próximas entregas sugeridas

- Persistência PostgreSQL com EF Core por módulo.
- Tabelas iniciais: catálogo, SEO, FAQ, blog, contato.
- Refinar fórmulas trabalhistas/fiscais com testes de regressão.
- Newsletter e envio real via Mailpit.
- Melhorias de SEO (schema, sitemap dinâmico).
- Preparação AdSense (placeholders apenas, sem script real).

## Validação

```powershell
dotnet restore .\MeuValorLiquido.slnx
dotnet build .\MeuValorLiquido.slnx
dotnet test .\MeuValorLiquido.slnx
docker compose config
```
