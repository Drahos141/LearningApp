import { useState } from 'react';

const D = { bg: '#0d1a0d', card: '#111f11', border: '#1e3a1e', text: '#e8ffe8', muted: '#66aa66', correct: { bg: '#0a2e0a', border: '#00cc44', text: '#00ee55' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { p: ['All mammals are warm-blooded.', 'Whales are mammals.'], c: 'Whales are warm-blooded.', answer: true, exp: 'Valid: Whales are mammals (premise 2), all mammals are warm-blooded (premise 1) → Whales must be warm-blooded.' },
  { p: ['All A are B.', 'All B are C.', 'X is an A.'], c: 'X is a C.', answer: true, exp: 'Valid transitive syllogism: X→A→B→C. So X is a C.' },
  { p: ['Some cats are black.', 'Whiskers is a cat.'], c: 'Whiskers is black.', answer: false, exp: 'Invalid: "Some cats are black" does not guarantee Whiskers is one of those cats.' },
  { p: ['No reptiles are warm-blooded.', 'Crocodiles are reptiles.'], c: 'Crocodiles are cold-blooded.', answer: true, exp: 'Valid: All reptiles are not warm-blooded, crocodiles are reptiles → crocodiles are cold-blooded.' },
  { p: ['If P then Q.', 'Q is true.'], c: 'P is true.', answer: false, exp: 'Invalid (affirming the consequent): Q could be true for reasons other than P. e.g. If it rains the ground is wet, the ground is wet — does not mean it rained.' },
  { p: ['All even numbers are divisible by 2.', '10 is even.'], c: '10 is divisible by 2.', answer: true, exp: 'Valid: 10 is even (premise 2), all even numbers are divisible by 2 (premise 1) → 10 is divisible by 2.' },
  { p: ['Most students passed the exam.', 'Anna is a student.'], c: 'Anna passed the exam.', answer: false, exp: 'Invalid: "Most" does not guarantee Anna specifically passed — she could be in the minority.' },
  { p: ['All squares are rectangles.', 'All rectangles have four sides.', 'Shape X is a square.'], c: 'Shape X has four sides.', answer: true, exp: 'Valid: X is square → X is rectangle → X has four sides.' },
  { p: ['No fish are mammals.', 'Dolphins are mammals.'], c: 'Dolphins are not fish.', answer: true, exp: 'Valid: No fish are mammals, Dolphins are mammals → Dolphins cannot be fish.' },
  { p: ['All philosophers are wise.', 'Socrates is wise.'], c: 'Socrates is a philosopher.', answer: false, exp: 'Invalid (undistributed middle): Wisdom is required for philosophers but wise people aren\'t all philosophers.' },
];

export default function SyllogismChallenge() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🧠</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Logical syllogism master!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#00cc44', color: '#001a00', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '1rem' }}>PREMISES</div>
        {q.p.map((premise, i) => (
          <div key={i} style={{ display: 'flex', gap: '0.5rem', marginBottom: '0.5rem', alignItems: 'flex-start' }}>
            <span style={{ background: D.border, borderRadius: 4, padding: '0.1rem 0.5rem', fontSize: '0.75rem', fontWeight: 700, flexShrink: 0, marginTop: '0.1rem' }}>{i + 1}</span>
            <span style={{ lineHeight: 1.6 }}>{premise}</span>
          </div>
        ))}
        <div style={{ borderTop: `1px solid ${D.border}`, marginTop: '1rem', paddingTop: '1rem' }}>
          <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.5rem' }}>CONCLUSION — IS THIS VALID?</div>
          <p style={{ fontWeight: 700, fontSize: '1.05rem', lineHeight: 1.6 }}>{q.c}</p>
        </div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {[true, false].map(v => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen !== null) {
            if (v === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (v === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={String(v)} onClick={() => pick(v)} style={{ padding: '1.25rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 800, fontSize: '1.1rem', cursor: 'pointer', fontFamily: 'inherit' }}>{v ? '✓ VALID' : '✗ INVALID'}</button>;
        })}
      </div>
      {chosen !== null && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem', lineHeight: 1.6 }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ This is ${q.answer ? 'VALID' : 'INVALID'}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
