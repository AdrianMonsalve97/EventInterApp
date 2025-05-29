using Application.Common.DTOs.EventosDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Queries.ListarIdXEvento
{
    public sealed class ListarIdXEventoQuery : IRequest<List<EventoIdNombreDto>>
    {
    }
}
