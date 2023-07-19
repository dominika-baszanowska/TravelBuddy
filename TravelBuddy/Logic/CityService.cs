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

    public async Task<City> GetCity(int cityId) =>
        await _context.Cities.Where(x => x.Id == cityId).SingleAsync();

    public async Task<List<City>> GetCities(int countryId) => 
        await _context.Cities.Where(x => x.Id == countryId).ToListAsync();
}