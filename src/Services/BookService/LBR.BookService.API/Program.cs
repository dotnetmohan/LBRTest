using LBR.BookService.API.Middleware;
using LBR.BookService.Application.Interfaces.Repositories;
using LBR.BookService.Infrastructure.Data;
using LBR.BookService.Infrastructure.Repositories;
using LBR.Shared.Authentication.Services;
// Uncomment when packages are installed:
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// NOTE: The following packages are commented out in .csproj due to network issues:
// - Microsoft.EntityFrameworkCore
// - Microsoft.EntityFrameworkCore.InMemory
// - System.IdentityModel.Tokens.Jwt
// - Microsoft.AspNetCore.Authentication.JwtBearer
// 
// Uncomment these services once packages are installed:

// Configure DbContext with InMemory database
// builder.Services.AddDbContext<BookDbContext>(options =>
//     options.UseInMemoryDatabase("LBRDatabase"));

// Register repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register authentication services
builder.Services.AddScoped<JwtTokenService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
    var issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
    var audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");
    var expiryInMinutes = int.Parse(configuration["Jwt:ExpiryInMinutes"] ?? "60");
    
    return new JwtTokenService(key, issuer, audience, expiryInMinutes);
});

builder.Services.AddScoped<PasswordHasher>();

// Configure JWT Authentication
// Uncomment this section once Microsoft.AspNetCore.Authentication.JwtBearer package is installed:
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     var key = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
//     };
// });

builder.Services.AddAuthorization();

// Add CORS (optional, for frontend integration)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Seed the database (uncomment when EF Core packages are installed)
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
//     await DbSeeder.SeedAsync(context);
// }

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    // Swagger temporarily disabled due to package download issues
    // Uncomment these lines once Swashbuckle.AspNetCore package is installed:
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Uncomment once JWT authentication packages are installed:
// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
