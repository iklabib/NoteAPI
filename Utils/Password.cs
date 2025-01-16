using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

public static class PasswordUtil
{
    public static (string, string) EncryptPassword(string clearText)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string encrypted = BCrypt.Net.BCrypt.HashPassword(clearText, salt);
        return (encrypted, salt);
    }

    public static bool VerifyPassword(string clearText, string encrypted, string salt)
    {
        return BCrypt.Net.BCrypt.Verify(clearText, encrypted);
    }

    public static string GenerateToken(User user, string secret)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static (bool isStrong, string message) CheckStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
            return (false, "Password cannot be empty");

        if (password.Length < 8)
            return (false, "Password must be at least 8 characters long");

        if (!password.Any(char.IsUpper))
            return (false, "Password must contain at least one uppercase letter");

        if (!password.Any(char.IsLower))
            return (false, "Password must contain at least one lowercase letter");

        if (!password.Any(char.IsDigit))
            return (false, "Password must contain at least one number");

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            return (false, "Password must contain at least one special character");

        var commonPatterns = new[] { "123", "abc", "qwerty", "password" };
        if (commonPatterns.Any(pattern => password.ToLower().Contains(pattern)))
            return (false, "Password contains common patterns that make it weak");

        return (true, "Password is strong");
    }
}