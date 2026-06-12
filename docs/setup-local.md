# Setup Local

## Pré-requisitos

- Docker Desktop (recomendado)
- .NET 10 SDK (apenas para desenvolvimento e testes fora do Docker)

## Opção A — Tudo com um comando Docker (recomendado)

Front (Razor Pages + CSS/JS) e back (ASP.NET Core + PostgreSQL) sobem juntos:

```powershell
cd meu-valor-liquido
copy .env.example .env
docker compose up --build
```

Para rodar em segundo plano:

```powershell
docker compose up --build -d
```

Acesse:

| Serviço | URL |
|---------|-----|
| **Site (front + API)** | http://localhost:8080 |
| Health | http://localhost:8080/health |
| Mailpit (e-mails) | http://localhost:8025 |
| PostgreSQL | localhost:5432 |

O container `webapp` aplica migrations e seed automaticamente na inicialização.

Parar tudo:

```powershell
docker compose down
```

Remover volumes (reset do banco):

```powershell
docker compose down -v
```

## Opção B — Infra no Docker + app no Visual Studio/Cursor

Útil para hot reload durante desenvolvimento:

```powershell
docker compose up -d postgres mailpit
dotnet ef database update --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
dotnet run --project .\src\WebApp\MeuValorLiquido.WebApp.csproj
```

- WebApp: http://localhost:5000

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
