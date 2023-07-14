using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Models;

public class Language
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Reverse navigation property for many-to-many relationship
    public ICollection<Guide> Guides { get; set; }

    // Constructor
    public Language()
    {
        Guides = new HashSet<Guide>();
    }
}