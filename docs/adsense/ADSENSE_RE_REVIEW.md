# Pedir revisão AdSense — após correção de conteúdo de baixo valor

**Não marcar “Confirmo que corrigi” até o deploy estar no ar há 7–14 dias.**

## O que foi corrigido (resumo para o painel)

1. Conteúdo editorial original e específico nas **19 calculadoras** ativas (funcionamento, exemplos do motor, fontes, FAQs, revisão).
2. **Dois artigos novos** educativos (≥850 palavras): VT/VR no orçamento e salário mínimo no holerite.
3. Redução de URLs programáticas repetitivas no índice: sitemap focado nas faixas de maior demanda; demais faixas acessíveis com `noindex,follow`.
4. Anúncios **continuam desligados** até aprovação.

## Texto sugerido (opcional, se houver campo livre)

> Atualizamos o site para elevar a qualidade editorial: todas as calculadoras públicas passaram a ter conteúdo original detalhado com fontes oficiais e exemplos calculados pelo mesmo motor; publicamos novos artigos educativos no blog; e reduzimos a indexação de páginas programáticas de menor demanda para evitar conteúdo superficial em escala. A verificação do site permanece ativa e os anúncios ainda não estão habilitados.

## Checklist operacional

1. [ ] Merge/push de `main` com Sprint 98
2. [ ] Deploy VPS (`docs/DEPLOY.md` / checklist final)
3. [ ] Smoke das URLs do checklist
4. [ ] Aguardar **7–14 dias** com conteúdo crawlável
5. [ ] No AdSense: marcar “Confirmo que corrigi os problemas” → **Pedir revisão**
6. [ ] Se aprovar: executar **Sprint 51** (ativar ads com consentimento)
7. [ ] Se rejeitar de novo: não reenviar imediatamente; ampliar conteúdo e UX antes

## O que NÃO fazer

- Ativar `Ads:Enabled=true` antes da aprovação
- Pedir revisão no mesmo dia do deploy
- Inventar métricas, depoimentos ou conteúdo genérico em massa
