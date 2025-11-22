using InventarioApi.Context;
using InventarioApi.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.DTOs;
using InventarioApi.Exceptions; // 👈 IMPORTANTE

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Ultidades _ultidades;

        public AccesoController(AppDbContext context, Ultidades ultidades)
        {
            _context = context;
            _ultidades = ultidades;
        }

        // POST: api/Acceso/Registrarse
        [HttpPost("Registrarse")]
        public async Task<IActionResult> Registrarse([FromBody] NewUsuarioDTO dto)
        {
            // Validación manual
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ValidationException("El email es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.PasswordHash) || dto.PasswordHash.Length < 6)
                throw new ValidationException("La contraseña debe tener al menos 6 caracteres.");

            // Validar usuario existente
            bool existe = await _context.Usuarios
                .AnyAsync(u => u.Email == dto.Email);

            if (existe)
                throw new BadRequestException("El usuario ya existe.");

            var nuevoUsuario = new Models.Users
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Username = dto.Username,
                Rol = "user",
                FechaCreacion = DateTime.Now,
                PasswordHash = _ultidades.encriptarSHA256(dto.PasswordHash)
            };

            await _context.Usuarios.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { isSuccess = true });
        }

        // POST: api/Acceso/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ValidationException("Email y contraseña son obligatorios.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Email == dto.Email &&
                    u.PasswordHash == _ultidades.encriptarSHA256(dto.Password)
                );

            if (usuario == null)
                throw new BadRequestException("Credenciales incorrectas.");

            var token = _ultidades.generarJwt(usuario);

            return Ok(new
            {
                isSuccess = true,
                token
            });
        }
    }
}
