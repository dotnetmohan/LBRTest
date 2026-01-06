# LBR Implementation Roadmap

## Project: Library Book Reservation System
**Architecture**: Clean Architecture  
**Framework**: .NET 8.0  
**Pattern**: Microservices  

---

## 📅 Implementation Timeline

### Week 1: Foundation & Core Domain

#### Day 1-2: Domain Layer ✅ PRIORITY
**Location**: `src/Services/BookService/LBR.BookService.Domain/`

**Tasks**:
1. Create folder structure:
   - `Entities/`
   - `Enums/`
   - `Common/`

2. Implement Entities:
   ```csharp
   // Entities/Book.cs
   public class Book
   {
       public Guid Id { get; set; }
       public string ISBN { get; set; }
       public string Title { get; set; }
       public string Author { get; set; }
       public string Genre { get; set; }
       public int PublishedYear { get; set; }
       public int TotalCopies { get; set; }
       public int AvailableCopies { get; set; }
       public DateTime CreatedAt { get; set; }
       public DateTime? UpdatedAt { get; set; }
   }

   // Entities/User.cs
   public class User
   {
       public Guid Id { get; set; }
       public string Username { get; set; }
       public string Email { get; set; }
       public string PasswordHash { get; set; }
       public UserRole Role { get; set; }
       public DateTime CreatedAt { get; set; }
   }

   // Entities/Reservation.cs
   public class Reservation
   {
       public Guid Id { get; set; }
       public Guid BookId { get; set; }
       public Guid UserId { get; set; }
       public DateTime ReservedAt { get; set; }
       public DateTime? ExpiresAt { get; set; }
       public ReservationStatus Status { get; set; }
       
       // Navigation properties
       public Book Book { get; set; }
       public User User { get; set; }
   }
   ```

3. Implement Enums:
   ```csharp
   // Enums/BookStatus.cs
   public enum BookStatus
   {
       Available,
       Reserved,
       CheckedOut
   }

   // Enums/UserRole.cs
   public enum UserRole
   {
       User,
       Admin
   }

   // Enums/ReservationStatus.cs
   public enum ReservationStatus
   {
       Active,
       Cancelled,
       Expired
   }
   ```

#### Day 3-4: Application Layer
**Location**: `src/Services/BookService/LBR.BookService.Application/`

**Tasks**:
1. Create folder structure:
   - `DTOs/`
   - `Interfaces/Repositories/`
   - `Interfaces/Services/`
   - `Commands/`
   - `Queries/`
   - `Validators/`
   - `Mappings/`

2. Install packages:
   ```bash
   dotnet add package AutoMapper
   dotnet add package FluentValidation
   dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
   ```

3. Create DTOs:
   ```csharp
   // DTOs/BookDto.cs
   public class BookDto
   {
       public Guid Id { get; set; }
       public string ISBN { get; set; }
       public string Title { get; set; }
       public string Author { get; set; }
       public string Genre { get; set; }
       public int PublishedYear { get; set; }
       public int AvailableCopies { get; set; }
   }

   // DTOs/CreateBookDto.cs
   public class CreateBookDto
   {
       public string ISBN { get; set; }
       public string Title { get; set; }
       public string Author { get; set; }
       public string Genre { get; set; }
       public int PublishedYear { get; set; }
       public int TotalCopies { get; set; }
   }
   ```

4. Create Repository Interfaces:
   ```csharp
   // Interfaces/Repositories/IBookRepository.cs
   public interface IBookRepository
   {
       Task<Book> GetByIdAsync(Guid id);
       Task<IEnumerable<Book>> GetAllAsync();
       Task<IEnumerable<Book>> SearchAsync(string genre, string author);
       Task<Book> AddAsync(Book book);
       Task UpdateAsync(Book book);
       Task DeleteAsync(Guid id);
       Task<bool> IsAvailableAsync(Guid bookId);
   }

   // Interfaces/Repositories/IReservationRepository.cs
   public interface IReservationRepository
   {
       Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);
       Task<Reservation> GetByIdAsync(Guid id);
       Task<Reservation> AddAsync(Reservation reservation);
       Task UpdateAsync(Reservation reservation);
       Task DeleteAsync(Guid id);
   }

   // Interfaces/Repositories/IUserRepository.cs
   public interface IUserRepository
   {
       Task<User> GetByIdAsync(Guid id);
       Task<User> GetByEmailAsync(string email);
       Task<User> AddAsync(User user);
       Task<bool> ExistsAsync(string email);
   }
   ```

