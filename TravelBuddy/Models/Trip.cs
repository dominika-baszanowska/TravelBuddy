using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Models;

public class Trip
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Guide")]
    public int GuideId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
    
    public string Comments { get; set; }

    public string? Review { get; set; }

    public float? Rating { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    [ForeignKey("Country")]
    public int CountryId { get; set; }
    
    [Required]
    [ForeignKey("City")]
    public int CityId { get; set; }

    // Navigational properties
    public User User { get; set; }
    public Guide Guide { get; set; }
    public Country Country { get; set; }
    public City City { get; set; }
}