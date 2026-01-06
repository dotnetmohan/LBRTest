namespace LBR.BookService.Domain.Enums;

/// <summary>
/// Represents the role of a user in the system
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Regular user with basic permissions (can reserve books)
    /// </summary>
    User = 0,

    /// <summary>
    /// Administrator with full permissions (can manage catalog)
    /// </summary>
    Admin = 1,

    /// <summary>
    /// Librarian with extended permissions
    /// </summary>
    Librarian = 2
}
