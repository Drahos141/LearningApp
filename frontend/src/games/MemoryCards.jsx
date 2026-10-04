import { useState, useEffect, useRef } from 'react';
import { shuffle } from '../utils/shuffle';

const EMOJIS = ['🐶','🐱','🐭','🐹','🐰','🦊','🐻','🐼'];

function makeCards() {
  return shuffle([...EMOJIS, ...EMOJIS].map((e, i) => ({ id: i, emoji: e, flipped: false, matched: false })));
}

export default function MemoryCards() {
  const [cards, setCards] = useState(makeCards);
  const [selected, setSelected] = useState([]);
  const [moves, setMoves] = useState(0);
  const won = cards.every(c => c.matched);
  const timeoutRef = useRef();

  useEffect(() => () => clearTimeout(timeoutRef.current), []);

  const flip = (i) => {
    if (selected.length === 2 || cards[i].flipped || cards[i].matched) return;
    setCards(c => c.map((card, idx) => idx === i ? { ...card, flipped: true } : card));
    if (selected.length === 0) { setSelected([i]); return; }

    const [a, b] = [selected[0], i];
    setMoves(m => m + 1);
    if (cards[a].emoji === cards[b].emoji) {
      setCards(c => c.map((card, idx) => idx === a || idx === b ? { ...card, matched: true } : card));
      setSelected([]);
    } else {
      // Keep both cards selected (blocking further flips) until they turn back over.
      setSelected([a, b]);
      timeoutRef.current = setTimeout(() => {
        setCards(c => c.map((card, idx) => idx === a || idx === b ? { ...card, flipped: false } : card));
        setSelected([]);
      }, 700);
    }
  };

  const reset = () => { clearTimeout(timeoutRef.current); setCards(makeCards()); setSelected([]); setMoves(0); };

  if (won) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🎉</div>
      <h2>You matched all pairs!</h2>
      <p>Completed in {moves} moves</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={reset}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Moves: {moves}</span><button className="play-btn-secondary" onClick={reset}>Restart</button></div>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4,1fr)', gap: '0.75rem' }}>
        {cards.map((card, i) => (
          <button key={card.id} onClick={() => flip(i)} style={{
            height: '80px', fontSize: '2rem', border: '1px solid #e0e0e0', borderRadius: '10px',
            background: card.flipped || card.matched ? '#f8f8f8' : '#0a0a0a',
            cursor: card.matched ? 'default' : 'pointer', transition: 'background 0.2s',
            opacity: card.matched ? 0.4 : 1
          }}>
            {(card.flipped || card.matched) ? card.emoji : ''}
          </button>
        ))}
      </div>
    </div>
  );
}
