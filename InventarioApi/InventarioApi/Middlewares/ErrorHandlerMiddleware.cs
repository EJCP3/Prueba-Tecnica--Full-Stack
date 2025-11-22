// Middlewares/ErrorHandlerMiddleware.cs
using InventarioApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

// Desambiguar usando alias para ValidationException
using DataAnnotationsValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using CustomValidationException = InventarioApi.Exceptions.ValidationException;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception caught by middleware");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var title = "Error interno del servidor";

        // Mapeo de excepciones personalizadas
        if (exception is KeyNotFoundException) { code = HttpStatusCode.NotFound; title = "Recurso no encontrado"; }
        else if (exception is UnauthorizedAccessException) { code = HttpStatusCode.Unauthorized; title = "No autorizado"; }
        else if (exception is BadRequestException) { code = HttpStatusCode.BadRequest; title = "Solicitud inválida"; }
        else if (exception is DataAnnotationsValidationException) { code = HttpStatusCode.BadRequest; title = "Errores de validación"; }
        else if (exception is CustomValidationException) { code = HttpStatusCode.BadRequest; title = "Errores de validación"; }

        var problem = new ProblemDetails
        {
            Title = title,
            Status = (int)code,
            Detail = _env.IsDevelopment() ? exception.Message : "Ocurrió un error. Contacte al administrador.",
            Instance = context.Request.Path
        };

        // Si es ValidationException, incluimos detalles
        if (exception is CustomValidationException vex)
        {
            // Intentar obtener la propiedad Errors si existe
            var errorsProperty = vex.GetType().GetProperty("Errors");
            if (errorsProperty != null)
            {
                var errorsValue = errorsProperty.GetValue(vex);
                if (errorsValue != null)
                {
                    problem.Extensions["errors"] = errorsValue;
                }
            }
        }

        // Correlación: trace id
        problem.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;

        var result = JsonSerializer.Serialize(problem, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
