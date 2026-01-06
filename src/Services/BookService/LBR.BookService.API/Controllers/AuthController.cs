using LBR.BookService.Application.DTOs;
using LBR.BookService.Application.Interfaces.Repositories;
using LBR.BookService.Domain.Entities;
using LBR.BookService.Domain.Enums;
using LBR.Shared.Authentication.Services;
using LBR.Shared.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace LBR.BookService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly PasswordHasher _passwordHasher;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IUserRepository userRepository,
        JwtTokenService jwtTokenService,
        PasswordHasher passwordHasher,
        ILogger<AuthController> logger)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register(
        [FromBody] RegisterUserDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Username) || 
                string.IsNullOrWhiteSpace(request.Email) || 
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(ApiResponse<AuthResponseDto>.Error("Username, email, and password are required."));
            }

            // Check if username already exists
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
            if (existingUserByUsername != null)
            {
                return BadRequest(ApiResponse<AuthResponseDto>.Error("Username already exists."));
            }

            // Check if email already exists
            var existingUserByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUserByEmail != null)
            {
                return BadRequest(ApiResponse<AuthResponseDto>.Error("Email already exists."));
            }

            // Hash the password
            var passwordHash = _passwordHasher.HashPassword(request.Password);

            // Create new user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                FullName = request.FullName,
                Role = UserRole.User, // Default role
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userRepository.AddAsync(user, cancellationToken);

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Username, user.Role.ToString());

            var response = new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString()
                }
            };

            _logger.LogInformation("User {Username} registered successfully", user.Username);

            return Ok(ApiResponse<AuthResponseDto>.Success(response, "User registered successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during user registration");
            return StatusCode(500, ApiResponse<AuthResponseDto>.Error("An error occurred during registration."));
        }
    }

    /// <summary>
    /// Login with username and password
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(
        [FromBody] LoginDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(ApiResponse<AuthResponseDto>.Error("Username and password are required."));
            }

            // Get user by username
            var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
            if (user == null)
            {
                return Unauthorized(ApiResponse<AuthResponseDto>.Error("Invalid username or password."));
            }

            // Verify password
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(ApiResponse<AuthResponseDto>.Error("Invalid username or password."));
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Username, user.Role.ToString());

            var response = new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString()
                }
            };

            _logger.LogInformation("User {Username} logged in successfully", user.Username);

            return Ok(ApiResponse<AuthResponseDto>.Success(response, "Login successful."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login");
            return StatusCode(500, ApiResponse<AuthResponseDto>.Error("An error occurred during login."));
        }
    }
}
