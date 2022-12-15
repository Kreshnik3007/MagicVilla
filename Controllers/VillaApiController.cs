
using System.Runtime.CompilerServices;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers;


[ApiController]
[Route("api/VillaApi")]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("get")]
    public ActionResult<List<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }
   
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("{id:int}", Name = "GetVilla")]

    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = Ok(VillaStore.villaList.FirstOrDefault(u => u.Id == id));
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
   
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
    {
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        if(villaDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDTO);
        
        return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);
    }

}