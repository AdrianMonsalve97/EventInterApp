using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs.EventosDtos
{
    public record CreateEventoDto(
      int Id,
      string Nombre,
      string? Descripcion,
      DateTime FechaHora,
      string? Ubicacion,
      int CapacidadMaxima
  );
}
