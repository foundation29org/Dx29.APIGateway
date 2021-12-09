using System;

using Microsoft.AspNetCore.Mvc;

namespace Dx29.APIGateway.Controllers
{
    [ApiController]
    public class AboutController : ControllerBase
    {
        public const string VERSION = "v0.0.1";

        [HttpGet("api/[controller]/version")]
        public IActionResult Version()
        {
            return Ok(VERSION);
        }
    }
}
