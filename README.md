# Meu Valor Líquido

[![CI](https://github.com/matteusoberdan95-sys/meu-valor-liquido/actions/workflows/ci.yml/badge.svg)](https://github.com/matteusoberdan95-sys/meu-valor-liquido/actions/workflows/ci.yml)

Plataforma brasileira de calculadoras trabalhistas, fiscais e financeiras para ajudar o usuário a entender quanto recebe, quanto desconta e quanto sobra.

Repositório: https://github.com/matteusoberdan95-sys/meu-valor-liquido

Design UI/UX baseado nos protótipos Stitch — ver [docs/ui-ux-stitch.md](docs/ui-ux-stitch.md).

**Tema atual:** dark **Premium Liquid** — trilha Stitch **Sprints 39–46 concluída**. Mocks de referência ficam em `stitch_redesing/` na máquina local (não versionados). Plano de fidelidade: [docs/STITCH_DARK_FIDELITY_PLAN.md](docs/STITCH_DARK_FIDELITY_PLAN.md).

## Continuidade entre Cursor e Codex

Para alternar o trabalho entre os dois agentes sem perder contexto:

- Leia primeiro `AGENTS.md` na raiz.
- Consulte `docs/agents.md` para papéis e limites por área.
- Antes de editar código, leia `docs/CODING_CONVENTIONS.md`.
- Para mudanças de produto e status, atualize `docs/sprint-plan.md` quando uma entrega relevante for concluída.

Estado operacional atual:

- Deploy de produção usa `/var/www/meu-valor-liquido` na VPS, não `~/meu-valor-liquido`.
- **Próxima sprint:** 53 (holerite completo) — ver `docs/sprint-plan.md` e `AGENTS.md`.
- Trilhas concluídas: Stitch 39–46; pós-auditoria 47–50. AdSense (Sprint 51) aguarda aprovação Google.
## Clonar o projeto

```powershell
git clone https://github.com/matteusoberdan95-sys/meu-valor-liquido.git
cd meu-valor-liquido
```
## Stack

- C# e .NET 10
- ASP.NET Core Razor Pages
- PostgreSQL
- Entity Framework Core preparado
- FluentValidation
- Serilog
- xUnit, FluentAssertions e Bogus
- Docker Compose com PostgreSQL, Mailpit e WebApp (front + back)

## Como rodar localmente

**Um comando — tudo no Docker:**

```powershell
copy .env.example .env
docker compose up --build
```

Acesse http://localhost:8080 (calculadoras, blog, contato). Mailpit: http://localhost:8025

Detalhes e opções alternativas em [docs/setup-local.md](docs/setup-local.md).

## Testes

```powershell
dotnet test .\MeuValorLiquido.slnx
```

Suite validada recentemente:

- `tests/MeuValorLiquido.WebApp.Tests`: 135 testes
- `tests/MeuValorLiquido.Calculators.Tests`: 117 testes

## Calculadoras

17 ferramentas em `/calculadoras` (trabalhistas, fiscais e financeiras). Cada uma usa o motor em `src/Modules/Calculators` com extrato, explicação simples, compartilhamento e PDF.

| Categoria | Slugs |
|-----------|--------|
| Trabalhista | `salario-liquido`, `salario-bruto-necessario`, `proposta-salarial`, `ferias`, `decimo-terceiro`, `rescisao-clt`, `hora-extra`, `fgts`, `custo-funcionario`, `conversor-salario` |
| Fiscal | `inss`, `irrf`, `simulador-mei` |
| Financeiro | `pj-vs-clt`, `juros-compostos`, `financiamento`, `multa-atraso` |

- **Criar ou alterar calculadora:** [docs/how-to-create-calculator.md](docs/how-to-create-calculator.md)
- **Planejamento de melhorias:** [docs/sprint-plan.md](docs/sprint-plan.md) — **próxima: Sprint 54** (jornadas); trilhas 47–50 e 53 concluídas; AdSense (51) aguardando aprovação
- **Redesign dark Stitch:** [docs/STITCH_DARK_FIDELITY_PLAN.md](docs/STITCH_DARK_FIDELITY_PLAN.md) (Sprints 39–46)
- **Metodologia e tabelas 2026:** página `/como-calculamos`

## Arquitetura

O projeto usa Modular Monolith com:

- `src/Core`: primitives, contratos e abstrações globais.
- `src/Shared`: utilidades sem regra de negócio pesada.
- `src/Modules`: módulos de negócio.
- `src/WebApp`: experiência pública e orquestração.
- `tests`: testes por camada/módulo.
- `docs`: documentação técnica e produto.

## Deploy de produção

Atualização típica na VPS:

```bash
cd /var/www/meu-valor-liquido
git pull origin main
docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```

Mais detalhes em [docs/VPS_HOSTINGER.md](docs/VPS_HOSTINGER.md) e [docs/DEPLOY.md](docs/DEPLOY.md).
