import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getMiniGame } from '../api/api';

export default function MiniGamePage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [cards, setCards] = useState([]);
  const [idx, setIdx] = useState(0);
  const [flipped, setFlipped] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getMiniGame(id)
      .then(data => setCards(data.items || []))
      .catch(() => {})
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (!cards.length) return <div className="page-center error-msg">No flashcards found.</div>;

  const card = cards[idx];

  return (
    <div className="page minigame-page">
      <button className="back-btn" onClick={() => navigate(`/lesson/${id}`)}>← Back to Lesson</button>
      <h1 className="minigame-title">Flashcards</h1>
      <p className="minigame-hint">Click the card to reveal the definition</p>

      <div className={`flashcard${flipped ? ' flipped' : ''}`} onClick={() => setFlipped(f => !f)}>
        <div className="flashcard-inner">
          <div className="flashcard-front">
            <span className="card-label">Term</span>
            <span className="card-content">{card.term}</span>
            <span className="card-flip-hint">Tap to flip</span>
          </div>
          <div className="flashcard-back">
            <span className="card-label">Definition</span>
            <span className="card-content">{card.definition}</span>
            <span className="card-flip-hint">Tap to flip back</span>
          </div>
        </div>
      </div>

      <div className="card-navigation">
        <button className="nav-btn" onClick={() => { setIdx(i => i - 1); setFlipped(false); }} disabled={idx === 0}>← Prev</button>
        <div className="card-dots">
          {cards.map((_, i) => (
            <button key={i} className={`dot${i === idx ? ' active' : ''}`} onClick={() => { setIdx(i); setFlipped(false); }} />
          ))}
        </div>
        <button className="nav-btn" onClick={() => { setIdx(i => i + 1); setFlipped(false); }} disabled={idx === cards.length - 1}>Next →</button>
      </div>

      <div className="card-actions-row">
        <button className="action-btn quiz-btn" onClick={() => navigate(`/lesson/${id}/quiz`)}>📝 Take Quiz</button>
      </div>
    </div>
  );
}
