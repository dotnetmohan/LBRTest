using LBR.BookService.Domain.Entities;

namespace LBR.BookService.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for Book operations
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Gets a book by its ID
    /// </summary>
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all books
    /// </summary>
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets available books
    /// </summary>
    Task<IEnumerable<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches books by genre and/or author
    /// </summary>
    Task<IEnumerable<Book>> SearchAsync(string? genre = null, string? author = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a book by ISBN
    /// </summary>
    Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new book to the catalog
    /// </summary>
    Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing book
    /// </summary>
    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a book from the catalog
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a book is available for reservation
    /// </summary>
    Task<bool> IsAvailableAsync(Guid bookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a book with the given ISBN already exists
    /// </summary>
    Task<bool> ExistsByISBNAsync(string isbn, CancellationToken cancellationToken = default);
}
