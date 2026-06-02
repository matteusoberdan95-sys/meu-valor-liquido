# Setup Local

## Pré-requisitos

- .NET 10 SDK
- Docker Desktop

## Opção A — Infra no Docker + app no Visual Studio/Cursor (recomendado)

1. Suba PostgreSQL e Mailpit:

```powershell
cd meu-valor-liquido
copy .env.example .env
docker compose up -d postgres mailpit
```

2. Aplique migrations e rode a WebApp:

```powershell
dotnet restore .\MeuValorLiquido.slnx
dotnet ef database update --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
dotnet run --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
```

3. Acesse:

- WebApp: http://localhost:5000
- Health: http://localhost:5000/health
- Mailpit: http://localhost:8025
- PostgreSQL: localhost:5432

## Opção B — Tudo no Docker

```powershell
docker compose --profile full up --build
```

- WebApp: http://localhost:8080

## Opção C — Sem PostgreSQL (memória)

Em `appsettings.Development.json`:

```json
"Database": { "UseInMemory": true }
```

Útil para testes rápidos; dados não persistem entre reinícios.

## Testes

```powershell
dotnet test .\MeuValorLiquido.slnx
```

## Migrations

```powershell
dotnet ef migrations add NomeDaMigration --project .\src\WebApp\MeuValorLiquido.WebApp.csproj --output-dir Data\Migrations
dotnet ef database update --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
```
