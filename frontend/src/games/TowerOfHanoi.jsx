import { useState } from 'react';

export default function TowerOfHanoi() {
  const N = 3;
  const [pegs, setPegs] = useState([[3,2,1],[],[]]);
  const [selected, setSelected] = useState(null);
  const [moves, setMoves] = useState(0);
  const [won, setWon] = useState(false);

  const minMoves = Math.pow(2, N) - 1;

  const clickPeg = (p) => {
    if (won) return;
    if (selected === null) {
      if (pegs[p].length === 0) return;
      setSelected(p);
    } else {
      if (selected === p) { setSelected(null); return; }
      const from = pegs[selected];
      const to = pegs[p];
      if (to.length > 0 && to[to.length-1] < from[from.length-1]) { setSelected(null); return; }
      const next = pegs.map(pg => [...pg]);
      next[p].push(next[selected].pop());
      setPegs(next); setMoves(m => m+1); setSelected(null);
      if (next[2].length === N) setWon(true);
    }
  };

  const reset = () => { setPegs([[3,2,1],[],[]]);setSelected(null);setMoves(0);setWon(false); };
  const colors = ['#0a0a0a','#555555','#aaaaaa'];

  return (
    <div>
      <div className="game-score-bar"><span>Moves: {moves} (min: {minMoves})</span>{won && <span style={{ color: '#16a34a', fontWeight: 700 }}>🎉 Solved!</span>}</div>
      <div style={{ fontSize: '0.85rem', color: '#6b6b6b', marginBottom: '1.5rem' }}>Click a peg to pick up its top disk, then click another peg to place it.</div>
      <div style={{ display: 'flex', gap: '1rem', justifyContent: 'center', marginBottom: '2rem' }}>
        {pegs.map((peg, p) => (
          <button key={p} onClick={() => clickPeg(p)} style={{ display: 'flex', flexDirection: 'column-reverse', alignItems: 'center', justifyContent: 'flex-start', minWidth: '120px', minHeight: '180px', border: `2px solid ${selected === p ? '#0a0a0a' : '#e0e0e0'}`, borderRadius: '12px', background: selected === p ? '#f0f0f0' : '#f8f8f8', padding: '0.5rem', gap: '4px', position: 'relative', cursor: 'pointer' }}>
            <div style={{ position: 'absolute', top: 0, bottom: 0, left: '50%', transform: 'translateX(-50%)', width: '4px', background: '#d0d0d0', borderRadius: '2px' }} />
            {peg.map((disk, i) => <div key={i} style={{ width: `${disk * 28}px`, height: '22px', background: colors[disk-1], borderRadius: '6px', zIndex: 1 }} />)}
            <div style={{ position: 'absolute', bottom: '0.4rem', fontSize: '0.72rem', color: '#6b6b6b', fontWeight: 600 }}>PEG {p+1}</div>
          </button>
        ))}
      </div>
      <button className="play-btn-secondary" onClick={reset}>Reset</button>
    </div>
  );
}
