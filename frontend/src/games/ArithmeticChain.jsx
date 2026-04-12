import { useState } from 'react';

function makeChain(len = 5) {
  let start = Math.floor(Math.random() * 20) + 10;
  const steps = [];
  let val = start;
  for (let i = 0; i < len; i++) {
    const ops = ['+','-'];
    const op = ops[Math.floor(Math.random() * ops.length)];
    const n = Math.floor(Math.random() * 10) + 1;
    steps.push({ op, n });
    val = op === '+' ? val + n : val - n;
  }
  return { start, steps, answer: val };
}

export default function ArithmeticChain() {
  const [game, setGame] = useState(() => makeChain());
  const [phase, setPhase] = useState('show');
  const [step, setStep] = useState(-1);
  const [input, setInput] = useState('');
  const [result, setResult] = useState(null);
  const [score, setScore] = useState(0);
  const [round, setRound] = useState(0);
  const ROUNDS = 5;

  const startReveal = () => { setPhase('reveal'); setStep(0); };

  const nextStep = () => {
    if (step + 1 >= game.steps.length) { setPhase('answer'); return; }
    setStep(s => s + 1);
  };

  const check = () => {
    const ok = parseInt(input) === game.answer;
    setResult(ok);
    if (ok) setScore(s => s + 1);
  };

  const next = () => {
    if (round + 1 >= ROUNDS) { setPhase('done'); return; }
    setGame(makeChain()); setPhase('show'); setStep(-1); setInput(''); setResult(null);
    setRound(r => r + 1);
  };

  if (phase === 'done') return (
    <div className="game-result">
      <h2>Score: {score}/{ROUNDS}</h2>
      <p>You tracked {score} chains correctly!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setScore(0); setRound(0); setGame(makeChain()); setPhase('show'); setStep(-1); setInput(''); setResult(null); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Round {round+1}/{ROUNDS}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem', minHeight: '120px' }}>
        {phase === 'show' && <div style={{ textAlign: 'center' }}><div style={{ fontSize: '2rem', fontWeight: 800 }}>Start: {game.start}</div><div style={{ color: '#6b6b6b', marginTop: '0.5rem' }}>Click "Reveal Steps" to see the operations one by one</div></div>}
        {phase === 'reveal' && <div style={{ textAlign: 'center' }}><div style={{ fontSize: '1.5rem', fontWeight: 800 }}>Start: {game.start}</div>{game.steps.slice(0, step+1).map((s, i) => <div key={i} style={{ fontSize: '1.3rem', fontWeight: 700, color: s.op === '+' ? '#16a34a' : '#dc2626' }}>{s.op} {s.n}</div>)}</div>}
        {phase === 'answer' && <div style={{ textAlign: 'center' }}><div style={{ fontSize: '1.3rem', fontWeight: 700 }}>Start: {game.start}</div>{game.steps.map((s, i) => <div key={i} style={{ color: s.op === '+' ? '#16a34a' : '#dc2626', fontWeight: 600 }}>{s.op} {s.n}</div>)}<div style={{ marginTop: '0.5rem', color: '#6b6b6b' }}>What is the final result?</div></div>}
      </div>
      {phase === 'show' && <button className="play-btn" style={{ width: '100%' }} onClick={startReveal}>Reveal Steps</button>}
      {phase === 'reveal' && (step + 1 < game.steps.length ? <button className="play-btn" style={{ width: '100%' }} onClick={nextStep}>Next Step</button> : <button className="play-btn" style={{ width: '100%' }} onClick={() => setPhase('answer')}>Enter Answer</button>)}
      {phase === 'answer' && result === null && (
        <div style={{ display: 'flex', gap: '0.75rem' }}>
          <input value={input} onChange={e => setInput(e.target.value)} onKeyDown={e => e.key === 'Enter' && input && check()} type="number" placeholder="Final result..." style={{ flex: 1, padding: '0.85rem 1rem', border: '1px solid #e0e0e0', borderRadius: '10px', fontSize: '1rem', fontFamily: 'inherit', outline: 'none' }} autoFocus />
          <button className="play-btn" disabled={!input} onClick={check}>Check</button>
        </div>
      )}
      {result !== null && (
        <div>
          <div style={{ padding: '1rem', borderRadius: '10px', background: result ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${result ? '#16a34a' : '#dc2626'}`, color: result ? '#166534' : '#991b1b', marginBottom: '1rem' }}>
            {result ? '✓ Correct!' : `✗ Answer was ${game.answer}`}
          </div>
          <button className="next-btn" onClick={next}>{round+1>=ROUNDS?'See Results':'Next Round →'}</button>
        </div>
      )}
    </div>
  );
}
