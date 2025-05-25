using Shared.Contracts.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helppers
{
    public static class RespuestaHelper
    {
        public static RespuestaGeneral<T> Exito<T>(T data, string mensaje = "Operación exitosa.")
            => new(false, data, mensaje);

        public static RespuestaGeneral<T> Error<T>(string mensaje)
            => new(true, default!, mensaje);
    }
}
