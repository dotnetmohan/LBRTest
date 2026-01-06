using Microsoft.AspNetCore.Mvc;

namespace LBR.BookService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Service = "LBR Book Service API",
            Version = "1.0.0",
            Timestamp = DateTime.UtcNow,
            Message = "API is running successfully!"
        });
    }

    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        return Ok(new
        {
            Service = "Library Book Reservation System",
            Architecture = "Clean Architecture",
            Layers = new[] { "Domain", "Application", "Infrastructure", "API" },
            Status = "Development",
            Features = new[]
            {
                "Book Management",
                "Reservation System",
                "User Authentication (JWT - pending packages)",
                "Role-based Authorization"
            },
            Endpoints = new
            {
                Health = "GET /api/health",
                Info = "GET /api/health/info",
                Books = "/api/books (pending implementation)",
                Auth = "/api/auth (pending implementation)"
            }
        });
    }
}
