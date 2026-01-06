namespace LBR.BookService.Domain.Entities;

/// <summary>
/// Represents a book in the library catalog
/// </summary>
public class Book
{
    /// <summary>
    /// Unique identifier for the book
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// International Standard Book Number
    /// </summary>
    public string ISBN { get; set; } = string.Empty;

    /// <summary>
    /// Book title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Author name(s)
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Book genre/category
    /// </summary>
    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Year the book was published
    /// </summary>
    public int PublishedYear { get; set; }

    /// <summary>
    /// Publisher name
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// Book description/summary
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Total number of copies in the library
    /// </summary>
    public int TotalCopies { get; set; }

    /// <summary>
    /// Number of copies currently available for reservation
    /// </summary>
    public int AvailableCopies { get; set; }

    /// <summary>
    /// When the book was added to the catalog
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When the book information was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Whether the book is active in the catalog
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for reservations of this book
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Checks if the book is available for reservation
    /// </summary>
    public bool IsAvailable => IsActive && AvailableCopies > 0;

    /// <summary>
    /// Gets the number of copies currently reserved
    /// </summary>
    public int ReservedCopies => TotalCopies - AvailableCopies;

    /// <summary>
    /// Reserves a copy of the book
    /// </summary>
    /// <returns>True if reservation was successful, false otherwise</returns>
    public bool ReserveCopy()
    {
        if (!IsAvailable)
            return false;

        AvailableCopies--;
        return true;
    }

    /// <summary>
    /// Returns a reserved copy back to available
    /// </summary>
    public void ReturnCopy()
    {
        if (AvailableCopies < TotalCopies)
        {
            AvailableCopies++;
        }
    }
}
