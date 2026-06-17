# AGENTS

Este repositório é alternado entre Cursor e Codex. Ao continuar um trabalho aqui, siga esta ordem:

1. Leia `README.md`.
2. Leia `docs/agents.md`.
3. Leia `docs/CODING_CONVENTIONS.md` antes de editar código.
4. Consulte `docs/sprint-plan.md` para contexto de produto e próximos passos.

## Regras de continuidade

- Não mova regra de negócio para `src/WebApp` quando ela pertencer a `src/Modules/Calculators`.
- Não reverta mudanças do usuário sem pedido explícito.
- Ao corrigir calculadoras compartilhadas, revise o template comum `src/WebApp/Pages/Calculadoras/Details.cshtml` antes de editar páginas isoladas.
- Se alterar layout dark, revise `src/WebApp/wwwroot/css/site.css` e preserve o padrão visual Premium Liquid.
- Ao concluir uma entrega relevante, atualize a documentação correspondente.

## Estado atual importante

- Tema atual: dark Premium Liquid, trilha Stitch sprints 39 a 46 concluída.
- Deploy de produção na VPS usa `/var/www/meu-valor-liquido`.
- As calculadoras CLT usam formulário compartilhado em `src/WebApp/Pages/Calculadoras/Details.cshtml`.
- Teste de regressão recente cobre campos principais dessas calculadoras em `tests/MeuValorLiquido.WebApp.Tests/CalculatorFormFieldsTests.cs`.

## Comandos úteis

```powershell
dotnet test .\MeuValorLiquido.slnx
```

```bash
cd /var/www/meu-valor-liquido
git pull origin main
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```
