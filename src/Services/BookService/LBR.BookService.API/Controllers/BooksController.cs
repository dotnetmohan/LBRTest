using LBR.BookService.Application.DTOs;
using LBR.BookService.Application.Interfaces.Repositories;
using LBR.BookService.Domain.Entities;
using LBR.BookService.Domain.Enums;
using LBR.Shared.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LBR.BookService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<BooksController> _logger;

    public BooksController(
        IBookRepository bookRepository,
        IReservationRepository reservationRepository,
        IUserRepository userRepository,
        ILogger<BooksController> logger)
    {
        _bookRepository = bookRepository;
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    private string GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
    }

    /// <summary>
    /// Get books reserved by the authenticated user
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetUserReservedBooks(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userId = GetCurrentUserId();
            var reservations = await _reservationRepository.GetActiveReservationsByUserIdAsync(userId, cancellationToken);
            
            var books = reservations.Select(r => new BookDto
            {
                Id = r.Book.Id,
                ISBN = r.Book.ISBN,
                Title = r.Book.Title,
                Author = r.Book.Author,
                Genre = r.Book.Genre,
                PublishedYear = r.Book.PublishedYear,
                TotalCopies = r.Book.TotalCopies,
                AvailableCopies = r.Book.AvailableCopies,
                Status = r.Book.Status.ToString()
            }).ToList();

            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(books, $"Found {books.Count} reserved books."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user reserved books");
            return StatusCode(500, ApiResponse<IEnumerable<BookDto>>.Error("An error occurred while retrieving books."));
        }
    }

    /// <summary>
    /// Get all available books
    /// </summary>
    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetAllBooks(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                PublishedYear = b.PublishedYear,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                Status = b.Status.ToString()
            }).ToList();

            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(bookDtos, $"Found {bookDtos.Count} books."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all books");
            return StatusCode(500, ApiResponse<IEnumerable<BookDto>>.Error("An error occurred while retrieving books."));
        }
    }

    /// <summary>
    /// Get book by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BookDto>>> GetBookById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            
            if (book == null)
            {
                return NotFound(ApiResponse<BookDto>.Error("Book not found."));
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                Status = book.Status.ToString()
            };

            return Ok(ApiResponse<BookDto>.Success(bookDto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving book {BookId}", id);
            return StatusCode(500, ApiResponse<BookDto>.Error("An error occurred while retrieving the book."));
        }
    }

    /// <summary>
    /// Search books by genre and/or author
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> SearchBooks(
        [FromQuery] string? genre = null,
        [FromQuery] string? author = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(genre) && string.IsNullOrWhiteSpace(author))
            {
                return BadRequest(ApiResponse<IEnumerable<BookDto>>.Error("Please provide at least one search parameter (genre or author)."));
            }

            var books = await _bookRepository.SearchAsync(genre, author, cancellationToken);
            
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                Author = b.Author,
                Genre = b.Genre,
                PublishedYear = b.PublishedYear,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                Status = b.Status.ToString()
            }).ToList();

            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(bookDtos, $"Found {bookDtos.Count} books matching your search."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching books");
            return StatusCode(500, ApiResponse<IEnumerable<BookDto>>.Error("An error occurred while searching books."));
        }
    }

    /// <summary>
    /// Add a new book to the catalog (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<ActionResult<ApiResponse<BookDto>>> CreateBook(
        [FromBody] CreateBookDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.ISBN) || 
                string.IsNullOrWhiteSpace(request.Title) || 
                string.IsNullOrWhiteSpace(request.Author))
            {
                return BadRequest(ApiResponse<BookDto>.Error("ISBN, Title, and Author are required."));
            }

            // Check if ISBN already exists
            var existingBook = await _bookRepository.GetByISBNAsync(request.ISBN, cancellationToken);
            if (existingBook != null)
            {
                return BadRequest(ApiResponse<BookDto>.Error("A book with this ISBN already exists."));
            }

            // Create new book
            var book = new Book
            {
                ISBN = request.ISBN,
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre ?? string.Empty,
                PublishedYear = request.PublishedYear,
                TotalCopies = request.TotalCopies,
                AvailableCopies = request.TotalCopies,
                Status = request.TotalCopies > 0 ? BookStatus.Available : BookStatus.Unavailable,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _bookRepository.AddAsync(book, cancellationToken);

            var bookDto = new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                Status = book.Status.ToString()
            };

            _logger.LogInformation("Book {BookTitle} added to catalog by user {UserId}", book.Title, GetCurrentUserId());

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, 
                ApiResponse<BookDto>.Success(bookDto, "Book added successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book");
            return StatusCode(500, ApiResponse<BookDto>.Error("An error occurred while creating the book."));
        }
    }

    /// <summary>
    /// Update an existing book (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Librarian")]
    public async Task<ActionResult<ApiResponse<BookDto>>> UpdateBook(
        Guid id,
        [FromBody] UpdateBookDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            
            if (book == null)
            {
                return NotFound(ApiResponse<BookDto>.Error("Book not found."));
            }

            // Update fields if provided
            if (!string.IsNullOrWhiteSpace(request.Title))
                book.Title = request.Title;
            
            if (!string.IsNullOrWhiteSpace(request.Author))
                book.Author = request.Author;
            
            if (!string.IsNullOrWhiteSpace(request.Genre))
                book.Genre = request.Genre;
            
            if (request.PublishedYear.HasValue)
                book.PublishedYear = request.PublishedYear.Value;
            
            if (request.TotalCopies.HasValue)
            {
                var difference = request.TotalCopies.Value - book.TotalCopies;
                book.TotalCopies = request.TotalCopies.Value;
                book.AvailableCopies += difference;
                
                if (book.AvailableCopies < 0)
                    book.AvailableCopies = 0;
                
                book.Status = book.AvailableCopies > 0 ? BookStatus.Available : BookStatus.Unavailable;
            }

            book.UpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(book, cancellationToken);

            var bookDto = new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                Status = book.Status.ToString()
            };

            _logger.LogInformation("Book {BookId} updated by user {UserId}", book.Id, GetCurrentUserId());

            return Ok(ApiResponse<BookDto>.Success(bookDto, "Book updated successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book {BookId}", id);
            return StatusCode(500, ApiResponse<BookDto>.Error("An error occurred while updating the book."));
        }
    }

    /// <summary>
    /// Delete a book (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBook(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            
            if (book == null)
            {
                return NotFound(ApiResponse<bool>.Error("Book not found."));
            }

            // Check if book has active reservations
            var activeReservations = await _reservationRepository.GetActiveReservationsByBookIdAsync(id, cancellationToken);
            if (activeReservations.Any())
            {
                return BadRequest(ApiResponse<bool>.Error("Cannot delete book with active reservations."));
            }

            await _bookRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Book {BookId} deleted by user {UserId}", id, GetCurrentUserId());

            return Ok(ApiResponse<bool>.Success(true, "Book deleted successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book {BookId}", id);
            return StatusCode(500, ApiResponse<bool>.Error("An error occurred while deleting the book."));
        }
    }

    /// <summary>
    /// Reserve a book
    /// </summary>
    [HttpPost("{id}/reserve")]
    public async Task<ActionResult<ApiResponse<ReservationDto>>> ReserveBook(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userId = GetCurrentUserId();
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            
            if (book == null)
            {
                return NotFound(ApiResponse<ReservationDto>.Error("Book not found."));
            }

            // Validate that book is available
            if (!await _bookRepository.IsAvailableAsync(id, cancellationToken))
            {
                return BadRequest(ApiResponse<ReservationDto>.Error("Book is not available for reservation."));
            }

            // Check if user already has an active reservation for this book
            if (await _reservationRepository.HasActiveReservationAsync(userId, id, cancellationToken))
            {
                return BadRequest(ApiResponse<ReservationDto>.Error("You already have an active reservation for this book."));
            }

            // Create reservation
            var reservation = new Reservation
            {
                UserId = userId,
                BookId = id,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7), // 7 days to pick up
                Status = ReservationStatus.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _reservationRepository.AddAsync(reservation, cancellationToken);

            // Update book availability
            book.ReserveCopy();
            await _bookRepository.UpdateAsync(book, cancellationToken);

            var reservationDto = new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                BookId = reservation.BookId,
                BookTitle = book.Title,
                ReservationDate = reservation.ReservationDate,
                ExpiryDate = reservation.ExpiryDate,
                CheckoutDate = reservation.CheckoutDate,
                ReturnDate = reservation.ReturnDate,
                Status = reservation.Status.ToString()
            };

            _logger.LogInformation("Book {BookId} reserved by user {UserId}", id, userId);

            return Ok(ApiResponse<ReservationDto>.Success(reservationDto, "Book reserved successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reserving book {BookId}", id);
            return StatusCode(500, ApiResponse<ReservationDto>.Error("An error occurred while reserving the book."));
        }
    }

    /// <summary>
    /// Return a reserved book
    /// </summary>
    [HttpPost("{id}/return")]
    public async Task<ActionResult<ApiResponse<bool>>> ReturnBook(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var userId = GetCurrentUserId();
            
            // Find active reservation
            var reservations = await _reservationRepository.GetActiveReservationsByUserIdAsync(userId, cancellationToken);
            var reservation = reservations.FirstOrDefault(r => r.BookId == id);
            
            if (reservation == null)
            {
                return NotFound(ApiResponse<bool>.Error("No active reservation found for this book."));
            }

            // Complete reservation
            reservation.Complete();
            await _reservationRepository.UpdateAsync(reservation, cancellationToken);

            // Update book availability
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            if (book != null)
            {
                book.ReturnCopy();
                await _bookRepository.UpdateAsync(book, cancellationToken);
            }

            _logger.LogInformation("Book {BookId} returned by user {UserId}", id, userId);

            return Ok(ApiResponse<bool>.Success(true, "Book returned successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error returning book {BookId}", id);
            return StatusCode(500, ApiResponse<bool>.Error("An error occurred while returning the book."));
        }
    }
}
