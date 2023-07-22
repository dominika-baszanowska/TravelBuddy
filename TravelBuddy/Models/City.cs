using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Country")]
    public int CountryId { get; set; }
    
    [Required]
    public string Name { get; set; }

    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    // Navigational properties
    public Country Country { get; set; }
    public ICollection<Trip> Trips { get; set; }
}