# Relatório da Sprint 9 — pré-revisão final do AdSense

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-9`  
**Referência no roadmap do repositório:** Sprint 94

## Objetivo

Consolidar uma auditoria go/no-go antes de solicitar ou reenviar o site ao Google AdSense, com checklist documentado e testes que travam regressões críticas.

## Entregas

- Checklist completo em `docs/adsense/FINAL_PRE_REVIEW_CHECKLIST.md`.
- Testes `Sprint94AdSensePreReviewTests` (institucional, SEO, ads off, consentimento, editorial, `ads.txt`).
- Correção P0: `adsense-init.js` só entra no layout quando `AdsOptions.IsActive` (antecipa pedaço crítico da Sprint 92).
- Atualização de `docs/adsense-checklist.md`, `CHANGELOG.md`, `AGENTS.md` e `docs/sprint-plan.md`.

## Veredito

**GO CONDICIONAL**

Pronto em `main` (Sprints 86–90): confiança, editorial prioritário, autoria, SEO técnico e consentimento.

Antes do envio ao Google, mergear e validar:

1. `feat/adsense-sprint-6` — matemática  
2. `feat/adsense-sprint-7` — performance/mobile  
3. `feat/adsense-sprint-8` — editorial lote 6  

Não ativar `Ads:Enabled` até aprovação.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release
dotnet test --filter FullyQualifiedName~Sprint94
dotnet test MeuValorLiquido.slnx -c Release
```

## Critérios de aceite

- [x] Checklist final documentado com status por item
- [x] Testes de pré-revisão verdes
- [x] Sem inventar métricas, depoimentos ou publisher IDs
- [x] Veredito explícito (GO / NO-GO / CONDICIONAL)

## Próximos passos operacionais

1. Merge ordenado das branches 6 → 7 → 8.  
2. Deploy + smoke HTTPS (`docs/DEPLOY.md`).  
3. Lighthouse mobile nas rotas home, calculadora e artigo.  
4. Solicitar/reenviar AdSense com anúncios ainda desligados (só verificação se necessário).
