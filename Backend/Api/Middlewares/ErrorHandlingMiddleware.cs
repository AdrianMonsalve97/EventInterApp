using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

/// <summary>
/// Middleware para manejar errores globales y retornar una respuesta estandarizada.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    /// <summary>
    /// Intercepta las solicitudes HTTP y maneja excepciones no controladas.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, " Excepción no controlada: {Mensaje}", ex.Message);
            await EscribirRespuestaError(context, ex);
        }
    }

    /// <summary>
    /// Escribe la respuesta de error en formato JSON.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ex"></param>
    /// <returns></returns>

    private static async Task EscribirRespuestaError(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        JsonSerializerOptions opcionesJson = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        object cuerpoError = new
        {
            status = context.Response.StatusCode,
            title = "Se produjo un error inesperado.",
            detail = ex.Message,
            timestamp = DateTime.UtcNow
        };

        string json = JsonSerializer.Serialize(cuerpoError, opcionesJson);
        await context.Response.WriteAsync(json);
    }
}
