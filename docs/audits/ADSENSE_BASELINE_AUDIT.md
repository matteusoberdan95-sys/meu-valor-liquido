# Auditoria de linha de base para AdSense

**Projeto:** Meu Valor Líquido  
**Sprint:** 0 — auditoria e linha de base  
**Data:** 17/07/2026  
**Branch:** `audit/adsense-sprint-0`  
**Produção avaliada:** `https://meuvalorliquido.com`  
**Escopo:** inventário de URLs, SEO técnico, AdSense/compliance e execução da suíte automatizada.  
**Alterações funcionais nesta sprint:** nenhuma.

## Resumo executivo

A base técnica e editorial é superior à de um projeto recém-publicado: o site tem 19 calculadoras, 47 artigos publicados no sitemap, páginas institucionais, metodologia, política editorial, autoria, fontes oficiais, testes matemáticos e navegação interna. O build está limpo e, depois da instalação da dependência local do Playwright, todos os 811 testes executáveis passaram.

O site ainda **não está pronto para uma nova avaliação rigorosa do AdSense**. Os bloqueadores não são ausência de páginas, mas sinais públicos de confiança insuficiente e aparência de monetização antecipada:

1. A home exibe `+250k cálculos realizados este mês`, cinco estrelas e avatares sem evidência auditável.
2. Placeholders de publicidade são renderizados mesmo quando `Ads:Enabled=false`, inclusive na home, calculadoras, dúvidas e assistente.
3. O assistente exibe o rótulo promocional `IA 2080`, sem base verificável.
4. A documentação e testes atuais exigem placeholders, em conflito com o novo requisito de não renderizar publicidade antes da aprovação.

Também há riscos importantes de indexação: o sitemap contém 390 URLs, das quais 278 são páginas programáticas de faixas salariais/CLT-PJ, além de incluir `/meu-painel`, `/assistente`, `/widget` e `/newsletter`. Essas famílias precisam de decisão editorial explícita antes de permanecerem indexáveis.

## Resultado de build e testes

Comandos executados na raiz:

```text
dotnet restore MeuValorLiquido.slnx
dotnet build MeuValorLiquido.slnx --no-restore
dotnet test MeuValorLiquido.slnx --no-build --no-restore
```

Resultado:

| Etapa | Resultado | Evidência |
|---|---:|---|
| Restore | Aprovado | 13 projetos restaurados |
| Build | Aprovado | 0 erros, 0 avisos |
| Core.Tests | Aprovado | 5/5 |
| Calculators.Tests | Aprovado | 243 aprovados, 1 teste gerador ignorado |
| Integration.Tests | Aprovado | 1/1 |
| WebApp.Tests | Aprovado | 551/551 |
| Playwright.Tests — primeira execução | Bloqueado pelo ambiente | Chromium `1161` ausente; os testes não chegaram a navegar |
| Playwright.Tests — após instalar Chromium | Aprovado | 11/11 |
| **Total efetivamente aprovado** | **811** | **0 falhas funcionais; 1 teste gerador ignorado** |

O primeiro erro do Playwright não era regressão da aplicação. O navegador exigido foi instalado com o script gerado pelo pacote e a suíte foi repetida com sucesso.

## Inventário da solução

| Área | Projeto |
|---|---|
| Núcleo | `src/Core/MeuValorLiquido.Core.csproj` |
| Compartilhado | `src/Shared/MeuValorLiquido.Shared.csproj` |
| Calculadoras | `src/Modules/Calculators/MeuValorLiquido.Modules.Calculators.csproj` |
| Conteúdo | `src/Modules/Content/MeuValorLiquido.Modules.Content.csproj` |
| Contato | `src/Modules/Contact/MeuValorLiquido.Modules.Contact.csproj` |
| Newsletter | `src/Modules/Newsletter/MeuValorLiquido.Modules.Newsletter.csproj` |
| Anúncios | `src/Modules/Ads/MeuValorLiquido.Modules.Ads.csproj` |
| Aplicação web | `src/WebApp/MeuValorLiquido.WebApp.csproj` |
| Testes | Core, Calculators, Integration, WebApp e Playwright em `tests/` |

