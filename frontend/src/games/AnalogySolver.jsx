import { useState } from 'react';

const D = { bg: '#111', card: '#1a1a1a', border: '#2a2a2a', text: '#f0f0f0', muted: '#888', correct: { bg: '#0a2e1a', border: '#00cc66', text: '#00ee77' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { a: 'Hot', b: 'Cold', c: 'Light', answer: 'Dark', options: ['Dark','Bright','Fast','Heavy'], exp: 'Hot is the opposite of Cold. Light is the opposite of Dark.' },
  { a: 'Fish', b: 'Scales', c: 'Bird', answer: 'Feathers', options: ['Wings','Feathers','Beak','Claws'], exp: 'Fish have Scales. Birds have Feathers.' },
  { a: 'Doctor', b: 'Hospital', c: 'Teacher', answer: 'School', options: ['Library','Office','School','Lab'], exp: 'A Doctor works in a Hospital. A Teacher works in a School.' },
  { a: 'Triangle', b: '3', c: 'Hexagon', answer: '6', options: ['4','5','6','8'], exp: 'A Triangle has 3 sides. A Hexagon has 6 sides.' },
  { a: 'Puppy', b: 'Dog', c: 'Kitten', answer: 'Cat', options: ['Lion','Cat','Tiger','Cheetah'], exp: 'A Puppy grows into a Dog. A Kitten grows into a Cat.' },
  { a: 'Painter', b: 'Brush', c: 'Writer', answer: 'Pen', options: ['Paper','Pen','Ink','Keyboard'], exp: 'A Painter uses a Brush. A Writer uses a Pen.' },
  { a: 'Sun', b: 'Day', c: 'Moon', answer: 'Night', options: ['Stars','Night','Dark','Space'], exp: 'The Sun brings Day. The Moon brings Night.' },
  { a: 'Glove', b: 'Hand', c: 'Boot', answer: 'Foot', options: ['Leg','Knee','Foot','Ankle'], exp: 'A Glove covers the Hand. A Boot covers the Foot.' },
  { a: '4', b: '16', c: '5', answer: '25', options: ['10','20','25','30'], exp: '4² = 16. 5² = 25. Each number is squared.' },
  { a: 'Herbivore', b: 'Plants', c: 'Carnivore', answer: 'Meat', options: ['Fruits','Grass','Meat','Fish'], exp: 'A Herbivore eats Plants. A Carnivore eats Meat.' },
];

export default function AnalogySolver() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🧩</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem', color: D.text }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Analogy reasoning complete!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#fff', color: '#111' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '1rem' }}>COMPLETE THE ANALOGY</div>
        <div style={{ display: 'flex', alignItems: 'center', gap: '0.75rem', fontSize: '1.2rem', flexWrap: 'wrap' }}>
          <span style={{ padding: '0.5rem 1rem', background: '#222', borderRadius: 8, fontWeight: 700 }}>{q.a}</span>
          <span style={{ color: D.muted }}>is to</span>
          <span style={{ padding: '0.5rem 1rem', background: '#222', borderRadius: 8, fontWeight: 700 }}>{q.b}</span>
          <span style={{ color: D.muted }}>as</span>
          <span style={{ padding: '0.5rem 1rem', background: '#222', borderRadius: 8, fontWeight: 700 }}>{q.c}</span>
          <span style={{ color: D.muted }}>is to</span>
          <span style={{ padding: '0.5rem 1rem', background: '#1a1a2e', border: '2px dashed #4488ff', borderRadius: 8, fontWeight: 700, color: '#4488ff' }}>?</span>
        </div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '1rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 700, fontSize: '1rem', cursor: 'pointer', fontFamily: 'inherit' }}>{o}</button>;
        })}
      </div>
      {chosen && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem' }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
