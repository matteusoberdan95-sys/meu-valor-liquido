# AdSense Compliance — Meu Valor Líquido

Regras internas para monetização via Google AdSense de forma **legítima e segura**. Antes da aprovação, permitir apenas o script de verificação solicitado pelo Google; não ativar slots de anúncios até aprovação e revisão deste documento.

> Pré-requisitos de conteúdo: `docs/adsense-checklist.md`

---

## O que é permitido

- Script de verificação do AdSense no `<head>` com `Ads:VerificationEnabled=true` e `Ads:Enabled=false`.
- Placeholders com altura reservada até aprovação.
- Anúncios em áreas claramente separadas do conteúdo interativo.
- Texto educativo e avisos legais sobre cálculos estimativos.
- Links naturais para outras calculadoras e artigos.
- Compartilhamento voluntário de resultados (Sprint 4).
- PDF com marca do site, **sem anúncios dentro do PDF** (Sprint 5).

---

## O que é proibido

- Pedir, sugerir ou incentivar clique em anúncios (“clique aqui”, “apoie o site clicando no anúncio”).
- Bot, tráfego artificial, auto-refresh abusivo ou impressões forçadas.
- Anúncios dentro de PDF, widget incorporável ou e-mail.
- Anúncios que imitam botões, menus ou resultado da calculadora.
- Colocar anúncios colados em:
  - Botão “Calcular agora”
  - Campos de formulário
  - Resultado líquido / extrato
  - Botões de compartilhar ou baixar PDF
- Pop-ups, interstitials agressivos ou anúncios que cubram o conteúdo principal.
- Conteúdo copiado, thin content ou páginas criadas só para ads.

---

## Slots atuais (placeholders)

| Key | Posição | Arquivo |
|-----|---------|---------|
| `calculator-top` | Acima do layout calculadora | `Details.cshtml` (após hero) |
| `calculator-bottom` | Sidebar, abaixo do resultado | `Details.cshtml` |

Implementação:

- Definições: `src/Modules/Ads/AdsModule.cs` (`PlaceholderAdSlotProvider`)
- Partial: `src/WebApp/Pages/Shared/_AdSlot.cshtml`
- Estilo: `src/WebApp/wwwroot/css/site.css` (`.ad-slot`)

---

## Critérios de layout (revisar antes de ativar AdSense real)

### Altura e CLS (Cumulative Layout Shift)

- Reservar altura mínima no container (`.ad-slot`: `min-height` ≥ 90px; revisar para 120–250px conforme formato do bloco).
- Não inserir script AdSense sem container com dimensões definidas.
- Evitar que o anúncio empurre o botão “Calcular” após o carregamento.

### Espaçamento

- Margem vertical mínima de **24px** entre slot e:
  - Formulário de entrada
  - Botão de ação primária
  - Painel de resultado
- Slot superior: após o hero/resumo, **antes** do formulário (posição atual aceitável se mantiver margem).

### Rótulos dos placeholders

- Usar texto neutro: “Espaço publicitário” (implementado em `_AdSlot.cshtml`).
- Placeholder interno do módulo Ads pode manter texto técnico; a UI exibe o rótulo público.
- **Não** usar: “Clique no anúncio”, “Anúncio importante”, setas apontando para o slot.

### Mobile

- Testar que o dedo não acerta o anúncio ao tocar “Calcular”.
- Sidebar de resultado pode empilhar abaixo do formulário; revisar ordem dos slots no mobile.

---

## Privacidade e cookies

Antes de ativar AdSense:

- Atualizar `PoliticaDePrivacidade.cshtml` com cookies de publicidade e parceiros Google.
- Implementar banner de consentimento se exigido (LGPD / políticas Google).
- Documentar em `/como-calculamos` (Sprint 14). **Concluído.**

---

## Checklist de revisão pré-ativação

- [x] Verificação de propriedade via script AdSense configurável (`Ads:VerificationEnabled`)
- [ ] Conta AdSense aprovada
- [x] Política de privacidade completa (cookies/ads) — Sprint 14
- [x] Páginas institucionais revisadas — Sprint 14
- [x] Página `/como-calculamos` — Sprint 14
- [ ] Nenhum texto incentivando clique
- [ ] Slots com espaçamento validado em desktop e mobile
- [x] Core Web Vitals — cache, defer, slots com altura fixa (Sprint 12)
- [x] Publisher ID via variável de ambiente (não hardcoded) — `Ads:PublisherId` Sprint 14
- [x] Rollback documentado (desabilitar slot por config) — `Ads:Enabled=false` Sprint 14

---

## Integração futura (pós-aprovação)

1. Para verificação: configurar `Ads:VerificationEnabled=true`, `Ads:PublisherId` e manter `Ads:Enabled=false`.
2. Após aprovação: configurar `Ads:Enabled=true` e IDs reais dos slots.
3. Manter CSP atualizada para domínios Google Ads.
4. Monitorar relatório de políticas no painel AdSense.

---

## Responsável por agente

| Agente | Foco |
|--------|------|
| AdSense Compliance | Este documento + revisão de layout |
| Frontend | Espaçamento, mobile, CLS |
| SEO/Content | Conteúdo suficiente por página |
| Security | Privacidade, cookies, CSP |
