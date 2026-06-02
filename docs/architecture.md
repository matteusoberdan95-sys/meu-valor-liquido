# Arquitetura

O Meu Valor Líquido usa Modular Monolith para equilibrar organização profissional e simplicidade operacional.

## Dependências

- `WebApp` depende de contratos públicos dos módulos.
- Módulos dependem de `Core` e `Shared`.
- Módulos não acessam tabelas ou entidades internas de outros módulos.
- `Core` deve permanecer pequeno e estável.
- `Shared` não deve conter regra de negócio pesada.

## Módulos MVP

- Calculators
- Content
- Contact
- Newsletter
- Ads

## Módulos futuros

- Users
- Reports