5. Create Validators:
   ```csharp
   // Validators/CreateBookValidator.cs
   public class CreateBookValidator : AbstractValidator<CreateBookDto>
   {
       public CreateBookValidator()
       {
           RuleFor(x => x.ISBN).NotEmpty().MaximumLength(20);
           RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
           RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
           RuleFor(x => x.Genre).NotEmpty().MaximumLength(50);
           RuleFor(x => x.PublishedYear).GreaterThan(1000).LessThanOrEqualTo(DateTime.Now.Year);
           RuleFor(x => x.TotalCopies).GreaterThan(0);
       }
   }
   ```

### Week 2: Infrastructure & Data Access

#### Day 5-6: Infrastructure Layer
**Location**: `src/Services/BookService/LBR.BookService.Infrastructure/`

**Tasks**:
1. Install packages:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.InMemory
   ```

2. Create DbContext:
   ```csharp
   // Data/BookDbContext.cs
   public class BookDbContext : DbContext
   {
       public BookDbContext(DbContextOptions<BookDbContext> options)
           : base(options) { }

       public DbSet<Book> Books { get; set; }
       public DbSet<Reservation> Reservations { get; set; }
       public DbSet<User> Users { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           // Configure entities
           modelBuilder.Entity<Book>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.ISBN).IsRequired().HasMaxLength(20);
               entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
               entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
               entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
           });

           modelBuilder.Entity<Reservation>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.HasOne(e => e.Book)
                   .WithMany()
                   .HasForeignKey(e => e.BookId);
               entity.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId);
           });

           modelBuilder.Entity<User>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
               entity.HasIndex(e => e.Email).IsUnique();
           });
       }
   }
   ```

3. Implement Repositories:
   ```csharp
   // Repositories/BookRepository.cs
   public class BookRepository : IBookRepository
   {
       private readonly BookDbContext _context;

       public BookRepository(BookDbContext context)
       {
           _context = context;
       }

       public async Task<Book> GetByIdAsync(Guid id)
       {
           return await _context.Books.FindAsync(id);
       }

       public async Task<IEnumerable<Book>> GetAllAsync()
       {
           return await _context.Books.ToListAsync();
       }

       public async Task<IEnumerable<Book>> SearchAsync(string genre, string author)
       {
           var query = _context.Books.AsQueryable();

           if (!string.IsNullOrEmpty(genre))
               query = query.Where(b => b.Genre.Contains(genre));

           if (!string.IsNullOrEmpty(author))
               query = query.Where(b => b.Author.Contains(author));

           return await query.ToListAsync();
       }

       public async Task<Book> AddAsync(Book book)
       {
           _context.Books.Add(book);
           await _context.SaveChangesAsync();
           return book;
       }

       public async Task UpdateAsync(Book book)
       {
           _context.Books.Update(book);
           await _context.SaveChangesAsync();
       }

       public async Task DeleteAsync(Guid id)
       {
           var book = await GetByIdAsync(id);
           if (book != null)
           {
               _context.Books.Remove(book);
               await _context.SaveChangesAsync();
           }
       }

       public async Task<bool> IsAvailableAsync(Guid bookId)
       {
           var book = await GetByIdAsync(bookId);
           return book != null && book.AvailableCopies > 0;
       }
   }
   ```

4. Create Data Seeder:
   ```csharp
   // Data/DbSeeder.cs
   public static class DbSeeder
   {
       public static async Task SeedAsync(BookDbContext context)
       {
           if (!context.Users.Any())
           {
               // Seed admin user
               context.Users.Add(new User
               {
                   Id = Guid.NewGuid(),
                   Username = "admin",
                   Email = "admin@lbr.com",
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                   Role = UserRole.Admin,
                   CreatedAt = DateTime.UtcNow
               });

               // Seed regular user
               context.Users.Add(new User
               {
                   Id = Guid.NewGuid(),
                   Username = "user",
                   Email = "user@lbr.com",
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                   Role = UserRole.User,
                   CreatedAt = DateTime.UtcNow
               });

               await context.SaveChangesAsync();
           }

           if (!context.Books.Any())
           {
               // Seed sample books
               context.Books.AddRange(
                   new Book
                   {
                       Id = Guid.NewGuid(),
                       ISBN = "978-0134685991",
                       Title = "Effective Java",
                       Author = "Joshua Bloch",
                       Genre = "Programming",
                       PublishedYear = 2017,
                       TotalCopies = 5,
                       AvailableCopies = 5,
                       CreatedAt = DateTime.UtcNow
                   },
                   new Book
                   {
                       Id = Guid.NewGuid(),
                       ISBN = "978-0134494166",
                       Title = "Clean Architecture",
                       Author = "Robert C. Martin",
                       Genre = "Software Engineering",
                       PublishedYear = 2017,
                       TotalCopies = 3,
                       AvailableCopies = 3,
                       CreatedAt = DateTime.UtcNow
                   }
               );

               await context.SaveChangesAsync();
           }
       }
   }
   ```

### Week 3: API Layer & Authentication

#### Day 7-8: Shared Authentication
**Location**: `src/Shared/LBR.Shared.Authentication/`

**Tasks**:
1. Install packages:
   ```bash
   dotnet add package System.IdentityModel.Tokens.Jwt
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   dotnet add package BCrypt.Net-Next
   ```

2. Create JWT Service:
   ```csharp
   // Services/JwtTokenService.cs
   public interface IJwtTokenService
   {
       string GenerateToken(User user);
       ClaimsPrincipal ValidateToken(string token);
   }

   public class JwtTokenService : IJwtTokenService
   {
       private readonly string _key;
       private readonly string _issuer;
       private readonly string _audience;
       private readonly int _expiryMinutes;

       public JwtTokenService(IConfiguration configuration)
       {
           _key = configuration["Jwt:Key"];
           _issuer = configuration["Jwt:Issuer"];
           _audience = configuration["Jwt:Audience"];
           _expiryMinutes = int.Parse(configuration["Jwt:ExpiryInMinutes"]);
       }

       public string GenerateToken(User user)
       {
           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
           var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           var claims = new[]
           {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Name, user.Username),
               new Claim(ClaimTypes.Role, user.Role.ToString())
           };

           var token = new JwtSecurityToken(
               issuer: _issuer,
               audience: _audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_expiryMinutes),
               signingCredentials: credentials
           );

           return new JwtSecurityTokenHandler().WriteToken(token);
       }

       public ClaimsPrincipal ValidateToken(string token)
       {
           var tokenHandler = new JwtSecurityTokenHandler();
           var key = Encoding.UTF8.GetBytes(_key);

           var validationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = true,
               ValidIssuer = _issuer,
               ValidateAudience = true,
               ValidAudience = _audience,
               ValidateLifetime = true
           };

           return tokenHandler.ValidateToken(token, validationParameters, out _);
       }
   }
   ```

#### Day 9-10: API Controllers
**Location**: `src/Services/BookService/LBR.BookService.API/`

**Tasks**:
1. Install packages:
   ```bash
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   dotnet add package Swashbuckle.AspNetCore
   ```

2. Create Controllers:
   ```csharp
   // Controllers/AuthController.cs
   [ApiController]
   [Route("api/[controller]")]
   public class AuthController : ControllerBase
   {
       private readonly IUserRepository _userRepository;
       private readonly IJwtTokenService _jwtService;

       [HttpPost("register")]
       public async Task<IActionResult> Register([FromBody] RegisterDto dto)
       {
           // Implementation
       }

       [HttpPost("login")]
       public async Task<IActionResult> Login([FromBody] LoginDto dto)
       {
           // Implementation
       }
   }

   // Controllers/BooksController.cs
   [ApiController]
   [Route("api/[controller]")]
   [Authorize]
   public class BooksController : ControllerBase
   {
       private readonly IBookRepository _bookRepository;
       private readonly IReservationRepository _reservationRepository;

       [HttpGet]
       public async Task<IActionResult> GetUserBooks()
       {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           // Return books reserved by user
       }

       [HttpGet("{id}")]
       public async Task<IActionResult> GetBook(Guid id)
       {
           // Implementation
       }

       [HttpPost]
       [Authorize(Roles = "Admin")]
       public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
       {
           // Implementation
       }

       [HttpPut("{id}")]
       [Authorize(Roles = "Admin")]
       public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto dto)
       {
           // Implementation
       }

       [HttpDelete("{id}")]
       [Authorize(Roles = "Admin")]
       public async Task<IActionResult> DeleteBook(Guid id)
       {
           // Implementation
       }

       [HttpGet("search")]
       public async Task<IActionResult> SearchBooks([FromQuery] string genre, [FromQuery] string author)
       {
           // Implementation
       }

       [HttpPost("{id}/reserve")]
       public async Task<IActionResult> ReserveBook(Guid id)
       {
           // Validate availability and create reservation
       }

       [HttpDelete("{id}/reserve")]
       public async Task<IActionResult> CancelReservation(Guid id)
       {
           // Implementation
       }
   }
   ```

3. Configure Program.cs:
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Add services
   builder.Services.AddControllers();
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();

   // Configure DbContext
   builder.Services.AddDbContext<BookDbContext>(options =>
       options.UseInMemoryDatabase("LBRDatabase"));

   // Register repositories
   builder.Services.AddScoped<IBookRepository, BookRepository>();
   builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
   builder.Services.AddScoped<IUserRepository, UserRepository>();

   // Register JWT service
   builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

   // Configure JWT Authentication
   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
           };
       });

   builder.Services.AddAuthorization();

   var app = builder.Build();

   // Seed database
   using (var scope = app.Services.CreateScope())
   {
       var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
       await DbSeeder.SeedAsync(context);
   }

   if (app.Environment.IsDevelopment())
   {
       app.UseSwagger();
       app.UseSwaggerUI();
   }

   app.UseHttpsRedirection();
   app.UseAuthentication();
   app.UseAuthorization();
   app.MapControllers();

   app.Run();
   ```

