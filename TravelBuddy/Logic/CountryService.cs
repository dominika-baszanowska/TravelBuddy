using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models;

namespace TravelBuddy.Logic;

public class CountryService
{
    private readonly AppDbContext _context;

    public CountryService(AppDbContext context)
    {
        _context = context;
    }

    public List<Country> GetCountries()
    {
        return _context.Countries.ToList();
    }

}