namespace LBR.BookService.Application.DTOs;

/// <summary>
/// DTO for creating a new book
/// </summary>
public class CreateBookDto
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public int TotalCopies { get; set; }
}
