using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;

namespace TravelBuddy.Logic;

public class TripService
{
    private readonly AppDbContext _dbContext;

    public TripService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Trip GetTrip(int tripId) =>
        _dbContext.Trips.Single(x => x.Id == tripId);

    public List<Trip> GetUserTrips(int userId) =>
        _dbContext.Trips.Where(x => x.UserId == userId).ToList();

    public Trip CreateTrip(int userId, int guideId, int cityId, DateTime startDate, DateTime endDate, 
        string comments)
    {
        var trip = new Trip
        {
            UserId = userId,
            GuideId = guideId,
            StartDate = startDate,
            EndDate = endDate,
            Comments = comments
        };

        _dbContext.Trips.Add(trip);
        _dbContext.SaveChanges();

        return trip;
    }

    public void DeleteTrip(int tripId) =>
        _dbContext.Trips.Where(x => x.Id == tripId).ExecuteDelete();
}