# Relatório da Sprint 5 — privacidade, cookies e AdSense

**Data:** 17/07/2026  
**Branch:** `feat/adsense-sprint-5`  
**Referência no roadmap do repositório:** Sprint 90

## Objetivo

Preparar o site para publicidade sem carregar scripts, cookies ou medições opcionais antes do consentimento, e alinhar as políticas ao comportamento real.

## Entregas

- Banner com quatro categorias:
  - Essenciais (sempre ativos);
  - Analytics;
  - Personalização;
  - Publicidade.
- Nenhuma categoria opcional marcada por padrão.
- Consentimento versionado:
  - `version: 2`;
  - `policyVersion: 2026-07-17`;
  - `updatedAt` ISO.
- `Aceitar todos`, `Rejeitar todos`, `Personalizar` e reabertura via Política de Cookies.
- Script `adsbygoogle.js` carregado somente após Publicidade.
- Verificação da conta via meta tag `google-adsense-account` (sem script externo).
- Slots com `data-ad-consent-required` ocultos até Publicidade.
- Métricas de interação e `localStorage` de painel/checklist respeitam as categorias.
- Políticas de Privacidade, Cookies, Termos e Aviso Legal atualizadas.
- `ads.txt` permanece com publisher concreto já existente.

## Decisões técnicas

- Removido `_AdSenseScript.cshtml`, que injetava o script de verificação sem consentimento.
- Criado `_AdSenseVerification.cshtml` para meta tag no `<head>`.
- CSP de anúncios só é ampliada quando `Ads:Enabled=true`.
- Mudança de versão da política invalida escolhas antigas e reexibe o banner.
- Revogar Personalização limpa `mvl-local-panel-v1` e `mvl-rescisao-checklist-v1`.

## Validação

```text
dotnet build MeuValorLiquido.slnx -c Release --no-restore
0 avisos, 0 erros

dotnet test MeuValorLiquido.slnx -c Release --no-build --no-restore
Core: 5 aprovados
Integration: 1 aprovado
Calculators: 243 aprovados, 1 ignorado
Playwright: 11 aprovados
WebApp: 618 aprovados
Total: 878 aprovados, 1 ignorado, 0 falhas
```

## Critérios de aceite

- [x] Consentimento testado em navegador limpo (Playwright).
- [x] Rejeição de cookies respeitada.
- [x] Política corresponde ao comportamento real.
- [x] Nenhum script publicitário no HTML inicial sem consentimento.
- [x] `ads.txt` sem ID inventado.
- [x] Build sem avisos e suíte completa verde.

## Riscos restantes

- Anúncios reais ainda não estão ativos; a validação completa de CLS/espaçamento com slots live fica para pós-aprovação.
- Cookies de terceiros já gravados pelo navegador em visitas anteriores precisam ser limpos pelo usuário ao revogar Publicidade.
- A ativação de `Ads:Enabled` continua bloqueada até aprovação do Google.

## Próxima sprint recomendada

Sprint 7 do plano AdSense, registrada como Sprint 91: validação matemática e testes das calculadoras prioritárias.
