using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace MagicVilla_VillaApi.Models.DTO;

public class VillaCreateDTO
{

    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    public string? Details { get; set; }
    [Required]
    public double Rate { get; set; }
    public int Ocupancy { get; set; }
    public int Sqft { get; set; }
    public string? imageUrl { get; set; }
    public string? Amenity { get; set; }
   
}