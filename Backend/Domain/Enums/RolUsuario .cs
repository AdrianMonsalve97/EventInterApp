using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{

    /// <summary>
    /// Define los roles posibles de un usuario dentro del sistema.
    /// </summary>
    public enum RolUsuario
    {
        [EnumMember(Value = "Asistente")]
        Asistente = 1,

        [EnumMember(Value = "Expositor")]
        Expositor = 2,

        [EnumMember(Value = "Gestionador")]
        Gestionador = 3,

        [EnumMember(Value = "Administrador")]
        Administrador = 4
    }
}
