using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarshipsRPGAlpha.Dtos.MainGun;
using WarshipsRPGAlpha.Services.MainGunService;

namespace WarshipsRPGAlpha.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MainGunController : ControllerBase
    {
        private readonly IMainGunService _mainGunService; 
        public MainGunController(IMainGunService mainGunService)
        {
            _mainGunService = mainGunService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetShipResponseDto>>> AddMainGun(AddMainGunDto addMainGun)
        {
            return Ok(await _mainGunService.AddMainGun(addMainGun));
        }
    }
}
