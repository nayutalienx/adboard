# Adboard - Advertisement Board Platform

![Adboard Logo](IdentityServer/IdentityServer/wwwroot/sale.png)

**Adboard** is a modern, full-featured classified advertisements (ads board) platform built with ASP.NET Core and IdentityServer4. It provides a comprehensive solution for creating, managing, and browsing classified advertisements with advanced search capabilities, user authentication, and a clean, responsive web interface.

## 🚀 Features

### Core Functionality
- **Advertisement Management**: Create, edit, update, and delete advertisements
- **Advanced Search & Filtering**: Search by category, title, region, with photo-only filtering
- **Category System**: Hierarchical category organization for better content structure
- **Photo Upload**: Support for multiple photos per advertisement
- **User Comments**: Interactive commenting system on advertisements
- **Location Support**: Detailed address system (area, city, street, house number)
- **Price Management**: Currency support with Russian Ruble formatting

### Authentication & Security
- **IdentityServer4 Integration**: Professional-grade authentication and authorization
- **User Profiles**: Personal user accounts with contact information management
- **Role-Based Access**: Admin and User role management
- **Secure API**: Protected endpoints with JWT bearer token authentication

### Technical Features
- **Responsive Design**: Mobile-first, responsive web interface
- **RESTful API**: Well-structured REST API for all operations
- **Docker Support**: Containerized deployment with Docker Compose
- **Clean Architecture**: Separation of concerns with layered architecture
- **Entity Framework**: Modern ORM with SQLite database support

## 🏗️ Architecture

The project follows Clean Architecture principles with clear separation of concerns:

```
├── Adboard/                    # Main Application
│   ├── Adboard.UI/            # Web Frontend (MVC)
│   ├── Adboard.API/           # REST API Backend
│   ├── Adboard.Contracts/     # DTOs and Data Contracts
│   ├── BusinessLogicLayer/    # Business Logic
│   │   ├── Abstraction/       # Business interfaces
│   │   ├── Implementation/    # Business implementations
│   │   └── Tests/            # Unit tests
│   ├── DataAccessLayer/       # Data Access
│   │   ├── Models/           # Entity models
│   │   ├── Abstraction/      # Repository interfaces
│   │   ├── EntityFramework/  # EF configuration
│   │   └── Repositories/     # Repository implementations
│   └── Infrastructure/        # Infrastructure concerns
│       └── DependencyInjection/
└── IdentityServer/            # Authentication Service
    └── IdentityServer/        # IdentityServer4 implementation
```

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 3.1+** - Web framework
- **Entity Framework Core** - ORM
- **SQLite** - Database
- **IdentityServer4** - Authentication/Authorization
- **AutoMapper** - Object mapping
- **Swagger/OpenAPI** - API documentation

### Frontend
- **ASP.NET Core MVC** - Web framework
- **Bootstrap 4** - CSS framework
- **jQuery** - JavaScript library
- **Responsive Design** - Mobile-first approach

### DevOps & Tools
- **Docker & Docker Compose** - Containerization
- **Visual Studio** - IDE support
- **Git** - Version control

## 📋 Prerequisites

