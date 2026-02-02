import { AuthProvider } from './context/AuthContext';
import { PermissionProvider } from './context/PermissionContext';
import AppRoutes from './routes/AppRoutes';
import './App.css';

function App() {
  return (
    <AuthProvider>
      <PermissionProvider>
        <AppRoutes />
      </PermissionProvider>
    </AuthProvider>
  );
}

export default App;
