using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.multitracks.com.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Artist : ControllerBase
    {
        //search for artist by name
        [HttpGet("search")]
        public IActionResult Get()
        {
            return null;
        }        
        //add artist
        [HttpPost("add")]
        public IActionResult Post()
        {
            return null;
        }
    }
}
