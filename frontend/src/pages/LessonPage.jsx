import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getLesson } from '../api/api';

function renderContent(content) {
  return content.split('\n\n').map((para, i) => {
    if (para.startsWith('* ')) {
      const lines = para.split('\n').filter(Boolean);
      return (
        <div key={i} style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
          {lines.map((line, j) => {
            const text = line.replace(/^\* /, '');
            return (
              <div key={j} className="lesson-point">
                <span className="lesson-point-star">★</span>
                <span>{text}</span>
              </div>
            );
          })}
        </div>
      );
    }
    return <p key={i} className="lesson-paragraph">{para}</p>;
  });
}

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
  const [depthLevel, setDepthLevel] = useState(0);

  useEffect(() => {
    setDepthLevel(0);
    getLesson(id).then(setLesson).catch(() => {}).finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (!lesson) return <div className="page-center error-msg">Lesson not found.</div>;

  const depths = lesson.depths || [];
  const maxDepth = depths.length;

  return (
    <div className="page lesson-page">
      <div className="lesson-header">
        <div className="nav-row">
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate(-1)}>← Back</button>
        </div>
        <h1 className="lesson-heading">{lesson.title}</h1>
      </div>

      <div className="lesson-content">
        {renderContent(lesson.content)}
      </div>

      {maxDepth > 0 && (
        <div className="depth-section">
          <div className="depth-header">
            <h3>🔬 Explore Deeper</h3>
            {depthLevel > 0 && (
              <span className="depth-level-badge">Depth {depthLevel} / {maxDepth}</span>
            )}
          </div>

          {depthLevel === 0 ? (
            <div className="depth-actions">
              <button
                className="depth-btn depth-btn-primary"
                onClick={() => setDepthLevel(1)}
              >
                🔍 Go Deeper — Level 1
              </button>
            </div>
          ) : (
            <>
              <div className="depth-content">
                {renderContent(depths[depthLevel - 1])}
              </div>
              <div className="depth-actions">
                <button
                  className="depth-btn"
                  onClick={() => setDepthLevel(d => d - 1)}
                  disabled={depthLevel <= 1}
                >
                  ← Shallower
                </button>
                <button
                  className="depth-btn depth-btn-primary"
                  onClick={() => setDepthLevel(d => d + 1)}
                  disabled={depthLevel >= maxDepth}
                >
                  {depthLevel >= maxDepth ? '🏆 Maximum Depth Reached' : `🔍 Go Deeper — Level ${depthLevel + 1}`}
                </button>
                <button
                  className="depth-btn"
                  onClick={() => setDepthLevel(0)}
                >
                  ✕ Close
                </button>
              </div>
            </>
          )}
        </div>
      )}

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

      <div className="lesson-actions" style={{ marginTop: '2rem' }}>
        <button className="action-btn quiz-btn" onClick={() => navigate(`/lesson/${id}/quiz`)}>
          📝 Take Quiz
        </button>
        <button className="action-btn game-btn" onClick={() => navigate('/lesson/' + id + '/minigame')}>
          🃏 Flashcards
        </button>
      </div>
    </div>
  );
}
