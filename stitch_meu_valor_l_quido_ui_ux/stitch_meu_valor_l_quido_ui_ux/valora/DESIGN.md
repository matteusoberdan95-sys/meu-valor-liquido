---
name: Valora
colors:
  surface: '#f9f9ff'
  surface-dim: '#d3daea'
  surface-bright: '#f9f9ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f0f3ff'
  surface-container: '#e7eefe'
  surface-container-high: '#e2e8f8'
  surface-container-highest: '#dce2f3'
  on-surface: '#151c27'
  on-surface-variant: '#3d4947'
  inverse-surface: '#2a313d'
  inverse-on-surface: '#ebf1ff'
  outline: '#6d7a77'
  outline-variant: '#bcc9c6'
  surface-tint: '#006a61'
  primary: '#00685f'
  on-primary: '#ffffff'
  primary-container: '#008378'
  on-primary-container: '#f4fffc'
  inverse-primary: '#6bd8cb'
  secondary: '#555f6f'
  on-secondary: '#ffffff'
  secondary-container: '#d6e0f3'
  on-secondary-container: '#596373'
  tertiary: '#825100'
  on-tertiary: '#ffffff'
  tertiary-container: '#a36700'
  on-tertiary-container: '#fffbff'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#89f5e7'
  primary-fixed-dim: '#6bd8cb'
  on-primary-fixed: '#00201d'
  on-primary-fixed-variant: '#005049'
  secondary-fixed: '#d9e3f6'
  secondary-fixed-dim: '#bdc7d9'
  on-secondary-fixed: '#121c2a'
  on-secondary-fixed-variant: '#3d4756'
  tertiary-fixed: '#ffddb8'
  tertiary-fixed-dim: '#ffb95f'
  on-tertiary-fixed: '#2a1700'
  on-tertiary-fixed-variant: '#653e00'
  background: '#f9f9ff'
  on-background: '#151c27'
  surface-variant: '#dce2f3'
typography:
  h1:
    fontFamily: Inter
    fontSize: 40px
    fontWeight: '700'
    lineHeight: '1.2'
    letterSpacing: -0.02em
  h1-mobile:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '700'
    lineHeight: '1.2'
  h2:
    fontFamily: Inter
    fontSize: 30px
    fontWeight: '600'
    lineHeight: '1.3'
  h3:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: '1.3'
  lead:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '400'
    lineHeight: '1.6'
  body:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.5'
  body-sm:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: '1.5'
  label-caps:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '700'
    lineHeight: '1'
    letterSpacing: 0.05em
  button:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '600'
    lineHeight: '1'
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  base: 8px
  container-max: 1200px
  gutter: 24px
  margin-mobile: 16px
  margin-desktop: 32px
  stack-sm: 12px
  stack-md: 24px
  stack-lg: 48px
---

## Brand & Style
The design system is built on the pillars of **clarity, precision, and public utility**. As a financial and labor calculator for the Brazilian market, the UI must feel authoritative yet accessible—demystifying complex calculations into actionable insights.

The style is **Corporate Modern**, leaning heavily into functional minimalism. It prioritizes data legibility and user confidence. Every element is designed to reduce cognitive load, using generous whitespace and a structured information hierarchy to guide users through multi-step financial forms. The emotional response should be one of "controlled transparency"—users should feel that their data is being handled by a professional, unbiased tool.

## Colors
The palette uses color as a functional signifier rather than mere decoration.
- **Primary (Teal):** Used for growth-oriented actions, success states, and main brand identifiers. It symbolizes the "net value" and financial health.
- **Secondary (Deep Slate):** The anchor for typography and structural elements, ensuring high contrast and readability.
- **Accents (Soft Amber):** Reserved for secondary CTAs and informational highlights. It should never be used for primary actions to avoid confusion with warning states.
- **Background:** A very light gray creates a soft surface that reduces eye strain during long reading sessions or complex data entry.
- **Validation:** Clear Red is utilized strictly for errors and required field omissions, maintaining a high signal-to-noise ratio.

## Typography
This design system utilizes **Inter** for its exceptional legibility and systematic weight distribution. 
- **Hierarchy:** Use H1 and Lead paragraphs to frame the context of each calculator immediately. 
- **Currency Formatting:** Monetary values (R$) should use a medium or semi-bold weight to stand out from surrounding descriptive text. 
- **Accessibility:** Ensure a minimum contrast ratio of 4.5:1 for all body text against the background. Small labels (12px) must only be used for non-critical metadata or secondary labels.

## Layout & Spacing
The layout follows a **Fixed-Fluid hybrid** model. On desktop, content is centered within a 1200px container to maintain optimal line length for readability. On mobile, the system transitions to a fluid 1-column stack with 16px side margins.

- **Grid:** Use a 12-column grid for complex calculators where inputs and results are displayed side-by-side. 
- **Vertical Rhythm:** Maintain a consistent vertical stack (24px between form groups) to ensure the user’s eye can easily track the progression of the calculation.
- **Touch Targets:** All interactive elements (buttons, inputs) must have a minimum height of 48px to comply with accessibility standards.

## Elevation & Depth
Depth is created through **Tonal Layering** and subtle ambient shadows. 
- **Base Surface:** The main background is #F9FAFB.
- **Cards:** All primary content areas (calculators, result summaries) are housed in white (#FFFFFF) cards.
- **Shadows:** Use a single, soft shadow level for cards: `0px 4px 6px -1px rgba(0, 0, 0, 0.1), 0px 2px 4px -1px rgba(0, 0, 0, 0.06)`. This lifts the interactive content above the utility background without appearing "heavy" or dated.
- **Interactive State:** Buttons use a slight vertical shift (1px down) on active state to simulate a physical press.

## Shapes
The design system adopts a **Soft (0.25rem / 4px)** corner radius. This provides a professional, "standardized" feel that resonates with government and financial software while remaining modern.
- **Inputs & Buttons:** 4px radius.
- **Large Cards:** 8px (rounded-lg) for a more approachable containment.
- **Badges:** Fully rounded (pill) to distinguish them clearly from interactive buttons.

## Components
- **Buttons:** 
  - *Primary:* Teal background, white text. 
  - *Secondary:* Amber background, dark slate text (high contrast). 
  - *Outline:* Transparent background, slate border for "Back" or "Cancel" actions.
- **Inputs:** Large, clear labels above the field. Focus state is a 2px teal border. Helper text is displayed below the field in #6B7280.
- **Badges:** 
  - "Trabalhista": Light Blue tint background.
  - "Fiscal": Light Purple tint background.
  - "Financeiro": Light Teal tint background.
- **Alert Boxes:** Yellow background (#FEF3C7) with a 1px amber border for legal disclaimers. Always include a "warning" icon.
- **FAQ Accordions:** Clean lines with a "+" or chevron icon. Only the title is visible initially.
- **Ad Placeholders:** Light gray bordered boxes (#E5E7EB) with centered text "Espaço reservado para anúncio futuro" in 12px uppercase. Fixed heights of 90px, 250px, or 600px depending on the slot.
- **Currency Input:** Must include a fixed "R$" prefix within the input field container to reinforce the Brazilian context.