# Relatório da Sprint 92 — performance e experiência mobile

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-7`  
**Referência no plano AdSense:** Performance e experiência mobile  
**Referência no roadmap do repositório:** Sprint 92

## Objetivo

Melhorar a experiência principal no celular (LCP/CLS/INP) sem regressão funcional, com evidências automatizadas e checklist Lighthouse.

## Antes → depois

| Área | Antes | Depois |
|------|-------|--------|
| Hero home/blog | `loading="lazy"` no LCP | `eager` + `fetchpriority="high"` |
| `adsense-init.js` | Sempre no layout | Só com `Ads:Enabled` ativo |
| Fontes | Idle 1200 ms | Idle 800 ms / fallback 400 ms |
| Compressão | Brotli/Gzip padrão | MIME extras (JS/CSS/JSON/SVG/XML) |
| Cache editorial | Só sitemap/conteúdo em memória | OutputCache 10 min em artigo, metodologia, sobre e política editorial |
| Mobile | Toggle sem mínimo 44 px | Touch targets ≥ 2.75 rem; `overflow-x: clip`; aspect-ratio em heróis |
| Checklist rescisão | Script sem `defer` | `defer` |

## Metas Core Web Vitals (produção)

| Métrica | Meta | Como validar |
|---------|------|--------------|
| LCP | < 2,5 s | Lighthouse mobile em `/` e `/blog/{slug}` |
| CLS | < 0,1 | Lighthouse + aspect-ratio/width/height |
| INP | < 200 ms | Lighthouse / field data |

Ambiente de CI não roda Lighthouse contra o domínio público; o checklist abaixo e os testes Playwright (360/390/412) são a evidência automatizada desta sprint.

## Checklist Lighthouse (manual pós-deploy)

- [ ] Home mobile: LCP < 2,5 s
- [ ] Artigo do blog: LCP < 2,5 s
- [ ] CLS < 0,1 nas rotas acima
- [ ] Sem erros de console
- [ ] Sem imagem quebrada
- [ ] Artigo legível com JS desabilitado (corpo no HTML)

## Testes automatizados

- `Sprint92PerformanceMobileTests`
- `Sprint92MobileViewportPlaywrightTests` (viewports 360, 390 e 412)

## Critérios de aceite

- [x] JS não crítico reduzido/condicionado
- [x] Imagens LCP priorizadas; demais lazy
- [x] Width/height + aspect-ratio
- [x] Cache de estáticos + compressão + cache editorial
- [x] Toque e overflow mobile cobertos por testes
- [x] Sem regressão na suíte

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx -c Release
Core: 5 aprovados
Integration: 1 aprovado
Calculators: 243 aprovados, 1 ignorado
Playwright: 26 aprovados
WebApp: 625 aprovados
Total: 900 aprovados, 1 ignorado, 0 falhas
```

## Próxima sprint recomendada

Sprint 93: conteúdo editorial contínuo (clusters salário/holerite, rescisão, PJ/MEI).
