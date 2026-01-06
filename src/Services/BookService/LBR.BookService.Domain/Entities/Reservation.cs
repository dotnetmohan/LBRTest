using LBR.BookService.Domain.Enums;

namespace LBR.BookService.Domain.Entities;

/// <summary>
/// Represents a book reservation by a user
/// </summary>
public class Reservation
{
    /// <summary>
    /// Unique identifier for the reservation
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the book being reserved
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// ID of the user making the reservation
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// When the reservation was made
    /// </summary>
    public DateTime ReservedAt { get; set; }

    /// <summary>
    /// When the reservation expires (optional)
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// When the book was picked up (optional)
    /// </summary>
    public DateTime? PickedUpAt { get; set; }

    /// <summary>
    /// When the book was returned (optional)
    /// </summary>
    public DateTime? ReturnedAt { get; set; }

    /// <summary>
    /// Current status of the reservation
    /// </summary>
    public ReservationStatus Status { get; set; }

    /// <summary>
    /// Additional notes about the reservation
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Navigation property to the book
    /// </summary>
    public virtual Book Book { get; set; } = null!;

    /// <summary>
    /// Navigation property to the user
    /// </summary>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Checks if the reservation is currently active
    /// </summary>
    public bool IsActive => Status == ReservationStatus.Active;

    /// <summary>
    /// Checks if the reservation has expired
    /// </summary>
    public bool IsExpired => ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow && Status == ReservationStatus.Active;

    /// <summary>
    /// Gets the number of days until expiration
    /// </summary>
    public int? DaysUntilExpiration
    {
        get
        {
            if (!ExpiresAt.HasValue || Status != ReservationStatus.Active)
                return null;

            var days = (ExpiresAt.Value - DateTime.UtcNow).Days;
            return days > 0 ? days : 0;
        }
    }

    /// <summary>
    /// Cancels the reservation
    /// </summary>
    public void Cancel()
    {
        if (Status == ReservationStatus.Active)
        {
            Status = ReservationStatus.Cancelled;
        }
    }

    /// <summary>
    /// Marks the reservation as completed
    /// </summary>
    public void Complete()
    {
        if (Status == ReservationStatus.Active)
        {
            Status = ReservationStatus.Completed;
            PickedUpAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Marks the reservation as expired
    /// </summary>
    public void MarkAsExpired()
    {
        if (Status == ReservationStatus.Active && IsExpired)
        {
            Status = ReservationStatus.Expired;
        }
    }
}
