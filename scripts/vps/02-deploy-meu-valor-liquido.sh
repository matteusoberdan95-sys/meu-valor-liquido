#!/usr/bin/env bash
# Deploy Meu Valor Líquido com Docker Compose (produção).
# Uso na VPS:
#   export REPO_URL=https://github.com/matteusoberdan95-sys/meu-valor-liquido.git
#   export APP_DIR=/var/www/meu-valor-liquido
#   sudo bash 02-deploy-meu-valor-liquido.sh
set -euo pipefail

REPO_URL="${REPO_URL:-https://github.com/matteusoberdan95-sys/meu-valor-liquido.git}"
APP_DIR="${APP_DIR:-/var/www/meu-valor-liquido}"
BRANCH="${BRANCH:-main}"

if ! command -v docker >/dev/null 2>&1; then
  echo "Docker não encontrado. Instale com: curl -fsSL https://get.docker.com | sh"
  exit 1
fi

if ! docker compose version >/dev/null 2>&1; then
  echo "Docker Compose plugin não encontrado."
  exit 1
fi

mkdir -p "$(dirname "$APP_DIR")"

if [[ ! -d "$APP_DIR/.git" ]]; then
  git clone --branch "$BRANCH" --depth 1 "$REPO_URL" "$APP_DIR"
else
  cd "$APP_DIR"
  git fetch origin "$BRANCH"
  git reset --hard "origin/$BRANCH"
fi

cd "$APP_DIR"

if [[ ! -f .env.prod ]]; then
  cp .env.prod.example .env.prod
  echo ""
  echo "Edite $APP_DIR/.env.prod antes de continuar:"
  echo "  - POSTGRES_PASSWORD"
  echo "  - SITE_BASE_URL=https://meuvalorliquido.com"
  echo "  - MAIL_HOST (ex.: smtp.hostinger.com se usar e-mail Hostinger)"
  echo ""
  exit 1
fi

docker compose -f docker-compose.prod.yml --env-file .env.prod up -d --build

echo ""
echo "==> Status"
docker compose -f docker-compose.prod.yml --env-file .env.prod ps

echo ""
echo "Smoke local: curl -sS http://127.0.0.1:8080/health"
curl -sf http://127.0.0.1:8080/health && echo "" || echo "Aguarde alguns segundos e teste de novo."

echo ""
echo "Próximo passo: configure nginx + HTTPS (scripts/vps/03-nginx-https.sh ou docs/VPS_HOSTINGER.md)"
