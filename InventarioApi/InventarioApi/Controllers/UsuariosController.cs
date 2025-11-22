using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Context;
using InventarioApi.Models;
using InventarioApi.DTOs;
using InventarioApi.Exceptions; // 👈 NECESARIO

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> GetUsuarios()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    ID = u.ID,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Password = u.PasswordHash,
                })
                .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<UsuarioDto> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new NotFoundException("El usuario no existe.");

            return new UsuarioDto
            {
                ID = usuario.ID,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Password = usuario.PasswordHash,
            };
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(Users usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ValidationException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new ValidationException("El correo electrónico es obligatorio.");

            var existe = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (existe)
                throw new BadRequestException("Ya existe un usuario con ese email.");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var dto = new UsuarioDto
            {
                ID = usuario.ID,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Password = usuario.PasswordHash,
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.ID }, dto);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Users usuario)
        {
            if (id != usuario.ID)
                throw new BadRequestException("El ID del usuario no coincide.");

            var existe = await _context.Usuarios.AnyAsync(u => u.ID == id);
            if (!existe)
                throw new NotFoundException("El usuario no existe.");

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                throw new NotFoundException("El usuario no existe.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
