using Infrastructure.Mailing.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Mailing
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destino, string asunto, string cuerpoHtml);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _opciones;

        public EmailService(IOptions<SmtpOptions> opciones)
        {
            _opciones = opciones.Value;
        }

        public async Task EnviarCorreoAsync(string destino, string asunto, string cuerpoHtml)
        {
            using SmtpClient cliente = new SmtpClient(_opciones.Servidor, _opciones.Puerto)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_opciones.Usuario, _opciones.Clave),
                EnableSsl = true
            };

            MailMessage mensaje = new MailMessage
            {
                From = new MailAddress(_opciones.Usuario, _opciones.NombreRemitente),
                Subject = asunto,
                Body = cuerpoHtml,
                IsBodyHtml = true
            };

            mensaje.To.Add(destino);
            await cliente.SendMailAsync(mensaje);
        }

    }
}
