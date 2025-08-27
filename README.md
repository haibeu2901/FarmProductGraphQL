# Farm Products Management System

A comprehensive full-stack e-commerce solution for agricultural businesses, featuring a robust ASP.NET Core Web API backend with both REST and GraphQL endpoints, and a modern React frontend dashboard.

## 🌾 System Overview

This Farm Products Management System is designed for agricultural businesses to manage their product catalog, process orders, track inventory, and handle customer relationships. The system provides a complete farm-to-consumer solution with modern web technologies.

### System Components

- **Backend API**: ASP.NET Core Web API with REST and GraphQL endpoints
- **Frontend Dashboard**: React-based administrative interface
- **Database**: SQL Server for data persistence
- **Real-time Features**: GraphQL subscriptions and efficient caching

## ✨ Features

### Backend Features
- **Product Management**: Create, read, update, and delete farm products with categories
- **Order Processing**: Complete order management system with order details
- **Inventory Tracking**: Track imported stock and product quantities
- **Account Management**: Handle customer and staff accounts with role-based access
- **Dual API Support**: Both REST and GraphQL endpoints
- **Real-time Queries**: Advanced filtering, sorting, and pagination
- **Data Validation**: Comprehensive input validation and error handling

### Frontend Features
- **Product Management Dashboard**: Browse products using GraphQL queries
- **Account Management Interface**: View user accounts with both REST API and GraphQL implementations
- **Modern UI**: Clean, responsive design with gradient backgrounds and card layouts
- **Real-time Data**: Apollo Client integration for efficient data fetching and caching
- **Client-side Routing**: React Router DOM for seamless navigation
- **Reusable Components**: Modular component architecture

## 🏗️ System Architecture

### Backend Architecture
The backend follows Clean Architecture principles:
```
├── Core/                    # Web API layer (Controllers, GraphQL)
├── Services/               # Business logic layer
├── Repository/             # Data access layer
└── BusinessObject/         # Models, ViewModels, and Data context
```

### Frontend Architecture
```
src/
├── components/             # React components
│   ├── ProductList.jsx        # Product management (GraphQL)
│   ├── AccountList.jsx        # Account management (REST API)
│   └── AccountListSimple.jsx  # Account management (GraphQL)
├── hooks/                  # Custom hooks
├── App.jsx                # Main application component
└── main.jsx              # Application entry point
```

## 🛠️ Technology Stack

### Backend Technologies
- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for data access
- **SQL Server** - Primary database
- **HotChocolate** - GraphQL server implementation
- **AutoMapper** - Object-object mapping
- **Swagger/OpenAPI** - API documentation

### Frontend Technologies
- **React 19.1.1** - Frontend framework
- **Vite 7.1.2** - Build tool and development server
- **Apollo Client 3.13.9** - GraphQL client
- **React Router DOM 7.8.1** - Client-side routing
- **CSS3** - Modern styling with Flexbox/Grid
- **ESLint** - Code quality assurance

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v16 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## 🚀 Complete Setup Guide | Hướng Dẫn Cài Đặt Toàn Bộ Hệ Thống

### Phase 1: Backend Setup | Thiết Lập Backend

#### Bước 1: Chuẩn Bị Môi Trường (Environment Setup)

**1.1 Cài Đặt .NET 8 SDK**
```bash
# Kiểm tra phiên bản .NET
dotnet --version
```

**1.2 Cài Đặt SQL Server**
- **SQL Server Express**: [Download Link](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **SQL Server Management Studio (SSMS)**: [Download Link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

#### Bước 2: Thiết Lập Backend API

**2.1 Tải Source Code**
```bash
git clone <repository-url>
cd farm-products-system
```

**2.2 Cấu Hình Database**

Mở file `Core/appsettings.json` và cập nhật connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;uid=sa;pwd=12345;database=FarmProductsAPI;TrustServerCertificate=True;"
  }
}
```

**Lưu ý**: Thay đổi thông tin kết nối phù hợp:
- `Server`: localhost, ., hoặc tên server cụ thể
- `uid`: Username SQL Server
- `pwd`: Password SQL Server
- `database`: Tên database

**2.3 Tạo Database và Migration**
```bash
# Di chuyển đến thư mục solution
cd <solution-directory>

