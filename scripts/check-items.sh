#!/usr/bin/env bash
set -euo pipefail

response=$(curl --fail --silent --show-error --retry 15 --retry-all-errors \
  --retry-delay 2 --max-time 5 http://127.0.0.1:8080/items)

printf 'GET /items: %s\n' "$response"
python3 -c '
import json
import sys

expected = [{"id": 1, "name": "Notebook"}, {"id": 2, "name": "Pen"}]
actual = json.loads(sys.argv[1])
if actual != expected:
    sys.exit(f"Unexpected items: {actual!r}")
' "$response"
echo "Сетевое взаимодействие между контейнерами работает!"
