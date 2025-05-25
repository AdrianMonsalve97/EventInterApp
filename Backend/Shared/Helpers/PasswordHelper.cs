using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helppers
{
    /// <summary>
    /// Clase de ayuda para generar contraseñas seguras.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Genera una contraseña segura de longitud especificada.
        /// </summary>
        /// <param name="longitud"></param>
        /// <returns></returns>
        public static string GenerarContrasenaSegura(int longitud = 12)
        {
            const string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789@$%&*";
            Random random = new Random();

            return new string(Enumerable.Repeat(caracteres, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool EsValida(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneMinuscula = password.Any(char.IsLower);
            bool tieneNumero = password.Any(char.IsDigit);
            bool tieneEspecial = password.Any(c => !char.IsLetterOrDigit(c));
            bool tieneEspacio = password.Any(char.IsWhiteSpace);

            return tieneMayuscula && tieneMinuscula && tieneNumero && tieneEspecial && !tieneEspacio;
        }
    }


}
