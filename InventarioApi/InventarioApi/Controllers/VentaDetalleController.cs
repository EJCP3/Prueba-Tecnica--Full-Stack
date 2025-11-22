using InventarioApi.Context;
using InventarioApi.DTOs;
using InventarioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaDetalleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentaDetalleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/VentaDetalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDetallaDto>>> GetVentaDetalles()
        {
            var detalles = await _context.VentaDetalles
                .Select(d => new VentaDetallaDto
                {
                    ProductoID = d.ProductoID,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                })
                .ToListAsync();

            return Ok(detalles);
        }

        // GET: api/VentaDetalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDetallaDto>> GetVentaDetalle(int id)
        {
            var detalle = await _context.VentaDetalles
                .Where(d => d.ID == id)
                .Select(d => new VentaDetallaDto
                {
                    ProductoID = d.ProductoID,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                })
                .FirstOrDefaultAsync();

            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        // POST: api/VentaDetalle
        [HttpPost]
        public async Task<ActionResult> PostVentaDetalle(VentaDetallaDto dto)
        {
            // Validar producto
            var producto = await _context.Productos.FindAsync(dto.ProductoID);
            if (producto == null)
                return BadRequest("El producto no existe.");

            var detalle = new VentaDetalle
            {
                ProductoID = dto.ProductoID,
                Cantidad = dto.Cantidad,
                Precio = producto.Precio
            };

            _context.VentaDetalles.Add(detalle);
            await _context.SaveChangesAsync();

            return Ok("Detalle registrado correctamente.");
        }

        // PUT: api/VentaDetalle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentaDetalle(int id, VentaDetallaDto dto)
        {
            var detalle = await _context.VentaDetalles.FindAsync(id);

            if (detalle == null)
                return NotFound();

            detalle.ProductoID = dto.ProductoID;
            detalle.Cantidad = dto.Cantidad;

            // actualizar precio desde producto
            var producto = await _context.Productos.FindAsync(dto.ProductoID);
            if (producto == null)
                return BadRequest("El producto no existe.");

            detalle.Precio = producto.Precio;

            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VentaDetalle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentaDetalle(int id)
        {
            var detalle = await _context.VentaDetalles.FindAsync(id);

            if (detalle == null)
                return NotFound();

            _context.VentaDetalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
