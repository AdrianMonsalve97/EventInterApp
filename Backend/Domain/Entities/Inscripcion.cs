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
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int EventoId { get; set; } // Cambiado de "IdEvento" a "EventoId" para que coincida con el error reportado.  
        public Evento Evento { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }
}
