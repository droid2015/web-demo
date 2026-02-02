import { Link } from 'react-router-dom';
import { usePermissions } from '../../../context/PermissionContext';
import './Sidebar.css';

const Sidebar = ({ isOpen, onClose }) => {
  const { hasModule } = usePermissions();

  // Define module navigation items
  const navItems = [
    { path: '/', label: 'Dashboard', module: 'Core' },
    { path: '/users', label: 'Users', module: 'Core' },
    { path: '/modules', label: 'Modules', module: 'Core' },
    { path: '/module-functions', label: 'Chức năng Module', module: 'Core' },
    { path: '/products', label: 'Products', module: 'ProductManagement' },
    { path: '/congviec', label: 'Công Việc', module: 'QuanLyCongViec' },
  ];

  // Filter nav items based on user's module permissions
  const authorizedItems = navItems.filter(item => hasModule(item.module));

  return (
    <>
      <div className={`sidebar-overlay ${isOpen ? 'open' : ''}`} onClick={onClose}></div>
      <aside className={`sidebar ${isOpen ? 'open' : ''}`}>
        <nav>
          <ul>
            {authorizedItems.map((item) => (
              <li key={item.path}>
                <Link to={item.path} onClick={onClose}>{item.label}</Link>
              </li>
            ))}
          </ul>
        </nav>
      </aside>
    </>
  );
};

export default Sidebar;
