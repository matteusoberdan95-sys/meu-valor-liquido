# Contribuindo

## Padrões

- Código em inglês.
- Interface em português.
- Nullable habilitado.
- Preferir Result Pattern para fluxos previsíveis.
- Não adicionar secrets ao repositório.
- Não colocar regra pesada de cálculo no WebApp.

## Validação local

Antes de abrir PR:

```powershell
dotnet restore .\MeuValorLiquido.slnx
dotnet build .\MeuValorLiquido.slnx
dotnet test .\MeuValorLiquido.slnx
docker compose config
```
