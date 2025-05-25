using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing.Models;

public class SmtpOptions
{
    public string Servidor { get; set; } = string.Empty;
    public int Puerto { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Clave { get; set; } = string.Empty;
    public string Remitente { get; set; } = string.Empty;
    public string NombreRemitente { get; set; } = string.Empty;
}
