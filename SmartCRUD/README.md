# SmartCRUD – .NET 8 WinForms Management System

## Quick Start

### 1. Setup Database
Run `SmartCRUD.App/Database/SmartCRUD.sql` in SQL Server Management Studio (SSMS).
This creates `SmartCRUDDB` with sample data.

### 2. Update Connection String
Edit `SmartCRUD.App/Data/DatabaseConnection.cs`:
```
// Windows Authentication (default):
"Server=.\SQLEXPRESS;Database=SmartCRUDDB;Trusted_Connection=True;TrustServerCertificate=True;"

// SQL Auth:
"Server=YOUR_SERVER;Database=SmartCRUDDB;User Id=sa;Password=YOUR_PASS;TrustServerCertificate=True;"
```

### 3. Build & Run
```bash
cd SmartCRUD.App
dotnet restore
dotnet run
```
Or open `SmartCRUD.sln` in Visual Studio 2022, press F5.

---

## Default Login Credentials
| Username | Password     | Role    |
|----------|-------------|---------|
| admin    | Admin@123   | Admin   |
| manager  | Manager@123 | Manager |
| user     | User@123    | User    |

---

## Project Structure
```
SmartCRUD/
├── SmartCRUD.sln
├── README.md
├── SmartCRUD.App/
│   ├── SmartCRUD.App.csproj        (.NET 8, WinForms)
│   ├── Program.cs
│   ├── Database/
│   │   └── SmartCRUD.sql
│   ├── Models/
│   │   ├── UserModel.cs
│   │   └── StudentModel.cs
│   ├── Data/
│   │   ├── DatabaseConnection.cs
│   │   ├── UserRepository.cs
│   │   └── StudentRepository.cs
│   ├── Utils/
│   │   ├── SessionManager.cs
│   │   ├── Validator.cs
│   │   └── AppColors.cs
│   └── Forms/
│       ├── LoginForm.cs + Designer.cs
│       ├── DashboardForm.cs + Designer.cs
│       ├── StudentForm.cs + Designer.cs
│       └── UserForm.cs + Designer.cs
```

---

## Features
- ✅ Login with role-based access (Admin / Manager / User)
- ✅ Dashboard with stat cards and quick actions
- ✅ Student CRUD: Add, Update, Delete, Search, DataGridView
- ✅ User Management (Admin only)
- ✅ Parameterized SQL queries (no SQL injection)
- ✅ Full form validation with friendly error messages
- ✅ Modern blue/white UI with sidebar navigation
- ✅ Soft-delete (IsActive flag) for Users and Students
- ✅ .NET 8 compliant, no deprecated APIs

---

## NuGet Packages
- `Microsoft.Data.SqlClient` 5.2.0
