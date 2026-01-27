import api from './api';

export const congViecService = {
  async getAll() {
    const response = await api.get('/congviec');
    return response.data;
  },

  async getById(id) {
    const response = await api.get(`/congviec/${id}`);
    return response.data;
  },

  async getByTrangThai(trangThai) {
    const response = await api.get(`/congviec/trangthai/${trangThai}`);
    return response.data;
  },

  async create(congViec) {
    const response = await api.post('/congviec', congViec);
    return response.data;
  },

  async update(id, congViec) {
    const response = await api.put(`/congviec/${id}`, congViec);
    return response.data;
  },

  async delete(id) {
    const response = await api.delete(`/congviec/${id}`);
    return response.data;
  },
};
