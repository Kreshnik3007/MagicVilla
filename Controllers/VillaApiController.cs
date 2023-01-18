using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTO;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers;


[ApiController]
[Route("api/VillaApi")]
public class VillaApiController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public VillaApiController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("get")]
    public async Task<ActionResult<List<VillaDTO>>> GetVillas()
    {
        
        return Ok( await _db.Villas.ToListAsync());
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("{id:int}", Name = "GetVilla")]

    public async Task<ActionResult<VillaDTO>> GetVilla(int id)
    {
        if (id == 0)
        {
            
            return BadRequest();
        }

        var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(villa);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("post")]
   
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDTO)
    {
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        if ( await _db.Villas.AnyAsync(u => string.Equals(u.Name, villaDTO.Name)))
        {
            ModelState.AddModelError("", "Villa Already Exists");
            return BadRequest(ModelState);
        }
        
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }

        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            imageUrl = villaDTO.imageUrl,
            Name = villaDTO.Name,
            Ocupancy = villaDTO.Ocupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft,

        };
       await _db.Villas.AddAsync(model);
       await _db.SaveChangesAsync();
        
        return CreatedAtRoute("GetVilla", new {id = model.Id}, model);
    }
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        _db.SaveChanges();
        return NoContent();
    }
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
    {
        if (villaDTO == null)
        {
            return BadRequest("Could not perform action");
        }

        // var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
        // villa.Name = villaDTO.Name;
        // villa.Sqft = villaDTO.Sqft;
        // villa.Ocupancy = villaDTO.Ocupancy;
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Id = villaDTO.Id,
            imageUrl = villaDTO.imageUrl,
            Name = villaDTO.Name,
            Ocupancy = villaDTO.Ocupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft,
        };
        _db.Villas.Update(model);
        _db.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest(); 
        }

        var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
        VillaUpdateDTO villaDto = new()
        {
            Amenity = villa.Amenity,
            Details = villa.Details,
            Id = villa.Id,
            imageUrl = villa.imageUrl,
            Name = villa.Name,
            Ocupancy = villa.Ocupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
            CreatedDate = DateTime.Now            
        };
        if (villa == null)
        {
            return BadRequest();
        }
        patchDTO.ApplyTo(villaDto, ModelState);
        Villa model = new Villa()
        {
            Amenity = villaDto.Amenity,
            Details = villaDto.Details,
            Id = villaDto.Id,
            imageUrl = villaDto.imageUrl,
            Name = villaDto.Name,
            Ocupancy = villaDto.Ocupancy,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
        };
        _db.Villas.Update(model);
        _db.SaveChanges();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}