## Inventário público de URLs

O sitemap de produção respondeu HTTP 200 via GET em 17/07/2026 e contém **390 URLs**.

### Distribuição exata do sitemap de produção

| Família | Quantidade | Origem |
|---|---:|---|
| `/salario-liquido/*` | 139 | `SalaryBandCatalog.GetAllIndexablePaths()` |
| `/clt-pj/*` | 139 | `CltPjBandCatalog.GetAllIndexablePaths()` |
| `/blog/*` | 47 | posts publicados no banco |
| `/duvidas/*` | 27 | `PopularQuestionsCatalog.GetAll()` |
| `/calculadoras` + detalhes | 20 | catálogo ativo: hub + 19 calculadoras |
| Demais URLs estáticas | 18 | lista fixa do `SitemapGenerator` |
| **Total** | **390** | produção |

### Matriz de decisão das rotas

| URL ou padrão | Tipo | Estado atual | Sitemap | Ação recomendada | Prioridade |
|---|---|---|---:|---|---|
| `/` | Home | `index,follow`, 200, canonical próprio | Sim | Manter após remover prova social não comprovada e placeholders | P0 |
| `/calculadoras` | Catálogo | `index,follow`, 200 | Sim | Manter; canonical consolida filtros e busca | P0 |
| `/calculadoras/{slug}` (19) | Ferramenta/editorial | 200 para slug válido; slug inválido produz soft 404 com HTTP 200 | Sim | Manter páginas válidas; corrigir inválidas para HTTP 404 | P0 |
| `/salario-liquido` | Hub programático | 200 | Sim | Manter se houver valor editorial independente | P1 |
| `/salario-liquido/{valor...}` (138) | Programática | 200 para combinação aceita; 404 caso inválido | Sim | Auditar unicidade e demanda; reduzir/noindex se conteúdo for repetitivo | P1 |
| `/clt-pj` | Hub programático | 200 | Sim | Manter se houver valor editorial independente | P1 |
| `/clt-pj/{valor...}` (138) | Programática | 200 para combinação aceita; 404 caso inválido | Sim | Auditar unicidade e demanda; reduzir/noindex se conteúdo for repetitivo | P1 |
| `/blog` e `/blog/{slug}` (47 posts) | Editorial | 200; inválido retorna 404 | Sim | Manter; revisar autoria, fontes e atualização por artigo | P0 |
| `/duvidas` e `/duvidas/{slug}` (27 itens) | Editorial/FAQ | 200; inválido retorna 404 | Sim | Manter itens substanciais; evitar FAQ genérica em escala | P1 |
| `/desligamento` | Hub temático | 200 | Sim | Manter | P1 |
| `/negociar-salario` | Hub temático | 200 | Sim | Manter | P1 |
| `/virar-pj` | Hub temático | 200 | Sim | Manter | P1 |
| `/conferir-holerite` | Ferramenta | 200 | Sim | Manter; validar fórmulas e conteúdo | P0 |
| `/como-calculamos` | Metodologia | 200 | Sim | Manter | P0 |
| `/sobre` | Institucional | 200 | Sim | Manter e ampliar experiência verificável | P0 |
| `/contato` | Institucional/formulário | 200 | Sim | Manter | P0 |
| `/politica-editorial` | Institucional | 200 | Sim | Manter | P0 |
| `/politica-de-privacidade` | Legal | 200 | Sim | Manter; alinhar texto ao comportamento após Sprint 1/5 | P0 |
| `/politica-de-cookies` | Legal | 200 | Sim | Manter; revisar categorias e revogação | P0 |
| `/termos-de-uso` | Legal | 200 | Sim | Manter | P0 |
| `/aviso-legal` | Legal | 200 | Sim | Manter | P0 |
| `/newsletter` | Conversão | `index,follow`, 200 | Sim | Avaliar valor independente; noindex se for apenas formulário | P1 |
| `/assistente` | Ferramenta guiada | `index,follow`, 200 | Sim | Remover placeholders e `IA 2080`; reavaliar indexação | P0 |
| `/meu-painel` | Estado local/pessoal | `index,follow`, 200 | Sim | Remover do sitemap e aplicar `noindex,follow` | P0 |
| `/widget` | Ferramenta de distribuição | `index,follow`, 200 | Sim | Manter somente se estratégico e substancial; não é página privada | P1 |
| `/metricas-internas` | Métrica pública agregada | `noindex,nofollow`, 200 | Não | Manter fora do sitemap; avaliar proteção/remoção de navegação pública | P1 |
| `/mapa-do-site` | Sitemap HTML | 200 | Sim | Manter se atualizado e útil | P2 |
| `/Error` | Erro | `noindex,nofollow`; acesso direto 200 | Não | Manter fora do sitemap; avaliar 404/410 no acesso direto | P2 |
| rota inexistente | Erro | HTTP 404 real | Não | Conforme | — |
| `/calculadora-salario-bruto` | Legado | 301 | Não | Conforme | — |
| `/duvidas/o-que-e-irrf` | Legado | 301 | Não | Conforme | — |
| `/quanto-preciso-ganhar-para-receber-liquido` | Legado | 302 | Não | Trocar para 301 se mudança definitiva | P1 |
| `/proposta-salarial` | Legado | 302 | Não | Trocar para 301 se mudança definitiva | P1 |
| `/comparar-proposta-salarial` | Legado | 302 | Não | Trocar para 301 se mudança definitiva | P1 |
| `/clt-vs-pj` | Legado | 302 | Não | Trocar para 301 se mudança definitiva | P1 |
| `/painel` | Legado | 302 | Não | Trocar para 301 e destino `noindex` | P1 |
| `/incorporar` | Legado | 302 | Não | Trocar para 301 se mudança definitiva | P1 |
| `/widget/{slug}` | Redirecionamento de embed | 302 ou 404 | Não | Manter temporário apenas se o destino depende de parâmetros | P2 |
| `/calculadoras/{slug}/resultado.pdf` | Arquivo gerado | 200/404; sem `X-Robots-Tag` | Não | Enviar `X-Robots-Tag: noindex, nofollow, noarchive`; manter sem anúncios | P1 |
| `/api/metrics/collect` | API POST | 200/erro de validação | Não | Manter fora do índice; rate limit já configurado | P1 |
| `/health` | Infraestrutura | 200/503 | Não | Manter fora do índice e da navegação | P2 |

