# Decisão: páginas programáticas e risco de thin content (AdSense)

**Data:** 01/08/2026  
**Contexto:** rejeição AdSense por “Conteúdo de baixo valor”.

## Problema

O sitemap incluía todas as faixas de `/salario-liquido/*` e `/clt-pj/*` × variantes de dependentes (0–2), gerando ~278 URLs programáticas com estrutura similar. Mesmo com ângulo editorial por faixa (`SalaryBandEditorialCatalog`), o volume em escala aumentava o risco de julgamento de conteúdo superficial.

## Decisão

1. Manter **todas** as faixas do catálogo acessíveis (HTTP 200, cálculo real, editorial por faixa).
2. Restringir o **sitemap** e o `index,follow` às **18 páginas-base Tier 1** de maior demanda (sem as variantes por dependentes):
   `1621, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 8000, 9000, 10000, 12000, 15000, 20000`.
3. Variantes por dependentes e faixas fora do Tier 1: `noindex,follow` (ainda linkáveis internamente, calculam normalmente e ficam fora do sitemap).
4. Hubs `/salario-liquido` e `/clt-pj` permanecem indexáveis.

## Evidência técnica

- `SalaryBandCatalog.IsSitemapIndexable` / `GetAllIndexablePaths`
- `CltPjBandCatalog.IsSitemapIndexable` / `GetAllIndexablePaths`
- Testes em `Sprint74ProgrammaticPagesTests`

## Resultado esperado no sitemap

- Antes: 46 faixas × 3 variantes × 2 famílias ≈ 276 URLs programáticas  
- Depois da Sprint 98: 18 × 3 × 2 = 108 URLs programáticas indexáveis
- Agora: 18 páginas-base × 2 famílias = **36** URLs programáticas indexáveis

## Refinamento pós-deploy — 11/09/2026

Após nova leitura da reprovação por conteúdo de baixo valor, as variações por dependentes deixaram de ser indexáveis mesmo nas faixas Tier 1. Elas continuam disponíveis para quem precisa simular aquele cenário, mas são estruturalmente próximas da página-base e não precisam competir por indexação. A redução de 108 para 36 URLs indexáveis prioriza profundidade e diferenciação das páginas restantes, sem remover ferramenta, cálculo ou links internos.
