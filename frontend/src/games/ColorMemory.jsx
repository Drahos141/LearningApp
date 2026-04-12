import { useState, useEffect, useRef } from 'react';

const COLORS = ['#111111','#555555','#aaaaaa','#dddddd'];
const NAMES = ['Black','Dark Gray','Light Gray','White'];

export default function ColorMemory() {
  const [seq, setSeq] = useState([]);
  const [player, setPlayer] = useState([]);
  const [phase, setPhase] = useState('idle');
  const [active, setActive] = useState(null);
  const [level, setLevel] = useState(0);
  const [lost, setLost] = useState(false);
  const timerRef = useRef();

  const showSeq = (s) => {
    setPhase('showing');
    let i = 0;
    const show = () => {
      if (i >= s.length) { setActive(null); setPhase('input'); return; }
      setActive(s[i]);
      timerRef.current = setTimeout(() => { setActive(null); setTimeout(() => { i++; show(); }, 300); }, 700);
    };
    setTimeout(show, 400);
  };

  const start = () => {
    const first = Math.floor(Math.random() * 4);
    setSeq([first]); setPlayer([]); setLost(false); setLevel(1); showSeq([first]);
  };

  const press = (i) => {
    if (phase !== 'input') return;
    const next = [...player, i];
    if (next[next.length-1] !== seq[next.length-1]) { setLost(true); setPhase('idle'); return; }
    if (next.length === seq.length) {
      const ns = [...seq, Math.floor(Math.random()*4)];
      setSeq(ns); setPlayer([]); setLevel(l=>l+1);
      setTimeout(() => showSeq(ns), 800);
    } else setPlayer(next);
  };

  useEffect(() => () => clearTimeout(timerRef.current), []);

  return (
    <div>
      <div className="game-score-bar"><span>Level: {level}</span><span style={{ color: '#6b6b6b', fontSize: '0.83rem' }}>{phase==='showing'?'Watch...':phase==='input'?'Repeat the sequence!':''}</span></div>
      {lost && <div style={{ background: '#fef2f2', border: '1px solid #dc2626', borderRadius: '10px', padding: '1rem', color: '#991b1b', marginBottom: '1rem', textAlign: 'center' }}>Wrong! You reached level {level}.</div>}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem', marginBottom: '2rem' }}>
        {COLORS.map((c, i) => (
          <button key={i} onClick={() => press(i)} style={{ height: '90px', borderRadius: '12px', border: '2px solid #e0e0e0', background: active === i ? '#000' : c, transform: active === i ? 'scale(0.95)' : 'scale(1)', transition: 'all 0.15s', color: c === '#dddddd' || c === '#aaaaaa' ? '#000' : '#fff', fontWeight: 600, fontSize: '0.85rem' }}>{NAMES[i]}</button>
        ))}
      </div>
      {phase === 'idle' && <button className="play-btn" onClick={start} style={{ width: '100%' }}>{lost ? 'Try Again' : 'Start Game'}</button>}
    </div>
  );
}