Não existem rotas de login/cadastro/autores/correções no estado atual.

## SEO técnico

### Itens conformes

- `robots.txt` responde 200, permite rastreamento e aponta para o sitemap HTTPS.
- Sitemap responde 200 via GET e contém URLs absolutas no domínio canônico.
- O layout define `title`, description, canonical, robots, Open Graph e Twitter.
- URL inexistente retorna HTTP 404 real.
- Slugs inexistentes de blog, dúvidas e páginas programáticas são tratados como 404.
- `/metricas-internas`, `/Error` e a página de 404 usam `noindex`.
- Parâmetros de busca/filtro em `/calculadoras` têm canonical para `/calculadoras`.
- Breadcrumbs e schemas são implementados por partials reutilizáveis.

### Achados

#### SEO-P0 — páginas pessoais/estados locais no sitemap

`/meu-painel` é indexável e consta no sitemap, embora o conteúdo principal dependa de `localStorage` e possa abrir em estado vazio. Remover do sitemap e aplicar `noindex,follow`.

Evidências:

- `src/WebApp/Infrastructure/SitemapGenerator.cs`
- `src/WebApp/Pages/MeuPainel/Index.cshtml`

#### SEO-P0 — calculadora inexistente retorna soft 404

`/calculadoras/{slug-invalido}` renderiza “Calculadora não encontrada” e define `noindex`, porém o PageModel termina em `Page()` e mantém HTTP 200. Retornar `NotFound()` para que o middleware produza a página global com status 404 real.

Evidências:

- `src/WebApp/Pages/Calculadoras/Details.cshtml.cs`
- `src/WebApp/Pages/Calculadoras/Details.cshtml`

#### SEO-P1 — escala programática sem decisão registrada

