// Uncomment when BCrypt.Net-Next package is installed
/*
namespace LBR.Shared.Authentication.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
*/

// Placeholder until BCrypt.Net-Next package is installed
namespace LBR.Shared.Authentication.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Temporary basic implementation - replace with BCrypt when package is installed
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            // Temporary basic implementation - replace with BCrypt when package is installed
            var hashedInput = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            return hashedInput == passwordHash;
        }
    }
}