4. Update appsettings.json:
   ```json
   {
     "Jwt": {
       "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!@#$",
       "Issuer": "LBR.BookService",
       "Audience": "LBR.Users",
       "ExpiryInMinutes": 60
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

### Week 4: Testing & Polish

#### Day 11-12: Unit Tests
**Location**: `tests/LBR.BookService.UnitTests/`

**Tasks**:
1. Install packages:
   ```bash
   dotnet add package Moq
   dotnet add package FluentAssertions
   dotnet add package Microsoft.EntityFrameworkCore.InMemory
   ```

2. Create test classes for repositories and services

#### Day 13-14: Documentation & Polish
- Complete API documentation
- Add XML comments
- Test all endpoints
- Fix bugs
- Performance optimization

---

## 🎯 Key Implementation Points

### Clean Architecture Rules
1. **Domain** has no dependencies
2. **Application** depends only on Domain
3. **Infrastructure** depends on Domain and Application
4. **API** depends on Application and Infrastructure (for DI only)

### Async/Await Requirements
- All repository methods are async
- All controller actions are async
- Use `Task<T>` return types
- Use `async/await` keywords properly

### Authentication Requirements
- JWT tokens with user claims
- Role-based authorization
- Password hashing with BCrypt
- User-specific data filtering

### Validation Requirements
- FluentValidation for input validation
- Business rule validation in domain/application
- Availability check before reservation

---

## 📊 Progress Tracking

- [x] Solution structure created
- [x] Projects created and configured
- [x] Project references established
- [ ] Domain entities implemented
- [ ] Application layer implemented
- [ ] Infrastructure layer implemented
- [ ] API controllers implemented
- [ ] Authentication configured
- [ ] Tests written
- [ ] Documentation complete

---

**Ready to implement!** Start with the Domain layer and work your way up.

**Last Updated**: January 6, 2026
