namespace TravelBuddy.Shared;

public record RegisterUser(string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ProfilePicture);