using Microsoft.EntityFrameworkCore;
using Radzen;
using TravelBuddy.Models;

namespace TravelBuddy.Logic;

public class TripService
{
    private readonly AppDbContext _dbContext;

    public TripService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Trip> CreateTrip(int userId, int guideId, int cityId, DateTime startDate, DateTime endDate)
    {
        /*
        var user = await _dbContext.Users.Where(x => x.Id == userId).SingleAsync();
        var guide = await _dbContext.Guides.Where(x => x.UserId == guideId).SingleAsync();
        var city = await _dbContext.Cities.Where(x => x.Id == cityId).SingleAsync();
        */

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
}