# 🎓 LearningApp

A full-stack learning platform with a React frontend and C# ASP.NET Core backend.

## Features

- **7 Learning Categories**: IT, Programming, Languages, Networking, AI & ML, Soft Skills, Hardware
- **14 Subcategories** with 3 lessons each (42 lessons total)
- **Quizzes**: 3 multiple-choice questions per lesson with explanations (126 total)
- **Flashcard Mini-Games**: 5 term-definition flashcards per lesson
- Clean dark-mode dashboard with category cards
- Lesson reader, interactive quiz, and flip-card game

## Tech Stack

| Layer    | Technology               |
|----------|--------------------------|
| Frontend | React 19 + Vite + React Router |
| Backend  | C# ASP.NET Core 10 Web API |
| Data     | In-memory seeded data    |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)

### 1. Start the Backend

```bash
cd backend/LearningApp.API
dotnet run
```

The API will be available at **http://localhost:5000**  
Swagger UI: http://localhost:5000/swagger

### 2. Start the Frontend

```bash
cd frontend
npm install
npm run dev
```

The app will be available at **http://localhost:5173**

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | All categories with subcategories |
| GET | `/api/categories/{id}` | Single category |
| GET | `/api/lessons/{id}` | Single lesson |
| GET | `/api/subcategories/{id}/lessons` | Lessons in a subcategory |
| GET | `/api/lessons/{lessonId}/quiz` | Quiz for a lesson |
| GET | `/api/lessons/{lessonId}/minigame` | Flashcard game for a lesson |

## Project Structure

```
LearningApp/
├── backend/
│   └── LearningApp.API/
│       ├── Controllers/     # API controllers
│       ├── Models/          # Data models
│       ├── Services/        # DataService with seeded content
│       └── Program.cs
└── frontend/
    └── src/
        ├── api/             # Axios API client
        ├── pages/           # Dashboard, Category, Lesson, Quiz, MiniGame
        └── index.css        # Global styles
```
