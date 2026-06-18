# Deploy e go-live

Checklist Sprint 19 — publicar com a identidade Valores Públicos e solicitar AdSense.

**VPS Hostinger (substituir Turnizio):** ver [`docs/VPS_HOSTINGER.md`](VPS_HOSTINGER.md) e scripts em `scripts/vps/`.

## 1. Pré-requisitos (código)

- [x] `dotnet test MeuValorLiquido.slnx` verde localmente e no CI
- [x] `GoLiveSmokeTests` — rotas, assets, sitemap, health, headers
- [x] CI com job `docker-build` (imagem `infra/docker/WebApp.Dockerfile`)
- [x] `docs/adsense-checklist.md` revisado
- [x] Brand assets em `wwwroot/`

## 2. Ambiente de produção

### Opção A — Docker Compose (recomendado)

```powershell
copy .env.prod.example .env.prod
# Edite senhas, SITE_BASE_URL (https://) e SMTP
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```

### Opção B — `dotnet run` + PostgreSQL gerenciado

1. Copie `src/WebApp/appsettings.Production.json.example` → `appsettings.Production.json` (não versionado).
2. Defina variáveis de ambiente ou secrets no host.

| Variável | Exemplo |
|----------|---------|
| `Site__BaseUrl` | `https://meuvalorliquido.com` |
| `ConnectionStrings__DefaultConnection` | PostgreSQL produção |
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `Ads__Enabled` | `false` até aprovação AdSense |
| `Mail__Host` / `Mail__Port` / `Mail__UseSsl` | SMTP real |
| `Mail__UserName` / `Mail__Password` | Se o provedor exigir auth |

### HTTPS e reverse proxy

O app escuta HTTP na porta **8080** (container) ou **5000** (dev). Coloque **nginx/Caddy** na frente com TLS.

Exemplo: `infra/nginx/meu-valor-liquido.conf.example` — repasse `X-Forwarded-Proto` e `X-Forwarded-For` (o app usa `ForwardedHeaders`).

Certificado gratuito: [Let's Encrypt](https://letsencrypt.org/) + certbot.

## 3. Smoke test pós-deploy

### Rotas e infraestrutura

1. `/` — logo, hero, OG PNG
2. `/blog`, `/duvidas`, `/como-calculamos`
3. `/favicon.ico`, `/images/og-default.png`
4. `/health` → `Healthy`
5. `/sitemap.xml`, `/robots.txt`
6. Formulário contato + newsletter (verificar caixa SMTP)
7. `/metricas-internas` — painel carrega, `noindex`, seletor 7/30 dias

### Calculadoras (17) — formulário e página 200

Para cada URL abaixo: status **200**, campo principal visível, botão calcular presente.

| # | URL |
|---|-----|
| 1 | `/calculadoras/salario-liquido` |
| 2 | `/calculadoras/salario-bruto-necessario` |
| 3 | `/calculadoras/proposta-salarial` |
| 4 | `/calculadoras/ferias` |
| 5 | `/calculadoras/decimo-terceiro` |
| 6 | `/calculadoras/rescisao-clt` |
| 7 | `/calculadoras/hora-extra` |
| 8 | `/calculadoras/inss` |
| 9 | `/calculadoras/irrf` |
| 10 | `/calculadoras/pj-vs-clt` |
| 11 | `/calculadoras/juros-compostos` |
| 12 | `/calculadoras/financiamento` |
| 13 | `/calculadoras/fgts` |
| 14 | `/calculadoras/simulador-mei` |
| 15 | `/calculadoras/custo-funcionario` |
| 16 | `/calculadoras/multa-atraso` |
| 17 | `/calculadoras/conversor-salario` |

**Automatizado:** `GoLiveSmokeTests.PostDeploy_All_Calculators_Should_Load` e `dotnet test` no CI.

### Fluxo crítico

1. `/calculadoras/salario-liquido` — calcular, extrato, share
2. `/calculadoras/rescisao-clt` — motivo de desligamento e resultado
3. PDF de resultado (sem anúncios) em calculadora com share

### Métricas pós-deploy (opcional)

Após tráfego real, conferir `/metricas-internas?days=7` na segunda seguinte — ver `docs/METRICS_ROUTINE.md`.

## 4. AdSense

1. Site público estável com domínio e HTTPS
2. Solicitar conta em [Google AdSense](https://www.google.com/adsense/)
3. Após aprovação: `Ads__Enabled=true`, `Ads__PublisherId`, IDs dos slots
4. Monitorar Core Web Vitals e políticas

## 5. Próxima sprint

**Sprint 20 — Monetização AdSense pós-aprovação:** ativar script real nos slots existentes.

Ver também: `docs/setup-local.md`, `docker-compose.yml` (dev com Mailpit).
