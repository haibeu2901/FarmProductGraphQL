# 🚀 Hướng Dẫn Chạy Source Code từ GitHub

## Farm Products Management System - Complete Setup Guide

Hướng dẫn chi tiết từ A-Z để clone và chạy thành công dự án Farm Products Management System từ GitHub.

---

## 📋 Yêu Cầu Hệ Thống (System Requirements)

### Phần mềm cần cài đặt trước:
- ✅ **Git** - [Download tại đây](https://git-scm.com/downloads)
- ✅ **.NET 8 SDK** - [Download tại đây](https://dotnet.microsoft.com/download/dotnet/8.0)
- ✅ **Node.js** (phiên bản 16+) - [Download tại đây](https://nodejs.org/)
- ✅ **SQL Server** - [Download SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- ✅ **Visual Studio 2022** hoặc **VS Code** - [Visual Studio](https://visualstudio.microsoft.com/) | [VS Code](https://code.visualstudio.com/)

### Kiểm tra cài đặt:
```bash
# Kiểm tra Git
git --version

# Kiểm tra .NET
dotnet --version

# Kiểm tra Node.js
node --version
npm --version
```

---

## 📥 BƯỚC 1: Clone Source Code từ GitHub

### 1.1 Tạo thư mục để chứa dự án
```bash
# Tạo thư mục (tùy chọn)
mkdir C:\Projects
cd C:\Projects

# Hoặc tạo ở Desktop
cd Desktop
```

### 1.2 Clone repository
```bash
# Clone dự án từ GitHub
git clone https://github.com/your-username/farm-products-system.git

# Di chuyển vào thư mục dự án
cd farm-products-system

# Kiểm tra cấu trúc thư mục
dir
# hoặc trên Mac/Linux
ls -la
```

### 1.3 Cấu trúc thư mục sau khi clone
```
farm-products-system/
├── Core/                    # Backend API project
├── Services/               # Business logic layer
├── Repository/             # Data access layer
├── BusinessObject/         # Models and DbContext
├── Frontend/               # React dashboard
├── FarmProductsAPI.sln     # Solution file
└── README.md              # Documentation
```

---

## ⚙️ BƯỚC 2: Thiết Lập Backend (API Server)

### 2.1 Restore NuGet Packages
```bash
# Ở thư mục gốc của dự án
dotnet restore

# Hoặc restore cho toàn solution
dotnet restore FarmProductsAPI.sln
```

### 2.2 Cấu hình Database Connection String

**Mở file `Core/appsettings.json`** và thay đổi connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FarmProductsAPI;Trusted_Connection=true;TrustServerCertificate=True;"
  }
}
```

**Các tùy chọn connection string phổ biến:**

```json
// Nếu dùng SQL Server Express với Windows Authentication
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FarmProductsAPI;Trusted_Connection=true;TrustServerCertificate=True;"

// Nếu dùng SQL Server với SQL Authentication
"DefaultConnection": "Server=localhost;Database=FarmProductsAPI;User Id=sa;Password=your_password;TrustServerCertificate=True;"

// Nếu dùng LocalDB
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FarmProductsAPI;Trusted_Connection=true;TrustServerCertificate=True;"
```

### 2.3 Tạo Database với Entity Framework

```bash
# Cài đặt EF Core tools (nếu chưa có)
dotnet tool install --global dotnet-ef

# Kiểm tra EF tools
dotnet ef

# Tạo migration đầu tiên
dotnet ef migrations add InitialCreate --project BusinessObject --startup-project Core

# Tạo database
dotnet ef database update --project BusinessObject --startup-project Core
```

### 2.4 Build và chạy Backend

```bash
# Build solution
dotnet build

# Chạy API server
cd Core
dotnet run

# Hoặc chạy với watch mode (tự động restart khi có thay đổi)
dotnet watch run
```

### 2.5 Kiểm tra Backend hoạt động

Sau khi chạy thành công, bạn sẽ thấy:
```
Now listening on: https://localhost:7097
Now listening on: http://localhost:5175
Application started. Press Ctrl+C to shut down.
```

**Truy cập các URL này để kiểm tra:**
- 🌐 **API Swagger**: https://localhost:7097/swagger
- 🔍 **GraphQL Playground**: https://localhost:7097/graphql
- 🏠 **API Base**: https://localhost:7097

---

## 🖥️ BƯỚC 3: Thiết Lập Frontend (React Dashboard)

### 3.1 Mở terminal mới và di chuyển đến Frontend

```bash
# Mở terminal/command prompt mới
# Di chuyển đến thư mục Frontend
cd farm-products-system/Frontend

# Kiểm tra file package.json có tồn tại
dir package.json
# hoặc trên Mac/Linux
ls package.json
```

### 3.2 Cài đặt Node.js Dependencies

```bash
# Cài đặt tất cả dependencies
npm install

# Nếu gặp lỗi, thử xóa cache và cài lại
npm cache clean --force
npm install

# Hoặc sử dụng yarn (nếu có)
yarn install
```

### 3.3 Kiểm tra và cấu hình Vite

**Kiểm tra file `vite.config.js`:**
```javascript
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/graphql': {
        target: 'https://localhost:7097',
        changeOrigin: true,
        secure: false
      },
      '/api': {
        target: 'https://localhost:7097',
        changeOrigin: true,
        secure: false
      }
    }
  }
})
```

### 3.4 Chạy Frontend Development Server

```bash
# Chạy frontend (đảm bảo backend đang chạy)
npm run dev

