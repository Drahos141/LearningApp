import { useState } from 'react';

const QUESTIONS = [
  { items: ['Cat','Dog','Bird','Apple'], odd: 'Apple', reason: 'Apple is a fruit; others are animals.' },
  { items: ['Red','Blue','Green','Seven'], odd: 'Seven', reason: 'Seven is a number; others are colors.' },
  { items: ['Piano','Violin','Trumpet','Hammer'], odd: 'Hammer', reason: 'Hammer is a tool; others are instruments.' },
  { items: ['Earth','Mars','Moon','Jupiter'], odd: 'Moon', reason: 'Moon is a natural satellite; others are planets.' },
  { items: ['Paris','London','Tokyo','Pacific'], odd: 'Pacific', reason: 'Pacific is an ocean; others are capital cities.' },
  { items: ['Python','Java','HTML','Ruby'], odd: 'HTML', reason: 'HTML is a markup language; others are programming languages.' },
  { items: ['2','4','6','7'], odd: '7', reason: '7 is odd; all others are even numbers.' },
  { items: ['Rose','Tulip','Daisy','Cactus'], odd: 'Cactus', reason: 'Cactus is a succulent; others are flowering garden flowers.' },
  { items: ['Mercury','Venus','Earth','Pluto'], odd: 'Pluto', reason: 'Pluto is a dwarf planet; others are full planets.' },
  { items: ['Oxygen','Gold','Iron','Carbon'], odd: 'Gold', reason: 'Gold is a metal; others are non-metals.' },
];

export default function OddOneOut() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (item) => {
    if (chosen !== null) return;
    setChosen(item);
    if (item === q.odd) setScore(s => s + 1);
  };
  const next = () => {
    if (idx + 1 >= QUESTIONS.length) { setDone(true); return; }
    setIdx(i => i + 1); setChosen(null);
  };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🎯</div>
      <h2>Score: {score} / {QUESTIONS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ fontSize: '0.88rem', color: '#6b6b6b', marginBottom: '1rem' }}>Which one does NOT belong?</div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem', marginBottom: '1rem' }}>
        {q.items.map(item => {
          let bg = '#fff', bc = '#e0e0e0', color = '#0a0a0a';
          if (chosen !== null) {
            if (item === q.odd) { bg = '#f0fdf4'; bc = '#16a34a'; color = '#166534'; }
            else if (item === chosen) { bg = '#fef2f2'; bc = '#dc2626'; color = '#991b1b'; }
          }
          return <button key={item} onClick={() => pick(item)} style={{ padding: '1rem', border: `1px solid ${bc}`, borderRadius: '10px', background: bg, color, fontWeight: 600, fontSize: '1rem' }}>{item}</button>;
        })}
      </div>
      {chosen !== null && (
        <div>
          <div style={{ padding: '0.9rem 1.1rem', borderRadius: '10px', background: chosen === q.odd ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${chosen === q.odd ? '#16a34a' : '#dc2626'}`, color: chosen === q.odd ? '#166534' : '#991b1b', marginBottom: '1rem', fontSize: '0.9rem' }}>
            {chosen === q.odd ? '✓ Correct! ' : `✗ The odd one was "${q.odd}". `}{q.reason}
          </div>
          <button className="next-btn" onClick={next}>{idx+1>=QUESTIONS.length?'See Results':'Next →'}</button>
        </div>
      )}
    </div>
  );
}
