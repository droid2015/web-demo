import { createContext, useContext } from 'react';
import { useAuth } from './AuthContext';

const PermissionContext = createContext(null);

export const PermissionProvider = ({ children }) => {
  const { user } = useAuth();

  const hasRole = (roleName) => {
    return user?.roles?.includes(roleName) || false;
  };

  const hasModule = (moduleName) => {
    return user?.modules?.some(m => m.name === moduleName) || false;
  };

  const getUserModules = () => {
    return user?.modules || [];
  };

  const value = {
    hasRole,
    hasModule,
    getUserModules,
  };

  return <PermissionContext.Provider value={value}>{children}</PermissionContext.Provider>;
};

export const usePermissions = () => {
  const context = useContext(PermissionContext);
  if (!context) {
    throw new Error('usePermissions must be used within a PermissionProvider');
  }
  return context;
};
