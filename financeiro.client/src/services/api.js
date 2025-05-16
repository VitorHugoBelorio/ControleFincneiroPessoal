// src/services/api.js
import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7277/v1', // ajuste a porta se necessário
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

export default api;
