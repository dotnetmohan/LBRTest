namespace LBR.BookService.Application.DTOs;

/// <summary>
/// DTO for creating a reservation
/// </summary>
public class CreateReservationDto
{
    public Guid BookId { get; set; }
    public string? Notes { get; set; }
    public int? ReservationDays { get; set; } = 7; // Default 7 days
}
