import { useState } from 'react';

const PUZZLES = [
  { grid: [[1,0,3,0],[0,2,0,4],[4,0,2,0],[0,3,0,1]], solution: [[1,4,3,2],[3,2,1,4],[4,1,2,3],[2,3,4,1]] },
  { grid: [[0,1,0,2],[3,0,4,0],[0,4,0,3],[2,0,1,0]], solution: [[4,1,3,2],[3,2,4,1],[1,4,2,3],[2,3,1,4]] },
];

export default function SudokuMini() {
  const [pIdx] = useState(() => Math.floor(Math.random() * PUZZLES.length));
  const puzzle = PUZZLES[pIdx];
  const [cells, setCells] = useState(() => puzzle.grid.map(r => [...r]));
  const [checked, setChecked] = useState(false);
  const [correct, setCorrect] = useState(false);

  const set = (r, c, v) => {
    if (puzzle.grid[r][c] !== 0) return;
    const n = parseInt(v);
    if (isNaN(n) || n < 1 || n > 4) return;
    const next = cells.map(row => [...row]);
    next[r][c] = n;
    setCells(next); setChecked(false);
  };

  const check = () => {
    const ok = cells.every((row, r) => row.every((v, c) => v === puzzle.solution[r][c]));
    setChecked(true); setCorrect(ok);
  };

  const reset = () => { setCells(puzzle.grid.map(r => [...r])); setChecked(false); };

  return (
    <div>
      <div style={{ fontSize: '0.85rem', color: '#6b6b6b', marginBottom: '1rem' }}>Fill the 4×4 grid so each row, column, and 2×2 box has the numbers 1–4.</div>
      <div style={{ display: 'inline-grid', gridTemplateColumns: 'repeat(4,1fr)', gap: '3px', background: '#0a0a0a', padding: '3px', borderRadius: '10px', marginBottom: '1.5rem' }}>
        {cells.map((row, r) => row.map((v, c) => {
          const fixed = puzzle.grid[r][c] !== 0;
          const borderRight = c === 1 ? '2px solid #0a0a0a' : '';
          const borderBottom = r === 1 ? '2px solid #0a0a0a' : '';
          return (
            <input key={`${r}-${c}`} value={v === 0 ? '' : v} onChange={e => set(r, c, e.target.value)} maxLength={1} readOnly={fixed} style={{ width: '52px', height: '52px', textAlign: 'center', fontSize: '1.3rem', fontWeight: fixed ? 800 : 400, fontFamily: 'inherit', border: 'none', outline: 'none', background: fixed ? '#f0f0f0' : '#fff', cursor: fixed ? 'default' : 'text', borderRight, borderBottom }} />
          );
        }))}
      </div>
      {checked && <div style={{ padding: '0.9rem 1.1rem', borderRadius: '10px', background: correct ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${correct ? '#16a34a' : '#dc2626'}`, color: correct ? '#166534' : '#991b1b', marginBottom: '1rem' }}>{correct ? '✓ Correct! Well done!' : '✗ Not quite right. Keep trying!'}</div>}
      <div style={{ display: 'flex', gap: '0.75rem' }}>
        <button className="play-btn" onClick={check}>Check</button>
        <button className="play-btn-secondary" onClick={reset}>Reset</button>
      </div>
    </div>
  );
}
