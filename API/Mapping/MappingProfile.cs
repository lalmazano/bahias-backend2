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

        }
    }
}
