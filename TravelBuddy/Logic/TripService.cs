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
        var readyTrips = new List<TripViewModel>();
        
        var trips = _dbContext.Trips.Where(x => x.UserId == userId).ToList();
        
        foreach (var trip in trips)
        {
            var guide = _guideService.GetGuide(trip.GuideId);
            var user = _userService.GetUser(trip.GuideId);
            
            var model = new TripViewModel
            {
                ProfilePicture = user.ProfilePicture,
                GuideFirstName = user.FirstName,
                GuideLastName = user.LastName,
                GuideRating = guide.Rating,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Rating = trip.Rating
            };
            
            readyTrips.Add(model);
        }

        return readyTrips;
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

    public void DeleteTrip(int tripId) =>
        _dbContext.Trips.Where(x => x.Id == tripId).ExecuteDelete();
}