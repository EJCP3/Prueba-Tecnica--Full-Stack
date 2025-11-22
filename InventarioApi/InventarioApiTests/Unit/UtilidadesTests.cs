using FluentAssertions;
using InventarioApi.Custom;
using InventarioApi.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace InventarioApi.Tests.Unit
{
    public class UltidadesTests
    {
        // -----------------------
        //  TEST: SHA256
        // -----------------------
        [Fact]
        public void EncriptarSHA256_DebeRetornarHashCorrecto()
        {
            // Arrange
            var mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            var utils = new Ultidades(mockConfig.Object);

            var texto = "hola123";

            // Act
            var resultado = utils.encriptarSHA256(texto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveLength(64); // SHA256 devuelve 64 caracteres hexadecimales
        }



        // -----------------------
        //  TEST: GENERAR JWT
        // -----------------------
        [Fact]
        public void GenerarJwt_DebeRetornarTokenValido()
        {
            // Arrange
            var mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            mockConfig.Setup(c => c["Jwt:Key"]).Returns("ClaveSuperSecretaDePrueba123456789");

            var utils = new Ultidades(mockConfig.Object);

            var usuario = new Users
            {
                ID = 1,
                Email = "test@example.com",
                Nombre = "Usuario Test"
            };

            // Act
            var token = utils.generarJwt(usuario);

            // Assert
            token.Should().NotBeNullOrWhiteSpace();
            token.Split('.').Length.Should().Be(3); // Un JWT tiene 3 partes
        }
    }
}
