# AdSense Checklist

Não integrar AdSense real no MVP. Regras de layout: `docs/ADSENSE_COMPLIANCE.md`.

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
- [x] Layout mobile-first (Valores Públicos)
- [x] `Ads:Enabled=false` por padrão

## Antes de solicitar (operacional)

- [ ] Domínio público com **HTTPS**
- [ ] `Site:BaseUrl` apontando para o domínio final
- [ ] Contato e newsletter com SMTP de produção testados
- [ ] Smoke test manual (`docs/DEPLOY.md` §3)

## Após aprovação

Ver Sprint 20 em `docs/sprint-plan.md`: `Ads__Enabled`, `Ads__PublisherId`, slot IDs via ambiente.
