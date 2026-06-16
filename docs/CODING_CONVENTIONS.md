# Convenções de código

Guia para humanos e agentes de IA. **Não reintroduza padrões antigos** removidos neste repositório.

## GlobalUsings (obrigatório)

Cada projeto .NET tem um arquivo `GlobalUsings.cs` na raiz do projeto:

| Projeto | Arquivo |
|---------|---------|
| Core | `src/Core/GlobalUsings.cs` |
| Shared | `src/Shared/GlobalUsings.cs` |
| Calculators | `src/Modules/Calculators/GlobalUsings.cs` |
| Ads, Contact, Content, Newsletter | `src/Modules/{Modulo}/GlobalUsings.cs` |
| WebApp | `src/WebApp/GlobalUsings.cs` |
| Testes | `tests/*/GlobalUsings.cs` |

### Regras

1. **Não adicione `using` no topo de arquivos `.cs`** — coloque novos namespaces em `GlobalUsings.cs` do projeto correspondente.
2. Use **namespace file-scoped** (`namespace X;`) sem linha em branco extra após a declaração, como no restante do código.
3. **Exceção:** migrations EF Core em `src/WebApp/Data/Migrations/` mantêm `using` locais (geradas/convencionais do EF).
4. Ao criar um **novo projeto de teste ou módulo**, crie `GlobalUsings.cs` antes de adicionar classes.
5. Prefira `global using` para tipos usados em vários arquivos; evite poluir o GlobalUsings com algo usado uma única vez se for API muito específica — nesse caso, documente a exceção no PR.

### O que os agentes NÃO devem fazer

- Reverter para `using Foo.Bar;` em cada arquivo “por hábito”.
- Remover ou esvaziar `GlobalUsings.cs` sem migrar os usings de volta de forma consciente.
- Alterar migrations só para remover `using` ou normalizar line endings.

## Arquitetura

- Regras fiscais e de cálculo ficam em `src/Modules/Calculators`.
- WebApp: Razor Pages, SEO, PDF, compartilhamento, infraestrutura — **sem duplicar fórmulas**.
- PDF e share usam o token `?r=` (`CalculatorInputShareCodec`); PDF **sem anúncios** (ver `docs/ADSENSE_COMPLIANCE.md`).

## Testes

- Rode `dotnet test` antes de concluir uma sprint.
- Testes de página: `tests/MeuValorLiquido.WebApp.Tests/`.
- Testes de cálculo: `tests/MeuValorLiquido.Calculators.Tests/`.

## Documentação de sprint

Após entregar uma sprint de produto:

1. Atualize `docs/sprint-plan.md` (status e entregas).
2. Atualize `CHANGELOG.md` (seção Unreleased ou nova sprint).
3. Mantenha `docs/ROADMAP_MONETIZACAO.md` alinhado ao backlog de monetização.
