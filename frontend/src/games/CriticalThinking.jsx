import { useState } from 'react';

const D = { bg: '#001a1a', card: '#002020', border: '#003030', text: '#e0ffff', muted: '#449999', correct: { bg: '#002e1a', border: '#00bb88', text: '#00ddaa' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { q: '"All swans I have ever seen are white, therefore all swans are white." What is the flaw?', answer: 'Hasty generalization', options: ['Circular reasoning','Hasty generalization','Ad hominem','False dichotomy'], exp: 'Hasty generalization: concluding all members of a group based on a limited sample (black swans exist in Australia!).' },
  { q: '"You must be wrong because you have never studied philosophy." What fallacy is this?', answer: 'Ad hominem', options: ['Straw man','Ad hominem','Slippery slope','False authority'], exp: 'Ad hominem: attacking the person rather than their argument. Credentials don\'t determine logical validity.' },
  { q: '"We must ban violent video games or youth crime will keep increasing." What is wrong?', answer: 'False dichotomy', options: ['Post hoc','False dichotomy','Circular reasoning','Bandwagon'], exp: 'False dichotomy (false dilemma): presents only two options when others exist (e.g., education, social programs).' },
  { q: '"After I wore my lucky socks, my team won. So the socks caused the win." Fallacy?', answer: 'Post hoc ergo propter hoc', options: ['Post hoc ergo propter hoc','Ad hominem','False dichotomy','Begging the question'], exp: 'Post hoc: correlation is not causation. The win came after the socks, but that doesn\'t mean the socks caused it.' },
  { q: '"Everyone is investing in crypto, so it must be a good investment." Fallacy?', answer: 'Bandwagon fallacy', options: ['Appeal to authority','Bandwagon fallacy','Sunk cost','Straw man'], exp: 'Bandwagon (appeal to popularity): something is not good/true just because many people believe or do it.' },
  { q: 'A study shows that cities with more hospitals have higher death rates. What is the best conclusion?', answer: 'More people go to hospitals when seriously ill', options: ['Hospitals cause deaths','More hospitals improve survival','More people go to hospitals when seriously ill','The data is wrong'], exp: 'Confounding variable: sick people go to hospitals. More hospitals correlate with illness concentration, not causation.' },
  { q: '"The bridge has been safe for 50 years, so it\'s safe now." What assumption is flawed?', answer: 'Past performance guarantees future safety', options: ['Bridges are well built','Inspections were done','Past performance guarantees future safety','Engineers are reliable'], exp: 'This is the "appeal to tradition" or "argument from past success" — ignoring that materials degrade over time.' },
  { q: '"If we allow gay marriage, next people will want to marry animals." Fallacy?', answer: 'Slippery slope', options: ['Slippery slope','False dichotomy','Hasty generalization','Straw man'], exp: 'Slippery slope: assumes that one action inevitably leads to extreme consequences with no justification.' },
  { q: 'A politician\'s argument is summarized differently and then attacked. What fallacy?', answer: 'Straw man', options: ['Red herring','Straw man','Circular reasoning','Appeal to emotion'], exp: 'Straw man: misrepresenting someone\'s argument to make it easier to attack.' },
  { q: '"This medicine must work — I took it for 2 weeks and recovered." What\'s the key issue?', answer: 'No control group / could have recovered naturally', options: ['Sample size too small','No control group / could have recovered naturally','The medicine is untested','Confirmation bias only'], exp: 'Without a control group (people who didn\'t take the medicine), natural recovery can\'t be ruled out.' },
];

export default function CriticalThinking() {
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
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Critical thinking champion!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#00bb88', color: '#001a1a', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>IDENTIFY THE FLAW IN REASONING</div>
        <p style={{ fontWeight: 600, fontSize: '1.02rem', lineHeight: 1.7 }}>{q.q}</p>
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
