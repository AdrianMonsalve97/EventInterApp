using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Autenticacion;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Usuarios.Commands.LoginUsuario;

public sealed class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, RespuestaGeneral<JwtResponseDto>>
{
    private readonly EventosDbContext _context;
    private readonly IConfiguration _configuration;

    public LoginUsuarioHandler(EventosDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<RespuestaGeneral<JwtResponseDto>> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario, cancellationToken);

        if (usuario is null)
        {
            return RespuestaHelper.Error<JwtResponseDto>("Usuario no encontrado.");
        }

        bool claveValida = PasswordHasher.VerificarPassword(request.Password, usuario.PasswordHash, usuario.PasswordSalt);
        if (!claveValida)
        {
            return RespuestaHelper.Error<JwtResponseDto>("Contraseña incorrecta.");
        }
        if (usuario.DebeCambiarPassword)
        {
            return new RespuestaGeneral<JwtResponseDto>(
                false,
                new JwtResponseDto(string.Empty, usuario.Id, usuario.Nombre, usuario.Rol.ToString(), true),
                "Debe cambiar su contraseña."
            );
        }

        string token = GenerarJwt(usuario);
        return RespuestaHelper.Exito(new JwtResponseDto(token, usuario.Id, usuario.Nombre, usuario.Rol.ToString(), false));
    }

    private string GenerarJwt(Usuario usuario)
    {
        string claveBase64 = _configuration["Jwt:Key"]!;
        byte[] claveBytes = Convert.FromBase64String(claveBase64);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NombreUsuario),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString())
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(claveBytes);
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
