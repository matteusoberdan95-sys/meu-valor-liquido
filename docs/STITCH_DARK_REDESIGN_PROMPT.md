# Prompt Google Stitch — Redesign Dark (todas as telas)

Copie o bloco abaixo integralmente no Google Stitch.

---

```
# Projeto: Meu Valor Líquido — Redesign UI/UX DARK completo (mobile + desktop)

## Contexto do produto
Redesenhe **todas as telas** de **Meu Valor Líquido** (meuvalorliquido.com.br), plataforma brasileira **gratuita, mobile-first e educativa** sobre salário, trabalho CLT, impostos e finanças pessoais.

**Tagline:** "Entenda quanto você recebe, quanto desconta e quanto sobra."

**Público:** trabalhadores CLT, PJ/MEI, quem negocia proposta salarial, estudantes e curiosos sobre holerite — Brasil, pt-BR, moeda R$.

**Tom:** claro, confiável, preciso — não parecer banco tradicional nem fintech agressiva. Ferramenta pública de **referência**: transparência, metodologia visível, números em destaque.

**Stack real:** ASP.NET Razor Pages. O dev portará seus mocks para HTML/CSS com classes `valora-*`. Entregue `code.html` por tela quando possível + `screen.png` + `DESIGN.md`.

---

## OBRIGATÓRIO: criar TODAS as telas em DOIS viewports

Para **cada tela** listada neste documento, gere:

| Viewport | Largura | Nome da pasta/arquivo |
|----------|---------|------------------------|
| **Mobile** | 390px (iPhone 14) | `*_mobile/` |
| **Desktop** | 1280px (mínimo) | `*_desktop/` |
| **Desktop wide** (opcional) | 1440px | `*_desktop_wide/` — só home e central calculadoras |

**Não pule nenhuma tela.** Se duas telas compartilham o mesmo layout, entregue o **template** + variações anotadas (ex.: formulário rescisão vs. INSS).

### Estados obrigatórios (por tipo de tela)

**Calculadoras:** vazio | preenchido | erro validação | resultado calculado | accordion "Ajustar detalhes" aberto | toggle "Extrato" / "Explicação simples"

**Formulários (contato, newsletter):** vazio | sucesso | erro

**Blog:** hub com posts | artigo longo com scroll | related no rodapé

**Meu painel:** empty state | lista com 3 itens salvos

**FAQ:** categoria expandida | busca com resultados

**Erro 404/500:** página de erro amigável

---

## Direção visual — DARK PREMIUM (obrigatório)

**NÃO use fundo branco como base.** Evite o visual genérico "site de calculadora branco + azul".

### Paleta
- **Background:** `#0A0A0B` ou `#121214`
- **Surface cards:** `#1C1C1F`, `#222226`, `#2A2A2E`
- **Primária (marca):** teal `#00685F` / `#00A896` — botões, links ativos, glow sutil
- **Secundária:** `#4B8BFF` / `#6A98FF`
- **Texto:** `#F4F4F5` (primário), `#A1A1AA` (muted)
- **Líquido / sucesso:** `#34D399` com glow leve
- **Aviso:** âmbar `#F59E0B` em fundo `#2A2210`
- **Bordas:** `rgba(255,255,255,0.08)`
- **Badges:** Trabalhista `#3B82F6` | Fiscal `#A855F7` | Financeiro `#14B8A6`

### Estética
Inspiração: Linear, Stripe dark, Vercel — **não** gov.br branco, **não** Bootstrap template.
- Header glass (blur + borda translúcida)
- Tipografia: Inter ou Plus Jakarta Sans; valores com `tabular-nums`
- Ícones: Material Symbols Outlined
- Sombras: glow teal suave em CTAs e painel de resultado
- Mobile: bottom nav fixa (Início | Cálculos | Ajuda | Perfil)

**Tema padrão do site = DARK.** Versão clara só opcional para PDF/impressão.

---

## Princípios de UX
1. Formulário em 2 camadas: essencial → accordion "Ajustar detalhes"
2. Resultado: Verbas → Descontos → **Líquido** (grande) → detalhes expansíveis
3. Wizard PJ×CLT em 3 passos
4. Tooltips (i) nos campos críticos
5. Desktop: form esquerda + painel resultado sticky direita
6. Slots AdSense com label "Espaço publicitário" (discreto, sem confundir com conteúdo)