# Hoặc với host binding để truy cập từ các thiết bị khác
npm run dev -- --host
```

### 3.5 Truy cập Frontend Dashboard

```
✅ Frontend đang chạy tại: http://localhost:5173
✅ Có thể truy cập từ mạng nội bộ: http://192.168.x.x:5173
```

---

## 🔄 BƯỚC 4: Kiểm Tra Tích Hợp Hệ Thống

### 4.1 Checklist hoạt động

**Backend (Terminal 1):**
```bash
cd farm-products-system/Core
dotnet run
# ✅ Should show: Now listening on: https://localhost:7097
```

**Frontend (Terminal 2):**
```bash
cd farm-products-system/Frontend  
npm run dev
# ✅ Should show: Local: http://localhost:5173
```

### 4.2 Test các chức năng

1. **Truy cập Frontend**: http://localhost:5173
2. **Navigation Menu** sẽ có các tùy chọn:
   - 🏠 **Home** - Trang chủ dashboard
   - 📦 **Products** - Danh sách sản phẩm (GraphQL)
   - 👥 **Accounts** - Quản lý tài khoản (REST API)
   - 👤 **Simple Accounts** - Tài khoản đơn giản (GraphQL)

3. **Test Backend riêng biệt:**
   - 📋 **Swagger UI**: https://localhost:7097/swagger
   - 🔍 **GraphQL**: https://localhost:7097/graphql

### 4.3 Sample GraphQL Test

Truy cập https://localhost:7097/graphql và thử query:

```graphql
query TestProducts {
  products {
    productId
    productName
    sellingPrice
    quantity
    productCategory {
      categoryName
    }
  }
}
```

---

## 🛠️ XỬ LÝ LỖI THƯỜNG GẶP

### ❌ Lỗi Backend

#### 1. Lỗi Database Connection
```
Error: Cannot open database "FarmProductsAPI" requested by the login
```

**Giải pháp:**
```bash
# Kiểm tra SQL Server đang chạy
# Cập nhật connection string trong appsettings.json
# Chạy lại migration
dotnet ef database update --project BusinessObject --startup-project Core
```

#### 2. Lỗi Migration
```
No migrations configuration type was found
```

**Giải pháp:**
```bash
# Tạo migration mới
dotnet ef migrations add InitialCreate --project BusinessObject --startup-project Core

# Nếu vẫn lỗi, xóa folder Migrations và tạo lại
rmdir /s BusinessObject\Migrations
dotnet ef migrations add InitialCreate --project BusinessObject --startup-project Core
```

#### 3. Lỗi Port đang sử dụng
```
Unable to bind to https://localhost:7097
```

**Giải pháp:**
- Tắt ứng dụng khác đang dùng port 7097
- Hoặc thay đổi port trong `Core/Properties/launchSettings.json`

### ❌ Lỗi Frontend

#### 1. Lỗi npm install
```
Error: ENOENT: no such file or directory
```

**Giải pháp:**
```bash
# Kiểm tra đang ở đúng thư mục Frontend
cd Frontend
pwd  # hoặc cd trên Windows

# Xóa node_modules và package-lock.json
rm -rf node_modules package-lock.json  # Linux/Mac
rmdir /s node_modules  # Windows
del package-lock.json  # Windows

