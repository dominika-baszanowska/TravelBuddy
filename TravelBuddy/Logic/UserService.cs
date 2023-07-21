using TravelBuddy.Models;
using TravelBuddy.Shared;

namespace TravelBuddy.Logic;

public class UserService
{
    private readonly AppDbContext _dbContext;
    private readonly SecurityService _security;

    public UserService(AppDbContext dbContext, SecurityService security)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _security = security ?? throw new ArgumentNullException(nameof(security));
    }

    public User CreateUser(RegisterUser registerUser)
    {
        var user = new User
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email,
            PhoneNumber = registerUser.PhoneNumber,
            PasswordHash = SecurityService.HashPassword(registerUser.Password),
            ProfilePicture = registerUser.ProfilePicture,
            CreatedAt = DateTime.UtcNow,
        };

        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();

        return user;
    }
}