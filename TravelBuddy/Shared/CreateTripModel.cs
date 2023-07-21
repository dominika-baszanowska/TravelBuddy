namespace TravelBuddy.Shared;

public record CreateTripModel(int userId, int guideId, DateTime startDate, DateTime endDate, string Comments);