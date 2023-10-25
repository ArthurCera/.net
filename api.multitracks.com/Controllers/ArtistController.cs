using api.multitracks.com.Model;
using api.multitracks.com.QueryHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api.multitracks.com.Controllers
{
    [ApiController]
    [Route("Artist")]
    public class ArtistController : ControllerBase
    {
        //search for artist by name
        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] string artistName, [FromServices] GetArtistQueryHandler handler)
        {
            try { 
                //getting the artist from the handler
                var ret = await handler.ExecuteAsync(artistName);
                //if null return not found
                if (ret == null)
                {
                    return NotFound();
                }
                //if the data was properly found return it
                else return Ok(ret);
            }
            catch (Exception ex)
            {
                //return the inner exception in case of any
                return BadRequest(ex.InnerException);
            }
        }        
        //add artist
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] AddArtistCommand command, [FromServices] AddArtistQueryHandler handler)
        {
            try
            {
                //trying to add the artist
                var ret = await handler.ExecuteAsync(command);
                //return a status based on the completion
                if (ret) return Ok();
                else return BadRequest();
            }
            catch (Exception ex)
            {
                //return the inner exception in case of any
                return BadRequest(ex.InnerException);
            }
        }
    }
}
