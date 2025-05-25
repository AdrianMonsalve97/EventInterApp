using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs.UsuariosDtos
{
    /// <summary>
    /// Clase DTO para representar un usuario sin exponer credenciales.
    /// </summary>
    public record UsuarioDto (
        int Id,
        string NombreUsuario,
        string Nombre,
        string Email,
        string Rol
        );
}
