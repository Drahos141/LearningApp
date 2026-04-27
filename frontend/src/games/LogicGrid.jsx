import { useState } from 'react';

const D = {
  bg: '#0a0a1a', card: '#10102a', border: '#1e1e4a', text: '#e8e8ff',
  muted: '#7070cc', accent: '#5555ff',
  yes: { bg: '#0a1a0a', border: '#00cc44', text: '#00ee55' },
  no:  { bg: '#1a0a0a', border: '#cc2222', text: '#ff4444' },
  sel: { bg: '#1a1a3a', border: '#5555ff', text: '#aaaaff' },
};

// ─── Puzzle data ───────────────────────────────────────────────────────────────
// Each puzzle: categories (array of {name, values[]}), clues[], solution (object: entity → {cat: value})
const PUZZLES = [
  {
    title: 'The Neighborhood',
    intro: '5 neighbors each live in a house of a different color, own a different pet, drink a different beverage, smoke a different brand, and have a different nationality.',
    categories: [
      { name: 'Nationality', values: ['English','Spanish','Ukrainian','Norwegian','Japanese'] },
      { name: 'Color',       values: ['Red','Blue','Green','Ivory','Yellow'] },
      { name: 'Pet',         values: ['Dog','Snail','Fox','Horse','Zebra'] },
      { name: 'Drink',       values: ['Coffee','Tea','Milk','OJ','Water'] },
      { name: 'Smoke',       values: ['Old Gold','Kools','Chesterfields','Lucky Strike','Parliaments'] },
    ],
    // Houses 1-5 (positions)
    entities: ['House 1','House 2','House 3','House 4','House 5'],
    solution: {
      'House 1': { Nationality:'Norwegian', Color:'Yellow',  Pet:'Fox',   Drink:'Water',  Smoke:'Kools' },
      'House 2': { Nationality:'Ukrainian', Color:'Blue',    Pet:'Horse', Drink:'Tea',    Smoke:'Chesterfields' },
      'House 3': { Nationality:'English',   Color:'Red',     Pet:'Snail', Drink:'Milk',   Smoke:'Old Gold' },
      'House 4': { Nationality:'Spanish',   Color:'Ivory',   Pet:'Dog',   Drink:'OJ',     Smoke:'Lucky Strike' },
      'House 5': { Nationality:'Japanese',  Color:'Green',   Pet:'Zebra', Drink:'Coffee', Smoke:'Parliaments' },
    },
    clues: [
      'The English person lives in the Red house.',
      'The Spanish person owns the Dog.',
      'Coffee is drunk in the Green house.',
      'The Ukrainian drinks Tea.',
      'The Green house is immediately to the right of the Ivory house.',
      'The Old Gold smoker owns Snails.',
      'Kools are smoked in the Yellow house.',
      'Milk is drunk in the middle house (House 3).',
      'The Norwegian lives in House 1.',
      'The Chesterfields smoker lives next to the Fox owner.',
      'Kools are smoked in the house next to the house with the Horse.',
      'The Lucky Strike smoker drinks OJ.',
      'The Japanese smokes Parliaments.',
      'The Norwegian lives next to the Blue house.',
      'Who drinks Water and who owns the Zebra? (Solve the full grid!)',
    ],
  },
  {
    title: 'The Research Team',
    intro: '4 researchers each work on a different project, use a different programming language, drink a different beverage, and sit in a different office (1–4).',
    categories: [
      { name: 'Name',     values: ['Alice','Bob','Carol','Dan'] },
      { name: 'Project',  values: ['AI','Security','Database','Frontend'] },
      { name: 'Language', values: ['Python','Rust','Java','JavaScript'] },
      { name: 'Drink',    values: ['Coffee','Tea','Water','Juice'] },
    ],
    entities: ['Office 1','Office 2','Office 3','Office 4'],
    solution: {
      'Office 1': { Name:'Alice', Project:'Security',  Language:'Rust',       Drink:'Tea' },
      'Office 2': { Name:'Dan',   Project:'Database',  Language:'Java',       Drink:'Coffee' },
      'Office 3': { Name:'Bob',   Project:'AI',        Language:'Python',     Drink:'Water' },
      'Office 4': { Name:'Carol', Project:'Frontend',  Language:'JavaScript', Drink:'Juice' },
    },
    clues: [
      'Alice is in a lower-numbered office than Bob.',
      'The AI researcher uses Python.',
      'The person in Office 2 drinks Coffee.',
      'Carol works on Frontend.',
      'Bob does not drink Tea.',
      'The Rust programmer works on Security.',
      'Dan is in Office 2.',
      'The person in Office 1 drinks Tea.',
      'Alice is not in Office 3.',
      'The JavaScript developer works on Frontend.',
    ],
  },
  {
    title: 'The Sports Club',
    intro: '4 athletes each play a different sport, wear a different color jersey, train on a different day, and are a different age.',
    categories: [
      { name: 'Name',   values: ['Mia','Leo','Sara','Jake'] },
      { name: 'Sport',  values: ['Tennis','Swimming','Cycling','Boxing'] },
      { name: 'Jersey', values: ['Red','Blue','Green','White'] },
      { name: 'Day',    values: ['Monday','Tuesday','Wednesday','Thursday'] },
    ],
    entities: ['Athlete 1','Athlete 2','Athlete 3','Athlete 4'],
    solution: {
      'Athlete 1': { Name:'Leo',  Sport:'Boxing',    Jersey:'Blue',  Day:'Monday' },
      'Athlete 2': { Name:'Mia',  Sport:'Swimming',  Jersey:'White', Day:'Thursday' },
      'Athlete 3': { Name:'Jake', Sport:'Cycling',   Jersey:'Green', Day:'Wednesday' },
      'Athlete 4': { Name:'Sara', Sport:'Tennis',    Jersey:'Red',   Day:'Tuesday' },
    },
    clues: [
      'Sara plays Tennis.',
      'The cyclist wears a Green jersey.',
      'Leo does not play Tennis or Swimming.',
      'The swimmer trains on Thursday.',
      'The person in Red trains on Tuesday.',
      'Leo trains on Monday.',
      'Mia does not wear Red or Green.',
      'Jake does not train on Monday or Tuesday.',
      'The boxer wears a Blue jersey.',
    ],
  },
  {
    title: 'The Apartment Building',
    intro: '4 tenants live on different floors (1–4), own different pets, drive different cars, and come from different cities.',
    categories: [
      { name: 'Name',  values: ['Omar','Petra','Quinn','Rosa'] },
      { name: 'Pet',   values: ['Cat','Dog','Parrot','Rabbit'] },
      { name: 'Car',   values: ['Tesla','BMW','Ford','Honda'] },
      { name: 'City',  values: ['Paris','Tokyo','Cairo','Sydney'] },
    ],
    entities: ['Floor 1','Floor 2','Floor 3','Floor 4'],
    solution: {
      'Floor 1': { Name:'Petra', Pet:'Rabbit', Car:'Honda',  City:'Cairo' },
      'Floor 2': { Name:'Quinn', Pet:'Parrot', Car:'BMW',    City:'Tokyo' },
      'Floor 3': { Name:'Rosa',  Pet:'Cat',    Car:'Ford',   City:'Paris' },
      'Floor 4': { Name:'Omar',  Pet:'Dog',    Car:'Tesla',  City:'Sydney' },
    },
    clues: [
      'Omar lives on the top floor.',
      'The Tesla driver is from Sydney.',
      'Petra does not own a Cat or Dog.',
      'The parrot owner lives on Floor 2.',
      'Quinn lives directly below Rosa.',
      'The Honda driver lives on Floor 1.',
      'The person from Cairo lives below the person from Tokyo.',
      'Rosa is from Paris.',
      'The cat owner drives a Ford.',
    ],
  },
];

