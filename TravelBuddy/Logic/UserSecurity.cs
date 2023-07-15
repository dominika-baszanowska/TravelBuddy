using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;
using TravelBuddy.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TravelBuddy.Logic;

public class UserSecurity
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;

    private AppDbContext _database;

    public UserSecurity(AppDbContext database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    private static string HashPassword(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(
            password,
            SaltSize,
            Iterations,
            HashAlgorithmName.SHA256);
        
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{salt}.{key}";
    }

    private static bool VerifyPassword(string hash, string password)
    {
        var parts = hash.Split('.', 2);

        if (parts.Length != 2)
        {
            throw new FormatException("Unexpected hash format");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256);
        
        var keyToCheck = algorithm.GetBytes(KeySize);
        
        return keyToCheck.SequenceEqual(key);
    }

    private static string GenerateBearerToken(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "YourSecretKey"u8.ToArray(); // TODO: this key should be a secret and stored in a safe location e.g. appsettings.json
    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim(ClaimTypes.Name, userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token validity period (one week in this example)
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string> GetLoginToken(string email, string password)
    {
        var passwordHash = HashPassword(password);

        var user = await _database.Users.Where(x => x.Email == email && x.PasswordHash == passwordHash).FirstAsync();

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var token = GenerateBearerToken(user.Id);

        return token;
    }
}