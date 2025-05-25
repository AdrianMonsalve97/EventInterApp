using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Representa un tipo de documento.
    /// </summary>
    public sealed class TipoDocumento
    {
        public string Codigo { get; }
        public string Nombre { get; }

        private TipoDocumento(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }

        public static readonly TipoDocumento Cedula = new("CC", "Cédula de ciudadanía");
        public static readonly TipoDocumento Pasaporte = new("PS", "Pasaporte");
        public static readonly TipoDocumento Nit = new("NIT", "NIT");
        public static readonly TipoDocumento CedulaExt = new("CE", "Cédula de extranjería");

        /// <summary>
        /// Lista de tipos de documentos disponibles.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TipoDocumento> List() =>
            new[] { Cedula, Pasaporte, Nit, CedulaExt };

        /// <summary>
        /// Obtiene un tipo de documento a partir de su código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static TipoDocumento FromCodigo(string codigo)
        {
            return List().FirstOrDefault(x => x.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                   ?? throw new ArgumentException($"Tipo de documento no válido: {codigo}");
        }

    }
}
