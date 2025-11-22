using InventarioApi.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text.Json;
using Xunit;

public class ErrorHandlerMiddlewareTests
{
    private static readonly JsonSerializerOptions CamelCaseOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    private static readonly string[] value = new[] { "El nombre es obligatorio" };

    [Fact]
    public async Task Invoke_KeyNotFoundException_DeberiaRetornar404()
    {
        var mockLogger = new Mock<ILogger<ErrorHandlerMiddleware>>();
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(e => e.EnvironmentName).Returns("Development");

        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream(); // FIX IMPORTANTE
        context.Request.Path = "/test";

        var next = new RequestDelegate((ctx) => throw new KeyNotFoundException("No encontrado"));

        var middleware = new ErrorHandlerMiddleware(next, mockLogger.Object, mockEnv.Object);

        await middleware.Invoke(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Contains("Recurso no encontrado", body);
    }


   
    [Fact]
    public async Task Invoke_BadRequestException_DeberiaRetornar400()
    {
        var mockLogger = new Mock<ILogger<ErrorHandlerMiddleware>>();
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(e => e.EnvironmentName).Returns("Production");

        var context = new DefaultHttpContext();
        context.Request.Path = "/crear";
        context.Response.Body = new MemoryStream();

        var next = new RequestDelegate((ctx) => throw new BadRequestException("Error en solicitud"));

        var middleware = new ErrorHandlerMiddleware(next, mockLogger.Object, mockEnv.Object);

        await middleware.Invoke(context);

        Assert.Equal(400, context.Response.StatusCode);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        var problem = JsonSerializer.Deserialize<ProblemDetails>(body);

        Assert.Equal("Solicitud inválida", problem.Title);
    }

    [Fact]
    public async Task Invoke_CustomValidationException_DebeIncluirErrorsEnExtensions()
    {
        var mockLogger = new Mock<ILogger<ErrorHandlerMiddleware>>();
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(e => e.EnvironmentName).Returns("Development");

        var context = new DefaultHttpContext();
        context.Request.Path = "/validar";
        context.Response.Body = new MemoryStream();


        var validationException = new InventarioApi.Exceptions.ValidationException(
            new Dictionary<string, string[]> {
            { "Nombre", new[] { "El nombre es obligatorio" } }
            }
        );

        var next = new RequestDelegate((ctx) => throw validationException);

        var middleware = new ErrorHandlerMiddleware(next, mockLogger.Object, mockEnv.Object);

        await middleware.Invoke(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Contains("errors", body);
        Assert.Contains("Nombre", body);
    }
}
