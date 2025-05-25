using AutoMapper;
using Domain.Entities;
using Application.Common.DTOs.EventosDtos;

namespace Application.Common.Mappings;

/// <summary>
/// Perfil de AutoMapper que define los mapeos entre entidades y DTOs.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Evento → EventoDto
        CreateMap<Evento, EventoDto>();
        CreateMap<EventoDto, Evento>();

        // Usuario → UsuarioDto


        // Puedes agregar más mapeos aquí según tu dominio
    }
}
