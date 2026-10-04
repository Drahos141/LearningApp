# 🎓 LearningApp

A learning platform with lessons, quizzes, flashcards and brain-training games.
React frontend + Node/Express API, shipped as a single container.

## Quick start (one container)

```bash
docker compose up --build
```

Open **http://localhost:4000**. That's it — no database required.

Without Compose:

```bash
docker build -t learningapp .
docker run --rm -p 4000:4000 learningapp
```

## Features

- **7 categories**: IT, Programming, Mathematics, Science, History, Languages, Psychology
- **25 subcategories, 49 lessons**, each with "Go Deeper" levels, a quiz and flashcards
- **34 brain games**: memory, logic, sequence, word, math, pattern and spatial puzzles

## Tech stack

| Layer    | Technology                                   |
|----------|----------------------------------------------|
| Frontend | React 19, Vite 8, React Router 7             |
| Server   | Node 24, Express — serves the API and the built frontend |
| Data     | Bundled in `server/content.js` and `server/lessons-json/`, served from memory. MongoDB optional. |

## Local development

Prerequisites: [Node.js 22.12+](https://nodejs.org/) (24 recommended).

```bash
# Terminal 1 — API on http://localhost:4000
cd server && npm ci && npm run dev

# Terminal 2 — frontend with hot reload on http://localhost:5175 (proxies /api to :4000)
cd frontend && npm ci && npm run dev
```

Or build and serve everything from one process: `./start.sh`.

### Configuration

| Variable     | Default                | Purpose |
|--------------|------------------------|---------|
| `PORT`       | `4000`                 | Server port |
| `MONGO_URI`  | _(unset)_              | If set, content is loaded from MongoDB instead of the bundled files |
| `SEED_DB`    | _(unset)_              | With `MONGO_URI`, `true` copies the bundled content into MongoDB on startup |
| `STATIC_DIR` | `../frontend/dist`     | Built frontend to serve |
| `API_URL`    | `http://localhost:4000`| (Vite dev server) where to proxy `/api` |

Content is loaded once at startup — restart the server after editing it.

## Adding content

- **Lessons via JSON** (Psychology): drop a file into `server/lessons-json/` — see its [README](server/lessons-json/README.md).
- **Other lessons and games**: edit `server/content.js`. Each game's `slug` must match an entry in `GAME_COMPONENTS` in `frontend/src/pages/GamePlay.jsx`.

## API

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/health` | Health check |
| GET | `/api/categories` | All categories with subcategories and lesson outlines |
| GET | `/api/categories/{id}` | Single category |
| GET | `/api/subcategories/{id}/lessons` | Full lessons in a subcategory |
| GET | `/api/lessons/{id}` | Single lesson |
| GET | `/api/lessons/{id}/quiz` | Quiz for a lesson |
| GET | `/api/lessons/{id}/minigame` | Flashcards for a lesson |
| GET | `/api/games` | All brain games |
| GET | `/api/games/{id or slug}` | Single game |

## Project structure

```
LearningApp/
├── Dockerfile             # Single-container build (frontend + server)
├── docker-compose.yml
├── server/
│   ├── index.js           # Express app: API + static frontend
│   ├── store.js           # In-memory content store (optionally loaded from MongoDB)
│   ├── content.js         # Bundled categories, lessons and games
│   ├── lessons-json/      # JSON-defined lessons
│   ├── routes/            # API routes
│   ├── models/            # Mongoose models (MongoDB mode only)
│   └── seed.js            # Copy bundled content into MongoDB
├── frontend/
│   └── src/
│       ├── api/           # API client
│       ├── pages/         # Dashboard, category, lesson, quiz, flashcards, games
│       ├── games/         # Brain game components
│       └── utils/
└── backend/               # Legacy C# ASP.NET Core API (not used by the frontend)
```
