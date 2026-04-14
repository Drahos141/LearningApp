import { useState } from 'react';

const D = { bg: '#0d1a1a', card: '#111f1f', border: '#1e3a3a', text: '#e8ffff', muted: '#66aaaa', correct: { bg: '#0a2020', border: '#00cccc', text: '#00eeee' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { clues: ['A is taller than B.','C is taller than A.','D is shorter than B.'], q: 'Who is the tallest?', answer: 'C', options: ['A','B','C','D'], exp: 'C > A > B > D. C is the tallest.' },
  { clues: ['If it is raining, the match is cancelled.','The match is not cancelled.'], q: 'What can we conclude?', answer: 'It is not raining', options: ['It is raining','It is not raining','The match is cancelled','Cannot determine'], exp: 'Contrapositive: if match NOT cancelled → NOT raining. It is not raining.' },
  { clues: ['All doctors earn more than nurses.','All nurses earn more than cleaners.','Sara is a nurse.'], q: 'Who earns more than Sara?', answer: 'Doctors', options: ['Cleaners','Nurses','Doctors','Managers'], exp: 'Sara is a nurse. All doctors earn more than nurses, so doctors earn more than Sara.' },
  { clues: ['Tom is either a liar or a truth-teller.','Tom says: "I am a liar."'], q: 'What is Tom?', answer: 'Cannot be determined', options: ['A liar','A truth-teller','Cannot be determined','Both'], exp: 'Paradox: if Tom is a liar, his statement is true (contradiction). If truth-teller, his statement is false (contradiction). This is the Liar Paradox.' },
  { clues: ['Exactly one of A,B,C is guilty.','A says: "I am innocent."','B says: "C is guilty."','C says: "B is lying."'], q: 'If only one person lies, who is guilty?', answer: 'B', options: ['A','B','C','All three'], exp: 'If B is guilty: A tells truth (innocent), B lies (says C guilty), C tells truth (B is lying). Exactly one liar = B. Consistent!' },
  { clues: ['P → Q (If P then Q)','Q → R (If Q then R)','P is true.'], q: 'What can we conclude?', answer: 'R is true', options: ['R is true','P is false','Q is false','Nothing'], exp: 'P is true → Q is true (modus ponens) → R is true (modus ponens again).' },
  { clues: ['5 people sit in a row.','Ann is left of Bob.','Bob is left of Carol.','Dan is right of Carol.','Eve is left of Ann.'], q: 'Who sits in the middle (3rd position)?', answer: 'Bob', options: ['Ann','Bob','Carol','Dan'], exp: 'Order: Eve, Ann, Bob, Carol, Dan. Bob is in position 3 (middle).' },
  { clues: ['Some A are B.','Some B are C.'], q: 'Does some A have to be C?', answer: 'Not necessarily', options: ['Yes, always','Not necessarily','Never','Cannot say'], exp: 'The A\'s that are B and the B\'s that are C may be different B\'s. There is no guarantee some A is also C.' },
  { clues: ['All roses are flowers.','Some flowers fade quickly.'], q: 'Do some roses fade quickly?', answer: 'Not necessarily', options: ['Yes, definitely','Not necessarily','All roses fade','None fade'], exp: 'The "some flowers" that fade may not include any roses. No necessary conclusion about roses.' },
  { clues: ['In a race: X finished before Y.','Z finished after Y.','W finished before X.'], q: 'Who finished last?', answer: 'Z', options: ['X','Y','Z','W'], exp: 'Order from first: W, X, Y, Z. Z finished last.' },
];

export default function LogicalDeduction() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🎯</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Logical deduction complete!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#00cccc', color: '#001a1a', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>CLUES</div>
        {q.clues.map((c, i) => (
          <div key={i} style={{ display: 'flex', gap: '0.5rem', marginBottom: '0.4rem', alignItems: 'flex-start' }}>
            <span style={{ background: '#1e3a3a', borderRadius: 4, padding: '0.1rem 0.45rem', fontSize: '0.72rem', fontWeight: 700, flexShrink: 0, marginTop: '0.2rem' }}>{i+1}</span>
            <span style={{ lineHeight: 1.6 }}>{c}</span>
          </div>
        ))}
        <div style={{ borderTop: `1px solid ${D.border}`, marginTop: '1rem', paddingTop: '1rem' }}>
          <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.4rem' }}>QUESTION</div>
          <p style={{ fontWeight: 700, fontSize: '1.05rem', lineHeight: 1.6 }}>{q.q}</p>
        </div>
      </div>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.6rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '0.9rem 1.25rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 600, fontSize: '0.95rem', cursor: 'pointer', fontFamily: 'inherit', textAlign: 'left' }}>{o}</button>;
        })}
      </div>
      {chosen && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem', lineHeight: 1.6 }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
