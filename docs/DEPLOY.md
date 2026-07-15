# Deploy e go-live

Checklist operacional — publicar releases (Sprints 65–68: hubs temáticos, calendário editorial, calculadora `seguro-desemprego`).

**VPS Hostinger:** ver [`docs/VPS_HOSTINGER.md`](VPS_HOSTINGER.md) e scripts em `scripts/vps/`.

**Diretório de produção:** `/var/www/meu-valor-liquido` (não `~/meu-valor-liquido`).

## 1. Pré-requisitos (código)

- [x] `dotnet test MeuValorLiquido.slnx` verde localmente e no CI
- [x] `GoLiveSmokeTests` — rotas, assets, sitemap, health, headers, hubs temáticos
- [x] `CalculatorSubmissionSmokeTests` — submissão das 19 calculadoras
- [x] CI com job `docker-build` (imagem `infra/docker/WebApp.Dockerfile`)
- [x] `docs/adsense-checklist.md` revisado
- [x] Brand assets em `wwwroot/` (inclui hero `/images/blog/{slug}.webp` por artigo)

## 2. Ambiente de produção

### Atualização típica na VPS

```bash
cd /var/www/meu-valor-liquido
git pull origin main
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```

O seed de blog e calculadoras sincroniza conteúdo editorial a cada deploy.

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
| `Ads__Enabled` | `false` até aprovação AdSense (Sprint 51) |
| `Ads__VerificationEnabled` | `true` somente durante verificação do site no AdSense |
| `Ads__PublisherId` | `ca-pub-...` informado pelo AdSense |
| `Mail__Host` / `Mail__Port` / `Mail__UseSsl` | SMTP real |
| `Mail__UserName` / `Mail__Password` | Se o provedor exigir auth |

### HTTPS e reverse proxy

O app escuta HTTP na porta **8080** (container) ou **5000** (dev). Coloque **nginx/Caddy** na frente com TLS.

Exemplo: `infra/nginx/meu-valor-liquido.conf.example` — repasse `X-Forwarded-Proto` e `X-Forwarded-For` (o app usa `ForwardedHeaders`).

Certificado gratuito: [Let's Encrypt](https://letsencrypt.org/) + certbot.

## 3. Smoke test pós-deploy

### Rotas e infraestrutura

1. `/` — logo, hero, OG PNG
2. `/blog`, `/duvidas`, `/como-calculamos`, `/mapa-do-site`
3. **Hubs temáticos:** `/desligamento`, `/negociar-salario`, `/virar-pj`
4. `/favicon.ico`, `/images/og-default.png`
5. `/health` → `Healthy`
6. `/sitemap.xml`, `/robots.txt`
7. Formulário contato + newsletter (verificar caixa SMTP)
8. `/metricas-internas` — painel carrega, `noindex`, seletor 7/30 dias, alertas 404/500/falhas

### Calculadoras (19) — formulário e página 200

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
| 14 | `/calculadoras/seguro-desemprego` |
| 15 | `/calculadoras/vale-transporte-hibrido` |
| 16 | `/calculadoras/simulador-mei` |
| 17 | `/calculadoras/custo-funcionario` |
| 18 | `/calculadoras/multa-atraso` |
| 19 | `/calculadoras/conversor-salario` |

**Automatizado:** `GoLiveSmokeTests.PostDeploy_All_Calculators_Should_Load` (lê slugs de `CalculatorSeedData`) e `dotnet test` no CI.

### Conteúdo editorial (amostra pós-deploy)

Conferir que o sitemap inclui artigos recentes e hubs:

| Tipo | URLs de referência |
|------|-------------------|
| Sprint 66 | `/blog/irrf-2026-reducao-imposto`, `/blog/seguro-desemprego-quem-tem-direito` |
| Sprint 68 | `/blog/quanto-preciso-ganhar-para-receber-x`, `/blog/mei-desenquadramento-o-que-fazer` |
| Hub desligamento | `/desligamento` → links para `rescisao-clt`, `fgts`, `seguro-desemprego` |

Catálogo completo: `BlogArticleSeedData` (37 artigos no seed).

### Fluxo crítico

1. `/calculadoras/salario-liquido` — calcular, extrato, share
2. `/calculadoras/rescisao-clt` — motivo de desligamento e resultado
3. `/calculadoras/seguro-desemprego` — parcelas e elegibilidade (tabela MTE 2026)
4. PDF de resultado (sem anúncios) em calculadora com share

### Métricas e observabilidade pós-deploy

Após tráfego real, conferir `/metricas-internas?days=7` na segunda seguinte — ver `docs/METRICS_ROUTINE.md`.

**Alertas no painel (Sprint 52):**

| Métrica | Ação se elevada |
|---------|-----------------|
| Erros 404 | Corrigir links; ver rotas em "404 mais frequentes" |
| Erros 500 | Logs (`Serilog`), `/health`, rollback |
| Falhas de cálculo | Reproduzir slug; `dotnet test --filter CalculatorSubmissionSmoke` |

**Smoke periódico em produção/staging:**

```bash
export SMOKE_BASE_URL=https://meuvalorliquido.com
dotnet test tests/MeuValorLiquido.WebApp.Tests --filter "FullyQualifiedName~ProductionSmokeTests"
```

Rotinas complementares: `docs/SEO_MONTHLY_REVIEW.md`, `docs/CALIBRATION_ROUTINE.md`.

## 4. AdSense

1. Site público estável com domínio e HTTPS
2. Solicitar conta em [Google AdSense](https://www.google.com/adsense/)
3. Após aprovação (Sprint 51): `Ads__Enabled=true`, `Ads__PublisherId`, IDs dos slots
4. Monitorar Core Web Vitals e políticas

## 5. Próxima sprint

**Sprint 51 — AdSense pós-aprovação:** ativar script real nos slots existentes.

Ver também: `docs/setup-local.md`, `docker-compose.yml` (dev com Mailpit).