O sitemap publica 278 páginas de faixas (`salario-liquido` e `clt-pj`), 71,3% de todas as URLs. Há builders editoriais e testes, mas a auditoria estática não comprova demanda, diferenciação percebida pelo Google nem ausência de padrões repetitivos. Criar amostra de conteúdo/índice por família antes de manter todas indexáveis.

#### SEO-P1 — rotas utilitárias indexáveis

`/assistente`, `/widget` e `/newsletter` são `index,follow` e estão no sitemap. Não devem ser removidas automaticamente: assistente e widget têm conteúdo próprio. A decisão precisa considerar qualidade real, estado completo e intenção de busca. Enquanto houver placeholders e linguagem artificial, `/assistente` é risco.

#### SEO-P1 — redirecionamentos permanentes implementados como 302

Se as rotas legadas não voltarão a ter conteúdo próprio, devem usar 301. O código atual usa `permanent: false` em seis aliases.

Evidência: `src/WebApp/Program.cs`.

#### SEO-P1 — PDFs personalizados não enviam diretiva de indexação

Os endpoints GET de PDF podem incluir resultado/token na URL e não enviam `X-Robots-Tag`. Como `robots.txt` permite rastrear tudo, URLs descobertas externamente podem ser rastreadas ou indexadas. Adicionar `noindex, nofollow, noarchive` por header e evitar dados pessoais em URLs.

Evidência: `src/WebApp/Infrastructure/CalculatorPdfEndpoints.cs`.

#### SEO-P1 — redirecionamento HTTPS é temporário por padrão

`UseHttpsRedirection()` sem configuração explícita usa 307. Confirmar o comportamento do proxy em produção e configurar 308/301 para a migração canônica HTTP→HTTPS.

Evidência: `src/WebApp/Program.cs`.

#### SEO-P1 — página “interna” é pública

`/metricas-internas` está corretamente em `noindex`, mas permanece acessível sem autorização e recebe links do mapa HTML e da política de privacidade. `noindex` não é controle de acesso; proteger, renomear ou assumir/documentar que a página é pública.

#### SEO-P1 — dois H1 na home

A home mantém um H1 mobile e outro desktop no DOM. Embora sejam variantes responsivas, o HTML entregue contém dois H1 com mensagens diferentes. Consolidar em um H1 ou documentar/testar a decisão semântica.

Evidência: `src/WebApp/Pages/Index.cshtml`.

#### SEO-P1 — `lastmod` estático para conteúdo mutável

A maior parte das URLs recebe `2026-06-29`, incluindo páginas que já tiveram mudanças posteriores. O valor não muda a cada deploy, o que é positivo, mas também não representa necessariamente a última revisão editorial real.

Evidência: `src/WebApp/Infrastructure/SitemapGenerator.cs`.

#### SEO-P2 — fallback inconsistente de domínio no schema da home

A home usa fallback `https://meuvalorliquido.com.br`, enquanto o restante usa `.com`. Em produção o `Site:BaseUrl` evita o problema, mas uma configuração ausente geraria schema divergente.

Evidência: `src/WebApp/Pages/Index.cshtml`.

#### SEO-P2 — marca pode ser duplicada nos títulos

O layout sempre acrescenta `- Meu Valor Líquido`, mas títulos seed de calculadoras e dúvidas já podem conter `| Meu Valor Líquido`. O resultado potencial é `... | Meu Valor Líquido - Meu Valor Líquido`. Auditar o banco renderizado e centralizar a inclusão da marca em um único lugar.

Evidências:

- `src/WebApp/Pages/Shared/_Layout.cshtml`
- `src/Modules/Calculators/CalculatorSeedData.cs`

#### SEO-P2 — resultados HTML ampliam o espaço de rastreamento

URLs `/calculadoras/{slug}?r=...` usam canonical limpo, mas continuam `index,follow`. Aplicar `noindex,follow` quando houver resultado serializado, preservando o canonical para a calculadora base.

## AdSense e compliance

### Mapa de publicidade

