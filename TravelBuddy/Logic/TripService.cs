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

    public async Task<Trip> GetTrip(int tripId) =>
        await _dbContext.Trips.Where(x => x.Id == tripId).SingleAsync();

    public async Task<Trip> CreateTrip(int userId, int guideId, int cityId, DateTime startDate, DateTime endDate)
    {
        var trip = new Trip
        {
            UserId = userId,
            GuideId = guideId,
            StartDate = startDate,
            EndDate = endDate
        };

        await _dbContext.Trips.AddAsync(trip);
        await _dbContext.SaveChangesAsync();

        return trip;
    }

    public async Task DeleteTrip(int tripId) =>
        await _dbContext.Trips.Where(x => x.Id == tripId).ExecuteDeleteAsync();
}