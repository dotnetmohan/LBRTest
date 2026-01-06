namespace LBR.BookService.Application.DTOs;

/// <summary>
/// DTO for updating a book
/// </summary>
public class UpdateBookDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int? PublishedYear { get; set; }
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public int? TotalCopies { get; set; }
}
