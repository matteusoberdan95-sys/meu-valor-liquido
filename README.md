# Meu Valor Líquido

Plataforma brasileira de calculadoras trabalhistas, fiscais e financeiras para ajudar o usuário a entender quanto recebe, quanto desconta e quanto sobra.

## Stack

- C# e .NET 10
- ASP.NET Core Razor Pages
- PostgreSQL
- Entity Framework Core preparado
- FluentValidation
- Serilog
- xUnit, FluentAssertions e Bogus
- Docker Compose com PostgreSQL e Mailpit

## Como rodar

```powershell
dotnet restore .\MeuValorLiquido.slnx
dotnet build .\MeuValorLiquido.slnx
dotnet test .\MeuValorLiquido.slnx
dotnet run --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
```

Para infraestrutura local:

```powershell
copy .env.example .env
docker compose up --build
```

Mailpit fica disponível em `http://localhost:8025`.

## Arquitetura

O projeto usa Modular Monolith com:

- `src/Core`: primitives, contratos e abstrações globais.
- `src/Shared`: utilidades sem regra de negócio pesada.
- `src/Modules`: módulos de negócio.
- `src/WebApp`: experiência pública e orquestração.
- `tests`: testes por camada/módulo.
- `docs`: documentação técnica e produto.
