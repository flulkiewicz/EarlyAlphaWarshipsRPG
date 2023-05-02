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
        public ActionResult<List<Ship>> Get()
        {
            return Ok(_shipService.GetAllShips());
        }

        [HttpGet("{id}")]
        public ActionResult<Ship> GetSingle(int id)
        {
            return Ok(_shipService.GetShipById(id));   
        }

        [HttpPost]
        public ActionResult<List<Ship>> AddShip(Ship newShip)
        {
            return Ok(_shipService.AddShip(newShip));
        }
    }
}
