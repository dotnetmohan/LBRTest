# Library Book Reservation System (LBR)

A simplified Library Book Reservation System built with .NET, implementing Clean Architecture principles and microservices architecture.

## Project Overview

This system provides a comprehensive solution for managing library books and reservations. It follows Clean Architecture patterns with a focus on separation of concerns, testability, and maintainability.

## Architecture

The solution follows **Clean Architecture** principles with the following structure:

- **Domain Layer**: Core business entities and logic
- **Application Layer**: Use cases, interfaces, and business rules
- **Infrastructure Layer**: Data access, external services, and cross-cutting concerns
- **Presentation Layer**: API endpoints and controllers

## Solution Structure

```
LBR/
├── src/
│   ├── Services/
│   │   └── BookService/
│   │       ├── LBR.BookService.API/              # Presentation Layer
│   │       ├── LBR.BookService.Application/      # Application Layer
│   │       ├── LBR.BookService.Domain/           # Domain Layer
│   │       └── LBR.BookService.Infrastructure/   # Infrastructure Layer
│   └── Shared/
│       ├── LBR.Shared.Authentication/            # JWT Authentication
│       └── LBR.Shared.Common/                    # Common utilities
├── tests/
│   ├── LBR.BookService.UnitTests/
│   └── LBR.BookService.IntegrationTests/
├── LBR.sln
└── README.md
```

## Features

### Book Service
- **CRUD Operations**: Create, Read, Update, and Delete books
- **User-Specific Retrieval**: GET returns only books reserved by authenticated users
- **Admin Operations**: POST allows admins to add books to the catalog
- **Advanced Search**: Search books by genre and author
- **Reservation Validation**: Prevents reservation of unavailable books

## Requirements

### 1. Microservices

**BookService** - ASP.NET Core Web API:
- Handles book operations: Create, Read, Update, Delete
- GET returns only books reserved by the authenticated user
- POST allows adding books to the catalog (Admin only)
- Search endpoint for books by genre and author
- Validates that a book cannot be reserved if it is not available

### 2. Authentication & Authorization

- **JWT-based authentication** for all API endpoints
- Role-based authorization (User, Admin)
- Only authenticated users can access BookService endpoints
- Admin-only operations for catalog management

### 3. Data Access

- Entity Framework Core with **in-memory database**
- Asynchronous operations using **async/await** patterns
- Repository pattern for data access abstraction

### 4. Asynchronous Programming

- All data access operations use **async/await**
- All controller actions implement async patterns
- Proper exception handling with async code

## Technical Stack

- **.NET 8.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API services
- **Entity Framework Core**: ORM with in-memory database
- **JWT Authentication**: Secure token-based authentication
- **Clean Architecture**: Maintainable and testable design
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Input validation

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 / VS Code / JetBrains Rider
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd LBRTest
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running the Application

