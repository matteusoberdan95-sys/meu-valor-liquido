# Setup Local

## Pré-requisitos

- .NET 10 SDK
- Docker Desktop

## Rodar localmente

```powershell
dotnet restore .\MeuValorLiquido.slnx
dotnet build .\MeuValorLiquido.slnx
dotnet run --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
```

## Infra local

```powershell
copy .env.example .env
docker compose up --build
```

Serviços:

- WebApp: `http://localhost:8080`
- PostgreSQL: `localhost:5432`
- Mailpit: `http://localhost:8025`
