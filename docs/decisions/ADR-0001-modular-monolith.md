# ADR-0001 - Modular Monolith

## Status

Aceita.

## Contexto

O produto precisa evoluir rápido, com SEO e calculadoras funcionais, sem custo operacional de microserviços ou muitos projetos no início.

## Decisão

Usar Modular Monolith em .NET 10 com projetos por camada/módulo principal: Core, Shared, Modules e WebApp.

## Consequências

- Menor complexidade operacional.
- Baixo acoplamento por contratos internos.
- Possibilidade de extrair módulos no futuro se houver necessidade real.
- Disciplina arquitetural é obrigatória para evitar acoplamento por conveniência.
