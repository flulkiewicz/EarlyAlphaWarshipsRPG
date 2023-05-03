using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TestApiV2.Dtos.Ship;
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
        public async Task<ActionResult<ServiceResponse<List<GetShipResponseDto>>>> Get()
        {
            return Ok(await _shipService.GetAllShips());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetShipResponseDto>>> GetSingle(int id)
        {
            return Ok(await _shipService.GetShipById(id));   
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetShipResponseDto>>>> AddShip(AddShipRequestDto newShip)
        {
            return Ok(await _shipService.AddShip(newShip));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetShipResponseDto>>>> UpdateShip(UpdateShipRequestDto updatedShip)
        {
            return Ok(await _shipService.UpdateShip(updatedShip));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetShipResponseDto>>>> DeleteShip(int id)
        {
            return Ok(await _shipService.DeleteShip(id));
        }
    }
}
