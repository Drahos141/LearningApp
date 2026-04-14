import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCategory, getLessonsBySubcategory } from '../api/api';

export default function CategoryPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [category, setCategory] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getCategory(id).then(setCategory).catch(() => {}).finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (!category) return <div className="page-center error-msg">Category not found.</div>;

  return (
    <div className="page">
      <div className="page-header">
        <div className="nav-row">
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate('/')}>← Back</button>
        </div>
        <div className="page-icon">{category.icon}</div>
        <h1 style={{ fontSize: '1.9rem', fontWeight: 800, marginTop: '0.5rem' }}>{category.name}</h1>
        <p className="page-subtitle">{category.description}</p>
      </div>

      <div className="page-body">
        {category.subcategories?.map(sub => (
          <div key={sub._id || sub.id}>
            <h2 className="subcategory-title">{sub.name}</h2>
            <p className="subcategory-desc">{sub.description}</p>
            <div className="lessons-grid">
              {sub.lessons?.map(lesson => (
                <button
                  key={lesson._id || lesson.id}
                  className="lesson-card"
                  onClick={() => navigate(`/lesson/${lesson._id || lesson.id}`)}
                >
                  <span className="lesson-number">Lesson {lesson.order}</span>
                  <span className="lesson-title">{lesson.title}</span>
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
