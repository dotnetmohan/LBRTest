# Missing NuGet Packages - Manual Installation Guide

## 📦 Complete Package List

### Project 1: LBR.BookService.API
**Location:** `src\Services\BookService\LBR.BookService.API`

| Package Name | Version | Purpose |
|-------------|---------|---------|
| Microsoft.AspNetCore.OpenApi | 8.0.22 | OpenAPI/Swagger specification |
| Swashbuckle.AspNetCore | 6.6.2 | Swagger UI for API documentation |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.0 | JWT authentication middleware |
| System.IdentityModel.Tokens.Jwt | 7.0.0 | JWT token generation/validation |

**Installation Commands:**
```powershell
cd C:\Projects\LBR\LBRTest\src\Services\BookService\LBR.BookService.API

dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.22
dotnet add package Swashbuckle.AspNetCore --version 6.6.2
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0
```

---

### Project 2: LBR.BookService.Infrastructure
**Location:** `src\Services\BookService\LBR.BookService.Infrastructure`

| Package Name | Version | Purpose |
|-------------|---------|---------|
| Microsoft.EntityFrameworkCore | 8.0.0 | EF Core ORM framework |
| Microsoft.EntityFrameworkCore.InMemory | 8.0.0 | In-memory database provider |

**Installation Commands:**
```powershell
cd C:\Projects\LBR\LBRTest\src\Services\BookService\LBR.BookService.Infrastructure

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0.0
```

---

### Project 3: LBR.Shared.Authentication
**Location:** `src\Shared\LBR.Shared.Authentication`

| Package Name | Version | Purpose |
|-------------|---------|---------|
| System.IdentityModel.Tokens.Jwt | 7.0.0 | JWT token handling |
| Microsoft.IdentityModel.Tokens | 7.0.0 | Token validation |
| BCrypt.Net-Next | 4.0.3 | Password hashing |

**Installation Commands:**
```powershell
cd C:\Projects\LBR\LBRTest\src\Shared\LBR.Shared.Authentication

dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0
dotnet add package Microsoft.IdentityModel.Tokens --version 7.0.0
dotnet add package BCrypt.Net-Next --version 4.0.3
```

---

### Project 4: LBR.BookService.UnitTests (Optional)
**Location:** `tests\LBR.BookService.UnitTests`

| Package Name | Version | Purpose |
|-------------|---------|---------|
| Microsoft.NET.Test.Sdk | 17.8.0 | Test SDK |
| xunit | 2.5.3 | Testing framework |
| xunit.runner.visualstudio | 2.5.3 | Visual Studio test runner |
| coverlet.collector | 6.0.0 | Code coverage |
| Moq | 4.20.70 | Mocking framework (recommended) |

**Installation Commands:**
```powershell
cd C:\Projects\LBR\LBRTest\tests\LBR.BookService.UnitTests

dotnet add package Microsoft.NET.Test.Sdk --version 17.8.0
dotnet add package xunit --version 2.5.3
dotnet add package xunit.runner.visualstudio --version 2.5.3
dotnet add package coverlet.collector --version 6.0.0
dotnet add package Moq --version 4.20.70
```

---

## 🚀 Quick Install - All Packages at Once

### Option 1: Install Everything (Copy and run all commands)
```powershell
# Navigate to solution root
cd C:\Projects\LBR\LBRTest

# Install API packages
cd src\Services\BookService\LBR.BookService.API
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.22
dotnet add package Swashbuckle.AspNetCore --version 6.6.2
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0

# Install Infrastructure packages
cd ..\LBR.BookService.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0.0

# Install Authentication packages
cd ..\..\..\Shared\LBR.Shared.Authentication
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0
dotnet add package Microsoft.IdentityModel.Tokens --version 7.0.0
dotnet add package BCrypt.Net-Next --version 4.0.3

# Install Test packages (Optional)
cd ..\..\..\tests\LBR.BookService.UnitTests
dotnet add package Microsoft.NET.Test.Sdk --version 17.8.0
dotnet add package xunit --version 2.5.3
dotnet add package xunit.runner.visualstudio --version 2.5.3
dotnet add package coverlet.collector --version 6.0.0
dotnet add package Moq --version 4.20.70

# Return to root and restore
cd ..\..
dotnet restore
```

### Option 2: Minimal Required Packages Only
```powershell
cd C:\Projects\LBR\LBRTest

# Critical packages for Phase 4 to work
cd src\Services\BookService\LBR.BookService.API
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.0

cd ..\LBR.BookService.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 8.0.0

cd ..\..\..\Shared\LBR.Shared.Authentication
dotnet add package BCrypt.Net-Next --version 4.0.3

cd ..\..\..\..
dotnet restore
```

---

## 📋 Package Summary by Category

### Authentication & Security (Required)
- `Microsoft.AspNetCore.Authentication.JwtBearer` - 8.0.0
- `System.IdentityModel.Tokens.Jwt` - 7.0.0
- `Microsoft.IdentityModel.Tokens` - 7.0.0
- `BCrypt.Net-Next` - 4.0.3

