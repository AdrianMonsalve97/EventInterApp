using Application.Usuarios.Commands.LoginUsuario;
using Application.Usuarios.Commands.RegistrarUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Autenticacion;
using Shared.Contracts.Genericos;

namespace Api.Controllers;

/// <summary>
/// Controlador para autenticación y gestión de usuarios.
/// </summary>

[ApiController]
[Route("api/[controller]")]
[Tags("Autenticacion")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="peticion">Contiene los datos del nuevo usuario y el ID del usuario que ejecuta el registro.</param>
    /// <returns>Mensaje de confirmación o error con estado detallado.</returns>
    /// <response code="200">Registro exitoso</response>
    /// <response code="400">Error en la solicitud</response>
    [HttpPost("registrar")]
    [ProducesResponseType(typeof(RespuestaGeneral<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespuestaGeneral<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistrarUsuario([FromBody] PeticionGeneral<RegistrarUsuarioCommand> peticion)
    {
        RegistrarUsuarioCommand comando = peticion.Data with { Usuario = peticion.Usuario };
        RespuestaGeneral<string> respuesta = await _mediator.Send(comando);
        return respuesta.Error ? BadRequest(respuesta) : Ok(respuesta);
    }
    /// <summary>
    /// Inicia sesión con nombre de usuario y contraseña. Retorna un token JWT si es válido.
    /// </summary>
    /// <param name="comando">Credenciales del usuario</param>
    /// <returns>Token JWT y datos del usuario</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(RespuestaGeneral<JwtResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RespuestaGeneral<JwtResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand comando)
    {
        RespuestaGeneral<JwtResponseDto> respuesta = await _mediator.Send(comando);

        return respuesta.Error
            ? Unauthorized(respuesta)
            : Ok(respuesta);
    }
}