// ─── Cell states: '' = unknown, 'Y' = yes, 'N' = no ──────────────────────────
function makeGrid(puzzle) {
  const grid = {};
  for (const entity of puzzle.entities) {
    grid[entity] = {};
    for (const cat of puzzle.categories) {
      grid[entity][cat.name] = {};
      for (const val of cat.values) {
        grid[entity][cat.name][val] = '';
      }
    }
  }
  return grid;
}

function checkSolution(grid, puzzle) {
  for (const entity of puzzle.entities) {
    for (const cat of puzzle.categories) {
      const expected = puzzle.solution[entity][cat.name];
      for (const val of cat.values) {
        const state = grid[entity][cat.name][val];
        if (val === expected) { if (state !== 'Y') return false; }
        else { if (state !== 'N') return false; }
      }
    }
  }
  return true;
}

function CellBtn({ state, onClick }) {
  let bg = D.card, border = D.border, content = '?', color = D.muted;
  if (state === 'Y') { bg = D.yes.bg; border = D.yes.border; content = '✓'; color = D.yes.text; }
  if (state === 'N') { bg = D.no.bg;  border = D.no.border;  content = '✗'; color = D.no.text; }
  return (
    <button onClick={onClick} style={{
      width: 34, height: 34, background: bg, border: `1px solid ${border}`,
      borderRadius: 6, color, fontWeight: 800, fontSize: '0.8rem',
      cursor: 'pointer', lineHeight: 1, padding: 0, flexShrink: 0,
    }}>{content}</button>
  );
}

