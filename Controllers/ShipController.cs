using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TestApiV2.Models;
using TestApiV2.Services.ShipService;

namespace TestApiV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Ship>>>> Get()
        {
            return Ok(await _shipService.GetAllShips());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Ship>>> GetSingle(int id)
        {
            return Ok(await _shipService.GetShipById(id));   
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Ship>>>> AddShip(Ship newShip)
        {
            return Ok(await _shipService.AddShip(newShip));
        }
    }
}
