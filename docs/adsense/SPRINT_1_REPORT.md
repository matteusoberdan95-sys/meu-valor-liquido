# Relatório da Sprint 1 — confiança e placeholders

**Data:** 17/07/2026  
**Branch:** `fix/adsense-sprint-1`  
**Referência no roadmap do repositório:** Sprint 86  
**Escopo:** remover sinais de baixa confiança e publicidade visual antes da aprovação.

## Entregas

- Removidos da home o contador `+250k`, estrelas, avatares e selo “Mais usada”.
- Removida a promoção incompleta “ML Prime”.
- Removidos selos de popularidade sem telemetria no catálogo de calculadoras.
- `IA 2080`, “alta precisão”, “insights profundos” e alegações equivalentes foram substituídos por descrições verificáveis de estimativas, metodologia e fontes.
- Home, central de dúvidas e assistente não possuem mais placeholders estáticos.
- `ConfigurableAdSlotProvider` retorna coleção vazia quando anúncios estão desligados.
- Slots ativos sem ID configurado são omitidos, sem markup nem altura vazia.
- `_AdSlot` só renderiza anúncio real quando publicidade, publisher e ID estão configurados.
- Política de Privacidade e documentação interna foram alinhadas ao comportamento real.
- Testes passaram a impedir a reintrodução das métricas, avaliações, linguagem e placeholders removidos.

## Critérios de aceite

- [x] Nenhuma métrica inventada ou sem comprovação.
- [x] Nenhuma avaliação, depoimento ou avatar usado como prova social.
- [x] Nenhum placeholder de anúncio visível.
- [x] Nenhum espaço vazio reservado com `Ads:Enabled=false`.
- [x] Nenhuma promoção de recurso “ML Prime” inexistente.
- [x] Linguagem de precisão moderada e vinculada à metodologia.
- [x] Layout desktop da home recomposto após remoção do card promocional.
- [x] Política e documentação correspondem ao comportamento.
- [x] Build e testes aprovados.

## Validação

```text
dotnet build MeuValorLiquido.slnx --no-restore
Compilação com êxito: 0 avisos, 0 erros.

dotnet test MeuValorLiquido.slnx --no-build --no-restore
Core: 5 aprovados
Calculators: 243 aprovados, 1 teste gerador ignorado
Integration: 1 aprovado
Playwright: 11 aprovados
WebApp: 557 aprovados
Total: 817 aprovados, 0 falhas, 1 ignorado
```

Uma execução intermediária dos testes WebApp encontrou duas expectativas antigas de texto. Os testes foram alinhados ao novo conteúdo e a suíte completa posterior passou.

## Fora do escopo

- Fórmulas e tabelas fiscais.
- Conteúdo editorial completo das calculadoras.
- Indexação, canonical, sitemap e redirects.
- CMP certificada, validade/revogação avançada de consentimento.
- Ativação do AdSense ou mudança do publisher.
- Configuração dos IDs de slots no deploy.

## Riscos restantes

- A infraestrutura de anúncios continua desligada e ainda precisa da Sprint de compliance antes da ativação.
- O deploy ainda não injeta os dois IDs de slots.
- O consentimento customizado ainda precisa de revisão para tráfego regulado pelo Google fora do Brasil.
- Calculadoras com conteúdo editorial genérico permanecem como principal risco de baixo valor.
- Achados técnicos da linha de base, como soft 404 e páginas utilitárias indexáveis, continuam pendentes nas sprints correspondentes.

## Próxima sprint recomendada

Sprint 2 do plano de aprovação AdSense, registrada como Sprint 87 no repositório: conteúdo mínimo completo e específico das calculadoras prioritárias.
