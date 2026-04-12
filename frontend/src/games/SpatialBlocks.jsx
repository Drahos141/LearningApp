import { useState } from 'react';

const QUESTIONS = [
  { desc: 'A single stack of 3 blocks (1×1 base, 3 high)', answer: 3, options: [2,3,4,5] },
  { desc: 'A 2×2 base with one block on top in the corner', answer: 5, options: [4,5,6,7] },
  { desc: 'An L-shape: 3 blocks in a row, 2 extra stacked on one end', answer: 5, options: [4,5,6,7] },
  { desc: 'A 3×1 row with 2 blocks stacked on the middle', answer: 5, options: [4,5,6,8] },
  { desc: 'A 2×2×2 cube', answer: 8, options: [6,7,8,9] },
  { desc: 'A pyramid: 4-block base (2×2), 1 block on top', answer: 5, options: [4,5,6,7] },
  { desc: 'A staircase: 1 block, then 2 stacked, then 3 stacked side by side', answer: 6, options: [5,6,7,8] },
  { desc: 'A T-shape: 3 blocks in a row, 1 block in the middle on top', answer: 4, options: [3,4,5,6] },
];

export default function SpatialBlocks() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s+1); };
  const next = () => { if (idx+1>=QUESTIONS.length){setDone(true);return;} setIdx(i=>i+1);setChosen(null); };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🧱</div>
      <h2>Score: {score}/{QUESTIONS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0);setScore(0);setChosen(null);setDone(false); }}>Play Again</button>
    </div>
  );

  const opts = [...new Set([...q.options])].sort((a,b)=>a-b);

  return (
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.75rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>PICTURE THIS ARRANGEMENT</div>
        <p style={{ fontWeight: 600, fontSize: '1rem', lineHeight: 1.6 }}>{q.desc}</p>
        <div style={{ marginTop: '1rem', color: '#6b6b6b', fontSize: '0.85rem' }}>How many blocks in total, including any you cannot see?</div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {opts.map(o => {
          let bg='#fff',bc='#e0e0e0',color='#0a0a0a';
          if(chosen!==null){
            if(o===q.answer){bg='#f0fdf4';bc='#16a34a';color='#166534';}
            else if(o===chosen){bg='#fef2f2';bc='#dc2626';color='#991b1b';}
          }
          return <button key={o} onClick={()=>pick(o)} style={{padding:'1rem',border:`1px solid ${bc}`,borderRadius:'10px',background:bg,color,fontWeight:700,fontSize:'1.4rem'}}>{o}</button>;
        })}
      </div>
      {chosen!==null&&<button className="next-btn" style={{marginTop:'1.5rem'}} onClick={next}>{idx+1>=QUESTIONS.length?'See Results':'Next →'}</button>}
    </div>
  );
}