---

## INVENTÁRIO COMPLETO DE TELAS (todas mobile + desktop)

### A. Shell global (aplicar em todas)
- [ ] Header glass + logo (ícone + "Meu Valor Líquido")
- [ ] Bottom navigation mobile
- [ ] Footer (links institucionais, disclaimer curto)
- [ ] Cookie consent bar
- [ ] Breadcrumb (desktop)

---

### B. Navegação principal

| ID | Tela | Rota | Mobile | Desktop |
|----|------|------|--------|---------|
| B1 | **Home** | `/` | ✓ | ✓ |
| B2 | **Central de calculadoras** | `/calculadoras` | ✓ | ✓ |
| B3 | **Mapa do site** | `/mapa-do-site` | ✓ | ✓ |

---

### C. Calculadoras — 17 ferramentas (cada uma mobile + desktop)

Use **3 templates de layout** e aplique a todas (mock individual ou template + screenshot por slug):

**Template C1 — Calculadora padrão** (form + painel resultado lado a lado no desktop)

| Slug | Nome | Categoria |
|------|------|-----------|
| C1.1 | `salario-liquido` | Salário líquido | Trabalhista |
| C1.2 | `salario-bruto-necessario` | Salário bruto necessário | Trabalhista |
| C1.3 | `proposta-salarial` | Proposta salarial | Trabalhista |
| C1.4 | `ferias` | Férias | Trabalhista |
| C1.5 | `decimo-terceiro` | Décimo terceiro | Trabalhista |
| C1.6 | `hora-extra` | Hora extra | Trabalhista |
| C1.7 | `fgts` | FGTS | Trabalhista |
| C1.8 | `custo-funcionario` | Custo de funcionário | Trabalhista |
| C1.9 | `conversor-salario` | Conversor de salário | Trabalhista |
| C1.10 | `inss` | INSS | Fiscal |
| C1.11 | `irrf` | IRRF | Fiscal |
| C1.12 | `simulador-mei` | Simulador MEI | Fiscal |
| C1.13 | `juros-compostos` | Juros compostos | Financeiro |
| C1.14 | `financiamento` | Financiamento (Price/SAC) | Financeiro |
| C1.15 | `multa-atraso` | Multa de atraso | Financeiro |

**Template C2 — Rescisão CLT** (form longo em seções + resultado agrupado Verbas/Descontos/FGTS)

| Slug | Nome |
|------|------|
| C2.1 | `rescisao-clt` |

**Template C3 — Comparador PJ×CLT** (wizard 3 passos + veredito visual)

| Slug | Nome |
|------|------|
| C3.1 | `pj-vs-clt` |

**Por calculadora, entregue:** mobile 390px + desktop 1280px + estado com resultado preenchido (ex.: salário R$ 3.000).

---

### D. SEO / programáticas

| ID | Tela | Rota | Mobile | Desktop |
|----|------|------|--------|---------|
| D1 | Hub salário líquido | `/salario-liquido` | ✓ | ✓ |
| D2 | Faixa salarial (ex. R$ 3.000) | `/salario-liquido/3000` | ✓ | ✓ |
| D3 | Hub CLT×PJ SEO | `/clt-pj` | ✓ | ✓ |
| D4 | Página equivalência (ex. 5000 CLT → PJ) | `/clt-pj/5000-clt-equivale-a-quanto-pj` | ✓ | ✓ |

---

### E. Conteúdo e ajuda

| ID | Tela | Rota | Mobile | Desktop |
|----|------|------|--------|---------|
| E1 | **FAQ / Dúvidas hub** | `/duvidas` | ✓ | ✓ |
| E2 | **FAQ detalhe** (ex. INSS 2026) | `/duvidas/{slug}` | ✓ | ✓ |
| E3 | **Blog hub** | `/blog` | ✓ | ✓ |
| E4 | **Artigo** (ex. PJ ou CLT) | `/blog/{slug}` | ✓ | ✓ |
| E5 | **Como calculamos** (metodologia + tabelas INSS/IRRF 2026) | `/como-calculamos` | ✓ | ✓ |

---

### F. Engajamento e utilitários

