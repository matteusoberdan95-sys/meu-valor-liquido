# Pré-revisão final AdSense — Checklist (atualizado Sprint 98)

**Data:** 01/08/2026  
**Contexto:** rejeição por “Conteúdo de baixo valor”; correções no código antes de pedir nova revisão.

## Veredito

**GO CONDICIONAL OPERACIONAL** — código pronto para deploy. Antes de pedir revisão:

| Item | Status |
|------|--------|
| Editorial 19/19 calculadoras | FEITO |
| Programáticas Tier 1 no sitemap | FEITO |
| Lote 10 blog | FEITO |
| `Ads:Enabled=false` | Mantido |
| Deploy HTTPS + smoke | PENDENTE (VPS) |
| Aguardar 7–14 dias no ar | Após deploy |
| Pedir revisão no AdSense | Após espera — ver `ADSENSE_RE_REVIEW.md` |

---

## Conteúdo

| Item | Status | Evidência |
|------|--------|-----------|
| 19 calculadoras ativas com editorial completo | PASS | `CalculatorEditorialCatalog` + `Sprint87CalculatorEditorialTests` |
| Sem páginas vazias nas calculadoras ativas | PASS | Seed + Details |
| Sem métricas/avaliações/depoimentos fictícios | PASS | Home + Sprint83 / Sprint94 |
| Datas de revisão reais | PASS | Editorial `2026-08-01`; lote 10 `2026-08-01` |
| Artigos lote 10 | PASS | `vale-transporte-vr-orcamento-mensal`, `salario-minimo-impacto-holerite` |

## Programáticas

| Item | Status | Evidência |
|------|--------|-----------|
| Sitemap só Tier 1 (18 faixas × 3 variantes × 2 famílias = 108) | PASS | `SalaryBandCatalog.IsSitemapIndexable` |
| Faixas fora do Tier 1 com `noindex,follow` | PASS | FaixaPageModelBase / ComparacaoPageModelBase |
| Decisão documentada | PASS | `docs/adsense/PROGRAMMATIC_INDEXATION_DECISION.md` |

## Institucional / SEO / Ads

| Item | Status |
|------|--------|
| Sobre, Contato, Privacidade, Cookies, Termos, Aviso Legal | PASS |
| Metodologia, Política Editorial, Autor, Correções | PASS |
| Sitemap só indexáveis; soft 404 corrigido | PASS |
| Placeholders off; script só com ads ativos + Publicidade | PASS |
| `ads.txt` com publisher concreto | PASS |

## Smoke pós-deploy (manual)

```text
GET https://meuvalorliquido.com/
GET https://meuvalorliquido.com/calculadoras/seguro-desemprego
GET https://meuvalorliquido.com/calculadoras/vale-transporte-hibrido
GET https://meuvalorliquido.com/blog/vale-transporte-vr-orcamento-mensal
GET https://meuvalorliquido.com/blog/salario-minimo-impacto-holerite
GET https://meuvalorliquido.com/sobre
GET https://meuvalorliquido.com/como-calculamos
GET https://meuvalorliquido.com/politica-editorial
GET https://meuvalorliquido.com/autores/matteus-oberdan
GET https://meuvalorliquido.com/salario-liquido/5800   # deve ter noindex,follow
GET https://meuvalorliquido.com/sitemap.xml           # sem /5800; com /6000 e lote 10
```

Deploy (VPS):

```bash
cd /var/www/meu-valor-liquido
git pull origin main
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```

Confirmar `.env.prod`: `ADS_ENABLED=false` (ou `Ads__Enabled=false`).
