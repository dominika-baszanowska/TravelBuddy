namespace TravelBuddy.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Guide
{
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public string Bio { get; set; }

    public string Availability { get; set; }

    public float Rating { get; set; }

    public int ExperienceYears { get; set; }

    public ICollection<Language> Languages { get; set; }
    
    [StringLength(3)]
    public string CurrencyIso { get; set; }

    public decimal PricePerHour { get; set; }

    public bool IsVolunteer { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    // Navigational properties
    public User User { get; set; }
    public ICollection<Trip> Trips { get; set; }

    // Constructor
    public Guide()
    {
        Languages = new HashSet<Language>();
    }
}