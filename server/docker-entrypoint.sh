#!/bin/sh
set -e

if [ "$SEED_DB" = "true" ]; then
  echo "Seeding database..."
  node seed.js
fi

exec node index.js
