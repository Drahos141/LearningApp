import { useState } from 'react';

// Each question: a 3x3 matrix of shapes, last cell is missing. Options are 4 choices.
const QUESTIONS = [
  { matrix: ['●','■','▲','■','▲','●','▲','●',null], answer: '■', options: ['■','▲','●','★'] },
  { matrix: ['A','B','C','B','C','A','C','A',null], answer: 'B', options: ['A','B','C','D'] },
  { matrix: ['1','2','3','2','3','1','3','1',null], answer: '2', options: ['1','2','3','4'] },
  { matrix: ['🔴','🔵','🟢','🔵','🟢','🔴','🟢','🔴',null], answer: '🔵', options: ['🔴','🔵','🟢','🟡'] },
  { matrix: ['X','O','X','O','X','O','X','O',null], answer: 'X', options: ['X','O','Z','Y'] },
  { matrix: ['↑','→','↓','→','↓','↑','↓','↑',null], answer: '→', options: ['↑','→','↓','←'] },
  { matrix: ['α','β','γ','β','γ','α','γ','α',null], answer: 'β', options: ['α','β','γ','δ'] },
  { matrix: ['I','II','III','II','III','I','III','I',null], answer: 'II', options: ['I','II','III','IV'] },
];

export default function MatrixPattern() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🔲</div>
      <h2>Score: {score}/{QUESTIONS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ display: 'inline-grid', gridTemplateColumns: 'repeat(3,1fr)', gap: '4px', background: '#e0e0e0', padding: '4px', borderRadius: '10px', marginBottom: '2rem' }}>
        {q.matrix.map((v, i) => (
          <div key={i} style={{ width: '60px', height: '60px', background: '#f8f8f8', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '1.4rem', fontWeight: 700, borderRadius: '6px', background: v === null ? '#0a0a0a' : '#f8f8f8', color: v === null ? '#fff' : '#0a0a0a' }}>{v === null ? '?' : v}</div>
        ))}
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = '#fff', bc = '#e0e0e0', color = '#0a0a0a';
          if (chosen !== null) {
            if (o === q.answer) { bg = '#f0fdf4'; bc = '#16a34a'; color = '#166534'; }
            else if (o === chosen) { bg = '#fef2f2'; bc = '#dc2626'; color = '#991b1b'; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '1rem', border: `1px solid ${bc}`, borderRadius: '10px', background: bg, color, fontSize: '1.5rem', fontWeight: 700 }}>{o}</button>;
        })}
      </div>
      {chosen !== null && <button className="next-btn" style={{ marginTop: '1.5rem' }} onClick={next}>{idx+1>=QUESTIONS.length?'See Results':'Next →'}</button>}
    </div>
  );
}
