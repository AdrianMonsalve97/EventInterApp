﻿using MediatR;
using Shared.Contracts.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Eventos.Commands.CreateEvento
{
    public record CreateEventoCommand(string Nombre,
                                      string? Descripcion,
                                      DateTime FechaHora,
                                      string? Ubicacion,
                                      int CapacidadMaxima) :  IRequest<RespuestaGeneral<string>>;

}
