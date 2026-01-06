namespace LBR.BookService.Domain.Enums;

/// <summary>
/// Represents the availability status of a book
/// </summary>
public enum BookStatus
{
    /// <summary>
    /// Book is available for reservation
    /// </summary>
    Available = 0,

    /// <summary>
    /// Book is currently reserved by a user
    /// </summary>
    Reserved = 1,

    /// <summary>
    /// Book is checked out and not available
    /// </summary>
    CheckedOut = 2,

    /// <summary>
    /// Book is unavailable (maintenance, lost, etc.)
    /// </summary>
    Unavailable = 3
}