**Run BookService:**
```bash
dotnet run --project src/Services/BookService/LBR.BookService.API
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

### Authentication

- `POST /api/auth/login` - User login (returns JWT token)
- `POST /api/auth/register` - User registration

### Book Service

| Method | Endpoint | Description | Auth Required | Role |
|--------|----------|-------------|---------------|------|
| GET | `/api/books` | Get books reserved by authenticated user | Yes | User |
| GET | `/api/books/{id}` | Get book by ID | Yes | User |
| POST | `/api/books` | Add new book to catalog | Yes | Admin |
| PUT | `/api/books/{id}` | Update book details | Yes | Admin |
| DELETE | `/api/books/{id}` | Delete book | Yes | Admin |
| GET | `/api/books/search` | Search books by genre/author | Yes | User |
| POST | `/api/books/{id}/reserve` | Reserve a book | Yes | User |
| DELETE | `/api/books/{id}/reserve` | Cancel reservation | Yes | User |
| GET | `/api/books/available` | Get all available books | Yes | User |

## Project Layers Detail

### Domain Layer (`LBR.BookService.Domain`)
**Entities:**
- `Book`: Core book entity with properties (ISBN, Title, Author, Genre, etc.)
- `Reservation`: Book reservation entity
- `User`: User entity with roles

**Enums:**
- `BookStatus`: Available, Reserved, CheckedOut
- `UserRole`: User, Admin

### Application Layer (`LBR.BookService.Application`)
**Features:**
- Commands: CreateBook, UpdateBook, DeleteBook, ReserveBook
- Queries: GetUserReservations, SearchBooks, GetBookById
- DTOs: BookDto, ReservationDto, CreateBookDto
- Validators: CreateBookValidator, ReserveBookValidator
- Interfaces: IBookRepository, IReservationRepository

### Infrastructure Layer (`LBR.BookService.Infrastructure`)
**Implementations:**
- DbContext and EF Core configurations
- Repository implementations
- In-memory database setup
- Data seeding

### API Layer (`LBR.BookService.API`)
**Components:**
- Controllers: BooksController, AuthController
- Middleware: Exception handling, JWT authentication
- Dependency injection configuration
- Swagger/OpenAPI documentation

## Authentication Flow

1. User registers or logs in via `/api/auth/login`
2. System returns JWT token with user claims (UserId, Role)
3. Client includes token in Authorization header: `Bearer <token>`
4. API validates token and authorizes based on role
5. User-specific data is filtered using authenticated user's ID

## Development Guidelines

### Code Standards
- Follow SOLID principles
- Use async/await for all I/O operations
- Implement proper exception handling
- Write unit tests for business logic
- Document public APIs with XML comments

### Naming Conventions
- Use PascalCase for classes, methods, and properties
- Use camelCase for local variables and parameters
- Prefix interfaces with 'I'
- Use descriptive names

### Testing
- Unit tests for application layer logic
- Integration tests for API endpoints
- Mock external dependencies
- Aim for >80% code coverage

## Sample Requests

### Register User
```json
POST /api/auth/register
{
  "username": "john.doe",
  "email": "john@example.com",
  "password": "SecurePass123!",
  "role": "User"
}
```

### Login
```json
POST /api/auth/login
{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

### Create Book (Admin)
```json
POST /api/books
Authorization: Bearer <admin-token>
{
  "isbn": "978-3-16-148410-0",
  "title": "Clean Architecture",
  "author": "Robert C. Martin",
  "genre": "Software Engineering",
  "publishedYear": 2017,
  "totalCopies": 5
}
```

### Search Books
```json
GET /api/books/search?genre=Fiction&author=Tolkien
Authorization: Bearer <token>
```

### Reserve Book
```json
POST /api/books/123/reserve
Authorization: Bearer <token>
```

## Database Schema

### Books Table
- Id (Guid, PK)
- ISBN (string)
- Title (string)
- Author (string)
- Genre (string)
- PublishedYear (int)
- TotalCopies (int)
- AvailableCopies (int)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)

### Reservations Table
- Id (Guid, PK)
- BookId (Guid, FK)
- UserId (Guid, FK)
- ReservedAt (DateTime)
- ExpiresAt (DateTime)
- Status (enum)

### Users Table
- Id (Guid, PK)
- Username (string)
- Email (string)
- PasswordHash (string)
- Role (enum)
- CreatedAt (DateTime)

## Future Enhancements

- [ ] User Service for separate authentication management
- [ ] Real database implementation (SQL Server, PostgreSQL)
- [ ] Notification system for reservation expiry
- [ ] Rate limiting and caching with Redis
- [ ] API versioning
- [ ] Comprehensive logging with Serilog
- [ ] Health checks and monitoring
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] API Gateway with Ocelot
- [ ] Message broker integration (RabbitMQ/Azure Service Bus)

## Project Timeline

### Phase 1: Foundation (Week 1)
- [x] Project structure setup
- [ ] Domain entities and enums
- [ ] Repository interfaces
- [ ] Basic authentication setup

### Phase 2: Core Features (Week 2)
- [ ] Book CRUD operations
- [ ] Reservation logic
- [ ] Search functionality
- [ ] Validation rules

### Phase 3: Security & Testing (Week 3)
- [ ] JWT implementation
- [ ] Role-based authorization
- [ ] Unit tests
- [ ] Integration tests

### Phase 4: Polish & Documentation (Week 4)
- [ ] API documentation
- [ ] Error handling
- [ ] Logging
- [ ] Performance optimization

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is for educational purposes.

## Contact

Project Maintainer: [Your Name]
Repository: [Repository URL]

---

**Solution Name**: LBR (Library Book Reservation)  
**Architecture**: Clean Architecture  
**Version**: 1.0.0  
**Last Updated**: January 6, 2026
