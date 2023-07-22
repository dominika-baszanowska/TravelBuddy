using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;
using TravelBuddy.Shared;

namespace TravelBuddy.Logic;

public class TripService
{
    private readonly AppDbContext _dbContext;
    private readonly GuideService _guideService;
    private readonly UserService _userService;

    public TripService(AppDbContext dbContext, GuideService guideService, UserService userService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _guideService = guideService ?? throw new ArgumentNullException(nameof(guideService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public Trip GetTrip(int tripId) =>
        _dbContext.Trips.Single(x => x.Id == tripId);

    public List<TripViewModel> GetUserTrips(int userId)
    {
        var trips = _dbContext.Trips.Where(x => x.UserId == userId).ToList();

        return (from trip in trips
            let guide = _guideService.GetGuide(trip.GuideId)
            let user = _userService.GetUser(trip.GuideId)
            select new TripViewModel
            {
                Id = trip.Id,
                ProfilePicture = user.ProfilePicture,
                GuideFirstName = user.FirstName,
                GuideLastName = user.LastName,
                GuideRating = guide.Rating,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Rating = trip.Rating
            }).ToList();
    }

    public Trip CreateTrip(int userId, int guideId, int cityId, DateTime startDate, DateTime endDate, 
        string comments, decimal price, int countryId)
    {
        var trip = new Trip
        {
            UserId = userId,
            GuideId = guideId,
            StartDate = startDate,
            EndDate = endDate,
            Comments = comments,
            Price = price,
            CountryId = countryId,
            CityId = cityId
        };

        _dbContext.Trips.Add(trip);
        _dbContext.SaveChanges();

        return trip;
    }

    public void UpdateTripRating(int tripId, float tripRating)
    {
        var trip = _dbContext.Trips.Single(x => x.Id == tripId);

        trip.Rating = tripRating;

        _dbContext.SaveChanges();
    }

    public void DeleteTrip(int tripId) =>
        _dbContext.Trips.Where(x => x.Id == tripId).ExecuteDelete();
}