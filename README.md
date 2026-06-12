# Meu Valor Líquido

[![CI](https://github.com/matteusoberdan95-sys/meu-valor-liquido/actions/workflows/ci.yml/badge.svg)](https://github.com/matteusoberdan95-sys/meu-valor-liquido/actions/workflows/ci.yml)

Plataforma brasileira de calculadoras trabalhistas, fiscais e financeiras para ajudar o usuário a entender quanto recebe, quanto desconta e quanto sobra.

Repositório: https://github.com/matteusoberdan95-sys/meu-valor-liquido

Design UI/UX baseado nos protótipos Stitch em `stitch_meu_valor_l_quido_ui_ux/` — ver [docs/ui-ux-stitch.md](docs/ui-ux-stitch.md).
## Clonar o projeto

```powershell
git clone https://github.com/matteusoberdan95-sys/meu-valor-liquido.git
cd meu-valor-liquido
```
## Stack

- C# e .NET 10
- ASP.NET Core Razor Pages
- PostgreSQL
- Entity Framework Core preparado
- FluentValidation
- Serilog
- xUnit, FluentAssertions e Bogus
- Docker Compose com PostgreSQL, Mailpit e WebApp (front + back)

## Como rodar localmente

**Um comando — tudo no Docker:**

```powershell
copy .env.example .env
docker compose up --build
```

Acesse http://localhost:8080 (calculadoras, blog, contato). Mailpit: http://localhost:8025

Detalhes e opções alternativas em [docs/setup-local.md](docs/setup-local.md).

## Testes

```powershell
dotnet test .\MeuValorLiquido.slnx
```

## Arquitetura

O projeto usa Modular Monolith com:

- `src/Core`: primitives, contratos e abstrações globais.
- `src/Shared`: utilidades sem regra de negócio pesada.
- `src/Modules`: módulos de negócio.
- `src/WebApp`: experiência pública e orquestração.
- `tests`: testes por camada/módulo.
- `docs`: documentação técnica e produto.
