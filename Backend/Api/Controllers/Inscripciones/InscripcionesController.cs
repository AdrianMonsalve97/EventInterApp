using Application.Inscripciones.Commands.EliminarInscripcion;
using Application.Inscripciones.Queries.ListarInscripciones;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Genericos;

namespace Api.Controllers;

[ApiController]
[Route("api/inscripciones")]
[Tags("Inscripciones")]
public class InscripcionesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InscripcionesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista los usuarios inscritos a un evento.
    /// </summary>
    [HttpGet("evento/{idEvento}")]
    public async Task<IActionResult> ListarPorEvento(int idEvento)
    {
        var resultado = await _mediator.Send(new ListarInscripcionesQuery(idEvento));
        return Ok(resultado);
    }

    /// <summary>
    /// Permite a un usuario cancelar su inscripción a un evento.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> CancelarInscripcion([FromBody] EliminarInscripcionCommand comando)
    {
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }

}
