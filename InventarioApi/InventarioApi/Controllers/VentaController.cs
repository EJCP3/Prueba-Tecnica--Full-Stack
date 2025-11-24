using InventarioApi.Context;
using InventarioApi.DTOs;
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Autenticado obligatorio
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ventas
        // Pueden ver admin y user
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<object>>> GetVentas()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Productos)
                .Select(v => new
                {
                    v.ID,
                    v.Fecha,
                    v.Total,
                    Cliente = new
                    {
                        v.Cliente.Id,
                        v.Cliente.Nombre,
                        v.Cliente.Email,
                        v.Cliente.Telefono
                    },
                    Detalles = v.Detalles.Select(d => new
                    {
                        d.ProductoID,
                        NombreProducto = d.Productos.Nombre,
                        d.Cantidad,
                        d.Precio
                    })
                })
                .ToListAsync();

            return Ok(ventas);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<VentaDto>> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Productos)
                .FirstOrDefaultAsync(v => v.ID == id);

            if (venta == null)
                return NotFound();

            var dto = new VentaDto
            {
                ID = venta.ID,
                Fecha = venta.Fecha,
                Total = venta.Total,

                Cliente = new ClienteDto
                {
                    Id = venta.Cliente.Id,
                    Nombre = venta.Cliente.Nombre,
                    Email = venta.Cliente.Email,
                    Telefono = venta.Cliente.Telefono
                },

                Detalles = venta.Detalles.Select(d => new VentaDetallaDto
                {
                    ProductoID = d.ProductoID,
                    NombreProducto = d.Productos.Nombre,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }).ToList()
            };

            return Ok(dto);
        }

        // POST: api/Ventas
        // Solo admin puede crear ventas
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        //public async Task<ActionResult> CrearVenta(VentaDto modelo)
        //{
        //    var venta = new Ventas
        //    {
        //        Fecha = modelo.Fecha,
        //        ClienteID = modelo.Cliente.Id,
        //    };

        //    foreach (var item in modelo.Detalles)
        //    {
        //        venta.Detalles.Add(new VentaDetalle
        //        {
        //            ProductoID = item.ProductoID,
        //            Cantidad = item.Cantidad,
        //            Precio = item.Precio
        //        });
        //    }

        //    venta.Total = venta.Detalles.Sum(d => d.Cantidad * d.Precio);

        //    _context.Ventas.Add(venta);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { mensaje = "Venta creada correctamente", ventaID = venta.ID });
        //}

        public async Task<IActionResult> Crear([FromBody] VentaCreateDto modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venta = new Ventas
            {
                ClienteID = modelo.ClienteID,
                Fecha = DateTime.Now,
                Total = 0,
                Detalles = new List<VentaDetalle>()
            };

            foreach (var item in modelo.Detalles)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoID);

                if (producto == null)
                    return BadRequest($"Producto {item.ProductoID} no existe.");

                venta.Detalles.Add(new VentaDetalle
                {
                    ProductoID = item.ProductoID,
                    Cantidad = item.Cantidad,
                    Precio = producto.Precio,
                    NombreProducto = producto.Nombre // ✔ ahora sí existe
                });

                venta.Total += producto.Precio * item.Cantidad;
            }

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return Ok(venta.ID);
        }


        // DELETE: api/Ventas/5
        // Solo admin puede eliminar ventas
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.ID == id);

            if (venta == null)
                return NotFound();

            _context.VentaDetalles.RemoveRange(venta.Detalles);
            _context.Ventas.Remove(venta);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
