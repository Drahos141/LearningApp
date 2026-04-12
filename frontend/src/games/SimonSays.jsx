import { useState, useEffect, useRef } from 'react';

const COLORS = ['#111111','#555555','#aaaaaa','#dddddd'];
const LABELS = ['Black','Dark','Light','White'];

export default function SimonSays() {
  const [seq, setSeq] = useState([]);
  const [player, setPlayer] = useState([]);
  const [active, setActive] = useState(null);
  const [phase, setPhase] = useState('idle');
  const [level, setLevel] = useState(0);
  const [lost, setLost] = useState(false);
  const timeout = useRef(null);

  const playSeq = (sequence) => {
    setPhase('showing');
    let i = 0;
    const show = () => {
      if (i >= sequence.length) { setActive(null); setPhase('input'); return; }
      setActive(sequence[i]);
      timeout.current = setTimeout(() => { setActive(null); setTimeout(() => { i++; show(); }, 200); }, 600);
    };
    setTimeout(show, 500);
  };

  const start = () => {
    const first = Math.floor(Math.random() * 4);
    setSeq([first]); setPlayer([]); setLost(false); setLevel(1);
    playSeq([first]);
  };

  const press = (i) => {
    if (phase !== 'input') return;
    const next = [...player, i];
    const pos = next.length - 1;
    if (next[pos] !== seq[pos]) { setLost(true); setPhase('idle'); return; }
    if (next.length === seq.length) {
      const newSeq = [...seq, Math.floor(Math.random() * 4)];
      setSeq(newSeq); setPlayer([]); setLevel(l => l + 1);
      setTimeout(() => playSeq(newSeq), 800);
    } else {
      setPlayer(next);
    }
  };

  useEffect(() => () => clearTimeout(timeout.current), []);

  return (
    <div>
      <div className="game-score-bar"><span>Level: {level}</span>
        <span style={{ color: '#6b6b6b', fontSize: '0.83rem' }}>{phase === 'showing' ? 'Watch...' : phase === 'input' ? 'Your turn!' : ''}</span>
      </div>
      {lost && <div style={{ background: '#fef2f2', border: '1px solid #dc2626', borderRadius: '10px', padding: '1rem', color: '#991b1b', marginBottom: '1rem', textAlign: 'center' }}>Wrong! You reached level {level}. Try again!</div>}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem', marginBottom: '2rem' }}>
        {COLORS.map((c, i) => (
          <button key={i} onClick={() => press(i)} style={{
            height: '100px', borderRadius: '12px', border: '2px solid #e0e0e0',
            background: active === i ? '#444' : c,
            opacity: active === i ? 1 : (c === '#dddddd' ? 1 : 0.85),
            transform: active === i ? 'scale(0.95)' : 'scale(1)',
            transition: 'all 0.15s', color: c === '#dddddd' || c === '#aaaaaa' ? '#000' : '#fff',
            fontSize: '0.85rem', fontWeight: 600
          }}>{LABELS[i]}</button>
        ))}
      </div>
      {phase === 'idle' && <button className="play-btn" onClick={start} style={{ width: '100%' }}>Start Game</button>}
    </div>
  );
}