| Local | Implementação | Comportamento com anúncios desligados |
|---|---|---|
| Home | markup fixo em `Pages/Index.cshtml` | Sempre exibe “Espaço publicitário” |
| Calculadoras | `_AdSlot.cshtml` via provider | Provider retorna placeholders |
| Faixas salariais | `_AdSlot.cshtml` | Provider retorna placeholders |
| Comparações CLT-PJ | `_AdSlot.cshtml` | Provider retorna placeholders |
| Dúvidas | `_AdSlot.cshtml` e markup no hub | Placeholders visíveis |
| Assistente | `_AdSlot.cshtml` + fallbacks fixos | Placeholders desktop e mobile visíveis |
| Script AdSense | `_AdSenseScript.cshtml` | Comportamento da linha de base; removido e substituído por meta tag + consentimento na Sprint 90 |
| Inicialização | `wwwroot/js/adsense-init.js` | Carregado em todas as páginas pelo layout |

### Achados

#### ADS-P0 — métrica e avaliação sem comprovação

A home publica:

- `+250k cálculos realizados este mês`;
- cinco estrelas com `aria-label="Avaliação 5 de 5 estrelas"`;
- três avatares como prova social;
- selo “Mais usada” sem vinculação explícita a telemetria.

Não foi encontrada evidência que sustente o volume mensal nem a nota. Remover a métrica, estrelas e qualquer prova social não auditável. Exibir métricas somente a partir da telemetria real, com período e definição documentados.

Evidência: `src/WebApp/Pages/Index.cshtml`.

#### ADS-P0 — placeholders visíveis com publicidade desativada

`ConfigurableAdSlotProvider.GetSlots()` devolve `PlaceholderAdSlotProvider` quando `Ads:Enabled=false`; `_AdSlot` então renderiza wrapper, rótulo e altura. Home, FAQ e assistente ainda possuem placeholders fixos fora do provider.

O comportamento desejado para a Sprint 1 é retornar coleção vazia/null e não produzir markup nem espaço antes da aprovação.

Evidências:

- `src/WebApp/Infrastructure/ConfigurableAdSlotProvider.cs`
- `src/WebApp/Pages/Shared/_AdSlot.cshtml`
- `src/WebApp/Pages/Index.cshtml`
- `src/WebApp/Pages/Assistente/Index.cshtml`
- `src/WebApp/Pages/Duvidas/Index.cshtml`

#### ADS-P0 — documentação e testes exigem o comportamento que deve ser removido

`docs/ADSENSE_COMPLIANCE.md`, `docs/adsense-checklist.md`, planos Stitch e `InstitutionalPagesTests` tratam placeholders como requisito. A Sprint 1 precisa atualizar código, testes e documentação no mesmo conjunto para evitar regressão ao comportamento antigo.

#### TRUST-P0 — linguagem artificial ou não comprovada

Foram encontrados:

- `IA 2080`;
- “alta precisão”;
- “insights profundos”;
- “ferramentas de precisão”;
- “algoritmos testados”;
- “sincronização com tabelas tributárias nacionais”.

Substituir por linguagem verificável, preferencialmente “estimativas baseadas nas tabelas oficiais indicadas na metodologia”.

Evidências:

- `src/WebApp/Pages/Assistente/Index.cshtml`
- `src/WebApp/Pages/Index.cshtml`
- `src/WebApp/Pages/Shared/_Layout.cshtml`

#### PRIV-P1 — consentimento não possui as quatro categorias planejadas

O banner implementa “Essenciais” e “Publicidade e medição”. Não separa analytics, personalização e publicidade. Isso é aceitável apenas enquanto esses serviços não existem; antes de ativar AdSense, o modelo deve refletir exatamente os scripts e finalidades reais.

#### PRIV-P1 — revogação não descarrega scripts/cookies já ativos

“Revisar preferências” remove o registro local e reabre o banner, mas não descarrega o AdSense já carregado nem remove cookies de terceiros na sessão atual. Documentar que a revogação completa pode exigir reload e implementar o comportamento necessário antes da ativação.

Evidência: `src/WebApp/wwwroot/js/cookie-consent.js`.

#### PRIV-P1 — script de verificação pode carregar sem consentimento

