using InventarioApi.Context;
using InventarioApi.DTOs;
using InventarioApi.Exceptions;
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // 👈 Requiere JWT siempre
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        [Authorize(Roles = "admin,user")] // 👈 ambos pueden ver
        public async Task<IEnumerable<ClienteDto>> GetClientes()
        {
            return await _context.Clientes
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono
                })
                .ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")] // 👈 ambos pueden ver
        public async Task<ClienteDto> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .Where(c => c.Id == id)
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
                throw new NotFoundException("El cliente no existe.");

            return cliente;
        }

        // POST: api/Clientes
        [HttpPost]
        [Authorize(Roles = "admin,user")] // 👈 solo admin crea
        public async Task<ActionResult<ClienteDto>> PostCliente(ClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ValidationException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ValidationException("El email es obligatorio.");

            var cliente = new Clientes
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            dto.Id = cliente.Id;

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, dto);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // 👈 solo admin edita
        public async Task<IActionResult> PutCliente(int id, ClienteDto dto)
        {
            if (id != dto.Id)
                throw new BadRequestException("El ID del cliente no coincide.");

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                throw new NotFoundException("El cliente no existe.");

            cliente.Nombre = dto.Nombre;
            cliente.Email = dto.Email;
            cliente.Telefono = dto.Telefono;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // 👈 solo admin elimina
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                throw new NotFoundException("El cliente no existe.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
