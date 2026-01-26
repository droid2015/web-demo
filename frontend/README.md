# Frontend - Platform UI

React application with Vite, React Router, and Axios for API integration.

## Setup

### Prerequisites

- Node.js 18+
- npm or yarn

### Install and Run

```bash
npm install
npm run dev
```

The application will be available at `http://localhost:5173` (Vite default)

### Build for Production

```bash
npm run build
```

## Configuration

Create a `.env` file (use `.env.example` as template):

```
VITE_API_URL=http://localhost:5000/api
```

## Features

- **Authentication** - JWT-based login
- **Dashboard** - Overview page
- **Users** - User management
- **Modules** - Enable/disable platform modules
- **Products** - Product catalog

## Default Credentials

- **Username**: admin
- **Password**: Admin@123

## Technologies

- React 18
- Vite
- React Router v6
- Axios

## Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
