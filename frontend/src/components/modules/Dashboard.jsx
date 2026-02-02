import { usePermissions } from '../../context/PermissionContext';
import './Dashboard.css';

const Dashboard = () => {
  const { hasModule } = usePermissions();

  // Define dashboard cards with their associated modules
  const dashboardCards = [
    {
      title: 'Users',
      description: 'Manage system users and their permissions',
      module: 'Core'
    },
    {
      title: 'Modules',
      description: 'Enable or disable platform modules',
      module: 'Core'
    },
    {
      title: 'Products',
      description: 'Manage product catalog',
      module: 'ProductManagement'
    },
    {
      title: 'Công Việc',
      description: 'Quản lý công việc và theo dõi tiến độ',
      module: 'QuanLyCongViec'
    }
  ];

  // Filter cards based on user's module permissions
  const authorizedCards = dashboardCards.filter(card => hasModule(card.module));

  return (
    <div className="dashboard">
      <h1>Welcome to Platform</h1>
      <p>Modular platform system with C# Backend, ReactJS Frontend, and Oracle Database.</p>
      
      <div className="dashboard-cards">
        {authorizedCards.map((card) => (
          <div key={card.title} className="dashboard-card">
            <h3>{card.title}</h3>
            <p>{card.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Dashboard;
