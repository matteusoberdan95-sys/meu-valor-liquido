# Relatório da Sprint 3 — autoria, autoridade e transparência

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-3`  
**Referência no roadmap do repositório:** Sprint 88

## Objetivo

Tornar autoria, responsabilidade editorial e processos de pesquisa, revisão e correção verificáveis, sem atribuir credenciais profissionais não comprovadas.

## Entregas

- Perfil indexável em `/autores/matteus-oberdan` com:
  - nome e papel editorial;
  - descrição limitada à atuação verificável no projeto;
  - LinkedIn e contato editorial;
  - processo de revisão;
  - data de revisão do perfil;
  - listagem dinâmica dos artigos publicados.
- Avatar SVG com monograma `MO`, apresentado explicitamente como avatar, substituindo a referência a um PNG inexistente.
- Nome do autor clicável nos artigos com `rel="author"`.
- JSON-LD `Person` com URL canônica do perfil interno e LinkedIn em `sameAs`.
- Página Sobre ampliada com criação, objetivo, fontes, atualização e relato de erros.
- Política Editorial ampliada com fontes, proibição de conteúdo inventado, revisão, correções, patrocínio, automação e frequência.
- Página `/correcoes` com processo público e estado real do histórico.
- Perfil incluído no sitemap XML, mapa do site e footer.

## Controle de indexação

| URL | Indexação | Sitemap XML | Motivo |
|---|---|---|---|
| `/autores/matteus-oberdan` | `index,follow` | Sim | Perfil completo, autor dos artigos e sinal institucional estável |
| `/correcoes` | `noindex,follow` | Não | Processo útil, mas sem histórico real de correções no momento |

Não foram criadas correções fictícias para preencher a página. Quando houver um caso confirmado, o histórico deverá registrar página, data, natureza e resumo da mudança sem dados pessoais do remetente.

## Decisões de confiança

- O perfil declara que não atribui formação, licença ou certificação não comprovada.
- A experiência descrita se limita à manutenção do produto, fontes, premissas, revisão e testes observáveis no repositório.
- A automação é tratada como apoio técnico; não pode inventar fatos, fontes, credenciais ou resultados.
- Conteúdo patrocinado futuro deverá ser rotulado e não poderá interferir em fórmulas ou fontes.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release --no-restore
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx -c Release --no-build --no-restore
Core: 5 aprovados
Calculators: 243 aprovados, 1 teste gerador ignorado
Integration: 1 aprovado
Playwright: 11 aprovados
WebApp: 587 aprovados
Total: 847 aprovados, 0 falhas, 1 ignorado
```

Os 12 testes focados das Sprints 83 e 88 também foram executados isoladamente e passaram.

## Critérios de aceite

- [x] Artigos possuem autor clicável.
- [x] Perfil de autor é indexável e consta no sitemap.
- [x] Não foram atribuídas credenciais não verificadas.
- [x] Sobre explica criação, pesquisa, atualização e relato de erros.
- [x] Política Editorial cobre fontes, revisão, correções, patrocínio e automação.
- [x] Existe canal para reportar divergências.
- [x] Processo de correções está público e sem histórico inventado.
- [x] Build sem avisos e suíte completa verde.

## Riscos restantes

- O conteúdo não passou por validação jurídica ou contábil externa.
- O LinkedIn é a única referência profissional externa associada ao autor.
- A Sprint 4 ainda precisa auditar todas as rotas, canonicals, status HTTP, sitemap, páginas órfãs e dados estruturados.
- A página de correções deve permanecer fora do sitemap até possuir histórico real útil.

## Próxima sprint recomendada

Sprint 4 do plano AdSense, registrada como Sprint 89: SEO técnico e controle de indexação.
