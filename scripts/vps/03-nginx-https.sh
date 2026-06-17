#!/usr/bin/env bash
# Instala nginx + certbot e publica meuvalorliquido.com na porta 443.
# Uso: sudo DOMAIN=meuvalorliquido.com EMAIL=seu@email.com bash 03-nginx-https.sh
set -euo pipefail

DOMAIN="${DOMAIN:-meuvalorliquido.com}"
EMAIL="${EMAIL:-}"
WEBAPP_PORT="${WEBAPP_PORT:-8080}"

if [[ -z "$EMAIL" ]]; then
  echo "Defina EMAIL= para o Let's Encrypt."
  exit 1
fi

export DEBIAN_FRONTEND=noninteractive
apt-get update -qq
apt-get install -y nginx certbot python3-certbot-nginx

cat > "/etc/nginx/sites-available/${DOMAIN}" <<EOF
upstream meu_valor_liquido {
    server 127.0.0.1:${WEBAPP_PORT};
    keepalive 32;
}

server {
    listen 80;
    server_name ${DOMAIN} www.${DOMAIN};
    location / {
        proxy_pass http://meu_valor_liquido;
        proxy_http_version 1.1;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
        proxy_set_header Connection "";
    }
}
EOF

ln -sf "/etc/nginx/sites-available/${DOMAIN}" "/etc/nginx/sites-enabled/${DOMAIN}"
rm -f /etc/nginx/sites-enabled/default 2>/dev/null || true
nginx -t
systemctl reload nginx

certbot --nginx -d "${DOMAIN}" -d "www.${DOMAIN}" --non-interactive --agree-tos -m "${EMAIL}" --redirect

systemctl enable nginx
echo "OK: https://${DOMAIN}"
