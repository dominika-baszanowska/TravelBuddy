using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;

namespace TravelBuddy.Logic;

public class CityService
{
    private readonly AppDbContext _context;

    public CityService(AppDbContext context)
    {
        _context = context;
    }

    public City GetCity(int cityId) =>
        _context.Cities.Single(x => x.Id == cityId);

    public List<City> GetCities(int countryId) => 
        _context.Cities.Where(x => x.CountryId == countryId).ToList();
}