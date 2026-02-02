# Tài Liệu Kỹ Thuật Frontend - React + Vite
# Frontend Technical Documentation - React + Vite

> Platform UI built with React 19, Vite 7, React Router 7, and Axios

---

## Mục lục / Table of Contents

1. [Tổng quan hệ thống / System Overview](#tổng-quan-hệ-thống--system-overview)
2. [Kiến trúc ứng dụng / Application Architecture](#kiến-trúc-ứng-dụng--application-architecture)
3. [Cấu trúc thư mục / Directory Structure](#cấu-trúc-thư-mục--directory-structure)
4. [Công nghệ sử dụng / Technology Stack](#công-nghệ-sử-dụng--technology-stack)
5. [Luồng xác thực / Authentication Flow](#luồng-xác-thực--authentication-flow)
6. [Quản lý quyền / Permission Management](#quản-lý-quyền--permission-management)
7. [Tích hợp API / API Integration](#tích-hợp-api--api-integration)
8. [Routing và Navigation](#routing-và-navigation)
9. [Quản lý State / State Management](#quản-lý-state--state-management)
10. [Components Chi tiết / Component Details](#components-chi-tiết--component-details)
11. [Cấu hình môi trường / Environment Configuration](#cấu-hình-môi-trường--environment-configuration)
12. [Build và Deployment](#build-và-deployment)
13. [Best Practices](#best-practices)
14. [Troubleshooting](#troubleshooting)

---

## Tổng quan hệ thống / System Overview

### Mô tả / Description

Frontend của Platform là một Single Page Application (SPA) được xây dựng bằng React và Vite, cung cấp giao diện người dùng hiện đại, responsive và module-based. Ứng dụng tích hợp với backend .NET 8 API thông qua RESTful endpoints và sử dụng JWT (JSON Web Tokens) cho xác thực.

The Platform Frontend is a Single Page Application (SPA) built with React and Vite, providing a modern, responsive, and module-based user interface. The application integrates with a .NET 8 backend API through RESTful endpoints and uses JWT (JSON Web Tokens) for authentication.

### Tính năng chính / Key Features

- **Xác thực JWT / JWT Authentication**: Secure login with token-based authentication
- **Kiểm soát truy cập dựa trên vai trò / Role-Based Access Control (RBAC)**: Permission-based UI rendering
- **Kiến trúc module / Modular Architecture**: Dynamic module loading based on user permissions
- **Giao diện responsive / Responsive UI**: Mobile-friendly layout with collapsible sidebar
- **Real-time validation**: Form validation and error handling
- **Lazy loading**: Optimized performance with code splitting

---

## Kiến trúc ứng dụng / Application Architecture

### Sơ đồ kiến trúc / Architecture Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                         Browser                              │
│  ┌───────────────────────────────────────────────────────┐  │
│  │              React Application (SPA)                   │  │
│  │  ┌─────────────────────────────────────────────────┐  │  │
│  │  │           App.jsx (Entry Point)                 │  │  │
│  │  │  ┌──────────────┐    ┌──────────────────────┐  │  │  │
│  │  │  │ AuthContext  │    │ PermissionContext    │  │  │  │
│  │  │  └──────────────┘    └──────────────────────┘  │  │  │
│  │  │                                                 │  │  │
│  │  │              AppRoutes (React Router)          │  │  │
│  │  │  ┌──────────────────────────────────────────┐  │  │  │
│  │  │  │  PrivateRoute / ModuleRoute Wrappers    │  │  │  │
│  │  │  └──────────────────────────────────────────┘  │  │  │
│  │  │                                                 │  │  │
│  │  │  ┌──────────────┐    ┌──────────────────────┐  │  │  │
│  │  │  │  MainLayout  │───▶│ Header / Sidebar     │  │  │  │
│  │  │  └──────────────┘    └──────────────────────┘  │  │  │
│  │  │         │                                       │  │  │
│  │  │         ▼                                       │  │  │
│  │  │  ┌────────────────────────────────────────┐    │  │  │
│  │  │  │  Module Components                     │    │  │  │
│  │  │  │  - Dashboard, Users, Products, etc.   │    │  │  │
│  │  │  └────────────────────────────────────────┘    │  │  │
│  │  │                                                 │  │  │
│  │  │  ┌────────────────────────────────────────┐    │  │  │
│  │  │  │  Service Layer (Axios)                 │    │  │  │
│  │  │  │  - API interceptors                    │    │  │  │
│  │  │  │  - JWT token injection                 │    │  │  │
│  │  │  └────────────────────────────────────────┘    │  │  │
│  │  └─────────────────────────────────────────────────┘  │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                              │
                              │ HTTP/HTTPS (REST API)
                              │ JWT Token in Headers
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    Backend API (.NET 8)                      │
│                  http://localhost:5000/api                   │
└─────────────────────────────────────────────────────────────┘
```

### Các lớp kiến trúc / Architecture Layers

1. **Presentation Layer**: React components, CSS styling
2. **State Management Layer**: Context API providers (Auth, Permissions)
3. **Routing Layer**: React Router with protected routes
4. **Service Layer**: Axios-based API communication
5. **Configuration Layer**: Environment variables, build configs

---

## Cấu trúc thư mục / Directory Structure

```
frontend/
├── public/                          # Static public assets
│   └── vite.svg                     # Vite logo
│
├── src/                             # Source code
│   ├── assets/                      # Static assets (images, icons)
│   │   └── react.svg
│   │
│   ├── components/                  # React components
│   │   ├── core/                    # Core/shared components
│   │   │   ├── Auth/
│   │   │   │   ├── Login.jsx        # Login page
│   │   │   │   ├── Login.css
│   │   │   │   ├── PrivateRoute.jsx # Authentication guard
│   │   │   │   └── ModuleRoute.jsx  # Module permission guard
│   │   │   ├── Common/
│   │   │   │   ├── Loading.jsx      # Loading spinner
│   │   │   │   └── Loading.css
│   │   │   └── Layout/
│   │   │       ├── MainLayout.jsx   # Main app layout
│   │   │       ├── MainLayout.css
│   │   │       ├── Header.jsx       # Top navigation
│   │   │       ├── Header.css
│   │   │       ├── Sidebar.jsx      # Side navigation
│   │   │       └── Sidebar.css
│   │   │
│   │   └── modules/                 # Feature modules
│   │       ├── Dashboard/
│   │       │   ├── Dashboard.jsx
│   │       │   └── Dashboard.css
│   │       ├── Users/
│   │       │   ├── UserList.jsx
│   │       │   └── UserList.css
│   │       ├── Products/
│   │       │   ├── ProductList.jsx
│   │       │   └── ProductList.css
│   │       ├── CongViec/            # Task management
│   │       │   ├── CongViecList.jsx
│   │       │   ├── CongViecForm.jsx
│   │       │   ├── CongViecList.css
│   │       │   └── CongViecForm.css
│   │       └── Modules/
│   │           ├── ModuleManager.jsx
│   │           └── ModuleManager.css
│   │
│   ├── context/                     # React Context providers
│   │   ├── AuthContext.jsx          # Authentication context
│   │   └── PermissionContext.jsx    # Permission context
│   │
│   ├── routes/                      # Route configuration
│   │   └── AppRoutes.jsx            # Main routing setup
│   │
│   ├── services/                    # API service layer
│   │   ├── api.js                   # Axios config & interceptors
│   │   ├── authService.js           # Auth API calls
│   │   ├── userService.js           # User CRUD operations
│   │   ├── moduleService.js         # Module management
│   │   ├── productService.js        # Product CRUD operations
│   │   └── congViecService.js       # Task management API
│   │
│   ├── App.jsx                      # Root component
│   ├── App.css                      # Root styles
│   ├── main.jsx                     # Application entry point
│   └── index.css                    # Global styles
│
├── .env.example                     # Environment template
├── .gitignore                       # Git ignore rules
├── Dockerfile                       # Docker container config
├── eslint.config.js                 # ESLint configuration
├── index.html                       # HTML template
├── nginx.conf                       # Nginx reverse proxy config
├── package.json                     # Dependencies & scripts
├── README.md                        # User documentation
├── TECHNICAL_DOCUMENTATION.md       # This file
└── vite.config.js                   # Vite build configuration
```

---

## Công nghệ sử dụng / Technology Stack

### Core Dependencies

| Technology | Version | Purpose |
|------------|---------|---------|
| **React** | 19.2.0 | UI framework |
| **React DOM** | 19.2.0 | DOM rendering |
| **Vite** | 7.2.4 | Build tool & dev server |
| **React Router** | 7.13.0 | Client-side routing |
| **Axios** | 1.13.3 | HTTP client for API calls |

### Development Dependencies

| Technology | Version | Purpose |
|------------|---------|---------|
| **ESLint** | 9.39.1 | Code linting |
| **@vitejs/plugin-react** | 5.1.1 | React support for Vite |
| **TypeScript Types** | Latest | Type definitions for development |

### Tại sao chọn Vite? / Why Vite?

✅ **Tốc độ phát triển nhanh / Fast Development**: Hot Module Replacement (HMR) cực nhanh  
✅ **Build tối ưu / Optimized Builds**: Sử dụng Rollup cho production builds  
✅ **Cấu hình đơn giản / Simple Configuration**: Zero-config cho hầu hết use cases  
✅ **ES Modules native / Native ES Modules**: Không cần bundling trong dev mode  
✅ **Plugin ecosystem**: Rich plugin ecosystem  

---

## Luồng xác thực / Authentication Flow

### Sơ đồ luồng / Flow Diagram

```
┌─────────────┐
│   Browser   │
└──────┬──────┘
       │
       │ 1. User enters credentials
       ▼
┌──────────────────┐
│   Login.jsx      │
└────────┬─────────┘
         │
         │ 2. authService.login(username, password)
         ▼
┌────────────────────────┐
│   authService.js       │
│   POST /api/auth/login │
└────────┬───────────────┘
         │
         │ 3. HTTP Request with credentials
         ▼
┌────────────────────────┐
│    Backend API         │
│  Validates credentials │
└────────┬───────────────┘
         │
         │ 4. Returns JWT token + user data
         ▼
┌────────────────────────┐
│   authService.js       │
│  Stores to localStorage│
│  - token               │
│  - user (roles, etc.)  │
└────────┬───────────────┘
         │
         │ 5. Updates AuthContext
         ▼
┌────────────────────────┐
│    AuthContext         │
│  Sets: isAuthenticated │
│        user            │
└────────┬───────────────┘
         │
         │ 6. Navigate to /
         ▼
┌────────────────────────┐
│   PrivateRoute checks  │
│   isAuthenticated      │
└────────┬───────────────┘
         │
         │ 7. If authenticated
         ▼
┌────────────────────────┐
│   MainLayout renders   │
│   with Dashboard       │
└────────────────────────┘
```

### Chi tiết implementation / Implementation Details

#### 1. Login Component (`src/components/core/Auth/Login.jsx`)

```jsx
const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    const data = await authService.login(username, password);
    login(data.user); // Update AuthContext
    navigate('/');    // Redirect to dashboard
  } catch (error) {
    setError(error.message);
  }
};
```

#### 2. Auth Service (`src/services/authService.js`)

```javascript
export const login = async (username, password) => {
  const response = await api.post('/auth/login', { username, password });
  
  // Store JWT token
  localStorage.setItem('token', response.data.token);
  
  // Store user data
  localStorage.setItem('user', JSON.stringify(response.data.user));
  
  return response.data;
};
```

#### 3. API Interceptor (`src/services/api.js`)

```javascript
// Automatically inject JWT token into all requests
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Handle 401 Unauthorized errors
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);
```

#### 4. AuthContext Provider (`src/context/AuthContext.jsx`)

```jsx
export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Initialize auth state from localStorage
    const storedUser = authService.getCurrentUser();
    if (storedUser) {
      setUser(storedUser);
    }
    setLoading(false);
  }, []);

  const login = (userData) => setUser(userData);
  const logout = () => {
    authService.logout();
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ 
      user, 
      loading, 
      isAuthenticated: !!user, 
      login, 
      logout 
    }}>
      {children}
    </AuthContext.Provider>
  );
};
```

#### 5. PrivateRoute Guard (`src/components/core/Auth/PrivateRoute.jsx`)

```jsx
const PrivateRoute = ({ children }) => {
  const { isAuthenticated, loading } = useAuth();

  if (loading) {
    return <Loading />;
  }

  return isAuthenticated ? children : <Navigate to="/login" />;
};
```

---

## Quản lý quyền / Permission Management

### Kiến trúc phân quyền / Permission Architecture

Hệ thống sử dụng kiến trúc **Role-Based Access Control (RBAC)** kết hợp với **Module-Based Permissions**.

The system uses **Role-Based Access Control (RBAC)** combined with **Module-Based Permissions**.

### Cấu trúc dữ liệu / Data Structure

```javascript
{
  "user": {
    "id": 1,
    "username": "admin",
    "roles": [
      {
        "id": 1,
        "name": "Administrator",
        "description": "Full system access"
      }
    ],
    "modules": [
      {
        "id": 1,
        "name": "Core",
        "isEnabled": true
      },
      {
        "id": 2,
        "name": "ProductManagement",
        "isEnabled": true
      }
    ]
  }
}
```

### PermissionContext Implementation

```jsx
export const PermissionProvider = ({ children }) => {
  const { user } = useAuth();

  // Check if user has specific role
  const hasRole = (roleName) => {
    return user?.roles?.some(role => role.name === roleName) || false;
  };

  // Check if user has access to module
  const hasModule = (moduleName) => {
    return user?.modules?.some(
      module => module.name === moduleName && module.isEnabled
    ) || false;
  };

  // Get all user modules
  const getUserModules = () => {
    return user?.modules?.filter(m => m.isEnabled) || [];
  };

  return (
    <PermissionContext.Provider value={{ 
      hasRole, 
      hasModule, 
      getUserModules 
    }}>
      {children}
    </PermissionContext.Provider>
  );
};
```

### ModuleRoute Guard

```jsx
const ModuleRoute = ({ children, requiredModule }) => {
  const { hasModule } = usePermissions();

  if (!hasModule(requiredModule)) {
    return (
      <div>
        <h2>Access Denied</h2>
        <p>You don't have access to the {requiredModule} module.</p>
      </div>
    );
  }

  return children;
};
```

### Sử dụng trong Routes / Usage in Routes

```jsx
<Route
  path="users"
  element={
    <ModuleRoute requiredModule="Core">
      <UserList />
    </ModuleRoute>
  }
/>
<Route
  path="products"
  element={
    <ModuleRoute requiredModule="ProductManagement">
      <ProductList />
    </ModuleRoute>
  }
/>
```

---

## Tích hợp API / API Integration

### API Service Configuration (`src/services/api.js`)

```javascript
import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5000/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor: Inject JWT token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor: Handle errors
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Unauthorized: Clear token and redirect to login
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;
```

### Service Layer Pattern

Mỗi module có một service file riêng để quản lý API calls:

Each module has a dedicated service file to manage API calls:

#### Example: User Service (`src/services/userService.js`)

```javascript
import api from './api';

const userService = {
  // Get all users
  getAll: async () => {
    const response = await api.get('/users');
    return response.data;
  },

  // Get user by ID
  getById: async (id) => {
    const response = await api.get(`/users/${id}`);
    return response.data;
  },

  // Create new user
  create: async (userData) => {
    const response = await api.post('/users', userData);
    return response.data;
  },

  // Update user
  update: async (id, userData) => {
    const response = await api.put(`/users/${id}`, userData);
    return response.data;
  },

  // Delete user
  delete: async (id) => {
    const response = await api.delete(`/users/${id}`);
    return response.data;
  },
};

export default userService;
```

### API Endpoints

| Endpoint | Method | Purpose | Auth Required |
|----------|--------|---------|---------------|
| `/api/auth/login` | POST | User authentication | No |
| `/api/users` | GET | Get all users | Yes |
| `/api/users/{id}` | GET | Get user by ID | Yes |
| `/api/users` | POST | Create user | Yes |
| `/api/users/{id}` | PUT | Update user | Yes |
| `/api/users/{id}` | DELETE | Delete user | Yes |
| `/api/modules` | GET | Get all modules | Yes |
| `/api/modules/{id}/toggle` | POST | Enable/disable module | Yes |
| `/api/products` | GET/POST/PUT/DELETE | Product CRUD | Yes |
| `/api/congviec` | GET/POST/PUT/DELETE | Task CRUD | Yes |

---

## Routing và Navigation

### Route Configuration (`src/routes/AppRoutes.jsx`)

```jsx
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import PrivateRoute from '../components/core/Auth/PrivateRoute';
import ModuleRoute from '../components/core/Auth/ModuleRoute';

const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* Public routes */}
        <Route path="/login" element={<Login />} />

        {/* Protected routes */}
        <Route
          path="/"
          element={
            <PrivateRoute>
              <MainLayout />
            </PrivateRoute>
          }
        >
          {/* Nested routes within MainLayout */}
          <Route index element={<Dashboard />} />
          
          <Route
            path="users"
            element={
              <ModuleRoute requiredModule="Core">
                <UserList />
              </ModuleRoute>
            }
          />
          
          <Route
            path="modules"
            element={
              <ModuleRoute requiredModule="Core">
                <ModuleManager />
              </ModuleRoute>
            }
          />
          
          <Route
            path="products"
            element={
              <ModuleRoute requiredModule="ProductManagement">
                <ProductList />
              </ModuleRoute>
            }
          />
          
          <Route
            path="congviec"
            element={
              <ModuleRoute requiredModule="QuanLyCongViec">
                <CongViecList />
              </ModuleRoute>
            }
          />
        </Route>

        {/* Catch-all: redirect to home */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
};
```

### Navigation Hierarchy

```
/login (public)
/
├── /                  → Dashboard (default)
├── /users             → UserList (Core module required)
├── /modules           → ModuleManager (Core module required)
├── /products          → ProductList (ProductManagement module required)
└── /congviec          → CongViecList (QuanLyCongViec module required)
```

### Sidebar Navigation (`src/components/core/Layout/Sidebar.jsx`)

```jsx
const Sidebar = ({ isCollapsed, toggleSidebar }) => {
  const { hasModule } = usePermissions();

  return (
    <nav className={`sidebar ${isCollapsed ? 'collapsed' : ''}`}>
      <ul>
        <li><Link to="/">Dashboard</Link></li>
        
        {hasModule('Core') && (
          <>
            <li><Link to="/users">Users</Link></li>
            <li><Link to="/modules">Modules</Link></li>
          </>
        )}
        
        {hasModule('ProductManagement') && (
          <li><Link to="/products">Products</Link></li>
        )}
        
        {hasModule('QuanLyCongViec') && (
          <li><Link to="/congviec">Công Việc</Link></li>
        )}
      </ul>
    </nav>
  );
};
```

---

## Quản lý State / State Management

### Context API Architecture

Ứng dụng sử dụng **React Context API** thay vì Redux để quản lý state toàn cục.

The application uses **React Context API** instead of Redux for global state management.

### Lý do không dùng Redux / Why Not Redux?

✅ **Đơn giản hơn / Simpler**: Ít boilerplate code  
✅ **Built-in**: Không cần thư viện bên ngoài  
✅ **Đủ cho app size này / Sufficient for app size**: App không có state phức tạp  
✅ **Performance tốt / Good performance**: Context API đủ nhanh cho use case này  

### Context Providers

#### 1. AuthContext (`src/context/AuthContext.jsx`)

**State:**
- `user`: Current user object (null if not logged in)
- `loading`: Boolean indicating auth check in progress
- `isAuthenticated`: Computed from user existence

**Methods:**
- `login(userData)`: Set user after successful login
- `logout()`: Clear user and redirect to login

**Usage:**
```jsx
const { user, isAuthenticated, login, logout } = useAuth();
```

#### 2. PermissionContext (`src/context/PermissionContext.jsx`)

**Methods:**
- `hasRole(roleName)`: Check if user has specific role
- `hasModule(moduleName)`: Check if user has module access
- `getUserModules()`: Get list of enabled modules

**Usage:**
```jsx
const { hasRole, hasModule, getUserModules } = usePermissions();

if (hasRole('Administrator')) {
  // Show admin features
}

if (hasModule('ProductManagement')) {
  // Show product features
}
```

### Local Component State

Components use `useState` and `useEffect` for local state:

```jsx
const UserList = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadUsers();
  }, []);

  const loadUsers = async () => {
    try {
      setLoading(true);
      const data = await userService.getAll();
      setUsers(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  // Component render logic...
};
```

---

## Components Chi tiết / Component Details

### Core Components

#### 1. Login Component

**Path:** `src/components/core/Auth/Login.jsx`

**Responsibility:** Handle user authentication

**Features:**
- Form validation
- Error handling
- Auto-redirect after login
- Loading state

**Props:** None

**Example:**
```jsx
<Login />
```

#### 2. PrivateRoute Component

**Path:** `src/components/core/Auth/PrivateRoute.jsx`

**Responsibility:** Protect routes requiring authentication

**Props:**
- `children`: React nodes to render if authenticated

**Example:**
```jsx
<PrivateRoute>
  <Dashboard />
</PrivateRoute>
```

#### 3. ModuleRoute Component

**Path:** `src/components/core/Auth/ModuleRoute.jsx`

**Responsibility:** Protect routes requiring module permission

**Props:**
- `children`: React nodes to render if authorized
- `requiredModule`: Module name required for access

**Example:**
```jsx
<ModuleRoute requiredModule="ProductManagement">
  <ProductList />
</ModuleRoute>
```

#### 4. MainLayout Component

**Path:** `src/components/core/Layout/MainLayout.jsx`

**Responsibility:** Main application layout with header, sidebar, and content area

**Features:**
- Responsive sidebar (collapsible)
- Header with logout
- `<Outlet />` for nested routes

**Example:**
```jsx
<Route path="/" element={<MainLayout />}>
  <Route index element={<Dashboard />} />
  {/* Other nested routes */}
</Route>
```

#### 5. Loading Component

**Path:** `src/components/core/Common/Loading.jsx`

**Responsibility:** Display loading spinner

**Props:** None

**Example:**
```jsx
{loading && <Loading />}
```

### Module Components

#### 1. Dashboard

**Path:** `src/components/modules/Dashboard/Dashboard.jsx`

**Purpose:** Main dashboard overview page

**Features:**
- Welcome message
- Quick stats
- Recent activities

#### 2. UserList

**Path:** `src/components/modules/Users/UserList.jsx`

**Purpose:** User management interface

**Features:**
- Display all users
- CRUD operations
- Role display

**API Integration:**
```javascript
import userService from '../../../services/userService';

const loadUsers = async () => {
  const data = await userService.getAll();
  setUsers(data);
};
```

#### 3. ProductList

**Path:** `src/components/modules/Products/ProductList.jsx`

**Purpose:** Product catalog management

**Features:**
- Product listing
- Add/Edit/Delete products
- Search and filter

#### 4. CongViecList & CongViecForm

**Path:** 
- `src/components/modules/CongViec/CongViecList.jsx`
- `src/components/modules/CongViec/CongViecForm.jsx`

**Purpose:** Task/Job management

**Features:**
- Task listing with status
- Create/edit tasks
- Assign tasks to users
- Filter by status, creator, assignee

**API Integration:**
```javascript
import congViecService from '../../../services/congViecService';

// Get all tasks
const tasks = await congViecService.getAll();

// Get tasks by status
const pendingTasks = await congViecService.getByTrangThai('PENDING');

// Get my tasks
const myTasks = await congViecService.getMyTasks();
```

#### 5. ModuleManager

**Path:** `src/components/modules/Modules/ModuleManager.jsx`

**Purpose:** Enable/disable platform modules

**Features:**
- List all modules
- Toggle module status
- View module details

**API Integration:**
```javascript
import moduleService from '../../../services/moduleService';

const toggleModule = async (moduleId, currentStatus) => {
  await moduleService.toggle(moduleId, !currentStatus);
  loadModules(); // Refresh list
};
```

---

## Cấu hình môi trường / Environment Configuration

### Environment Variables

**File:** `.env`

**Template:** `.env.example`

```bash
# API Backend URL
VITE_API_URL=http://localhost:5000/api
```

### Vite Environment Variable Access

```javascript
// Accessing env vars in code
const apiUrl = import.meta.env.VITE_API_URL;
```

### Multi-Environment Setup

**Development:**
```bash
# .env.development
VITE_API_URL=http://localhost:5000/api
```

**Production:**
```bash
# .env.production
VITE_API_URL=https://api.production.com/api
```

**Staging:**
```bash
# .env.staging
VITE_API_URL=https://api.staging.com/api
```

### Build Commands

```bash
# Development
npm run dev

# Production build
npm run build

# Staging build
npm run build -- --mode staging
```

---

## Build và Deployment

### Development Server

```bash
# Install dependencies
npm install

# Start dev server (default port: 5173)
npm run dev

# Start dev server with custom port
npm run dev -- --port 3000

# Start dev server with host exposed
npm run dev -- --host
```

### Production Build

```bash
# Build for production
npm run build

# Output: dist/ folder
# - Minified JavaScript
# - Optimized CSS
# - Compressed assets
# - Index.html with hashed filenames
```

### Build Output Structure

```
dist/
├── assets/
│   ├── index-[hash].js      # Main bundle
│   ├── index-[hash].css     # Styles
│   └── react.svg
├── vite.svg
└── index.html               # Entry HTML with asset links
```

### Preview Production Build

```bash
# Preview production build locally
npm run preview

# Default port: 4173
```

### Docker Deployment

**Dockerfile:**
```dockerfile
# Build stage
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Production stage
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

**Build and run:**
```bash
# Build Docker image
docker build -t platform-frontend .

# Run container
docker run -p 80:80 platform-frontend
```

### Nginx Configuration

**File:** `nginx.conf`

```nginx
server {
    listen 80;
    server_name localhost;

    root /usr/share/nginx/html;
    index index.html;

    # Enable gzip compression
    gzip on;
    gzip_types text/css application/javascript application/json;

    # SPA routing: always return index.html
    location / {
        try_files $uri $uri/ /index.html;
    }

    # Cache static assets
    location /assets/ {
        expires 1y;
        add_header Cache-Control "public, immutable";
    }
}
```

### CI/CD Pipeline Example

**GitHub Actions:**
```yaml
name: Build and Deploy Frontend

on:
  push:
    branches: [main]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Setup Node.js
        uses: actions/setup-node@v3
        with:
          node-version: '18'
          
      - name: Install dependencies
        working-directory: ./frontend
        run: npm ci
        
      - name: Build
        working-directory: ./frontend
        run: npm run build
        
      - name: Deploy to server
        # Deploy dist/ folder to server
        run: |
          # Your deployment commands here
```

---

## Best Practices

### 1. Component Structure

✅ **DO:**
```jsx
// Good: Separate concerns
const UserList = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadUsers();
  }, []);

  const loadUsers = async () => {
    try {
      setLoading(true);
      const data = await userService.getAll();
      setUsers(data);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <Loading />;

  return (
    <div className="user-list">
      {users.map(user => (
        <UserCard key={user.id} user={user} />
      ))}
    </div>
  );
};
```

❌ **DON'T:**
```jsx
// Bad: Mixing concerns, no error handling
const UserList = () => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    api.get('/users').then(res => setUsers(res.data));
  }, []);

  return users.map(u => <div>{u.name}</div>);
};
```

### 2. API Calls

✅ **DO:**
```jsx
// Good: Use service layer
import userService from '../../../services/userService';

const data = await userService.getAll();
```

❌ **DON'T:**
```jsx
// Bad: Direct API calls in components
import api from '../../../services/api';

const response = await api.get('/users');
```

### 3. Error Handling

✅ **DO:**
```jsx
// Good: Proper error handling
try {
  const data = await userService.getAll();
  setUsers(data);
  setError(null);
} catch (error) {
  setError(error.message || 'Failed to load users');
} finally {
  setLoading(false);
}
```

❌ **DON'T:**
```jsx
// Bad: No error handling
const data = await userService.getAll();
setUsers(data);
```

### 4. State Management

✅ **DO:**
```jsx
// Good: Minimal re-renders
const { hasModule } = usePermissions();
if (!hasModule('Core')) return <AccessDenied />;
```

❌ **DON'T:**
```jsx
// Bad: Unnecessary context usage
const { user, roles, modules, ... } = useAuth();
// Using everything even if not needed
```

### 5. Component Organization

✅ **DO:**
- Group related components together
- Use meaningful folder names
- Separate core and module components
- Co-locate CSS with components

❌ **DON'T:**
- Put all components in one folder
- Mix business logic with presentation
- Create deeply nested folder structures

### 6. Performance Optimization

✅ **DO:**
```jsx
// Good: Memoization for expensive computations
const filteredUsers = useMemo(() => 
  users.filter(u => u.name.includes(searchTerm)),
  [users, searchTerm]
);
```

```jsx
// Good: Debounced search
const debouncedSearch = useCallback(
  debounce((term) => setSearchTerm(term), 300),
  []
);
```

### 7. Security

✅ **DO:**
- Always validate user input
- Use HTTPS in production
- Never store sensitive data in localStorage (only JWT token)
- Implement CORS properly
- Use Content Security Policy (CSP)

❌ **DON'T:**
- Store passwords in state or localStorage
- Trust client-side validation only
- Expose sensitive API keys in frontend code

### 8. Code Style

✅ **DO:**
```jsx
// Good: Consistent naming
const UserList = () => { /* ... */ };
const loadUsers = async () => { /* ... */ };
const handleDelete = (id) => { /* ... */ };
```

❌ **DON'T:**
```jsx
// Bad: Inconsistent naming
const userlist = () => { /* ... */ };
const LoadUsers = async () => { /* ... */ };
const delete_handler = (id) => { /* ... */ };
```

---

## Troubleshooting

### Common Issues

#### 1. "Cannot read property 'modules' of null"

**Problem:** Trying to access user data before it's loaded

**Solution:**
```jsx
// Add null check
const { hasModule } = usePermissions();
if (!user) return <Loading />;
```

#### 2. "401 Unauthorized" on API calls

**Problem:** JWT token expired or invalid

**Solution:**
- Token is automatically cleared on 401 response
- User is redirected to login
- User needs to login again

#### 3. Routes not working after page refresh

**Problem:** Using HashRouter instead of BrowserRouter, or server not configured

**Solution:**
```jsx
// Use BrowserRouter (already implemented)
import { BrowserRouter } from 'react-router-dom';

// Ensure nginx.conf has:
location / {
    try_files $uri $uri/ /index.html;
}
```

#### 4. Module not showing in sidebar

**Problem:** User doesn't have module permission or module is disabled

**Solution:**
```jsx
// Check if module is enabled in database
SELECT * FROM MODULES WHERE Name = 'YourModule';

// Check if user has module permission
SELECT * FROM USER_MODULES WHERE UserId = 1 AND ModuleId = X;
```

#### 5. Build fails with "out of memory"

**Problem:** Build process requires more memory

**Solution:**
```bash
# Increase Node.js memory limit
NODE_OPTIONS="--max-old-space-size=4096" npm run build
```

#### 6. CORS errors

**Problem:** Backend not configured to accept frontend origin

**Solution:**
```csharp
// Backend: Program.cs
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

#### 7. Hot reload not working

**Problem:** Vite dev server not detecting file changes

**Solution:**
```bash
# Kill dev server and restart
npm run dev

# Or use --force flag
npm run dev -- --force
```

#### 8. Environment variables not working

**Problem:** Variables not prefixed with `VITE_`

**Solution:**
```bash
# ❌ Bad
API_URL=http://localhost:5000/api

# ✅ Good
VITE_API_URL=http://localhost:5000/api
```

### Debugging Tips

#### 1. Check Network Tab
- Open browser DevTools → Network
- Look for failed API calls
- Check request/response headers and body

#### 2. Check Console
- Look for JavaScript errors
- Check for failed imports
- Verify environment variables

#### 3. Check LocalStorage
```javascript
// In browser console
localStorage.getItem('token');
localStorage.getItem('user');
```

#### 4. Check AuthContext State
```jsx
// Add console.log in AuthContext
console.log('Auth state:', { user, isAuthenticated });
```

#### 5. Verify API Endpoint
```bash
# Test API directly
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"Admin@123"}'
```

---

## Appendix

### Useful Commands

```bash
# Development
npm install              # Install dependencies
npm run dev              # Start dev server
npm run build            # Production build
npm run preview          # Preview production build
npm run lint             # Run linter

# Clean & Reinstall
rm -rf node_modules package-lock.json
npm install

# Check outdated packages
npm outdated

# Update packages
npm update

# Audit security vulnerabilities
npm audit
npm audit fix
```

### File Size Optimization

```bash
# Analyze bundle size
npm run build
npx vite-bundle-visualizer

# Tips:
# - Use lazy loading for routes
# - Split vendor bundles
# - Optimize images (use WebP)
# - Remove unused dependencies
```

### References

- [React Documentation](https://react.dev/)
- [Vite Documentation](https://vitejs.dev/)
- [React Router Documentation](https://reactrouter.com/)
- [Axios Documentation](https://axios-http.com/)

---

## Changelog

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2026-02-02 | Initial technical documentation |

---

**Tác giả / Author:** Platform Development Team  
**Ngôn ngữ / Language:** Vietnamese & English  
**Phiên bản / Version:** 1.0.0  
**Cập nhật cuối / Last Updated:** February 2, 2026
