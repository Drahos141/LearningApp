import { useState } from 'react';

const QUESTIONS = [
  { seq: ['🔴','🔵','🔴','🔵','🔴'], answer: '🔵', options: ['🔵','🟢','🔴','🟡'] },
  { seq: ['⬛','⬜','⬛','⬜','⬛'], answer: '⬜', options: ['⬛','⬜','🟥','🟦'] },
  { seq: ['▲','▲','▲','■','▲','▲','▲'], answer: '■', options: ['▲','■','●','★'] },
  { seq: ['1️⃣','2️⃣','1️⃣','2️⃣','1️⃣'], answer: '2️⃣', options: ['1️⃣','2️⃣','3️⃣','4️⃣'] },
  { seq: ['🌕','🌖','🌗','🌘'], answer: '🌑', options: ['🌕','🌑','🌓','🌗'] },
  { seq: ['A','B','C','A','B'], answer: 'C', options: ['A','B','C','D'] },
  { seq: ['⬆️','➡️','⬇️','⬅️','⬆️'], answer: '➡️', options: ['⬆️','➡️','⬇️','⬅️'] },
  { seq: ['🌱','🌿','🌳','🍂','🌱'], answer: '🌿', options: ['🌱','🌿','🌳','🍂'] },
  { seq: ['◼','◾','◼','◾','◼'], answer: '◾', options: ['◼','◾','⬜','⬛'] },
  { seq: ['🎵','🎵','🎶','🎵','🎵'], answer: '🎶', options: ['🎵','🎶','🎼','🥁'] },
];

export default function PatternMatch() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => {
    if (chosen !== null) return;
    setChosen(v);
    if (v === q.answer) setScore(s => s + 1);
  };
  const next = () => {
    if (idx + 1 >= QUESTIONS.length) { setDone(true); return; }
    setIdx(i => i + 1); setChosen(null);
  };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🔮</div>
      <h2>Score: {score} / {QUESTIONS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.78rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>WHAT COMES NEXT?</div>
        <div style={{ display: 'flex', gap: '0.75rem', fontSize: '2rem', flexWrap: 'wrap' }}>
          {q.seq.map((s, i) => <span key={i}>{s}</span>)}
          <span style={{ border: '2px dashed #0a0a0a', borderRadius: '8px', minWidth: '2.5rem', textAlign: 'center' }}>?</span>
        </div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = '#fff', bc = '#e0e0e0', color = '#0a0a0a';
          if (chosen !== null) {
            if (o === q.answer) { bg = '#f0fdf4'; bc = '#16a34a'; color = '#166534'; }
            else if (o === chosen) { bg = '#fef2f2'; bc = '#dc2626'; color = '#991b1b'; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '1rem', border: `1px solid ${bc}`, borderRadius: '10px', background: bg, color, fontSize: '1.8rem' }}>{o}</button>;
        })}
      </div>
      {chosen !== null && <button className="next-btn" style={{ marginTop: '1.5rem' }} onClick={next}>{idx+1>=QUESTIONS.length?'See Results':'Next →'}</button>}
    </div>
  );
}
