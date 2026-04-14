import { useState } from 'react';

const D = { bg: '#001100', card: '#001a00', border: '#002a00', text: '#ccffcc', muted: '#448844', correct: { bg: '#002200', border: '#00cc00', text: '#00ff00' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

// Number codes: clues like "1234 → 2 correct digits, 2 correct positions"
const QUESTIONS = [
  { code: [6,8,2,3], clues: ['5616: one digit is correct and in the right position.','4832: two digits are correct but in wrong positions.','2804: two digits are correct with one in the right position.'], answer: '6823', options: ['6823','6832','8623','2863'], exp: 'The digit 6 is correct and in position 1 (from clue 1). 8 and 2 are present. Position analysis gives 6823.' },
  { code: [3,1,7], clues: ['529: nothing is correct.','174: one correct digit, wrong position.','317: one correct digit, right position.'], answer: '317', options: ['137','317','731','173'], exp: 'From clue 3: one of 3,1,7 is correct and in right position. Combined with clue 2: 1 or 7 is in the answer. Answer is 317.' },
  { code: [9,4,2], clues: ['562: nothing correct.','746: one digit correct, wrong position.','916: one digit correct, right position.','428: one digit correct, wrong position.'], answer: '942', options: ['924','942','294','429'], exp: '9 is correct (position 1 from clue 3). 4 is in the answer but not position 2. 2 is in answer. So 9-4-2.' },
  { code: [7,3,8], clues: ['158: nothing correct.','234: one correct, wrong position.','783: two correct, one right position.','738: two correct, two right positions.'], answer: '738', options: ['378','783','738','837'], exp: 'From clue 4: 7 and 3 are both in right position (pos 1&2). From clue 3: one of 7,8,3 is right pos. 8 must be pos 3. Answer: 738.' },
  { code: [5,0,6], clues: ['123: nothing.','456: one correct, wrong position.','906: two correct, one right position.','516: one correct, right position.'], answer: '506', options: ['056','506','560','605'], exp: 'From clue 4: 5 is in position 1. From clue 3: 0 and 6 — 6 is right position (3). 0 is at position 2. Answer: 506.' },
  { code: [4,8,1], clues: ['234: one correct, wrong position.','618: one correct, wrong position.','941: two correct, one right position.'], answer: '481', options: ['184','481','841','148'], exp: 'From clue 3: 9,4,1 — two correct, one right. 4 and 1 are both in code. Position 2 for 8 (from clue 2). So 4-8-1.' },
  { code: [2,6,9], clues: ['135: nothing.','269: all correct, all right position.'], answer: '269', options: ['296','629','269','926'], exp: 'Clue 2 directly reveals the answer: 2,6,9 all correct and in correct positions.' },
  { code: [8,5,3], clues: ['741: nothing.','852: two correct, one right position.','653: two correct, one right position.','835: two correct, two right positions.'], answer: '853', options: ['835','853','583','358'], exp: 'From clue 4: 8 and 3 in right positions (1 and 3? or 1 and ?). From clue 2: 8 and 5 correct. Cross-referencing: 8-5-3.' },
];

export default function CodeBreaker() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🔐</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Code cracked!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#00cc00', color: '#001100', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text, fontFamily: 'monospace, monospace' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem', fontFamily: 'monospace' }}>
        <span>PUZZLE {idx + 1} / {QUESTIONS.length}</span><span>SCORE: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: '#00cc00', fontWeight: 700, letterSpacing: '0.15em', marginBottom: '1rem' }}>CRACK THE CODE — CLUES:</div>
        {q.clues.map((c, i) => (
          <div key={i} style={{ display: 'flex', gap: '0.75rem', marginBottom: '0.6rem', alignItems: 'flex-start', fontSize: '0.9rem', lineHeight: 1.5 }}>
            <span style={{ color: '#00cc00', fontWeight: 700, flexShrink: 0 }}>▶</span>
            <span>{c}</span>
          </div>
        ))}
      </div>
      <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>SELECT THE CORRECT CODE:</div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '1rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 800, fontSize: '1.4rem', letterSpacing: '0.2em', cursor: 'pointer', fontFamily: 'monospace' }}>{o}</button>;
        })}
      </div>
      {chosen && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem', lineHeight: 1.6 }}>
          <strong>{chosen === q.answer ? '✓ Code cracked!' : `✗ Code was: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
