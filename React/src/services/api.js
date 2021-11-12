import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:44309'
})

export default api;
