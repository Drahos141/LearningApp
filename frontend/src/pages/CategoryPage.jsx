import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCategory } from '../api/api';

export default function CategoryPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [category, setCategory] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getCategory(id)
      .then(setCategory)
      .catch(() => setError('Category not found.'))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (error) return <div className="page-center error-msg">{error}</div>;

  return (
    <div className="page">
      <div className="page-header" style={{ '--accent': category.color }}>
        <button className="back-btn" onClick={() => navigate('/')}>← Back</button>
        <span className="page-icon">{category.icon}</span>
        <h1>{category.name}</h1>
        <p className="page-subtitle">{category.description}</p>
      </div>

      <div className="page-body">
        {category.subcategories.map(sub => (
          <div key={sub.id} className="subcategory-section">
            <h2 className="subcategory-title">{sub.name}</h2>
            <p className="subcategory-desc">{sub.description}</p>
            <div className="lessons-grid">
              {sub.lessons.map(lesson => (
                <button
                  key={lesson.id}
                  className="lesson-card"
                  style={{ '--accent': category.color }}
                  onClick={() => navigate(`/lesson/${lesson.id}`)}
                >
                  <span className="lesson-number">Lesson {lesson.order}</span>
                  <h3 className="lesson-title">{lesson.title}</h3>
                  <span className="lesson-arrow">→</span>
                </button>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
