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

    public User GetUser(int userId) =>
        _dbContext.Users.Single(x => x.Id == userId);

    public User CreateUser(CreateUserModel createTripModel)
    {
        var user = new User
        {
            FirstName = createTripModel.FirstName,
            LastName = createTripModel.LastName,
            Email = createTripModel.Email,
            PhoneNumber = createTripModel.PhoneNumber,
            PasswordHash = SecurityService.HashPassword(createTripModel.Password),
            ProfilePicture = createTripModel.ProfilePicture,
            CreatedAt = DateTime.UtcNow,
        };

        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();

        return user;
    }
}