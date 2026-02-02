import { Navigate } from 'react-router-dom';
import { usePermissions } from '../../../context/PermissionContext';

const ModuleRoute = ({ module, children }) => {
  const { hasModule } = usePermissions();

  if (!hasModule(module)) {
    // Redirect to dashboard if user doesn't have access to this module
    return <Navigate to="/" replace />;
  }

  return children;
};

export default ModuleRoute;