export default function LogicGrid() {
  const [puzzleIdx, setPuzzleIdx] = useState(0);
  const [grid, setGrid]     = useState(() => makeGrid(PUZZLES[0]));
  const [solved, setSolved] = useState(false);
  const [revealed, setRevealed] = useState(false);
  const [showClues, setShowClues] = useState(true);
  const puzzle = PUZZLES[puzzleIdx];

  const cycleCell = (entity, catName, val) => {
    if (solved || revealed) return;
    setGrid(g => {
      const cur = g[entity][catName][val];
      const next = cur === '' ? 'Y' : cur === 'Y' ? 'N' : '';
      // enforce: at most one Y per (entity, category) and per (category-value, all entities)
      const updated = JSON.parse(JSON.stringify(g));
      updated[entity][catName][val] = next;
      // if setting Y, clear other Y's in same row (entity+cat)
      if (next === 'Y') {
        for (const v of puzzle.categories.find(c => c.name === catName).values) {
          if (v !== val) updated[entity][catName][v] = 'N';
        }
        // clear other Y's in same column (all entities, same cat+val)
        for (const ent of puzzle.entities) {
          if (ent !== entity) {
            if (updated[ent][catName][val] === 'Y') updated[ent][catName][val] = 'N';
          }
        }
      }
      const correct = checkSolution(updated, puzzle);
      if (correct) setSolved(true);
      return updated;
    });
  };

  const loadPuzzle = (idx) => {
    setPuzzleIdx(idx);
    setGrid(makeGrid(PUZZLES[idx]));
    setSolved(false);
    setRevealed(false);
  };

  const reveal = () => {
    const sol = {};
    for (const entity of puzzle.entities) {
      sol[entity] = {};
      for (const cat of puzzle.categories) {
        sol[entity][cat.name] = {};
        for (const val of cat.values) {
          sol[entity][cat.name][val] = val === puzzle.solution[entity][cat.name] ? 'Y' : 'N';
        }
      }
    }
    setGrid(sol);
    setRevealed(true);
  };

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text, fontFamily: 'inherit' }}>
      {/* Header */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem', flexWrap: 'wrap', gap: '0.5rem' }}>
        <div>
          <span style={{ color: D.muted, fontSize: '0.75rem', fontWeight: 700, letterSpacing: '0.1em' }}>PUZZLE {puzzleIdx + 1} / {PUZZLES.length}</span>
          <h3 style={{ margin: '0.2rem 0 0', fontSize: '1.1rem', fontWeight: 800 }}>{puzzle.title}</h3>
        </div>
        <div style={{ display: 'flex', gap: '0.5rem', flexWrap: 'wrap' }}>
          {PUZZLES.map((_, i) => (
            <button key={i} onClick={() => loadPuzzle(i)} style={{
              padding: '0.35rem 0.75rem', borderRadius: 8, fontWeight: 700, fontSize: '0.8rem',
              background: i === puzzleIdx ? D.accent : D.card, border: `1px solid ${i === puzzleIdx ? D.accent : D.border}`,
              color: i === puzzleIdx ? '#fff' : D.muted, cursor: 'pointer',
            }}>#{i + 1}</button>
          ))}
        </div>
      </div>

      {/* Intro */}
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 10, padding: '0.85rem 1rem', marginBottom: '1rem', fontSize: '0.88rem', lineHeight: 1.6, color: '#c0c0e8' }}>
        {puzzle.intro}
      </div>

      {/* Clues toggle */}
      <button onClick={() => setShowClues(v => !v)} style={{
        background: 'transparent', border: `1px solid ${D.border}`, borderRadius: 8,
        color: D.muted, padding: '0.35rem 0.85rem', fontSize: '0.8rem', fontWeight: 700,
        cursor: 'pointer', marginBottom: '0.75rem',
      }}>
        {showClues ? '▲ Hide Clues' : '▼ Show Clues'}
      </button>

      {showClues && (
        <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 10, padding: '0.85rem 1rem', marginBottom: '1rem' }}>
          <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.6rem' }}>CLUES</div>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(280px, 1fr))', gap: '0.3rem 1rem' }}>
            {puzzle.clues.map((c, i) => (
              <div key={i} style={{ fontSize: '0.85rem', lineHeight: 1.6, display: 'flex', gap: '0.5rem' }}>
                <span style={{ color: D.accent, fontWeight: 800, flexShrink: 0 }}>{i + 1}.</span>
                <span>{c}</span>
              </div>
            ))}
          </div>
        </div>
      )}

      {/* Instructions */}
      <div style={{ fontSize: '0.75rem', color: D.muted, marginBottom: '1rem', lineHeight: 1.6 }}>
        Click a cell to cycle: <span style={{ color: D.muted }}>? (unknown)</span> → <span style={{ color: D.yes.text }}>✓ (Yes)</span> → <span style={{ color: D.no.text }}>✗ (No)</span>. Each row and column can have only one ✓.
      </div>

      {/* Grid */}
      <div style={{ overflowX: 'auto', marginBottom: '1rem' }}>
        {puzzle.categories.map((cat) => (
          <div key={cat.name} style={{ marginBottom: '1.25rem' }}>
            <div style={{ fontSize: '0.72rem', color: D.accent, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.4rem' }}>{cat.name.toUpperCase()}</div>
            <table style={{ borderCollapse: 'collapse', minWidth: '100%' }}>
              <thead>
                <tr>
                  <th style={{ width: 90, textAlign: 'left', color: D.muted, fontSize: '0.72rem', fontWeight: 700, padding: '0 0.5rem 0.3rem 0', letterSpacing: '0.05em' }}>ENTITY</th>
                  {cat.values.map(v => (
                    <th key={v} style={{ color: D.text, fontSize: '0.72rem', fontWeight: 600, padding: '0 2px 0.3rem', textAlign: 'center', minWidth: 38, maxWidth: 70, whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }} title={v}>{v}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {puzzle.entities.map(entity => (
                  <tr key={entity}>
                    <td style={{ color: '#a0a0cc', fontSize: '0.78rem', fontWeight: 600, padding: '2px 0.5rem 2px 0', whiteSpace: 'nowrap' }}>{entity}</td>
                    {cat.values.map(val => (
                      <td key={val} style={{ padding: '2px', textAlign: 'center' }}>
                        <CellBtn state={grid[entity][cat.name][val]} onClick={() => cycleCell(entity, cat.name, val)} />
                      </td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ))}
      </div>

      {/* Controls */}
      <div style={{ display: 'flex', gap: '0.75rem', flexWrap: 'wrap' }}>
        <button onClick={() => { setGrid(makeGrid(puzzle)); setSolved(false); setRevealed(false); }} style={{
          padding: '0.6rem 1.2rem', background: D.card, border: `1px solid ${D.border}`, borderRadius: 10,
          color: D.muted, fontWeight: 700, fontSize: '0.85rem', cursor: 'pointer',
        }}>↺ Reset</button>
        {!revealed && !solved && (
          <button onClick={reveal} style={{
            padding: '0.6rem 1.2rem', background: '#1a1020', border: '1px solid #553355', borderRadius: 10,
            color: '#cc88cc', fontWeight: 700, fontSize: '0.85rem', cursor: 'pointer',
          }}>💡 Reveal Solution</button>
        )}
      </div>

      {/* Outcome banners */}
      {solved && !revealed && (
        <div style={{ marginTop: '1rem', padding: '1rem 1.25rem', borderRadius: 12, background: D.yes.bg, border: `1px solid ${D.yes.border}`, color: D.yes.text, fontWeight: 700, fontSize: '1.05rem', textAlign: 'center' }}>
          🎉 Puzzle Solved! Excellent deductive reasoning!
          {puzzleIdx + 1 < PUZZLES.length && (
            <button onClick={() => loadPuzzle(puzzleIdx + 1)} style={{ marginLeft: '1rem', background: D.yes.border, color: '#001a00', border: 'none', borderRadius: 8, padding: '0.4rem 1rem', fontWeight: 800, cursor: 'pointer', fontSize: '0.9rem' }}>Next Puzzle →</button>
          )}
        </div>
      )}
      {revealed && (
        <div style={{ marginTop: '1rem', padding: '1rem 1.25rem', borderRadius: 12, background: '#1a1020', border: '1px solid #553355', color: '#cc88cc', fontWeight: 600, fontSize: '0.9rem', textAlign: 'center' }}>
          Solution revealed. Study it, then try the next puzzle!
          {puzzleIdx + 1 < PUZZLES.length && (
            <button onClick={() => loadPuzzle(puzzleIdx + 1)} style={{ marginLeft: '1rem', background: '#553355', color: '#fff', border: 'none', borderRadius: 8, padding: '0.4rem 1rem', fontWeight: 800, cursor: 'pointer', fontSize: '0.9rem' }}>Next Puzzle →</button>
          )}
        </div>
      )}
    </div>
  );
}
