namespace TravelBuddy.Shared;

public record CreateUserModel(string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ProfilePicture);