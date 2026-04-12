import { useState, useEffect, useCallback } from 'react';

const EMOJIS = ['🐶','🐱','🐭','🐹','🐰','🦊','🐻','🐼'];

function shuffle(arr) {
  return [...arr].sort(() => Math.random() - 0.5);
}

function makeCards() {
  return shuffle([...EMOJIS, ...EMOJIS].map((e, i) => ({ id: i, emoji: e, flipped: false, matched: false })));
}

export default function MemoryCards() {
  const [cards, setCards] = useState(makeCards);
  const [selected, setSelected] = useState([]);
  const [moves, setMoves] = useState(0);
  const [won, setWon] = useState(false);

  useEffect(() => {
    if (selected.length === 2) {
      const [a, b] = selected;
      if (cards[a].emoji === cards[b].emoji) {
        setCards(c => c.map((card, i) => i === a || i === b ? { ...card, matched: true } : card));
      } else {
        setTimeout(() => {
          setCards(c => c.map((card, i) => i === a || i === b ? { ...card, flipped: false } : card));
        }, 700);
      }
      setSelected([]);
      setMoves(m => m + 1);
    }
  }, [selected]);

  useEffect(() => {
    if (cards.every(c => c.matched)) setWon(true);
  }, [cards]);

  const flip = (i) => {
    if (selected.length === 2 || cards[i].flipped || cards[i].matched) return;
    setCards(c => c.map((card, idx) => idx === i ? { ...card, flipped: true } : card));
    setSelected(s => [...s, i]);
  };

  const reset = () => { setCards(makeCards()); setSelected([]); setMoves(0); setWon(false); };

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
