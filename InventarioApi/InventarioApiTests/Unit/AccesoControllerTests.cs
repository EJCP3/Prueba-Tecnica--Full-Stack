using Microsoft.EntityFrameworkCore;
using InventarioApi.Context;
using InventarioApi.Controllers;
using InventarioApi.DTOs;
using InventarioApi.Models;
using InventarioApi.Exceptions;
using InventarioApi.Custom;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory;


public class AccesoControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private Mock<Ultidades> GetMockUtils()
    {
        var utils = new Mock<Ultidades>();
        utils.Setup(u => u.encriptarSHA256(It.IsAny<string>()))
             .Returns<string>(p => "HASH_" + p);

        utils.Setup(u => u.generarJwt(It.IsAny<Users>()))
             .Returns("FAKE_JWT");

        return utils;
    }

    // =============================
    //        REGISTRO
    // =============================

    [Fact]
    public async Task Registrarse_DeberiaCrearUsuario()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();

        var controller = new AccesoController(db, utils.Object);

        var dto = new NewUsuarioDTO
        {
            Email = "test@test.com",
            PasswordHash = "123456",
            Nombre = "Eddy",
            Username = "eddy"
        };

        var result = await controller.Registrarse(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((dynamic)ok.Value).isSuccess);
    }

    [Fact]
    public async Task Registrarse_EmailVacio_DeberiaLanzarValidationException()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();
        var controller = new AccesoController(db, utils.Object);

        var dto = new NewUsuarioDTO
        {
            Email = "",
            PasswordHash = "123456"
        };

        await Assert.ThrowsAsync<ValidationException>(() => controller.Registrarse(dto));
    }

    [Fact]
    public async Task Registrarse_ContrasenaCorta_DeberiaLanzarValidationException()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();
        var controller = new AccesoController(db, utils.Object);

        var dto = new NewUsuarioDTO
        {
            Email = "test@mail.com",
            PasswordHash = "12"
        };

        await Assert.ThrowsAsync<ValidationException>(() => controller.Registrarse(dto));
    }

    [Fact]
    public async Task Registrarse_UsuarioExistente_DeberiaLanzarBadRequestException()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();

        // Usuario existente
        db.Usuarios.Add(new Users
        {
            Email = "test@mail.com",
            PasswordHash = "HASH_12345"
        });
        db.SaveChanges();

        var controller = new AccesoController(db, utils.Object);

        var dto = new NewUsuarioDTO
        {
            Email = "test@mail.com",
            PasswordHash = "123456"
        };

        await Assert.ThrowsAsync<BadRequestException>(() => controller.Registrarse(dto));
    }

    // =============================
    //           LOGIN
    // =============================

    [Fact]
    public async Task Login_UsuarioValido_DeberiaRetornarToken()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();

        // Crear usuario
        db.Usuarios.Add(new Users
        {
            Email = "user@mail.com",
            PasswordHash = "HASH_123456"
        });
        db.SaveChanges();

        var controller = new AccesoController(db, utils.Object);

        var dto = new UsuarioDto
        {
            Email = "user@mail.com",
            Password = "123456"
        };

        var result = await controller.Login(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.True(((dynamic)ok.Value).isSuccess);
        Assert.Equal("FAKE_JWT", ((dynamic)ok.Value).token);
    }

    [Fact]
    public async Task Login_CredencialesIncorrectas_DeberiaLanzarBadRequestException()
    {
        var db = GetDbContext();
        var utils = GetMockUtils();
        var controller = new AccesoController(db, utils.Object);

        var dto = new UsuarioDto
        {
            Email = "fake@mail.com",
            Password = "wrongpass"
        };

        await Assert.ThrowsAsync<BadRequestException>(() => controller.Login(dto));
    }
}
