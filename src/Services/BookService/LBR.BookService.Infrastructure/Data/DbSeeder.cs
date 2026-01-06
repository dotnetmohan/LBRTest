// Uncomment when Microsoft.EntityFrameworkCore package is installed
/*
using LBR.BookService.Domain.Entities;
using LBR.BookService.Domain.Enums;

namespace LBR.BookService.Infrastructure.Data;

/// <summary>
/// Seeds initial data into the database
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(BookDbContext context)
    {
        // Seed Users
        if (!context.Users.Any())
        {
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@lbr.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                FirstName = "Admin",
                LastName = "User",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var regularUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "john.doe",
                Email = "john.doe@lbr.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                FirstName = "John",
                LastName = "Doe",
                Role = UserRole.User,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var librarianUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "librarian",
                Email = "librarian@lbr.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Librarian123!"),
                FirstName = "Jane",
                LastName = "Smith",
                Role = UserRole.Librarian,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.AddRange(adminUser, regularUser, librarianUser);
            await context.SaveChangesAsync();
        }

        // Seed Books
        if (!context.Books.Any())
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0134685991",
                    Title = "Effective Java",
                    Author = "Joshua Bloch",
                    Genre = "Programming",
                    Publisher = "Addison-Wesley",
                    Description = "A comprehensive guide to Java programming best practices and design patterns.",
                    PublishedYear = 2017,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0134494166",
                    Title = "Clean Architecture",
                    Author = "Robert C. Martin",
                    Genre = "Software Engineering",
                    Publisher = "Prentice Hall",
                    Description = "A craftsman's guide to software structure and design.",
                    PublishedYear = 2017,
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0201633610",
                    Title = "Design Patterns",
                    Author = "Gang of Four",
                    Genre = "Software Engineering",
                    Publisher = "Addison-Wesley",
                    Description = "Elements of reusable object-oriented software.",
                    PublishedYear = 1994,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0735619678",
                    Title = "Code Complete",
                    Author = "Steve McConnell",
                    Genre = "Programming",
                    Publisher = "Microsoft Press",
                    Description = "A practical handbook of software construction.",
                    PublishedYear = 2004,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0132350884",
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Genre = "Programming",
                    Publisher = "Prentice Hall",
                    Description = "A handbook of agile software craftsmanship.",
                    PublishedYear = 2008,
                    TotalCopies = 7,
                    AvailableCopies = 7,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0321125217",
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    Genre = "Software Engineering",
                    Publisher = "Addison-Wesley",
                    Description = "Tackling complexity in the heart of software.",
                    PublishedYear = 2003,
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0596007126",
                    Title = "Head First Design Patterns",
                    Author = "Eric Freeman & Elisabeth Robson",
                    Genre = "Programming",
                    Publisher = "O'Reilly Media",
                    Description = "A brain-friendly guide to design patterns.",
                    PublishedYear = 2004,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    ISBN = "978-0201616224",
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt & David Thomas",
                    Genre = "Programming",
                    Publisher = "Addison-Wesley",
                    Description = "Your journey to mastery.",
                    PublishedYear = 1999,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Books.AddRange(books);
            await context.SaveChangesAsync();
        }
    }
}
*/

// Placeholder until EF Core packages are installed
namespace LBR.BookService.Infrastructure.Data
{
    public static class DbSeeder
    {
        // Uncomment above code when packages are installed
    }
}
