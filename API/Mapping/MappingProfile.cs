using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<RolDto, Rol> ().ReverseMap();
            CreateMap<EstadoBahium, EstadoBahium>().ReverseMap();
            CreateMap<Bahium, Bahium>().ReverseMap();
            CreateMap<EstadoReserva, EstadoReserva>().ReverseMap();
            CreateMap<Parametro, Parametro>().ReverseMap();
            CreateMap<Reserva, Reserva>().ReverseMap();
            CreateMap<TipoVehiculo, TipoVehiculo>().ReverseMap();
            CreateMap<Ubicacion,Ubicacion>().ReverseMap();
            CreateMap<Vehiculo, Vehiculo>().ReverseMap();

        }
    }
}
