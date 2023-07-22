using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;

namespace TravelBuddy.Logic;

public class GuideService
{
    private readonly AppDbContext _context;

    public GuideService(AppDbContext context)
    {
        _context = context;
    }

    public Guide GetGuide(int guideId) =>
        _context.Guides.Single(x => x.UserId == guideId);
    
    public List<Guide> GetAllGuidesAsync() => 
        _context.Guides.ToList();

    public List<Guide> GetNearbyGuides(double userLat, double userLon, double maxDistance = 30)
    {
        var dummy = _context.Users.Where(x => x.GuideDetails != null).ToList();
        var allGuides = _context.Guides.ToList();
        var nearbyGuides = new List<Guide>();

        foreach (var guide in allGuides)
        {
            var distance = CalculateDistance(userLat, userLon, (double)guide.Latitude, (double)guide.Longitude);
            if (distance <= maxDistance)
            {
                nearbyGuides.Add(guide);
            }
        }

        return nearbyGuides;
    }
    
    // Haversine formula
    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const int r = 6371; // Radius of the earth in km
        var dLat = DegreesToRadians(lat2 - lat1);
        var dLon = DegreesToRadians(lon2 - lon1);
        var a = 
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2); 
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)); 
        var distance = r * c; // Distance in km
        return distance;
    }

    private static double DegreesToRadians(double deg)
    {
        return deg * (Math.PI / 180);
    }
}
