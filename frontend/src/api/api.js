import axios from 'axios';

const BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

const api = axios.create({ baseURL: BASE_URL });

export const getCategories = () => api.get('/api/categories').then(r => r.data);
export const getCategory = (id) => api.get(`/api/categories/${id}`).then(r => r.data);
export const getLesson = (id) => api.get(`/api/lessons/${id}`).then(r => r.data);
export const getLessonsBySubcategory = (subcategoryId) =>
  api.get(`/api/subcategories/${subcategoryId}/lessons`).then(r => r.data);
export const getQuiz = (lessonId) => api.get(`/api/lessons/${lessonId}/quiz`).then(r => r.data);
export const getMiniGame = (lessonId) => api.get(`/api/lessons/${lessonId}/minigame`).then(r => r.data);
