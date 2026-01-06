using LBR.BookService.Domain.Enums;

namespace LBR.BookService.Application.DTOs;

/// <summary>
/// DTO for reservation information
/// </summary>
public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReservedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? PickedUpAt { get; set; }
    public ReservationStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Related entities
    public BookDto? Book { get; set; }
    public UserDto? User { get; set; }
    
    public bool IsActive { get; set; }
    public int? DaysUntilExpiration { get; set; }
}
