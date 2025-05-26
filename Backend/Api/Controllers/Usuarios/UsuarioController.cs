using Application.Usuarios.Commands.ActualizarUsuario;
using Application.Usuarios.Queries.ListarAsistentes;
using Application.Usuarios.Queries.ListarTodosUsuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Genericos;

namespace Api.Controllers;

/// <summary>
/// clase controller usuarios 
/// </summary>
[ApiController]
[Route("api/usuarios")]
[Tags("Usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lista todos los usuarios (solo para administradores).
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ListarTodos([FromQuery] int idSolicitante)
    {
        var respuesta = await _mediator.Send(new ListarTodosUsuariosQuery(idSolicitante));
        return Ok(respuesta);
    }

    /// <summary>
    /// Lista solo los usuarios asistentes (accesible por cualquier rol).
    /// </summary>
    [HttpGet("asistentes")]
    public async Task<IActionResult> ListarAsistentes([FromQuery] int idSolicitante)
    {
        var respuesta = await _mediator.Send(new ListarAsistentesQuery(idSolicitante));
        return Ok(respuesta);
    }

    /// <summary>
    /// Permite actualizar datos de un usuario (solo administradores).
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> ActualizarUsuario([FromBody] ActualizarUsuarioCommand comando)
    {
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }

}