| ID | Tela | Rota | Mobile | Desktop |
|----|------|------|--------|---------|
| F1 | **Meu painel** (histórico local) | `/meu-painel` | ✓ | ✓ |
| F2 | **Newsletter** | `/newsletter` | ✓ | ✓ |
| F3 | **Contato** | `/contato` | ✓ | ✓ |
| F4 | **Widget / embed** | `/widget` + embed `?embed=1` | ✓ | ✓ (iframe mínimo) |

---

### G. Institucionais e legal

| ID | Tela | Rota | Mobile | Desktop |
|----|------|------|--------|---------|
| G1 | **Sobre** | `/sobre` | ✓ | ✓ |
| G2 | **Aviso legal** | `/aviso-legal` | ✓ | ✓ |
| G3 | **Política de privacidade** | `/politica-de-privacidade` | ✓ | ✓ |
| G4 | **Termos de uso** | `/termos-de-uso` | ✓ | ✓ |

---

### H. Sistema

| ID | Tela | Mobile | Desktop |
|----|------|--------|---------|
| H1 | **Página de erro** (404) | ✓ | ✓ |
| H2 | **Página de erro** (500) | ✓ | ✓ |

---

## Componentes do design system (DESIGN.md)

Documente tokens e componentes reutilizáveis:

**Cores:** background, surface-1/2/3, primary, on-primary, text-primary, text-muted, border-subtle, success, warning, category-*

**Componentes:**
- Input com prefixo R$ / máscara
- Radio group / choice chips
- Accordion "Ajustar detalhes"
- Botão primário (teal glow) + ghost
- Card bento calculadora
- Painel resultado + toggle Extrato | Explicação
- Badge categoria
- Ad slot placeholder
- Share bar (WhatsApp, link, PDF)
- Tabela INSS/IRRF (metodologia)
- Blog card 16:9
- FAQ accordion
- Wizard steps (PJ×CLT)
- Empty state (painel)
- Cookie consent

---

## Logo e marca
- Logo texto + ícone (carteira / gota / gráfico) — legível em 32px favicon **fundo escuro**
- Favicon + apple-touch-icon conceito
- OG image 1200×630 dark com tagline

---

## Acessibilidade
- Contraste WCAG AA mínimo em texto e botões
- Focus ring visível (teal)
- Touch targets ≥ 44px mobile
- Reservar altura para ad slots (evitar CLS)

---

## Ordem de geração sugerida (Stitch)

**Fase 1 — Fundação**
1. DESIGN.md + paleta dark
2. Shell (header, bottom nav, footer)
3. Home mobile + desktop

**Fase 2 — Core produto**
4. Central calculadoras mobile + desktop
5. Template calculadora padrão (salário líquido) mobile + desktop + resultado
6. Rescisão CLT mobile + desktop
7. PJ×CLT wizard mobile + desktop

**Fase 3 — Conteúdo**
8. FAQ hub + detalhe
9. Blog hub + artigo
10. Como calculamos

**Fase 4 — Restante**
11. Demais 14 calculadoras (aplicar template C1 com variações de campo)
12. Páginas SEO (faixas salariais, CLT-PJ)
13. Meu painel, newsletter, contato, widget
14. Institucionais + erros + mapa do site

---

## Entregáveis finais (checklist)

- [ ] `DESIGN.md` completo
- [ ] **Todas** as telas da seção B a H em **mobile (390px)** e **desktop (1280px)**
- [ ] `code.html` exportável por tela (Tailwind ou CSS inline documentado)
- [ ] `screen.png` por tela e viewport
- [ ] Estados críticos (resultado calculado, empty, erro)
- [ ] Logo + favicon + OG dark
- [ ] Nenhuma dependência de imagem externa (sem URLs Google CDN)

---

## Resumo em uma frase
> Redesenhar **100% das telas** do Meu Valor Líquido em **tema escuro premium** (preto/cinza carvão + teal), **mobile 390px e desktop 1280px**, com calculadoras em camadas, resultado tipo holerite moderno e visual de **referência** — não mais site branco genérico.

Comece pela Fase 1 e confirme o inventário antes de avançar. Não omita telas institucionais, SEO, widget nem estados de erro.
```

---

Usar com: [docs/ui-ux-stitch.md](ui-ux-stitch.md), [STITCH_FIDELITY_PLAN.md](STITCH_FIDELITY_PLAN.md).