# Restore packages
dotnet restore

# Tạo migration (nếu chưa có)
dotnet ef migrations add InitialCreate --project BusinessObject --startup-project Core

# Cập nhật database
dotnet ef database update --project BusinessObject --startup-project Core
```

**2.4 Build và Chạy Backend**
```bash
# Build solution
dotnet build

# Chạy API server
cd Core
dotnet run
```

**2.5 Xác Nhận Backend Hoạt Động**
- **HTTPS**: `https://localhost:7097`
- **HTTP**: `http://localhost:5175`
- **Swagger UI**: `https://localhost:7097/swagger`
- **GraphQL Playground**: `https://localhost:7097/graphql`

### Phase 2: Frontend Setup | Thiết Lập Frontend

#### Bước 3: Thiết Lập Frontend Dashboard

**3.1 Di Chuyển Đến Thư Mục Frontend**
```bash
cd Frontend
```

**3.2 Cài Đặt Dependencies**
```bash
# Cài đặt tất cả packages
npm install

# Hoặc sử dụng yarn
yarn install
```

**3.3 Chạy Frontend Development Server**
```bash
# Khởi động development server
npm run dev

# Server sẽ chạy tại http://localhost:5173
```

### Phase 3: System Integration | Tích Hợp Hệ Thống

#### Bước 4: Kiểm Tra Kết Nối API

**4.1 Truy Cập Frontend Dashboard**
- Mở trình duyệt tại: `http://localhost:5173`
- Navigation menu sẽ hiển thị các tùy chọn:
  - Home (Dashboard)
  - Products (GraphQL)
  - Accounts (REST API)
  - Simple Accounts (GraphQL)

**4.2 Test API Endpoints**

**Backend Testing:**
- **Swagger UI**: `https://localhost:7097/swagger`
- **GraphQL Playground**: `https://localhost:7097/graphql`

**Sample GraphQL Queries:**
```graphql
# Products Query
query {
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

# Accounts Query
query {
  allAccount {
    accountId
    fullName
    username
    password
  }
}
```

### Bước 5: Xử Lý Lỗi Thường Gặp

#### 5.1 Backend Issues

**Lỗi Connection String:**
```
Error: Cannot open database "FarmProductsAPI" requested by the login
```
**Giải pháp**: Kiểm tra và cập nhật connection string trong `appsettings.json`

**Lỗi Migration:**
```
Error: No migrations were found
```
**Giải pháp**:
```bash
dotnet ef migrations add InitialCreate --project BusinessObject --startup-project Core
dotnet ef database update --project BusinessObject --startup-project Core
```

**Lỗi Port Conflict:**
```
Error: Unable to bind to https://localhost:7097
```
**Giải pháp**: Thay đổi port trong `Core/Properties/launchSettings.json`

#### 5.2 Frontend Issues

**Lỗi Kết Nối API:**
```
Error: Network error / CORS error
```
**Giải pháp**: Đảm bảo backend server đang chạy trên đúng port

**Lỗi Dependencies:**
```
Module not found
```
**Giải pháp**:
```bash
rm -rf node_modules
npm install
```

**Lỗi Port Already in Use:**
```
Port 5173 is already in use
```
**Giải pháp**: Vite sẽ tự động chọn port khác (5174, 5175, ...)

### Bước 6: Development Commands | Các Lệnh Development

#### Backend Commands
```bash
# Build solution
dotnet build

# Run with hot reload
dotnet watch run --project Core

# Clean and restore
dotnet clean
dotnet restore

# Publish for production
dotnet publish Core/Core.csproj -c Release -o ./publish
```

