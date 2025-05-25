using Application.Usuarios.Commands.RegistrarUsuario;
using MediatR;
using Shared.Contracts.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs.UsuariosDtos
{
    public sealed record RegistrarUsuarioContextualizado(
        RegistrarUsuarioCommand Datos,
        int IdCreador
    ) : IRequest<RespuestaGeneral<string>>;

}
