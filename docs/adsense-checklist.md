# AdSense Checklist

Não integrar AdSense real no MVP. Regras de layout: `docs/ADSENSE_COMPLIANCE.md`.
Pré-revisão formal: `docs/adsense/FINAL_PRE_REVIEW_CHECKLIST.md` (Sprint 94).

## Pré-requisitos no código (OK)

- [x] Home e central de calculadoras
- [x] 15 calculadoras funcionais
- [x] 15+ artigos no blog (seed editorial)
- [x] Sobre, Contato, Privacidade, Termos, Aviso Legal, Como calculamos
- [x] Sitemap dinâmico e `robots.txt`
- [x] Nenhum slot, placeholder ou altura reservada com `Ads:Enabled=false`
- [x] Slots reais só são renderizados com publisher e ID configurados
- [x] Home sem métricas, avaliações ou provas sociais não comprovadas
- [x] Perfil indexável do responsável editorial com autoria clicável nos artigos
- [x] Política Editorial documenta fontes, revisão, correções, patrocínio e automação
- [x] Processo de correções público em `noindex,follow`, sem histórico fictício
- [x] Sitemap contém somente URLs canônicas e indexáveis
- [x] Assistente, painel, newsletter, widget, métricas, filtros e resultados parametrizados fora do índice
- [x] URLs inexistentes retornam 404 real; erros 500 não possuem canonical
- [x] Aliases e variações de caixa/barra redirecionam permanentemente para a URL canônica
- [x] Consentimento com quatro categorias, rejeição real e reabertura
- [x] Script AdSense bloqueado até Publicidade; verificação por meta tag
- [x] Políticas de Privacidade/Cookies alinhadas ao comportamento real
- [x] Layout mobile-first (Valores Públicos)
- [x] `Ads:Enabled=false` por padrão
- [x] Testes `Sprint94AdSensePreReviewTests` travam regressões da pré-revisão

## Antes de solicitar (operacional)

- [ ] Merge de `feat/adsense-sprint-6` (matemática), `-7` (performance) e `-8` (editorial lote 6)
- [ ] Domínio público com **HTTPS**
- [ ] `Site:BaseUrl` apontando para o domínio final
- [ ] Contato e newsletter com SMTP de produção testados
- [ ] Smoke test manual (`docs/DEPLOY.md` §3)
- [ ] Lighthouse mobile pós-deploy (home, calculadora, artigo)

## Após aprovação

Ver Sprint 20 em `docs/sprint-plan.md`: `Ads__Enabled`, `Ads__PublisherId`, slot IDs via ambiente.

