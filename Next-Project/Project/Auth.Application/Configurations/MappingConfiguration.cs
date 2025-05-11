using Auth.Domain.Models;
using Auth.Shared.DTOs;
using AutoMapper;

namespace Auth.Application.Configurations;

public class MappingConfiguration
{
    public static Mapper InitializeConfig()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, SignUpDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(u => u.Name))
                .ForMember(
                    dest => dest.Surname,
                    opt => opt.MapFrom(u => u.Surname))
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(u => u.Username))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(u => u.Email));

            cfg.CreateMap<AppRole, RoleDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(a => a.Name)).ReverseMap();
        });

        var mapper = new Mapper(mapperConfig);

        return mapper;
    }
}