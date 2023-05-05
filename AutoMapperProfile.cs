using WarshipsRPGAlpha.Dtos.MainGun;
using WarshipsRPGAlpha.Dtos.SpecialWaepon;

namespace WarshipsRPGAlpha
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ship, GetShipResponseDto>();
            CreateMap<AddShipRequestDto, Ship>();
            CreateMap<UpdateShipRequestDto, Ship>();
            CreateMap<MainGun, GetMainGunDto>();
            CreateMap<SpecialWaepon, GetSpecialWaeponDto>();
        }
    }
}
