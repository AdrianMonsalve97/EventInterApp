using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using Shared.Helppers;

namespace Api.Middlewares;

/// <summary>
/// Middleware para validar el encabezado 'Cliente' personalizado.
/// </summary>
public class ValidateClientHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _valorEsperado;

    public ValidateClientHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _valorEsperado = ConfiguracionApp.ObtenerStringBase64Decodificado(configuration, "Variables:Cliente");
    }

    public async Task Invoke(HttpContext context)
    
    {
        // Excluir login
        // Excluir login, Swagger UI y archivos estáticos
        if (context.Request.Path.StartsWithSegments("/api/auth/login") ||
            context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/favicon") ||
            context.Request.Path.StartsWithSegments("/swagger-ui") ||
            context.Request.Path.StartsWithSegments("/index.html"))
        {
            await _next(context);
            return;
        }

        // Validar que venga el header
        if (!context.Request.Headers.TryGetValue("Cliente", out var valorHeader) || valorHeader != _valorEsperado)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Encabezado de seguridad inválido o faltante.");
            return;
        }

        await _next(context);
    }
}
