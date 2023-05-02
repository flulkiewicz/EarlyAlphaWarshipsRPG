using Microsoft.AspNetCore.Mvc;
using TestApiV2.Models;

namespace TestApiV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : ControllerBase
    {
        private static Ship boat = new Ship();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(boat);
        }
    }
}
