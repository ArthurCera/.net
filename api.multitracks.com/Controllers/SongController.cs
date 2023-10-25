using api.multitracks.com.QueryHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api.multitracks.com.Controllers
{
    [ApiController]
    [Route("Song")]
    public class SongController : ControllerBase
    {
        //list using pagination
        [HttpGet("List")]
        public async Task<IActionResult> Get([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string orderBy, [FromQuery] string orderDir, [FromQuery] string search, [FromServices] GetSongListQueryHandler handler)
        {
            try {
                return Ok(await handler.ExecuteAsync(pageIndex, pageSize, search, orderBy, orderDir));
            }
            catch (Exception ex)
            {
                //return the inner exception in case of any
                return BadRequest(ex.InnerException);
            }
        }
    }
}
