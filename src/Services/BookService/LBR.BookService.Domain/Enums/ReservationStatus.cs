namespace LBR.BookService.Domain.Enums;

/// <summary>
/// Represents the status of a book reservation
/// </summary>
public enum ReservationStatus
{
    /// <summary>
    /// Reservation is active and valid
    /// </summary>
    Active = 0,

    /// <summary>
    /// Reservation has been cancelled by the user
    /// </summary>
    Cancelled = 1,

    /// <summary>
    /// Reservation has expired (not picked up in time)
    /// </summary>
    Expired = 2,

    /// <summary>
    /// Reservation is completed (book was picked up)
    /// </summary>
    Completed = 3
}
