import { Link } from 'react-router-dom';
import './Sidebar.css';

const Sidebar = ({ isOpen, onClose }) => {
  return (
    <>
      <div className={`sidebar-overlay ${isOpen ? 'open' : ''}`} onClick={onClose}></div>
      <aside className={`sidebar ${isOpen ? 'open' : ''}`}>
        <nav>
          <ul>
            <li>
              <Link to="/" onClick={onClose}>Dashboard</Link>
            </li>
            <li>
              <Link to="/users" onClick={onClose}>Users</Link>
            </li>
            <li>
              <Link to="/modules" onClick={onClose}>Modules</Link>
            </li>
            <li>
              <Link to="/products" onClick={onClose}>Products</Link>
            </li>
            <li>
              <Link to="/congviec" onClick={onClose}>Công Việc</Link>
            </li>
          </ul>
        </nav>
      </aside>
    </>
  );
};

export default Sidebar;
