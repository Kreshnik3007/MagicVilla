using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace MagicVilla_VillaApi.Models.DTO;

public class VillaUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    public string? Details { get; set; }
    [Required]
    public double Rate { get; set; }
    [Required]
    public int Ocupancy { get; set; }
    [Required]
    public int Sqft { get; set; }
    [Required]
    public string? imageUrl { get; set; }
    public string? Amenity { get; set; }
    
    public DateTime CreatedDate { get; set; }
   
}