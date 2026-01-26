import './Dashboard.css';

const Dashboard = () => {
  return (
    <div className="dashboard">
      <h1>Welcome to Platform</h1>
      <p>Modular platform system with C# Backend, ReactJS Frontend, and Oracle Database.</p>
      
      <div className="dashboard-cards">
        <div className="dashboard-card">
          <h3>Users</h3>
          <p>Manage system users and their permissions</p>
        </div>
        <div className="dashboard-card">
          <h3>Modules</h3>
          <p>Enable or disable platform modules</p>
        </div>
        <div className="dashboard-card">
          <h3>Products</h3>
          <p>Manage product catalog</p>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
