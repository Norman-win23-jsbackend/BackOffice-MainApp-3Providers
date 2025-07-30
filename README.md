# 🧩 BackOffice MainApp – 3 Providers Architecture

**BackOffice-MainApp** is a modular backend & frontend system developed with **.NET 8**, built around multiple independent providers and a centralized Blazor Server application.  
It showcases how to separate concerns across authentication, verification, and core logic using a distributed architecture.

---

## 🏗️ Projects Overview

### 🔐 Norman.AccountProvider (Azure Function App)
Handles user authentication, password management, and verification codes via HTTP-triggered Azure Functions.

- `SignUp.cs`, `SignIn.cs`, `ChangePassword.cs`, `Verify.cs`
- Uses Entity Framework Core for persistence (`ApplicationDbContext`)
- Supports migrations, validation, and modular extension

### 🖥️ Norman.App (Blazor Server UI)
BackOffice interface with route protection, role-based access, and integration with all external providers.

- Auth modules: `DefaultRoles.cs`, `DefaultUsers.cs`
- UI Components: `App.razor`, `Routes.razor`
- Custom `PermissionRequirement.cs` for protected actions

---

## 🚀 Features

- ✅ Modular separation: UI, Auth, Business Logic  
- 📡 Azure Functions for account workflows  
- 🔒 Role-based and permission-based access control  
- 🧱 Built with EF Core migrations and flexible service injection  
- 🔄 Ready for integration with additional microservices

---

## 📁 Solution Structure

```

BackOffice-MainApp/
├── Norman.sln
├── Norman.App/                     # Blazor Server UI
│   ├── Authorization/
│   ├── Components/
│   └── Program.cs
├── Norman.AccountProvider/        # Azure Function (Auth)
│   ├── Functions/
│   ├── Migrations/
│   └── Program.cs

````

---

## 🛠️ Getting Started

### Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Azure Functions Core Tools (for providers)
- Visual Studio 2022+ (Blazor workload)

### Run the Solution

```bash
dotnet restore
dotnet build
dotnet run --project Norman.App
````

To run providers:

```bash
cd Norman.AccountProvider
func start
```

---

## ☁️ Azure Integration

* Functions ready for deployment via Azure CLI or VS
* Configured with `host.json`, `launchSettings.json`, `appsettings.json`
* Can extend with Azure AD B2C, Blob Storage, CosmosDB

---

## 📄 License

This architecture is designed for scalable, modular SaaS or admin systems.
Released under [MIT License](LICENSE).

---

**Nour Tinawi**

[LinkedIn](https://www.linkedin.com/in/nour-tinawi) • [Portfolio](https://www.pure-art.co) • [GitHub](https://github.com/Norman-Deen)
