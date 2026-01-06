// Uncomment when Microsoft.EntityFrameworkCore package is installed
/*
using LBR.BookService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LBR.BookService.Infrastructure.Data;

/// <summary>
/// Database context for the Book Service
/// </summary>
public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ISBN)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Genre)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Publisher)
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            // Index on ISBN for faster searches
            entity.HasIndex(e => e.ISBN)
                .IsUnique();
            
            // Index on Genre and Author for search optimization
            entity.HasIndex(e => e.Genre);
            entity.HasIndex(e => e.Author);
        });

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.FirstName)
                .HasMaxLength(50);
            
            entity.Property(e => e.LastName)
                .HasMaxLength(50);
            
            entity.Property(e => e.Role)
                .IsRequired();
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            // Unique constraints
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.HasIndex(e => e.Username)
                .IsUnique();
            
            // Ignore computed property
            entity.Ignore(e => e.FullName);
        });

        // Configure Reservation entity
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ReservedAt)
                .IsRequired();
            
            entity.Property(e => e.Status)
                .IsRequired();
            
            entity.Property(e => e.Notes)
                .HasMaxLength(500);
            
            // Relationships
            entity.HasOne(e => e.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Indexes
            entity.HasIndex(e => e.BookId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.ReservedAt);
            
            // Ignore computed properties
            entity.Ignore(e => e.IsActive);
            entity.Ignore(e => e.IsExpired);
            entity.Ignore(e => e.DaysUntilExpiration);
        });
    }
}
*/

// Placeholder class until EF Core packages are installed
namespace LBR.BookService.Infrastructure.Data
{
    public class BookDbContext
    {
        // Uncomment above code when Microsoft.EntityFrameworkCore package is installed
    }
}