`_AdSenseScript.cshtml` carregava diretamente quando `VerificationEnabled=true`, independentemente do consentimento. **Resolvido na Sprint 90:** verificação por meta tag e script externo somente após consentimento de Publicidade.

O partial também era renderizado no fim do `<body>`. **Resolvido na Sprint 90:** a meta tag de verificação fica no `<head>` e o partial de script foi removido.

#### ADS-P1 — deploy não injeta IDs dos slots

`AdsOptions` prevê `CalculatorTopSlotId` e `CalculatorBottomSlotId`, mas `docker-compose.prod.yml` e `.env.prod.example` só injetam enabled, verification e publisher. Ativar anúncios pelo procedimento documentado pode carregar o script e continuar exibindo placeholders por falta dos IDs. Incluir as duas variáveis, validação de configuração e instruções concretas antes da ativação.

#### PRIV-P1 — consentimento não expira como documentado

A política informa duração de 12 meses, mas o código salva `updatedAt` sem verificar expiração. O fallback legado `"accepted"` também pode manter consentimento publicitário sem prazo. Implementar validade e migração explícitas.

#### ADS-P1 — CMP customizada pode ser insuficiente fora do Brasil

Para tráfego do EEE, Reino Unido ou Suíça, validar exigências atuais do Google para CMP certificada/IAB TCF, fornecedores, finalidades e anúncios personalizados versus não personalizados. O banner customizado não deve ser considerado suficiente sem essa validação.

#### PERF-P1 — slot responsivo ainda pode causar CLS

O placeholder reserva 120px, mas o slot live usa altura automática e pode crescer após o carregamento. Definir formatos/dimensões por breakpoint e validar com anúncios reais em ambiente controlado.

#### PRIV-P1 — política descreve placeholders como comportamento desejado

A Política de Privacidade informa que placeholders são exibidos enquanto anúncios estão inativos. Esse texto deve mudar junto com a Sprint 1.

#### ADS-P1 — `ads.txt` já está publicado

Produção responde 200 com:

```text
google.com, pub-4150358596824425, DIRECT, f08c47fec0942fa0
```

O identificador também está versionado em `wwwroot/ads.txt`, enquanto os exemplos de configuração mantêm publisher vazio. Confirmar no painel AdSense que esse é o ID real da conta. Não alterar nem inventar ID durante as sprints.

Também deve existir validação operacional para impedir divergência entre `ADS_PUBLISHER_ID` e o publisher público do `ads.txt`.

### Itens conformes

- `Ads:Enabled=false` por padrão e no exemplo de produção.
- Publisher e IDs de slot são configuráveis, sem segredo de API no código.
- Script e CSP só liberam domínios de anúncios quando necessário.
- Banner oferece “Aceitar todos”, “Rejeitar todos” e personalização.
- Consentimento tem versão e data (`updatedAt`).
- Preferências podem ser reabertas pela Política de Cookies.
- Política de Privacidade, Cookies, Termos, Aviso Legal e Política Editorial existem.
- PDFs e widgets incorporados foram projetados sem anúncios.
- Rate limiting existe para formulários e métricas.

## Conteúdo, autoria e E-E-A-T

### Itens conformes

- Autor editorial identificado como Matteus Oberdan, com foto, papel, biografia e LinkedIn.
- Schema `Person` é incluído nos artigos.
- Política editorial descreve fontes, revisão, limites e canal de correção.
- Metodologia, avisos educativos e fontes oficiais estão presentes.
- 19 calculadoras possuem title, description, resumo e FAQ.
- Algumas calculadoras recentes têm introdução específica e FAQ exclusiva.
- O motor de cálculo fica fora da UI e possui benchmarks automatizados.

### Lacunas

#### CONTENT-P0 — parte das calculadoras usa conteúdo genérico

O factory padrão cria apenas resumo e duas FAQs repetidas (“é oficial?” e “os dados são salvos?”). Isso afeta várias ferramentas e não atende à estrutura editorial completa exigida para a Sprint 2. É necessário medir página por página: explicação, entradas, exclusões, exemplo, interpretação, erros comuns, fontes, revisão, responsável e limitações.

