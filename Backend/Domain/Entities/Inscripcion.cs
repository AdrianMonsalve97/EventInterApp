using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Representa la inscripción de un usuario a un evento.
    /// </summary>
    public class Inscripcion
    {
        /// <summary>
        /// Identificador autoincremental de la inscripción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID del usuario inscrito (documento).
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Usuario relacionado a la inscripción.
        /// </summary>
        public Usuario Usuario { get; set; } = null!;

        /// <summary>
        /// ID del evento al que se inscribe.
        /// </summary>
        public int EventoId { get; set; }

        /// <summary>
        /// Evento relacionado a la inscripción.
        /// </summary>
        public Evento Evento { get; set; } = null!;

        /// <summary>
        /// Fecha en que se realiza la inscripción.
        /// </summary>
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
    }
}
