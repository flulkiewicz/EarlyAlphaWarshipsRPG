namespace WarshipsRPGAlpha
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ship, GetShipResponseDto>();
            CreateMap<AddShipRequestDto, Ship>();
            CreateMap<UpdateShipRequestDto, Ship>();
        }
    }
}
