# 📚 CurotecTest

CurotecTest is a clean architecture .NET 8 Web API project that showcases core principles of modern application development, including:

- ✅ CQRS pattern with MediatR
- 🗄️ Entity Framework Core for data access
- 👥 Domain-Driven Design (DDD) structure
- 🔐 Authentication and user management
- 🔍 Swagger/OpenAPI for easy testing
- 📦 Built-in support for testing with xUnit

It currently features two main domain entities: **Author** and **Book**, which are related via EF Core.

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (local or remote)

---

### ⚙️ Configuration

1. **Clone the repository:**

   ```bash
   git clone https://github.com/TrestorMend/CurotecTest.git
   cd CurotecTest/CurotecTest

2. **Update your connection string:**

Update the connection string:

Open appsettings.json and update the DefaultConnection string with your SQL Server connection:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CurotecDb;User Id=your_user;Password=your_password;"
}

First-time login credentials:

After seeding or setting up your database, use the following admin credentials for your first login:

Username: admin

Password: Curotec@2025

✍️ Author
Developed by @TrestorMend
Feel free to contribute, fork, or raise issues!