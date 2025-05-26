using Application.Eventos.Commands.CreateEvento;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Eventos.Commands.CrearEvento;

public sealed class CrearEventoHandler : IRequestHandler<CreateEventoComman, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public CrearEventoHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(CreateEventoComman request, CancellationToken cancellationToken)
    {
        Evento nuevoEvento = new Evento
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            FechaHora = request.FechaHora,
            Ubicacion = request.Ubicacion,
            CapacidadMaxima = request.CapacidadMaxima,
            IdCreador = request.Usuario
        };

        _context.Eventos.Add(nuevoEvento);
        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Evento creado correctamente.");
    }
}
