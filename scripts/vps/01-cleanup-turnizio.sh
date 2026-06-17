#!/usr/bin/env bash
# Remove stacks antigas (ex.: Turnizio) e libera disco na VPS Hostinger.
# Uso na VPS: sudo bash 01-cleanup-turnizio.sh
set -euo pipefail

echo "==> Containers em execução"
docker ps --format 'table {{.Names}}\t{{.Image}}\t{{.Ports}}' || true

read -r -p "Parar e remover TODOS os containers Docker? [y/N] " confirm
if [[ "${confirm,,}" != "y" ]]; then
  echo "Cancelado."
  exit 0
fi

docker stop $(docker ps -aq) 2>/dev/null || true
docker rm $(docker ps -aq) 2>/dev/null || true

echo "==> Compose projects (se existirem)"
for dir in /var/www/turnizio /root/turnizio /home/*/turnizio; do
  if [[ -f "$dir/docker-compose.yml" ]] || [[ -f "$dir/docker-compose.yaml" ]]; then
    echo "   docker compose down em $dir"
    (cd "$dir" && docker compose down -v --remove-orphans) 2>/dev/null || true
  fi
done

echo "==> Limpeza de imagens, cache e volumes não usados"
docker system prune -af --volumes

echo "==> Espaço em disco"
df -h /

echo "OK. VPS pronta para deploy do Meu Valor Líquido."
