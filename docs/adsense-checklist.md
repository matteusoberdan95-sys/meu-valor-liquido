# AdSense Checklist

Não integrar AdSense real no MVP. Regras de layout: `docs/ADSENSE_COMPLIANCE.md`.

## Pré-requisitos no código (OK)

- [x] Home e central de calculadoras
- [x] 15 calculadoras funcionais
- [x] 15+ artigos no blog (seed editorial)
- [x] Sobre, Contato, Privacidade, Termos, Aviso Legal, Como calculamos
- [x] Sitemap dinâmico e `robots.txt`
- [x] Slots com label “Espaço publicitário” e altura reservada (CLS)
- [x] Layout mobile-first (Valores Públicos)
- [x] `Ads:Enabled=false` por padrão — sem publisher ID no repositório

## Antes de solicitar (operacional)

- [ ] Domínio público com **HTTPS**
- [ ] `Site:BaseUrl` apontando para o domínio final
- [ ] Contato e newsletter com SMTP de produção testados
- [ ] Smoke test manual (`docs/DEPLOY.md` §3)

## Após aprovação

Ver Sprint 20 em `docs/sprint-plan.md`: `Ads__Enabled`, `Ads__PublisherId`, slot IDs via ambiente.
