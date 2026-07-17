# AdSense Compliance — Meu Valor Líquido

Regras internas para monetização via Google AdSense de forma **legítima e segura**. Antes da aprovação, usar somente a meta tag de verificação da conta; não ativar slots de anúncios até aprovação e revisão deste documento.

> Pré-requisitos de conteúdo: `docs/adsense-checklist.md`

---

## O que é permitido

- Meta tag `google-adsense-account` no `<head>` com `Ads:VerificationEnabled=true` e `Ads:Enabled=false`, sem script externo.
- Nenhum slot, placeholder ou espaço reservado enquanto `Ads:Enabled=false`.
- Anúncios em áreas claramente separadas do conteúdo interativo.
- Texto educativo e avisos legais sobre cálculos estimativos.
- Perfil editorial visível com autor responsável, foto e LinkedIn quando fizer sentido.
- Página de Política Editorial com fontes oficiais, revisão e fluxo de correções.
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
- Métricas, avaliações, depoimentos ou provas sociais sem evidência auditável.

---

## Slots configuráveis

| Key | Posição | Arquivo |
|-----|---------|---------|
| `calculator-top` | Após o hero/resumo | Calculadoras, faixas salariais, comparativos CLT/PJ e dúvidas |
| `calculator-bottom` | Após resultado/conteúdo | Calculadoras, faixas salariais, comparativos CLT/PJ, dúvidas e assistente |

Implementação:

- Provider: `src/WebApp/Infrastructure/ConfigurableAdSlotProvider.cs`
- Partial: `src/WebApp/Pages/Shared/_AdSlot.cshtml`
- Estilo: `src/WebApp/wwwroot/css/site.css` (`.ad-slot`)

Comportamento obrigatório:

- `Ads:Enabled=false`: provider retorna coleção vazia e a página não produz markup nem reserva altura.
- `Ads:Enabled=true`: somente slots com publisher e ID real configurados são retornados.
- Não criar placeholders estáticos fora do provider.

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

### Rótulos dos anúncios ativos

- Usar texto neutro: “Publicidade” quando um anúncio real estiver ativo.
- **Não** usar: “Clique no anúncio”, “Anúncio importante”, setas apontando para o slot.

### Mobile

- Testar que o dedo não acerta o anúncio ao tocar “Calcular”.
- Sidebar de resultado pode empilhar abaixo do formulário; revisar ordem dos slots no mobile.

---

## Privacidade e cookies

Antes de ativar AdSense:

- Manter `PoliticaDePrivacidade.cshtml` e `PoliticaDeCookies.cshtml` alinhadas ao comportamento real.
- Exibir Essenciais, Analytics, Personalização e Publicidade sem seleção opcional por padrão.
- `Rejeitar todos` deve manter todas as categorias opcionais desativadas.
- Não carregar `adsbygoogle.js` antes do consentimento de Publicidade.
- Permitir reabertura e revogação; versão vigente: `2`, política: `2026-07-17`.
- Documentar em `/como-calculamos` (Sprint 14). **Concluído.**

---

## Checklist de revisão pré-ativação

- [x] Verificação de propriedade via meta tag configurável (`Ads:VerificationEnabled`)
- [ ] Conta AdSense aprovada
- [x] Política de privacidade completa (cookies/ads) — Sprint 14
- [x] Páginas institucionais revisadas — Sprint 14
- [x] Página `/como-calculamos` — Sprint 14
- [x] Política editorial indexável + autoria visível — Sprint 83
- [x] Nenhum placeholder ou espaço reservado com anúncios desligados — Sprint 86
- [x] Nenhuma métrica, avaliação ou prova social sem comprovação na home — Sprint 86
- [x] Consentimento versionado com quatro categorias e rejeição real — Sprint 90
- [x] Script AdSense bloqueado até consentimento de Publicidade — Sprint 90
- [ ] Nenhum texto incentivando clique
- [ ] Slots com espaçamento validado em desktop e mobile
- [x] Core Web Vitals — cache, defer, slots com altura fixa (Sprint 12)
- [x] Publisher ID via variável de ambiente (não hardcoded) — `Ads:PublisherId` Sprint 14
- [x] Rollback documentado (desabilitar slot por config) — `Ads:Enabled=false` Sprint 14

---

## Integração futura (pós-aprovação)

1. Para verificação: configurar `Ads:VerificationEnabled=true`, `Ads:PublisherId` e manter `Ads:Enabled=false`; somente a meta tag será renderizada.
2. Após aprovação: configurar `Ads:Enabled=true` e IDs reais dos slots.
3. O JavaScript externo e os slots permanecem bloqueados até consentimento de Publicidade.
4. Manter CSP atualizada para domínios Google Ads.
5. Monitorar relatório de políticas no painel AdSense.

---

## Responsável por agente

| Agente | Foco |
|--------|------|
| AdSense Compliance | Este documento + revisão de layout |
| Frontend | Espaçamento, mobile, CLS |
| SEO/Content | Conteúdo suficiente por página |
| Security | Privacidade, cookies, CSP |
