# Pré-revisão final AdSense — Checklist Sprint 94

**Data:** 17/07/2026  
**Branch de auditoria:** `feat/adsense-sprint-9`  
**Base:** `main` com Sprint 90 mergeada (`8106be6`)

## Veredito

**GO CONDICIONAL** — sprints 86–96 estão em `main`. Antes de solicitar/reenviar ao Google:

| Item | Status |
|------|--------|
| Merge Sprints 91–96 | FEITO em `main` |
| Deploy HTTPS + smoke | Validar em produção |
| Lighthouse mobile | Após deploy |
| `Ads:Enabled=false` até aprovação | Mantido |

---

## Conteúdo

| Item | Status | Evidência |
|------|--------|-----------|
| 12 calculadoras prioritárias com editorial completo | PASS | `CalculatorEditorialCatalog` + `Sprint87CalculatorEditorialTests` |
| Sem páginas vazias nas calculadoras ativas | PASS | `CalculatorSeedData` + páginas Details |
| 7 calculadoras sem bloco editorial longo | PARTIAL | Aceitável para MVP; opcional pós-aprovação |
| Sem métricas/avaliações/depoimentos fictícios | PASS | Home + `Sprint83` / `Sprint94` |
| Datas de revisão reais | PASS | `LastReviewedAt` 2026-07-17; blog sem datas futuras |
| Artigos lote 6 | PENDING_MERGE | `feat/adsense-sprint-8` |

## Institucional

| Item | Status |
|------|--------|
| Sobre, Contato, Privacidade, Cookies, Termos, Aviso Legal | PASS |
| Metodologia (`/como-calculamos`) | PASS |
| Política Editorial | PASS |
| Autor `/autores/matteus-oberdan` | PASS |
| Correções (`noindex,follow`) | PASS |

## SEO

| Item | Status |
|------|--------|
| Sitemap válido (só indexáveis) | PASS |
| robots.txt + Sitemap | PASS |
| Canonical | PASS |
| Soft 404 de calculadora corrigido (404 real) | PASS |
| Assistente/painel/newsletter fora do índice | PASS |

## UX / anúncios

| Item | Status | Evidência |
|------|--------|-----------|
| Sem placeholders com ads off | PASS | Partial Ads + Sprint94 |
| `adsense-init.js` só quando `Ads:Enabled` ativo | PASS | `_Layout.cshtml` + Sprint94 (P0 corrigido nesta sprint) |
| Sem botões de login falsos | PASS | Auditoria home |
| Mobile / performance extras (LCP hero, cache, viewports) | PENDING_MERGE | `feat/adsense-sprint-7` |

## AdSense / privacidade

| Item | Status |
|------|--------|
| `Ads:Enabled=false` padrão | PASS |
| Meta verificação sem script externo | PASS |
| Script só após Publicidade | PASS |
| Consentimento v2 (4 categorias) | PASS |
| `ads.txt` com `pub-4150358596824425` | PASS |
| CLS com slots live | BLOQUEADO até aprovação |

## Operacional (fora do código)

| Item | Status |
|------|--------|
| Domínio HTTPS público | Validar em produção |
| `Site:BaseUrl` final | Validar em `.env.prod` |
| SMTP contato/newsletter | Smoke manual |
| Lighthouse mobile pós-deploy | Após merge Sprint 92 |

## Testes automatizados desta sprint

`Sprint94AdSensePreReviewTests` — trava institucional, SEO, ads off, consentimento, editorial prioritário e `ads.txt`.
