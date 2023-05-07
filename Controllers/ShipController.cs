using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;
using WarshipsRPGAlpha.Dtos.Ship;
using WarshipsRPGAlpha.Dtos.SpecialWaepon;
using WarshipsRPGAlpha.Models;
using WarshipsRPGAlpha.Services.ShipService;

namespace WarshipsRPGAlpha.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;
        private readonly ILogger<ShipController> _logger;

        public ShipController(IShipService shipService, ILogger<ShipController> logger)
        {
            _shipService = shipService;
            _logger = logger;
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
            _logger.LogWarning($"DELETE ACTION INVOKED: Ship with id:{id}");
            return Ok(await _shipService.DeleteShip(id));
        }

        [HttpPost("SpecialWaepon")]
        public async Task<ActionResult<ServiceResponse<GetShipResponseDto>>> AddNewSpecialWaepon(AddSpecialWaeponDto newSpecialWaepon)
        {
            return Ok(await _shipService.AddSpecialWaepon(newSpecialWaepon));
        }
    }
}
