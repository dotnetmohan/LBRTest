// Uncomment when Microsoft.EntityFrameworkCore package is installed
/*
using LBR.BookService.Application.Interfaces.Repositories;
using LBR.BookService.Domain.Entities;
using LBR.BookService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LBR.BookService.Infrastructure.Repositories;

/// <summary>
/// Implementation of IBookRepository using Entity Framework Core
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .Where(b => b.IsActive)
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .Where(b => b.IsActive && b.AvailableCopies > 0)
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> SearchAsync(string? genre = null, string? author = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Books.Where(b => b.IsActive);

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(b => b.Genre.Contains(genre));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        return await query
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.ISBN == isbn, cancellationToken);
    }

    public async Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.CreatedAt = DateTime.UtcNow;
        book.AvailableCopies = book.TotalCopies;
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);
        
        return book;
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.UpdatedAt = DateTime.UtcNow;
        
        _context.Books.Update(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await GetByIdAsync(id, cancellationToken);
        if (book != null)
        {
            // Soft delete
            book.IsActive = false;
            book.UpdatedAt = DateTime.UtcNow;
            
            _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> IsAvailableAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        var book = await GetByIdAsync(bookId, cancellationToken);
        return book != null && book.IsAvailable;
    }

    public async Task<bool> ExistsByISBNAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .AnyAsync(b => b.ISBN == isbn, cancellationToken);
    }
}
*/

// Placeholder until EF Core packages are installed
using LBR.BookService.Application.Interfaces.Repositories;
using LBR.BookService.Domain.Entities;

namespace LBR.BookService.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<bool> ExistsByISBNAsync(string isbn, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<IEnumerable<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<bool> IsAvailableAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task<IEnumerable<Book>> SearchAsync(string? genre = null, string? author = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }

        public Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Install EF Core packages to enable this functionality");
        }
    }
}
