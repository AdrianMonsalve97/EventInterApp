using Api.Security;
using Application.Eventos.Commands.CrearEvento;
using Application.Eventos.Commands.CreateEvento;
using Application.Eventos.Commands.EditarEvento;
using Application.Eventos.Commands.EliminarEvento;
using Application.Eventos.Commands.InscribirseEvento;
using Application.Eventos.Queries.ListarEventos;
using Application.Eventos.Queries.ListarPorUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Genericos;

namespace Api.Controllers;

[Route("api/eventos")]
[Tags("Eventos")]
[Authorize]
public class EventosController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo evento. Solo requiere información básica.
    /// </summary>
    [HttpPost("crear")]
    public async Task<IActionResult> CrearEvento([FromBody] PeticionGeneral<CreateEventoComman> peticion)
    {
        CreateEventoComman comando = peticion.Data with { Usuario = int.Parse(peticion.Usuario) };
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }
    /// <summary>
    /// Lista todos los eventos disponibles con estado de inscripción para el usuario.
    /// </summary>
    [HttpGet("disponibles")]
    public async Task<IActionResult> ListarEventos([FromQuery] int usuarioId)
    {
        var respuesta = await _mediator.Send(new ListarEventosQuery(usuarioId));
        return Ok(respuesta);
    }

    /// <summary>
    /// Permite a un usuario inscribirse en un evento.
    /// </summary>
    [HttpPost("inscribirse")]
    public async Task<IActionResult> InscribirseEvento([FromBody] InscribirseEventoCommand comando)
    {
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }

    /// <summary>
    /// Permite editar un evento solo si el usuario es su creador.
    /// </summary>
    [HttpPut("editar")]
    public async Task<IActionResult> EditarEvento([FromBody] EditarEventoCommand comando)
    {
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }

    /// <summary>
    /// Lista los eventos en los que el usuario está inscrito.
    /// </summary>
    [HttpGet("mis-eventos")]
    public async Task<IActionResult> MisEventos([FromQuery] int idUsuario)
    {
        var respuesta = await _mediator.Send(new ListarEventosPorUsuarioQuery(idUsuario));
        return Ok(respuesta);
    }

    [AuthorizeRole("Administrador, Expositor")]
    [HttpDelete("eliminar")]
    public async Task<IActionResult> EliminarEvento([FromBody] EliminarEventoCommand comando)
    {
        var respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }

}