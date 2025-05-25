using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shared.Helppers

/// <summary>
/// Clase auxiliar para acceder a variables de configuración desde appsettings.json.
/// </summary>

{
    public static class ConfiguracionApp
    {
        /// <summary>
        /// Método para obtener un valor de configuración como cadena.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        public static string ObtenerString(IConfiguration configuration, string clave) { 
        
            string? valor = configuration[clave];
            if (string.IsNullOrEmpty(valor))
            {
                throw new ArgumentNullException($"La clave '{clave}' no se encuentra en la configuración.");
            }
            return valor;
        }

        /// <summary>
        /// Método para obtener un valor de configuración como cadena y decodificarlo desde Base64.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static string ObtenerStringBase64Decodificado(IConfiguration configuration, string clave) { 
        
            string base64 = ObtenerString(configuration, clave);

            Byte[] bytes = Convert.FromBase64String(base64);
            string valorDecodificado = Encoding.UTF8.GetString(bytes);

            return valorDecodificado;

        }
    }
}
