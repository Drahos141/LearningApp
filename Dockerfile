# One container: builds the React frontend, then serves it plus the API from Node.
#   docker build -t learningapp . && docker run --rm -p 4000:4000 learningapp
# Content is served from memory; set MONGO_URI to read it from MongoDB instead.

# ---- Build the frontend ----
FROM node:24-alpine AS frontend
WORKDIR /app/frontend
COPY frontend/package.json frontend/package-lock.json ./
RUN npm ci --no-audit --no-fund
COPY frontend/ ./
RUN npm run build

# ---- Runtime ----
FROM node:24-alpine
ENV NODE_ENV=production \
    PORT=4000
WORKDIR /app/server

COPY server/package.json server/package-lock.json ./
RUN npm ci --omit=dev --no-audit --no-fund && npm cache clean --force

COPY server/ ./
COPY --from=frontend /app/frontend/dist /app/frontend/dist

USER node
EXPOSE 4000
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD wget -qO- http://127.0.0.1:4000/api/health || exit 1

CMD ["node", "index.js"]