### Database (Required)
- `Microsoft.EntityFrameworkCore` - 8.0.0
- `Microsoft.EntityFrameworkCore.InMemory` - 8.0.0

### API Documentation (Optional but Recommended)
- `Microsoft.AspNetCore.OpenApi` - 8.0.22
- `Swashbuckle.AspNetCore` - 6.6.2

### Testing (Optional)
- `Microsoft.NET.Test.Sdk` - 17.8.0
- `xunit` - 2.5.3
- `xunit.runner.visualstudio` - 2.5.3
- `coverlet.collector` - 6.0.0
- `Moq` - 4.20.70

---

## ✅ Post-Installation Steps

After installing packages, you need to:

### Step 1: Uncomment Code in Project Files
```powershell
# Uncomment package references in .csproj files
# - LBR.BookService.API.csproj
# - LBR.BookService.Infrastructure.csproj
# - LBR.BookService.UnitTests.csproj
```

### Step 2: Uncomment Implementation Code
```
✓ src/Services/BookService/LBR.BookService.Infrastructure/Data/BookDbContext.cs
✓ src/Services/BookService/LBR.BookService.Infrastructure/Data/DbSeeder.cs
✓ src/Services/BookService/LBR.BookService.Infrastructure/Repositories/BookRepository.cs
✓ src/Services/BookService/LBR.BookService.Infrastructure/Repositories/ReservationRepository.cs
✓ src/Services/BookService/LBR.BookService.Infrastructure/Repositories/UserRepository.cs
✓ src/Shared/LBR.Shared.Authentication/Services/JwtTokenService.cs
✓ src/Shared/LBR.Shared.Authentication/Services/PasswordHasher.cs
```

### Step 3: Uncomment in Program.cs
```csharp
// Uncomment these lines in Program.cs:
// - using Microsoft.AspNetCore.Authentication.JwtBearer;
// - using Microsoft.IdentityModel.Tokens;
// - builder.Services.AddDbContext<BookDbContext>()
// - builder.Services.AddAuthentication()
// - app.UseSwagger()
// - app.UseSwaggerUI()
// - app.UseAuthentication()
// - Database seeding code
```

### Step 4: Build the Solution
```powershell
cd C:\Projects\LBR\LBRTest
dotnet build
```

### Step 5: Run the API
```powershell
dotnet run --project src\Services\BookService\LBR.BookService.API
```

### Step 6: Test with Swagger
Open browser: `http://localhost:5025/swagger`

---

## 🔍 Verification Commands

### Check if packages are installed:
```powershell
cd C:\Projects\LBR\LBRTest
dotnet list src\Services\BookService\LBR.BookService.API\LBR.BookService.API.csproj package
dotnet list src\Services\BookService\LBR.BookService.Infrastructure\LBR.BookService.Infrastructure.csproj package
dotnet list src\Shared\LBR.Shared.Authentication\LBR.Shared.Authentication.csproj package
```

### Check build status:
```powershell
dotnet build --no-restore
```

### Check for errors:
```powershell
dotnet build > build-output.txt 2>&1
```

---

## 📊 Total Package Count

- **Required for Phase 4:** 6 packages
- **Recommended (Swagger):** 2 packages
- **Optional (Testing):** 5 packages
- **TOTAL:** 13 packages

---

## 💡 Troubleshooting

### If packages still fail to download:
1. **Check NuGet sources:**
   ```powershell
   dotnet nuget list source
   ```

2. **Clear NuGet cache:**
   ```powershell
   dotnet nuget locals all --clear
   ```

3. **Try with explicit source:**
   ```powershell
   dotnet add package PackageName --source https://api.nuget.org/v3/index.json
   ```

4. **Check proxy settings:**
   ```powershell
   # If behind corporate proxy
   set HTTP_PROXY=http://proxy:port
   set HTTPS_PROXY=http://proxy:port
   ```

5. **Use offline package source** (if you have .nupkg files):
   ```powershell
   dotnet add package PackageName --source C:\LocalPackages
   ```

---

## 📝 Notes

- All packages target **.NET 8.0**
- Package versions are tested and compatible
- JWT packages (7.0.0) work with .NET 8.0
- EF Core (8.0.0) matches .NET 8.0 version
- Swagger packages (6.6.2) are latest stable for .NET 8

---

## 🎯 Priority Installation Order

1. **High Priority** (API won't work without these):
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.InMemory
   - BCrypt.Net-Next
   - System.IdentityModel.Tokens.Jwt
   - Microsoft.AspNetCore.Authentication.JwtBearer

2. **Medium Priority** (API will work but no UI documentation):
   - Microsoft.AspNetCore.OpenApi
   - Swashbuckle.AspNetCore

3. **Low Priority** (Only for testing):
   - xunit packages
   - Microsoft.NET.Test.Sdk
   - Moq
