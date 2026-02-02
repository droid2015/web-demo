import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from '../components/core/Auth/Login';
import PrivateRoute from '../components/core/Auth/PrivateRoute';
import ModuleRoute from '../components/core/Auth/ModuleRoute';
import MainLayout from '../components/core/Layout/MainLayout';
import Dashboard from '../components/modules/Dashboard';
import UserList from '../components/modules/Users/UserList';
import ModuleManager from '../components/modules/Modules/ModuleManager';
import ProductList from '../components/modules/Products/ProductList';
import CongViecList from '../components/modules/CongViec/CongViecList';

const AppRoutes = () => {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        
        <Route path="/" element={
          <PrivateRoute>
            <MainLayout />
          </PrivateRoute>
        }>
          <Route index element={<Dashboard />} />
          <Route path="users" element={
            <ModuleRoute module="Core">
              <UserList />
            </ModuleRoute>
          } />
          <Route path="modules" element={
            <ModuleRoute module="Core">
              <ModuleManager />
            </ModuleRoute>
          } />
          <Route path="products" element={
            <ModuleRoute module="ProductManagement">
              <ProductList />
            </ModuleRoute>
          } />
          <Route path="congviec" element={
            <ModuleRoute module="QuanLyCongViec">
              <CongViecList />
            </ModuleRoute>
          } />
        </Route>
      </Routes>
    </Router>
  );
};

export default AppRoutes;
