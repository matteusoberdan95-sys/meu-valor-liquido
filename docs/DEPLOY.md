# Deploy e go-live

Checklist para Sprint 19 após o redesign UI (Sprints 15–18).

## Pré-requisitos

- [ ] `dotnet test MeuValorLiquido.slnx` verde localmente e no CI
- [ ] `docs/adsense-checklist.md` revisado
- [ ] `docs/ADSENSE_COMPLIANCE.md` — slots com label “Espaço publicitário”
- [ ] Brand assets em `wwwroot/`: favicon, `og-default.png`, logo

## Variáveis de produção

| Variável | Exemplo |
|----------|---------|
| `Site__BaseUrl` | `https://meuvalorliquido.com.br` |
| `ConnectionStrings__DefaultConnection` | PostgreSQL produção |
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `Ads__Enabled` | `false` até aprovação AdSense |

## Smoke test pós-deploy

1. `/` — logo, hero, OG tags com PNG
2. `/calculadoras/salario-liquido` — calcular, extrato, share
3. `/blog`, `/duvidas`, `/como-calculamos`
4. `/favicon.ico`, `/images/og-default.png`
5. `/health`, `/sitemap.xml`, `/robots.txt`
6. Formulário contato + newsletter (Mail/SMTP produção)

## AdSense

1. Solicitar conta com site público estável
2. Após aprovação: `Ads__Enabled=true`, `Ads__PublisherId`, slot IDs via ambiente
3. Monitorar Core Web Vitals e políticas no painel Google

Ver também: `docs/setup-local.md` (Docker) e `docker-compose.yml`.