# Cài đặt lại
npm install
```

#### 2. Lỗi CORS hoặc Network Error
```
Access to fetch at 'https://localhost:7097/api/...' from origin 'http://localhost:5173' has been blocked by CORS policy
```

**Giải pháp:**
- Đảm bảo backend đang chạy trên port 7097
- Kiểm tra file `vite.config.js` có cấu hình proxy đúng
- Restart cả backend và frontend

#### 3. Lỗi GraphQL Connection
```
Error: Network error: Failed to fetch
```

**Giải pháp:**
```bash
# Kiểm tra GraphQL endpoint hoạt động
curl https://localhost:7097/graphql

# Hoặc truy cập trực tiếp trên browser
# https://localhost:7097/graphql
```

### ❌ Lỗi Chung

#### 1. Lỗi SSL Certificate
```
SSL connection could not be established
```

**Giải pháp:**
```bash
# Trust development certificate
dotnet dev-certs https --trust
```

#### 2. Lỗi Permission (Windows)
```
Access is denied
```

**Giải pháp:**
- Chạy Command Prompt/PowerShell với quyền Administrator
- Hoặc chạy Visual Studio với quyền Administrator

---

## 📖 DEVELOPMENT WORKFLOW

### Quy trình làm việc hàng ngày:

```bash
# 1. Mở 2 terminals/command prompts

# Terminal 1 - Backend
cd farm-products-system/Core
dotnet watch run

# Terminal 2 - Frontend  
cd farm-products-system/Frontend
npm run dev

# 3. Mở browser và code editor
# - Frontend: http://localhost:5173
# - Backend API: https://localhost:7097/swagger
# - Code editor: Visual Studio hoặc VS Code
```

### Commands hữu ích:

```bash
# Backend commands
dotnet build                    # Build solution
dotnet clean                    # Clean build files
dotnet watch run               # Run with auto-restart
dotnet ef database update     # Update database

# Frontend commands
npm run dev                    # Start development server
npm run build                  # Build for production
npm run preview               # Preview production build
npm run lint                  # Check code quality
```

---

## 🎯 KIỂM TRA THÀNH CÔNG

### ✅ Checklist hoàn thành setup:

- [ ] **Git clone thành công** - Source code đã tải về
- [ ] **Backend build thành công** - `dotnet build` không lỗi
- [ ] **Database tạo thành công** - Migration chạy thành công
- [ ] **Backend server chạy** - https://localhost:7097/swagger mở được
- [ ] **GraphQL hoạt động** - https://localhost:7097/graphql mở được
- [ ] **Frontend install thành công** - `npm install` không lỗi
- [ ] **Frontend server chạy** - http://localhost:5173 mở được
- [ ] **API integration hoạt động** - Frontend load được data từ backend

### 📱 Test các chức năng:

1. **Products Page** - Hiển thị danh sách sản phẩm từ GraphQL
2. **Accounts Page** - Hiển thị danh sách tài khoản từ REST API
3. **Simple Accounts Page** - Hiển thị tài khoản từ GraphQL
4. **Navigation** - Chuyển trang mượt mà
5. **Error Handling** - Hiển thị lỗi khi backend tắt

---

## 🔄 GIT WORKFLOW (Khi Develop)

### Cập nhật code từ GitHub:

```bash
# Pull latest changes
git pull origin main

# Restore backend packages
dotnet restore

# Update database if needed
dotnet ef database update --project BusinessObject --startup-project Core

# Restore frontend packages
cd Frontend
npm install

# Start development
cd ../Core
dotnet watch run

# In another terminal
cd Frontend
npm run dev
```

### Commit và push changes:

```bash
# Check changes
git status

# Add changes
git add .

# Commit with message
git commit -m "Add new feature: [description]"

# Push to GitHub
git push origin main
```

---

## 📞 HỖ TRỢ

### Nếu gặp vấn đề:

1. **Kiểm tra Prerequisites** - Đảm bảo đã cài đủ phần mềm
2. **Đọc Error Messages** - Thông báo lỗi thường chỉ ra vấn đề
3. **Check GitHub Issues** - Xem có ai gặp lỗi tương tự không
4. **Restart Everything** - Tắt và mở lại terminals, restart Visual Studio
5. **Clean và Rebuild** - `dotnet clean`, xóa `node_modules`, cài đặt lại

### Thông tin hệ thống:
```bash
# Kiểm tra phiên bản
dotnet --version
node --version  
npm --version
git --version

# Kiểm tra port đang sử dụng
netstat -an | findstr :7097
netstat -an | findstr :5173
```

---

**🎉 Chúc bạn setup thành công! Happy coding! 🚀**
