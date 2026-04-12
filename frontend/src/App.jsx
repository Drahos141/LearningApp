import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import CategoryPage from './pages/CategoryPage';
import LessonPage from './pages/LessonPage';
import QuizPage from './pages/QuizPage';
import MiniGamePage from './pages/MiniGamePage';
import GamesHub from './pages/GamesHub';
import GamePlay from './pages/GamePlay';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/category/:id" element={<CategoryPage />} />
        <Route path="/lesson/:id" element={<LessonPage />} />
        <Route path="/lesson/:id/quiz" element={<QuizPage />} />
        <Route path="/lesson/:id/minigame" element={<MiniGamePage />} />
        <Route path="/games" element={<GamesHub />} />
        <Route path="/games/:slug" element={<GamePlay />} />
      </Routes>
    </BrowserRouter>
  );
}
