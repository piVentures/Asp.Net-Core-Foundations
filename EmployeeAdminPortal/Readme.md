# Employee Admin Portal (ASP.NET Core + EF Core + SQLite)

A simple **Employee Management REST API** built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQLite**, developed entirely using the **Ubuntu terminal workflow**.

This project demonstrates clean separation of concerns, Entity Framework Core concepts, and a practical, command‑driven development flow.

---

## 🧠 Core Concepts Used

### ASP.NET Core Web API

* Attribute‑based routing using `[ApiController]` and `[Route]`
* RESTful endpoints following HTTP semantics
* Dependency Injection (DI) built into ASP.NET Core

### Entity Framework Core (EF Core)

* Code‑first approach
* DbContext as a **Unit of Work**
* DbSet<TEntity> as a **repository abstraction**
* Migrations for schema versioning
* SQLite as a lightweight relational database

### DTO Pattern

* DTOs (Data Transfer Objects) decouple API contracts from database entities
* Prevents over‑posting and leaking persistence concerns

### SQLite

* File‑based relational database
* Ideal for local development and learning
* Inspected directly via terminal (`sqlite3`)

---

## 📁 Project Structure

```
EmployeeAdminPortal/
│
├── Controllers/
│   └── EmployeesController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   ├── Entities/
│   │   └── Employee.cs
│   └── Dtos/
│       ├── AddEmployeeDto.cs
│       └── UpdateEmployeeDto.cs
│
├── Migrations/
│
├── appsettings.json
├── Program.cs
└── EmployeeAdminPortal.csproj
```

---

## 🔄 Application Flow (High Level)

1. **HTTP request** hits an API endpoint
2. ASP.NET Core **model binding** maps JSON → DTO
3. DTO is mapped → **Entity**
4. Entity Framework Core **tracks changes** via DbContext
5. `SaveChanges()` translates object changes into SQL
6. SQLite persists data to a `.db` file
7. API returns a standardized HTTP response

---

## 🛠 Development Environment

* OS: Ubuntu (Terminal‑based workflow)
* .NET SDK: 8.x
* Database: SQLite
* ORM: Entity Framework Core
* API Testing: curl / Swagger

---

## 🚀 Terminal Commands Used

### .NET Project Setup

```bash
dotnet --version
dotnet new webapi -n EmployeeAdminPortal
cd EmployeeAdminPortal
dotnet restore
dotnet build
dotnet run
```

---

### NuGet Packages (EF Core + SQLite)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore
dotnet list package
```

---

### Entity Framework Core Commands

```bash
dotnet ef
dotnet ef migrations add InitialCreate
dotnet ef migrations add AddEmployeesTable
dotnet ef migrations list
dotnet ef migrations remove
dotnet ef database update
dotnet ef database drop
```

Explicit project usage (when required):

```bash
dotnet ef migrations add InitialCreate --project EmployeeAdminPortal
dotnet ef database update --project EmployeeAdminPortal
```

---

### SQLite (Terminal Usage)

Open database:

```bash
sqlite3 EmployeeAdminPortal.db
```

Inspect schema:

```sql
.tables
.schema
.schema Employees
PRAGMA table_info(Employees);
SELECT * FROM Employees;
```

Exit:

```sql
.exit
```

---

### API Testing via curl

#### GET all employees

```bash
curl http://localhost:5030/api/Employees
```

#### GET employee by ID

```bash
curl http://localhost:5030/api/Employees/{guid}
```

#### POST create employee

```bash
curl -X POST http://localhost:5030/api/Employees \
-H "Content-Type: application/json" \
-d '{"name":"John","email":"john@gmail.com","salary":5000}'
```

#### PUT update employee

```bash
curl -X PUT http://localhost:5030/api/Employees/{guid} \
-H "Content-Type: application/json" \
-d '{"name":"Updated","email":"updated@gmail.com","salary":6000}'
```

#### DELETE employee

```bash
curl -X DELETE http://localhost:5030/api/Employees/{guid}
```

---

## ⚙️ EF Core Mental Model

* **DbContext** = Unit of Work
* **DbSet<TEntity>** = Table abstraction
* **Change Tracker** detects entity state
* **Migrations** = Version‑controlled schema
* **SaveChanges()** = SQL generation + transaction

---

## 🧪 Common Recovery Commands

```bash
rm EmployeeAdminPortal.db
dotnet ef database update
dotnet clean
dotnet build
```

---

## ✅ Summary

This project demonstrates:

* Clean REST API design
* Practical EF Core usage
* SQLite integration
* DTO‑based architecture
* Terminal‑first Ubuntu workflow

It is intentionally minimal, readable, and extendable — ideal as a foundation for larger systems.