Evidência: `src/Modules/Calculators/CalculatorSeedData.cs`.

#### EEAT-P1 — não existe perfil de autor interno

O cartão leva diretamente ao LinkedIn. Não existe `/autores/matteus-oberdan`, catálogo de artigos do autor nem data de revisão do perfil. Criar a página sem inventar credenciais.

#### EEAT-P1 — não existe rota de correções

A Política Editorial descreve o processo e aponta para Contato, mas não há `/correcoes`. Isso não bloqueia sozinho a aprovação; pode ser criado como `noindex` até existir histórico real.

#### EEAT-P1 — experiência verificável ainda é genérica

A biografia informa atividades editoriais, mas não explica experiência real, histórico do projeto ou critérios de revisão em profundidade. Expandir somente com fatos comprováveis.

## Priorização consolidada

### P0 — antes de nova avaliação

1. Remover métrica `+250k`, estrelas, avatares e qualquer avaliação não comprovada.
2. Remover todos os placeholders e espaços de publicidade quando anúncios estiverem desativados.
3. Atualizar testes e documentação que hoje exigem placeholders.
4. Remover/moderar “IA 2080”, “alta precisão”, “insights profundos” e alegações similares.
5. Tirar `/meu-painel` do sitemap e aplicar `noindex,follow`.
6. Completar conteúdo específico das calculadoras prioritárias.
7. Preservar build e os 811 testes aprovados ao executar a Sprint 1.

### P1 — antes de ativar anúncios

1. Decidir indexação de `/assistente`, `/widget` e `/newsletter`.
2. Auditar amostra e qualidade das 278 páginas programáticas.
3. Converter redirects legados definitivos de 302 para 301.
4. Corrigir soft 404 e aplicar `X-Robots-Tag` nos PDFs/resultados personalizados.
5. Implementar perfil interno de autor.
6. Tornar `lastmod` editorialmente verdadeiro.
7. Revisar categorias, validade, CMP e revogação de consentimento.
8. Injetar e validar IDs de slots no deploy.
9. Confirmar publisher de `ads.txt` no painel.
10. Revisar individualmente conteúdo, fontes e revisão das 19 calculadoras.

### P2 — melhoria

1. Consolidar o H1 responsivo da home.
2. Corrigir fallback `.com.br` do schema.
3. Eliminar duplicação da marca nos títulos.
4. Avaliar resposta direta de `/Error`.
5. Criar página/processo público de correções quando houver histórico útil.

## Limitações da auditoria

- A análise de títulos, H1, canonical e conteúdo foi estática e complementada por amostragem de produção; não foi usado crawler visual de todas as 390 URLs.
- Não houve acesso ao painel AdSense, Search Console, telemetria real ou configuração `.env.prod`.
- O ID de `ads.txt` não pode ser validado como pertencente à conta sem acesso ao painel.
- Não foram alteradas fórmulas nem executada comparação manual de todos os benchmarks nesta sprint.
- Lighthouse/Core Web Vitals ficam fora da Sprint 0 e devem ser medidos na Sprint 6.

## Critério de saída da Sprint 0

- [x] Inventário da solução e famílias de URLs.
- [x] Contagem e classificação das 390 URLs do sitemap.
- [x] Robots, sitemap, canonical, redirects, noindex e status HTTP auditados.
- [x] Componentes de anúncio e consentimento mapeados.
- [x] Páginas legais verificadas.
- [x] Restore e build executados.
- [x] Testes executados; dependência Playwright corrigida localmente e suíte repetida.
- [x] Achados priorizados em P0, P1 e P2.
- [x] Nenhuma alteração funcional.

## Recomendação para a próxima sprint

Prosseguir com a **Sprint 1 — remover sinais de baixa confiança**, em branch separada, limitada a:

1. prova social não comprovada;
2. placeholders de anúncios;
3. linguagem promocional não verificável;
4. recursos incompletos expostos na navegação;
5. testes e documentação diretamente afetados.

Não iniciar conteúdo em massa, mudanças de fórmula ou ativação do AdSense na mesma sprint.
