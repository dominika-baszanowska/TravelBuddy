namespace TravelBuddy.Shared;

public class TripViewModel
{
    public int Id { get; set; }
    
    public string ProfilePicture { get; set; }
    
    public string GuideFirstName { get; set; }
    
    public string GuideLastName { get; set; }
    
    public float? GuideRating { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public float? Rating { get; set; }
}