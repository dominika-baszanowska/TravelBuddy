namespace TravelBuddy.Shared;

public class MockData
{
    // Define your EU countries here
    public static List<Country> EU_Countries = new ()
    {
        new Country { Flag = "fi fi-de", Name = "Germany", PopularDestinations = new List<string> { "Berlin", "Munich", "Frankfurt" } },
        new Country { Flag = "fi fi-pl", Name = "Poland", PopularDestinations = new List<string> { "Warsaw", "Cracov", "Gdansk" } },
        // add other countries
    };
}