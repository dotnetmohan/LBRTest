# LBR Project Structure

## Created Project Structure

The Library Book Reservation System has been successfully set up with Clean Architecture principles.

## Solution Structure

```
LBR.sln
├── src/
│   ├── Services/
│   │   └── BookService/
│   │       ├── LBR.BookService.Domain/           ✓ Created
│   │       ├── LBR.BookService.Application/      ✓ Created
│   │       ├── LBR.BookService.Infrastructure/   ✓ Created
│   │       └── LBR.BookService.API/              ✓ Created
│   └── Shared/
│       ├── LBR.Shared.Authentication/            ✓ Created
│       └── LBR.Shared.Common/                    ✓ Created
└── tests/
    └── LBR.BookService.UnitTests/                ✓ Created
```

## Project Dependencies (Clean Architecture Flow)

```
┌─────────────────────────────────────────────────────────┐
│                 LBR.BookService.API                     │
│              (Presentation Layer)                       │
│         - Controllers                                   │
│         - Middleware                                    │
│         - DI Configuration                              │
└─────────────────┬──────────────────┬────────────────────┘
                  │                  │
                  ▼                  ▼
     ┌────────────────────┐ ┌──────────────────────────┐
     │  Application       │ │  Infrastructure          │
     │  Layer             │ │  Layer                   │
     │  - Use Cases       │ │  - EF Core DbContext     │
     │  - DTOs            │ │  - Repositories          │
     │  - Interfaces      │ │  - Data Access           │
     │  - Validators      │ │  - External Services     │
     └────────┬───────────┘ └───────┬──────────────────┘
              │                     │
              │        ┌────────────┘
              │        │
              ▼        ▼
     ┌─────────────────────────┐
     │    Domain Layer         │
     │    - Entities           │
     │    - Enums              │
     │    - Domain Logic       │
     └─────────────────────────┘
```

## Shared Projects

### LBR.Shared.Authentication
- JWT token generation
- Authentication middleware
- Authorization policies

### LBR.Shared.Common
- Common utilities
- Extension methods
- Base classes

## Test Projects

### LBR.BookService.UnitTests
- Unit tests for Domain and Application layers
- Mock implementations
- Test fixtures

## Next Steps

### 1. Domain Layer Implementation
Create entities:
- `Book.cs` - Core book entity
- `Reservation.cs` - Reservation entity
- `User.cs` - User entity
- Enums: `BookStatus`, `UserRole`, `ReservationStatus`

### 2. Application Layer Implementation
Create:
- DTOs (Data Transfer Objects)
- Commands and Queries (CQRS pattern)
- Validators using FluentValidation
- Repository interfaces
- Service interfaces

### 3. Infrastructure Layer Implementation
Create:
- `BookDbContext` - EF Core DbContext
- Repository implementations
- Entity configurations
- In-memory database setup
- Data seeding

### 4. API Layer Implementation
Create:
- `BooksController` - Book CRUD operations
- `AuthController` - Authentication endpoints
- Exception handling middleware
- JWT authentication configuration
- Swagger documentation
- Dependency injection setup

### 5. Shared Projects Implementation
**Authentication:**
- JWT token service
- User claims
- Password hashing

**Common:**
- Result pattern
- API response models
- Extension methods

### 6. Testing Implementation
- Unit tests for business logic
- Integration tests for API endpoints
- Mock repositories
- Test data builders

## Required NuGet Packages

### Domain Layer
- No external dependencies (Pure domain logic)

### Application Layer
```bash
dotnet add package AutoMapper
dotnet add package FluentValidation
dotnet add package MediatR
```

### Infrastructure Layer
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package BCrypt.Net-Next
```

### API Layer
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Swashbuckle.AspNetCore
```

### Shared.Authentication
```bash
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### Test Projects
```bash
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

## API Endpoints to Implement

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Books
- `GET /api/books` - Get user's reserved books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Add new book (Admin only)
- `PUT /api/books/{id}` - Update book (Admin only)
- `DELETE /api/books/{id}` - Delete book (Admin only)
- `GET /api/books/search` - Search by genre/author
- `GET /api/books/available` - Get available books
- `POST /api/books/{id}/reserve` - Reserve a book
- `DELETE /api/books/{id}/reserve` - Cancel reservation

## Configuration Files

### appsettings.json
```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere-MustBe32CharactersOrMore",
    "Issuer": "LBR.BookService",
    "Audience": "LBR.Users",
    "ExpiryInMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

## Clean Architecture Principles Applied

1. **Dependency Rule**: Dependencies point inward
   - API → Application & Infrastructure
   - Application → Domain
   - Infrastructure → Domain & Application
   - Domain → Nothing (Pure)

2. **Separation of Concerns**: Each layer has distinct responsibility

3. **Testability**: Business logic isolated from infrastructure

4. **Independence**: Domain logic independent of frameworks

5. **Flexibility**: Easy to swap implementations (e.g., change database)

## Build and Run

```bash
# Build entire solution
dotnet build

# Run tests
dotnet test

# Run API
dotnet run --project src/Services/BookService/LBR.BookService.API

# Access Swagger UI
https://localhost:5001/swagger
```

## Development Workflow

1. Start with Domain entities
2. Define Application interfaces
3. Implement Infrastructure
4. Build API controllers
5. Add authentication/authorization
6. Write tests
7. Document APIs

---

**Status**: Project structure created ✓  
**Next**: Implement domain entities and business logic  
**Date**: January 6, 2026