#### Frontend Commands
```bash
# Start development server
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview

# Run linting
npm run lint
```

---

## 📚 API Documentation

### REST Endpoints

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/products` | GET/POST | Product management |
| `/api/orders` | GET/POST | Order management |
| `/api/accounts` | GET/POST | Account management |
| `/api/categories` | GET/POST | Category management |
| `/api/importedstock` | GET/POST | Stock management |

### GraphQL Schema

**Available Queries:**
```graphql
type Query {
  products: [Product]
  allAccount: [Account]
  orders: [Order]
  categories: [ProductCategory]
}

type Product {
  productId: Int
  productName: String
  description: String
  unit: String
  sellingPrice: Float
  quantity: Int
  productCategory: ProductCategory
}

type Account {
  accountId: Int
  fullName: String
  username: String
  password: String
  email: String
  role: String
  phoneNumber: String
  address: String
}
```

## 🎯 Frontend Features in Detail

### Available Routes

| Route | Component | API Type | Description |
|-------|-----------|----------|-------------|
| `/` | Home | - | Dashboard landing page |
| `/products` | ProductList | GraphQL | Product catalog management |
| `/accounts` | AccountList | REST API | Full account management |
| `/accounts-simple` | AccountListSimple | GraphQL | Simple account display |

### Component Architecture

**Reusable Components:**
- **AccountCard**: Displays account information with conditional rendering
- **Navigation**: Responsive navigation with active route highlighting
- **Layout**: Main application layout wrapper

**Data Fetching Patterns:**
1. **GraphQL with Apollo Client** (Recommended for complex queries)
2. **REST API with Custom Hooks** (Traditional approach)

### UI/UX Features

- **Gradient Backgrounds**: Purple gradients for account pages, light gradients for products
- **Card-based Design**: Clean white cards with shadows and hover effects
- **Responsive Grid**: Auto-fill layouts that adapt to screen size
- **Loading States**: Elegant loading indicators
- **Error Handling**: User-friendly error messages with retry options
- **Security**: Password masking for privacy

## 🗄️ Database Schema

### Core Entities

```sql
-- Product table
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Unit NVARCHAR(50),
    SellingPrice DECIMAL(10,2),
    Quantity INT,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory(CategoryId)
);

-- Account table
CREATE TABLE Account (
    AccountId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(255),
    Username NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255),
    Role NVARCHAR(50),
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(MAX)
);

-- Order and OrderDetail tables
CREATE TABLE [Order] (
    OrderId INT PRIMARY KEY IDENTITY,
    OrderDate DATETIME,
    TotalAmount DECIMAL(10,2),
    CustomerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Account(AccountId)
);

CREATE TABLE OrderDetail (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT,
    ProductId INT,
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);
```

## 🔧 Configuration Details

### Backend Configuration

**CORS Policy (Development):**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
```

**AutoMapper Configuration:**
```csharp
var config = new MapperConfiguration(mc =>
{
    mc.CreateMap<Product, ProductViewModel>();
    mc.CreateMap<Account, AccountViewModel>();
    // Add more mappings as needed
});
```

### Frontend Configuration

**Vite Proxy Setup:**
```javascript
export default defineConfig({
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
});
```

**Apollo Client Setup:**
```javascript
const client = new ApolloClient({
  uri: '/graphql',
  cache: new InMemoryCache(),
  defaultOptions: {
    watchQuery: { errorPolicy: 'ignore' },
    query: { errorPolicy: 'all' }
  }
});
```

## 🧪 Testing the System

### Backend Testing

1. **Swagger UI Testing**: Visit `https://localhost:7097/swagger`
2. **GraphQL Playground**: Visit `https://localhost:7097/graphql`
3. **HTTP Files**: Use `Core.http` in Visual Studio

### Frontend Testing

1. **Product Management**: Navigate to `/products` to test GraphQL integration
2. **Account Management**: Navigate to `/accounts` to test REST API integration
3. **GraphQL Accounts**: Navigate to `/accounts-simple` to test GraphQL accounts

