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
    public class Song : ControllerBase
    {
        //list using pagination
        [HttpGet("List")]
        public IActionResult Get()
        {
            return null;
        }
    }
}
