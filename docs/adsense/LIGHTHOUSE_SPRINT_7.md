# Auditoria Lighthouse — Sprint 92 (baseline interno)

**Data:** 17/07/2026  
**Escopo:** evidência de performance/mobile sem inventar scores de campo.

## Método

1. Alterações de código listadas em `SPRINT_7_REPORT.md`.
2. Validação automatizada:
   - HTML sem `adsense-init` com ads off
   - Hero com `fetchpriority=high`
   - CSS com overflow/touch/aspect-ratio
   - Playwright em 360×800, 390×844 e 412×915 sem overflow horizontal
3. Após deploy em produção, rodar Lighthouse mobile em:
   - `https://meuvalorliquido.com.br/`
   - `https://meuvalorliquido.com.br/calculadoras/salario-liquido`
   - `https://meuvalorliquido.com.br/blog/o-que-e-salario-liquido`

## Comparativo esperado

| Item | Baseline pré-sprint | Pós-sprint (código) |
|------|---------------------|---------------------|
| Script ads idle | Sempre presente | Ausente até ativação |
| LCP candidate | Lazy no hero | Eager + high priority |
| CLS de imagem | width/height parciais | + aspect-ratio CSS |
| Overflow mobile | Teste só 430 px | 360 / 390 / 412 |
| Cache HTML editorial | Não | OutputCache 10 min |

Scores numéricos de Lighthouse devem ser preenchidos após o deploy (não inventar valores aqui).
