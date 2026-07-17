# Relatório da Sprint 8 — editorial contínuo (lote 6)

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-8`  
**Referência no roadmap do repositório:** Sprint 93 / Sprint 70 lote 6

## Objetivo

Manter ritmo editorial de 2 artigos long tail por ciclo, com links para calculadoras, hubs temáticos, FAQs e sinais de validação exigidos pelo plano AdSense — sem alterar fórmulas ou inventar métricas.

## Entregas

- Artigo `aviso-previo-trabalhado-vs-indenizado`:
  - Hub `/desligamento`
  - Calculadora `rescisao-clt`
  - Publicado em 10/07/2026
  - FAQ relacionada: `/duvidas/rescisao-pedido-demissao-o-que-recebo`
  - Fonte oficial: CLT art. 487 (Planalto)
- Artigo `adicional-noturno-clt-como-calcular`:
  - Hub `/negociar-salario`
  - Calculadora `hora-extra`
  - Publicado em 15/07/2026
  - FAQ relacionada: `/duvidas/hora-extra-valor-minimo`
  - Fonte oficial: CLT art. 73 (Planalto)
- `BlogEditorialCatalog.Sprint70Lote6EditorialSlugs` e helper `IsSprint70Editorial`
- Cross-links em `ThematicHubCatalog` (desligamento e negociar-salario)
- Seções `dica-pratica` via `AppendPracticalSection`
- Capas WebP em `wwwroot/images/blog/{slug}.webp`
- Testes `Sprint70Lote6BlogTests` e regressão `BrandAssetsTests`

## Decisões editoriais

- Tom educativo; estimativas apenas; sem credenciais inventadas.
- ≥850 palavras por artigo (corpo HTML + blocos enriquecidos).
- Autor: Matteus Oberdan (padrão seed existente).
- Nenhuma alteração em fórmulas de calculadoras ou IDs AdSense.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release
0 avisos, 0 erros

dotnet test (Sprint70Lote6 + BrandAssets): 22 aprovados
dotnet test MeuValorLiquido.slnx -c Release:
Core: 5 | Integration: 1 | Calculators: 243 (+1 skip) | Playwright: 11 | WebApp: 630
Total: 890 aprovados, 1 ignorado, 0 falhas
```

## Critérios de aceite

- [x] 2 artigos publicados com slugs únicos
- [x] Links para calculadora, `/como-calculamos` e ≥1 `/duvidas/`
- [x] Hubs temáticos atualizados
- [x] Sitemap inclui novos slugs
- [x] Capas WebP geradas
- [x] Testes de lote e suíte verde

## Riscos restantes

- AdSense continua desligado até aprovação Google; métricas de receita não aplicáveis neste ciclo.
- Convenções coletivas podem alterar percentuais de adicional noturno ou regras de aviso — artigos reforçam caráter educativo.

## Próxima sprint recomendada

Sprint 94 — pré-revisão final do AdSense (auditoria completa antes de solicitar/reenviar ao Google).
