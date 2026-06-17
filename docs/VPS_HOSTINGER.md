# VPS Hostinger — só Meu Valor Líquido

Roteiro para **substituir o Turnizio** e publicar **meuvalorliquido.com** na VPS `177.7.54.131` (Ubuntu).

## Antes de começar

1. **DNS** (painel Hostinger → Domínios → meuvalorliquido.com):
   - Registro **A** `@` → `177.7.54.131`
   - Registro **A** `www` → `177.7.54.131`
   - Aguarde propagação (5 min a 48 h).

2. **SSH** na VPS (senha ou chave do painel Hostinger → VPS → SSH).

3. **E-mail SMTP** (contato/newsletter): crie `noreply@meuvalorliquido.com` no Hostinger Email ou use SMTP do provedor.

---

## Passo 1 — Limpar Turnizio e liberar disco

```bash
# Copie o script do repo ou cole na VPS
sudo bash 01-cleanup-turnizio.sh
```

Isso para containers Docker antigos e roda `docker system prune`. Confirme com `y`.

Opcional — remover pasta antiga do Turnizio:

```bash
sudo rm -rf /var/www/turnizio   # ajuste o caminho se for outro
```

Verifique disco: `df -h /` (ideal ficar abaixo de ~70%).

---

## Passo 2 — Clonar e configurar o app

```bash
sudo mkdir -p /var/www
cd /var/www
sudo git clone https://github.com/matteusoberdan95-sys/meu-valor-liquido.git
cd meu-valor-liquido
sudo cp .env.prod.example .env.prod
sudo nano .env.prod
```

Preencha **obrigatório**:

| Variável | Exemplo |
|----------|---------|
| `POSTGRES_PASSWORD` | senha forte aleatória |
| `SITE_BASE_URL` | `https://meuvalorliquido.com` |
| `MAIL_HOST` | `smtp.hostinger.com` |
| `MAIL_PORT` | `587` |
| `MAIL_USER` | `noreply@meuvalorliquido.com` |
| `MAIL_PASSWORD` | senha do e-mail |
| `MAIL_FROM_ADDRESS` | `noreply@meuvalorliquido.com` |
| `ADS_ENABLED` | `false` (até AdSense aprovar) |

Subir:

```bash
sudo docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
curl -sS http://127.0.0.1:8080/health
```

Deve retornar `Healthy`.

---

## Passo 3 — HTTPS (nginx + Let's Encrypt)

```bash
cd /var/www/meu-valor-liquido/scripts/vps
sudo DOMAIN=meuvalorliquido.com EMAIL=seu-email@gmail.com bash 03-nginx-https.sh
```

Ou use o exemplo manual: `infra/nginx/meu-valor-liquido.conf.example`.

---

## Passo 4 — Smoke test público

- https://meuvalorliquido.com/
- https://meuvalorliquido.com/calculadoras/salario-liquido
- https://meuvalorliquido.com/blog
- https://meuvalorliquido.com/sitemap.xml
- https://meuvalorliquido.com/health
- Formulário de contato

---

## Atualizar depois (novo deploy)

```bash
cd /var/www/meu-valor-liquido
sudo git pull origin main
sudo docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build
```

---

## Turnizio / turnizio.com

- **Não precisa** rodar nada na VPS para o Turnizio agora.
- No painel Hostinger você pode deixar `turnizio.com` sem apontar para esta VPS ou apontar para parking.

---

## AdSense (depois do site estável)

1. Site online com HTTPS por algumas semanas.
2. Solicitar em https://www.google.com/adsense/
3. Após aprovação: `ADS_ENABLED=true` + Publisher ID no `.env.prod` e rebuild (Sprint 20).

Ver: `docs/DEPLOY.md`, `docs/ADSENSE_COMPLIANCE.md`.

---

## Problemas comuns

| Sintoma | Solução |
|---------|---------|
| `502 Bad Gateway` | `docker compose ps` — webapp subiu? Porta 8080 livre? |
| Certificado SSL falha | DNS propagou? Porta 80 aberta no firewall Hostinger? |
| Disco cheio | `docker system prune -af` + remover logs antigos |
| E-mail não envia | Testar credenciais SMTP Hostinger (587 + TLS) |
