# Sprint Plan

## Status do roadmap

| Sprint | Tema | Status |
|--------|------|--------|
| 0–13 | Fundação, calculadoras, infra, docs | Concluído |
| **14** | **UI/UX, SEO avançado, newsletter** | **Concluído** |
| 15 | Conteúdo editorial (10–20 artigos) | Planejado |
| 16 | Staging e deploy | Planejado |

## Sprint 14 - UI/UX, SEO avançado e newsletter

Objetivo: elevar a experiência visual, SEO técnico e captura de e-mail.

Entregas:
- Design system CSS (cores, tipografia DM Sans, cards, extrato, hero)
- Layout renovado (navbar, footer, trust badges)
- Filtro por categoria nas calculadoras
- Open Graph + Twitter Cards + canonical
- JSON-LD: WebSite (home), FAQPage (calculadoras), Article (blog)
- Página `/newsletter` com persistência e Mailpit
- 5 artigos adicionais no seed do blog
- Testes de SEO e página newsletter

Definition of Done: `dotnet test` verde; páginas com meta tags e schema; newsletter funcional.

## Sprint 15 - Conteúdo editorial (próxima)

Objetivo: expandir blog para 10–20 artigos originais (readiness AdSense).

## Sprint 16 - Staging e deploy

Objetivo: ambiente staging, CI/CD deploy, domínio e HTTPS.
