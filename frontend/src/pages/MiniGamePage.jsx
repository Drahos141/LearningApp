import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getMiniGame } from '../api/api';

export default function MiniGamePage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [game, setGame] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [current, setCurrent] = useState(0);
  const [flipped, setFlipped] = useState(false);

  useEffect(() => {
    getMiniGame(id)
      .then(setGame)
      .catch(() => setError('Mini-game not found.'))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (error) return <div className="page-center error-msg">{error}</div>;

  const items = game.items;
  const item = items[current];

  function prev() {
    setFlipped(false);
    setTimeout(() => setCurrent(c => Math.max(0, c - 1)), 150);
  }

  function next() {
    setFlipped(false);
    setTimeout(() => setCurrent(c => Math.min(items.length - 1, c + 1)), 150);
  }

  return (
    <div className="page minigame-page">
      <div className="quiz-header">
        <button className="back-btn" onClick={() => navigate(`/lesson/${id}`)}>← Back</button>
        <span className="quiz-counter">Card {current + 1} of {items.length}</span>
      </div>

      <h2 className="minigame-title">🎴 Flashcards</h2>
      <p className="minigame-hint">Click the card to reveal the definition</p>

      <div
        className={`flashcard ${flipped ? 'flipped' : ''}`}
        onClick={() => setFlipped(f => !f)}
        role="button"
        tabIndex={0}
        onKeyDown={e => e.key === 'Enter' && setFlipped(f => !f)}
        aria-label={flipped ? `Definition: ${item.definition}` : `Term: ${item.term}. Click to reveal.`}
      >
        <div className="flashcard-inner">
          <div className="flashcard-front">
            <span className="card-label">TERM</span>
            <p className="card-content">{item.term}</p>
            <span className="card-flip-hint">Click to flip →</span>
          </div>
          <div className="flashcard-back">
            <span className="card-label">DEFINITION</span>
            <p className="card-content">{item.definition}</p>
            <span className="card-flip-hint">Click to flip back →</span>
          </div>
        </div>
      </div>

      <div className="card-navigation">
        <button className="nav-btn" onClick={prev} disabled={current === 0}>
          ← Previous
        </button>
        <div className="card-dots">
          {items.map((_, i) => (
            <button
              key={i}
              className={`dot ${i === current ? 'active' : ''}`}
              onClick={() => { setFlipped(false); setCurrent(i); }}
              aria-label={`Go to card ${i + 1}`}
            />
          ))}
        </div>
        <button className="nav-btn" onClick={next} disabled={current === items.length - 1}>
          Next →
        </button>
      </div>

      <div className="card-actions-row">
        <button className="action-btn quiz-btn" onClick={() => navigate(`/lesson/${id}/quiz`)}>
          📝 Take Quiz Instead
        </button>
      </div>
    </div>
  );
}
