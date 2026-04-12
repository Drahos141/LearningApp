import { useState } from 'react';

const QUESTIONS = [
  { premise: 'All dogs are animals. Rex is a dog.', conclusion: 'Rex is an animal.', answer: true },
  { premise: 'Some birds can fly. Penguins are birds.', conclusion: 'Penguins can fly.', answer: false },
  { premise: 'No fish are mammals. Salmon is a fish.', conclusion: 'Salmon is not a mammal.', answer: true },
  { premise: 'All squares are rectangles. Shape A is a square.', conclusion: 'Shape A is a rectangle.', answer: true },
  { premise: 'All reptiles are cold-blooded. Snakes are warm-blooded.', conclusion: 'Snakes are reptiles.', answer: false },
  { premise: 'If it rains, the ground is wet. The ground is wet.', conclusion: 'It is raining.', answer: false },
  { premise: 'All prime numbers above 2 are odd. 7 is a prime number above 2.', conclusion: '7 is odd.', answer: true },
  { premise: 'No vegetarians eat meat. Anna is a vegetarian.', conclusion: 'Anna does not eat meat.', answer: true },
  { premise: 'If P then Q. Q is true.', conclusion: 'P is true.', answer: false },
  { premise: 'All A are B. All B are C. X is an A.', conclusion: 'X is a C.', answer: true },
];

export default function TrueFalseLogic() {
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
      <div style={{ fontSize: '3rem' }}>🧠</div>
      <h2>Score: {score}/{QUESTIONS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.75rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>PREMISES</div>
        <p style={{ marginBottom: '1rem', lineHeight: 1.7 }}>{q.premise}</p>
        <div style={{ fontSize: '0.75rem', color: '#6b6b6b', marginBottom: '0.5rem', fontWeight: 600 }}>CONCLUSION</div>
        <p style={{ fontWeight: 700, fontSize: '1.05rem' }}>{q.conclusion}</p>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem', marginBottom: '1rem' }}>
        {[true, false].map(v => {
          let bg = '#fff', bc = '#e0e0e0', color = '#0a0a0a';
          if (chosen !== null) {
            if (v === q.answer) { bg = '#f0fdf4'; bc = '#16a34a'; color = '#166534'; }
            else if (v === chosen) { bg = '#fef2f2'; bc = '#dc2626'; color = '#991b1b'; }
          }
          return <button key={String(v)} onClick={() => pick(v)} style={{ padding: '1.25rem', border: `1px solid ${bc}`, borderRadius: '10px', background: bg, color, fontWeight: 700, fontSize: '1.1rem' }}>{v ? '✓ TRUE' : '✗ FALSE'}</button>;
        })}
      </div>
      {chosen !== null && (
        <div>
          <div style={{ padding: '0.9rem 1.1rem', borderRadius: '10px', background: chosen === q.answer ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${chosen === q.answer ? '#16a34a' : '#dc2626'}`, color: chosen === q.answer ? '#166534' : '#991b1b', marginBottom: '1rem', fontSize: '0.9rem' }}>
            {chosen === q.answer ? '✓ Correct!' : `✗ The answer is ${q.answer ? 'TRUE' : 'FALSE'}. ${q.answer ? 'The conclusion follows logically.' : 'The conclusion does not necessarily follow.'}`}
          </div>
          <button className="next-btn" onClick={next}>{idx+1>=QUESTIONS.length?'See Results':'Next →'}</button>
        </div>
      )}
    </div>
  );
}
