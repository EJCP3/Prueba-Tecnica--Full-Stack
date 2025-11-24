using InventarioApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

// Alias para distinguir la excepción del sistema
using DataAnnotationsValidationException = System.ComponentModel.DataAnnotations.ValidationException;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger,
        IWebHostEnvironment env)
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
            _logger.LogError(ex, "Excepción capturada en el middleware");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError;
        string title = "Error interno del servidor";

        // 1️⃣ Excepciones personalizadas
        if (exception is BadRequestException)
        {
            code = HttpStatusCode.BadRequest;
            title = "Solicitud inválida";
        }
        else if (exception is InventarioApi.Exceptions.ValidationException customValidation)
        {
            code = HttpStatusCode.BadRequest;
            title = "Errores de validación";
        }
        // 2️⃣ Excepciones del framework
        else if (exception is DataAnnotationsValidationException)
        {
            code = HttpStatusCode.BadRequest;
            title = "Errores de validación";
        }
        // 3️⃣ Otras excepciones comunes
        else if (exception is KeyNotFoundException)
        {
            code = HttpStatusCode.NotFound;
            title = "Recurso no encontrado";
        }
        else if (exception is UnauthorizedAccessException)
        {
            code = HttpStatusCode.Unauthorized;
            title = "No autorizado";
        }

        var problem = new ProblemDetails
        {
            Title = title,
            Status = (int)code,
            Detail = _env.IsDevelopment() ? exception.Message : "Ocurrió un error procesando su solicitud.",
            Instance = context.Request.Path
        };

        // 4️⃣ Agregar mensajes de error personalizados si existen
        if (exception is InventarioApi.Exceptions.ValidationException vex)
        {
            var errorsProp = vex.GetType().GetProperty("Errors");

            if (errorsProp != null)
            {
                var validationErrors = errorsProp.GetValue(vex);
                if (validationErrors != null)
                {
                    problem.Extensions["errors"] = validationErrors;
                }
            }
        }

        // Trace ID
        problem.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;

        string result = JsonSerializer.Serialize(problem,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
