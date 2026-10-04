import { Fragment } from 'react';
import { BrowserRouter, Routes, Route, useParams } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import CategoryPage from './pages/CategoryPage';
import LessonPage from './pages/LessonPage';
import QuizPage from './pages/QuizPage';
import MiniGamePage from './pages/MiniGamePage';
import GamesHub from './pages/GamesHub';
import GamePlay from './pages/GamePlay';

// Remount the page when its route param changes (e.g. lesson 3 → lesson 4),
// so loading/progress state starts fresh instead of leaking from the previous item.
function KeyedBy({ param, children }) {
  const params = useParams();
  return <Fragment key={params[param]}>{children}</Fragment>;
}

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/category/:id" element={<KeyedBy param="id"><CategoryPage /></KeyedBy>} />
        <Route path="/lesson/:id" element={<KeyedBy param="id"><LessonPage /></KeyedBy>} />
        <Route path="/lesson/:id/quiz" element={<KeyedBy param="id"><QuizPage /></KeyedBy>} />
        <Route path="/lesson/:id/minigame" element={<KeyedBy param="id"><MiniGamePage /></KeyedBy>} />
        <Route path="/games" element={<GamesHub />} />
        <Route path="/games/:slug" element={<KeyedBy param="slug"><GamePlay /></KeyedBy>} />
      </Routes>
    </BrowserRouter>
  );
}