### Sample Test Data

**GraphQL Product Query:**
```graphql
query GetProducts {
  products {
    productName
    description
    unit
    sellingPrice
    quantity
    productCategory {
      categoryName
    }
  }
}
```

**REST API Test (PowerShell):**
```powershell
Invoke-RestMethod -Uri "https://localhost:7097/api/Account/GetAllAccounts" -Method GET
```

## 🚀 Production Deployment

### Backend Deployment

1. **Publish Application:**
```bash
dotnet publish Core/Core.csproj -c Release -o ./publish
```

2. **Update Configuration:**
- Update connection strings for production database
- Configure CORS policies for production domains
- Set up logging and monitoring

### Frontend Deployment

1. **Build for Production:**
```bash
npm run build
```

2. **Deploy Static Files:**
- Upload `dist/` folder to web server
- Configure server for SPA routing
- Update API endpoints for production

## 🔒 Security Considerations

### Backend Security
- **Input Validation**: All API endpoints validate input data
- **SQL Injection Protection**: Entity Framework provides parameterized queries
- **CORS Configuration**: Restrict origins in production
- **Authentication**: Implement JWT token authentication (future enhancement)

### Frontend Security
- **Password Masking**: User passwords displayed as dots
- **XSS Protection**: React's built-in XSS protection
- **API Security**: Secure communication with HTTPS
- **Input Sanitization**: Proper handling of user inputs

## 🌟 Future Enhancements

### Planned Features
- [ ] **Authentication & Authorization**: JWT-based user authentication
- [ ] **Real-time Features**: WebSocket integration for live updates
- [ ] **Advanced Filtering**: Complex search and filter capabilities
- [ ] **File Upload**: Product image management
- [ ] **Reporting**: Analytics dashboard with charts
- [ ] **Mobile App**: React Native mobile application
- [ ] **Payment Integration**: Payment gateway integration
- [ ] **Email Notifications**: Order and inventory alerts
- [ ] **Multi-language Support**: Internationalization
- [ ] **Testing Suite**: Unit and integration tests

### Technical Improvements
- [ ] **Caching**: Redis implementation for better performance
- [ ] **Database Optimization**: Query optimization and indexing
- [ ] **Logging**: Structured logging with Serilog
- [ ] **Monitoring**: Application performance monitoring
- [ ] **CI/CD Pipeline**: Automated deployment pipeline
- [ ] **Docker Support**: Containerization for easy deployment

## 📖 Learning Resources

This project demonstrates:
- **Clean Architecture** principles in .NET
- **GraphQL** implementation with HotChocolate
- **React** modern development patterns
- **Apollo Client** for GraphQL client management
- **Entity Framework Core** for data access
- **RESTful API** design principles

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Guidelines
- Follow Clean Architecture principles
- Write descriptive commit messages
- Add appropriate documentation
- Test your changes thoroughly
- Follow existing code style and conventions

## 📝 License

This project is developed as part of an academic assignment for PRN212 - Advanced Programming with .NET.

## 🙏 Acknowledgments

- **.NET Team** for the excellent framework and tools
- **React Team** for the modern frontend framework
- **Apollo Team** for GraphQL client tools
- **HotChocolate Team** for .NET GraphQL implementation
- **Vite Team** for the fast build tool
- **Open Source Community** for inspiration and resources

---

**Built with ❤️ for the agricultural community using modern web technologies**

## 📞 Support

For technical support and questions:
- Open an issue in the repository
- Contact the development team
- Check the documentation at `/docs` folder

**System Requirements Summary:**
- Backend: .NET 8, SQL Server, Visual Studio/VS Code
- Frontend: Node.js 16+, Modern web browser
- Development: Git, Package managers (npm/yarn)

---

*This comprehensive documentation covers both backend API and frontend dashboard setup, configuration, and usage. Follow the step-by-step guide to get the complete system running successfully.*
