import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from '../components/core/Auth/Login';
import PrivateRoute from '../components/core/Auth/PrivateRoute';
import MainLayout from '../components/core/Layout/MainLayout';
import Dashboard from '../components/modules/Dashboard';
import UserList from '../components/modules/Users/UserList';
import ModuleManager from '../components/modules/Modules/ModuleManager';
import ProductList from '../components/modules/Products/ProductList';

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
          <Route path="users" element={<UserList />} />
          <Route path="modules" element={<ModuleManager />} />
          <Route path="products" element={<ProductList />} />
        </Route>
      </Routes>
    </Router>
  );
};

export default AppRoutes;
