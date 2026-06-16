# Security Checklist

- Validar inputs.
- Usar CSRF em formulários.
- Manter CORS restrito se API for exposta.
- Aplicar rate limiting em contato/newsletter.
- Usar security headers.
- Não logar dados sensíveis.
- Não salvar cálculos pessoais no MVP.
- Não versionar secrets.
- Exibir aviso legal nas calculadoras.
- Não prometer aconselhamento jurídico, contábil ou financeiro.

## Política de logging (Sprint 0)

**Não registrar em logs:**

- E-mail completo (usar máscara: `u***@dominio.com` — ver `SmtpEmailSender.MaskEmail`)
- Salário, valores de calculadora ou inputs de formulário de simulação
- CPF, RG, telefone ou endereço
- Corpo completo de mensagens de contato
- IP completo em logs de aplicação (rate limiter usa IP internamente; não persistir em log)

**Permitido:**

- Slug da calculadora, status HTTP, duração de request
- Erros técnicos sem payload do usuário
- Host/porta SMTP (sem credenciais)

**Produção:** `appsettings.Production.json` não versionado; secrets via variáveis de ambiente ou secret manager.

