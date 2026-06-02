# Modular Architecture

Cada módulo deve expor apenas application services, contratos e modelos de apresentação necessários ao WebApp.

## Regras

1. Não acessar diretamente banco de outro módulo.
2. Não referenciar entidades internas de outro módulo.
3. Usar contratos, eventos internos ou application services para comunicação.
4. Manter fórmulas no módulo Calculators.
5. Promover tipos para Core apenas quando forem realmente globais.

## Estrutura de módulo

```text
Domain/
Application/
Infrastructure/
Presentation/
```
