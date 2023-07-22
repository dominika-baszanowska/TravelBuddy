using System.ComponentModel.DataAnnotations;

namespace TravelBuddy.Models;

public class Country
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ShortDescription { get; set; }
    
    [Required]
    public string Description { get; set; }

    [Required]
    public string Flag { get; set; }

    [Required]
    public string Capital { get; set; }

    [Required]
    public string Continent { get; set; }
    
    public float Rating { get; set; }
    
    // nav
    public List<City> Cities { get; set; }
    public ICollection<Trip> Trips { get; set; }
}