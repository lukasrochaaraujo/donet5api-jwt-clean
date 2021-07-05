using System;
using Microsoft.AspNetCore.Mvc;
using WebApiJw.Authorization;

namespace WebApiJwt
{
    [ApiController]
    [Route("[controller]")]
    public class AnythingController : ControllerBase
    {
        [Authorization]
        [HttpGet]
        public IActionResult AuthrizedGetAll()
        {
            return Ok(new {
                NowIs = DateTime.Now
            });
        }
    }
}