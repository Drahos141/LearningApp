import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getGame } from '../api/api';

import MemoryCards from '../games/MemoryCards';
import NumberSequence from '../games/NumberSequence';
import WordScramble from '../games/WordScramble';
import MathSprint from '../games/MathSprint';
import PatternMatch from '../games/PatternMatch';
import SimonSays from '../games/SimonSays';
import OddOneOut from '../games/OddOneOut';
import Anagram from '../games/Anagram';
import SudokuMini from '../games/SudokuMini';
import TowerOfHanoi from '../games/TowerOfHanoi';
import NBack from '../games/NBack';
import StroopTest from '../games/StroopTest';
import ArithmeticChain from '../games/ArithmeticChain';
import VisualRotation from '../games/VisualRotation';
import WordChain from '../games/WordChain';
import TrueFalseLogic from '../games/TrueFalseLogic';
import ColorMemory from '../games/ColorMemory';
import MatrixPattern from '../games/MatrixPattern';
import SpeedMath from '../games/SpeedMath';
import SpatialBlocks from '../games/SpatialBlocks';

const GAME_COMPONENTS = {
  'memory-cards': MemoryCards,
  'number-sequence': NumberSequence,
  'word-scramble': WordScramble,
  'math-sprint': MathSprint,
  'pattern-match': PatternMatch,
  'simon-says': SimonSays,
  'odd-one-out': OddOneOut,
  'anagram': Anagram,
  'sudoku-mini': SudokuMini,
  'tower-of-hanoi': TowerOfHanoi,
  'n-back': NBack,
  'stroop-test': StroopTest,
  'arithmetic-chain': ArithmeticChain,
  'visual-rotation': VisualRotation,
  'word-chain': WordChain,
  'true-false-logic': TrueFalseLogic,
  'color-memory': ColorMemory,
  'matrix-pattern': MatrixPattern,
  'speed-math': SpeedMath,
  'spatial-blocks': SpatialBlocks,
};

export default function GamePlay() {
  const { slug } = useParams();
  const navigate = useNavigate();
  const [game, setGame] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getGame(slug)
      .then(setGame)
      .catch(() => {})
      .finally(() => setLoading(false));
  }, [slug]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;

  const GameComponent = GAME_COMPONENTS[slug];

  return (
    <div className="gameplay">
      <div className="gameplay-header">
        <button className="back-btn" onClick={() => navigate('/games')}>← Back to Games</button>
        <h1 className="gameplay-title">{game?.name ?? slug}</h1>
        {game?.description && <p className="gameplay-desc">{game.description}</p>}
        <div style={{ display: 'flex', gap: '0.5rem', marginTop: '0.5rem', flexWrap: 'wrap' }}>
          {game?.type && <span className="badge">{game.type}</span>}
          {game?.difficulty && <span className={`badge badge-difficulty-${game.difficulty}`}>{game.difficulty}</span>}
        </div>
      </div>

      {game?.instructions && (
        <div className="gameplay-instructions">📖 {game.instructions}</div>
      )}

      {GameComponent ? (
        <GameComponent />
      ) : (
        <div style={{ textAlign: 'center', padding: '3rem', color: '#6b6b6b' }}>
          Game component not found for: {slug}
        </div>
      )}
    </div>
  );
}