Before running the application, ensure you have:

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) or later
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized deployment)
- [Visual Studio 2019+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (recommended)
- [Git](https://git-scm.com/) for version control

## 🚀 Quick Start

### Option 1: Docker Compose (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/nayutalienx/adboard.git
   cd adboard
   ```

2. **Run with Docker Compose**
   ```bash
   # Start all services
   docker-compose up -d
   ```

3. **Access the application**
   - **Web UI**: http://localhost:82
   - **API**: http://localhost:81
   - **IdentityServer**: http://localhost:80

### Option 2: Manual Setup

1. **Clone and restore packages**
   ```bash
   git clone https://github.com/nayutalienx/adboard.git
   cd adboard
   
   # Restore main application
   cd Adboard
   dotnet restore
   
   # Restore IdentityServer
   cd ../IdentityServer
   dotnet restore
   ```

2. **Configure connection strings**
   
   Update the configuration files with your database settings:
   - `Adboard/Adboard.API/appsettings.json`
   - `Adboard/Adboard.UI/appsettings.json`
   - `IdentityServer/IdentityServer/appsettings.json`

3. **Run database migrations**
   ```bash
   # From Adboard directory
   dotnet ef database update --project DataAccessLayer/DataAccessLayer.EntityFramework
   
   # From IdentityServer directory  
   dotnet ef database update --context IdentityDbContext
   dotnet ef database update --context ConfigurationDbContext
   dotnet ef database update --context PersistedGrantDbContext
   ```

4. **Start the services**
   ```bash
   # Terminal 1: Start IdentityServer
   cd IdentityServer/IdentityServer
   dotnet run
   
   # Terminal 2: Start API
   cd Adboard/Adboard.API
   dotnet run
   
   # Terminal 3: Start UI
   cd Adboard/Adboard.UI
   dotnet run
   ```

## 📚 API Documentation

The API provides comprehensive endpoints for all advertisement operations:

### Authentication
- **POST** `/connect/token` - Get access token
- **GET** `/connect/userinfo` - Get user information

### Advertisements
- **GET** `/api/v1/Adverts` - Get all advertisements
- **GET** `/api/v1/Adverts/{id}` - Get advertisement by ID
- **POST** `/api/v1/Adverts` - Create new advertisement
- **PUT** `/api/v1/Adverts/{id}` - Update advertisement
- **DELETE** `/api/v1/Adverts/{id}` - Delete advertisement
- **GET** `/api/v1/Adverts/filter` - Search and filter advertisements

### Categories
- **GET** `/api/v1/Categories` - Get all categories
- **POST** `/api/v1/Categories` - Create new category

### Comments
- **POST** `/api/v1/Adverts/comments` - Add comment to advertisement

### Swagger Documentation
When running in development mode, API documentation is available at:
- http://localhost:81/swagger (API service)

## 📱 Usage Guide

### For Users

1. **Browse Advertisements**
   - Visit the homepage to see latest advertisements
   - Use category filters in the header navigation
   - Search by title, region, or apply photo filters

2. **Create Account**
   - Click "Login or Register" (Вход или регистрация)
   - Complete the registration process through IdentityServer
   - Add your contact information in your personal account

3. **Post Advertisements**
   - Click "Add" (Добавить) button
   - Fill in title, description, price, and location
   - Upload photos (optional)
   - Select appropriate category
   - Submit your advertisement

4. **Manage Your Ads**
   - Access "Personal Account" (Личный кабинет)
   - View, edit, or delete your advertisements
   - Update your contact information

### For Developers

#### Development Workflow
```bash
# Make changes to the code
# Build the solution
dotnet build

# Run tests
dotnet test

# Apply database migrations
dotnet ef database update
```

#### Adding New Features
1. Create models in `DataAccessLayer.Models`
2. Add DTOs in `Adboard.Contracts`
3. Implement business logic in `BusinessLogicLayer`
4. Add API endpoints in `Adboard.API`
5. Update UI in `Adboard.UI`

## 🔧 Configuration

### Environment Variables
The application uses the following configuration:

```bash
# Database
ConnectionStrings__ConnectionSqlite=Filename=AdvertService.db;

# IdentityServer
OpenIdConnect__Authority=http://localhost:80
OpenIdConnect__ClientId=dashboard-app
OpenIdConnect__ClientSecret=dashboard-app

# API Endpoints
ApiClient__BaseUrl=http://localhost:81
IdentityClient__BaseUrl=http://localhost:80
```

### Docker Configuration
Services are configured in `docker-compose.yml`:
- **IdentityServer**: Port 80
- **API**: Port 81  
- **UI**: Port 82

## 🧪 Testing

Run the test suite:

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes**
4. **Add tests** for new functionality
5. **Commit your changes**
   ```bash
   git commit -m "Add: your feature description"
   ```
6. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```
7. **Create a Pull Request**

### Coding Standards
- Follow C# coding conventions
- Add XML documentation for public APIs
- Include unit tests for new features
- Ensure all tests pass before submitting

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors & Contributors

- **nayutalienx** - *Initial work* - [GitHub Profile](https://github.com/nayutalienx)

## 📞 Support & Contact

For questions, issues, or contributions:

- **GitHub Issues**: [Create an issue](https://github.com/nayutalienx/adboard/issues)
- **GitHub Discussions**: [Join the discussion](https://github.com/nayutalienx/adboard/discussions)

## 🎯 Roadmap

### Planned Features
- [ ] Email notifications for new comments
- [ ] Advanced user messaging system  
- [ ] Favorite advertisements
- [ ] Enhanced search with Elasticsearch
- [ ] Mobile application (React Native/Flutter)
- [ ] Payment integration
- [ ] Multi-language support
- [ ] Advanced admin dashboard

### Technical Improvements
- [ ] Migration to .NET 6/7
- [ ] GraphQL API support
- [ ] Redis caching layer
- [ ] Automated CI/CD pipeline
- [ ] Performance monitoring
- [ ] Security audit and improvements

## 🖼️ Screenshots

> Screenshots of the application interface will be preserved from the original documentation when available.

---

**Built with ❤️ using ASP.NET Core and IdentityServer4**