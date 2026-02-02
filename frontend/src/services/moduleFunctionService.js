import api from './api';

export const moduleFunctionService = {
  async getAll() {
    const response = await api.get('/modulefunctions');
    return response.data;
  },

  async getByModuleId(moduleId) {
    const response = await api.get(`/modulefunctions/module/${moduleId}`);
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/modulefunctions/${id}`);
    return response.data;
  },

  async create(moduleFunction) {
    const response = await api.post('/modulefunctions', moduleFunction);
    return response.data;
  },

  async update(id, moduleFunction) {
    const response = await api.put(`/modulefunctions/${id}`, moduleFunction);
    return response.data;
  },

  async delete(id) {
    const response = await api.delete(`/modulefunctions/${id}`);
    return response.data;
  },

  async toggle(id, isEnabled) {
    const response = await api.put(`/modulefunctions/${id}/toggle`, { isEnabled });
    return response.data;
  },
};
