import { useState } from 'react';

const PUZZLES = [
  { start: 'CAT', end: 'DOG', steps: ['CAT','COT','COG','DOG'] },
  { start: 'COLD', end: 'WARM', steps: ['COLD','CORD','WORD','WARD','WARM'] },
  { start: 'HEAD', end: 'TAIL', steps: ['HEAD','HEAL','TEAL','TELL','TALL','TAIL'] },
];

export default function WordChain() {
  const [pidx, setPidx] = useState(0);
  const [score, setScore] = useState(0);
  const puz = PUZZLES[pidx];
  const [chain, setChain] = useState([puz.start]);
  const [input, setInput] = useState('');
  const [error, setError] = useState('');
  const [won, setWon] = useState(false);
  const [done, setDone] = useState(false);

  const current = chain[chain.length - 1];

  const diffCount = (a, b) => {
    if (a.length !== b.length) return 99;
    return [...a].filter((c, i) => c !== b[i]).length;
  };

  const submit = () => {
    const w = input.trim().toUpperCase();
    if (w.length !== puz.start.length) { setError(`Word must be ${puz.start.length} letters.`); return; }
    if (diffCount(current, w) !== 1) { setError('Change exactly 1 letter.'); return; }
    if (chain.includes(w)) { setError('Already used that word.'); return; }
    const newChain = [...chain, w];
    setChain(newChain); setInput(''); setError('');
    if (w === puz.end) { setScore(s => s + 1); setWon(true); }
  };

  const next = () => {
    if (pidx + 1 >= PUZZLES.length) { setDone(true); return; }
    const next = PUZZLES[pidx + 1];
    setPidx(p => p + 1); setChain([next.start]); setInput(''); setError(''); setWon(false);
  };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🔗</div>
      <h2>Score: {score}/{PUZZLES.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setPidx(0); setScore(0); setChain([PUZZLES[0].start]); setInput(''); setError(''); setWon(false); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Puzzle {pidx+1}/{PUZZLES.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '1.5rem', marginBottom: '1.5rem' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '1rem' }}>
          <span><strong>Start:</strong> {puz.start}</span><span>→</span><span><strong>End:</strong> {puz.end}</span>
        </div>
        <div style={{ display: 'flex', gap: '0.5rem', flexWrap: 'wrap' }}>
          {chain.map((w, i) => <span key={i} style={{ padding: '0.4rem 0.8rem', border: '1px solid #e0e0e0', borderRadius: '8px', fontWeight: i === chain.length-1 ? 800 : 400, background: i === chain.length-1 ? '#0a0a0a' : '#fff', color: i === chain.length-1 ? '#fff' : '#0a0a0a' }}>{w}</span>)}
        </div>
        <div style={{ marginTop: '0.75rem', fontSize: '0.8rem', color: '#6b6b6b' }}>Reference path: {puz.steps.join(' → ')}</div>
      </div>
      {!won ? (
        <div>
          {error && <div style={{ color: '#dc2626', marginBottom: '0.75rem', fontSize: '0.88rem' }}>{error}</div>}
          <div style={{ display: 'flex', gap: '0.75rem' }}>
            <input value={input} onChange={e => setInput(e.target.value)} onKeyDown={e => e.key === 'Enter' && input && submit()} placeholder="Next word..." maxLength={puz.start.length} style={{ flex: 1, padding: '0.85rem 1rem', border: '1px solid #e0e0e0', borderRadius: '10px', fontSize: '1rem', fontFamily: 'inherit', outline: 'none', textTransform: 'uppercase' }} autoFocus />
            <button className="play-btn" disabled={!input} onClick={submit}>Go</button>
          </div>
        </div>
      ) : (
        <div>
          <div style={{ padding: '1rem', background: '#f0fdf4', borderLeft: '4px solid #16a34a', color: '#166534', borderRadius: '10px', marginBottom: '1rem' }}>✓ You reached {puz.end} in {chain.length - 1} steps!</div>
          <button className="next-btn" onClick={next}>{pidx+1>=PUZZLES.length?'See Results':'Next Puzzle →'}</button>
        </div>
      )}
    </div>
  );
}
