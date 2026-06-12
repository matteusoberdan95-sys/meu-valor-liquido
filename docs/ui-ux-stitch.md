# UI/UX — Design Stitch / Valora

A pasta `stitch_meu_valor_l_quido_ui_ux/` na raiz do repositório contém os protótipos gerados no Google Stitch e o design system **Valora**.

## Estrutura

```
stitch_meu_valor_l_quido_ui_ux/
└── stitch_meu_valor_l_quido_ui_ux/
    ├── valora/DESIGN.md          # Tokens, tipografia e componentes
    ├── home_desktop/             # Home (desktop)
    ├── home_mobile/              # Home (mobile)
    ├── calculadoras_index_*      # Listagem de calculadoras
    ├── sal_rio_l_quido_*         # Calculadora de salário líquido
    ├── blog_mobile/              # Blog
    └── contato_mobile/           # Contato
```

Cada pasta de tela inclui `code.html` (HTML/Tailwind de referência) e `screen.png` (screenshot).

## Implementação no WebApp

O design foi portado para CSS puro em `src/WebApp/wwwroot/css/site.css` (classes `valora-*`), sem depender do Tailwind CDN em produção.

| Stitch / Valora | WebApp |
|-----------------|--------|
| Inter + Material Symbols | `_Layout.cshtml` (Google Fonts) |
| Tokens de cor (`#00685f`, `#f9f9ff`, etc.) | `:root` em `site.css` |
| Header sticky, nav, footer | `_Layout.cshtml` |
| Cards de calculadora + ícones | `Index.cshtml`, `Calculadoras/Index.cshtml` |
| Sidebar de categorias | `Calculadoras/Index.cshtml` |
| Form + extrato (2 colunas) | `Calculadoras/Details.cshtml` |
| Badges por categoria | `_CalculatorCategoryBadge.cshtml` |
| FAQ accordion | `Calculadoras/Details.cshtml` |
| Ad placeholders | `_AdSlot.cshtml` |
| Mapeamento slug → ícone | `Infrastructure/CalculatorUiHelper.cs` |

## Referência rápida (DESIGN.md)

- **Primary:** `#00685f`
- **Background:** `#f9f9ff`
- **Fonte:** Inter
- **Ícones:** Material Symbols Outlined

Ao criar novas telas ou ajustar UX, consulte primeiro os HTMLs em `stitch_meu_valor_l_quido_ui_ux/` e mantenha consistência com as classes `valora-*`.
