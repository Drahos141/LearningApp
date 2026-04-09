import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getLesson } from '../api/api';

export default function LessonPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [lesson, setLesson] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getLesson(id)
      .then(setLesson)
      .catch(() => setError('Lesson not found.'))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (error) return <div className="page-center error-msg">{error}</div>;

  const paragraphs = lesson.content.split('\n\n').filter(Boolean);

  return (
    <div className="page lesson-page">
      <div className="lesson-header">
        <button className="back-btn" onClick={() => navigate(-1)}>← Back</button>
        <h1 className="lesson-heading">{lesson.title}</h1>
      </div>

      <div className="lesson-content">
        {paragraphs.map((para, i) => (
          <p key={i} className="lesson-paragraph">{para}</p>
        ))}
      </div>

      <div className="lesson-actions">
        <button
          className="action-btn quiz-btn"
          onClick={() => navigate(`/lesson/${id}/quiz`)}
        >
          📝 Take Quiz
        </button>
        <button
          className="action-btn game-btn"
          onClick={() => navigate(`/lesson/${id}/minigame`)}
        >
          🎴 Play Flashcards
        </button>
      </div>
    </div>
  );
}
