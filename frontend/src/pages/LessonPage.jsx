import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getLesson } from '../api/api';

function ExpandableSection({ title, icon, content, className }) {
  const [open, setOpen] = useState(false);
  return (
    <div className={`expandable-section ${className || ''}`}>
      <button className="expandable-toggle" onClick={() => setOpen(o => !o)}>
        <span className="expandable-icon">{icon}</span>
        <span className="expandable-title">{title}</span>
        <span className="expandable-chevron">{open ? '▲' : '▼'}</span>
      </button>
      {open && (
        <div className="expandable-body">
          {content.split('\n\n').map((para, i) => (
            <p key={i} className="lesson-paragraph">{para}</p>
          ))}
        </div>
      )}
    </div>
  );
}

export default function LessonPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [lesson, setLesson] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getLesson(id).then(setLesson).catch(() => {}).finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (!lesson) return <div className="page-center error-msg">Lesson not found.</div>;

  return (
    <div className="page lesson-page">
      <div className="lesson-header">
        <button className="back-btn" onClick={() => navigate(-1)}>← Back</button>
        <h1 className="lesson-heading">{lesson.title}</h1>
      </div>

      <div className="lesson-content">
        {lesson.content.split('\n\n').map((para, i) => (
          <p key={i} className="lesson-paragraph">{para}</p>
        ))}
      </div>

      {lesson.additionalInfo && (
        <ExpandableSection
          title="Additional Information"
          icon="📖"
          content={lesson.additionalInfo}
          className="expandable-additional"
        />
      )}

      {lesson.deepDive && (
        <ExpandableSection
          title="Deep Dive"
          icon="🔬"
          content={lesson.deepDive}
          className="expandable-deepdive"
        />
      )}

      <div className="lesson-actions">
        <button className="action-btn quiz-btn" onClick={() => navigate(`/lesson/${id}/quiz`)}>
          📝 Take Quiz
        </button>
        <button className="action-btn game-btn" onClick={() => navigate(`/lesson/${id}/minigame`)}>
          🃏 Flashcards
        </button>
      </div>
    </div>
  );
}
