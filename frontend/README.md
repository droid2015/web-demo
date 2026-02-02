# Frontend - Platform UI

React application with Vite, React Router, and Axios for API integration.

## Hướng dẫn chạy Frontend (Vietnamese Instructions)

### Yêu cầu trước khi cài đặt

- Node.js phiên bản 18 trở lên
- npm hoặc yarn

### Các bước chạy ứng dụng

1. **Di chuyển vào thư mục frontend:**
   ```bash
   cd frontend
   ```

2. **Cài đặt các thư viện cần thiết:**
   ```bash
   npm install
   ```

3. **Tạo file cấu hình môi trường:**
   ```bash
   cp .env.example .env
   ```
   
   File `.env` sẽ chứa cấu hình kết nối API:
   ```
   VITE_API_URL=http://localhost:5000/api
   ```

4. **Chạy ứng dụng:**
   ```bash
   npm run dev
   ```

5. **Truy cập ứng dụng:**
   
   Mở trình duyệt và truy cập: `http://localhost:5173`

### Thông tin đăng nhập mặc định

- **Tên đăng nhập**: admin
- **Mật khẩu**: Admin@123

---

## Setup (English)

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

## Technical Documentation

📚 **[Comprehensive Technical Documentation](TECHNICAL_DOCUMENTATION.md)**

For detailed technical information including:
- Complete architecture overview
- Component structure and organization
- Authentication and permission flow
- API integration patterns
- State management with Context API
- Build and deployment processes
- Best practices and troubleshooting

Available in both Vietnamese and English.
