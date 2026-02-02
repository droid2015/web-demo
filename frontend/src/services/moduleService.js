import api from './api';

export const moduleService = {
  async getAll() {
    const response = await api.get('/modules');
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/modules/${id}`);
    return response.data;
  },

  async register(module) {
    const response = await api.post('/modules', module);
    return response.data;
  },

  async update(id, module) {
    const response = await api.put(`/modules/${id}`, module);
    return response.data;
  },

  async toggle(id, isEnabled) {
    const response = await api.put(`/modules/${id}/toggle`, { isEnabled });
    return response.data;
  },
};
