using AutoMapper;
using Domain.Entities;
using Application.Common.DTOs.EventosDtos;
using Application.Common.DTOs.InscripcionesDtos;
using Application.Common.DTOs.UsuariosDtos;

namespace Application.Common.Mappings;

/// <summary>
/// Perfil de AutoMapper que define los mapeos entre entidades y DTOs.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioListadoDto>();
        CreateMap<Usuario, UsuarioAdminListadoDto>();
        CreateMap<Inscripcion, InscripcionDto>()
            .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.UsuarioId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Usuario.Nombre))
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.NombreUsuario))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usuario.Email))
            .ForMember(dest => dest.FechaInscripcion, opt => opt.MapFrom(src => src.FechaInscripcion));
        CreateMap<Evento, EventoDisponibleDto>()
        .ForMember(dest => dest.CantidadInscritos, opt => opt.MapFrom(src => src.Inscripciones.Count))
        .ForMember(dest => dest.EstaInscrito, opt => opt.Ignore()); 


    }
}
