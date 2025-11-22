using InventarioApi.Context;
using InventarioApi.DTOs;
using InventarioApi.Exceptions; // 👈 IMPORTANTE
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<IEnumerable<ProductoDto>> GetProductos()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.ID,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock
                })
                .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ProductoDto> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Where(p => p.ID == id)
                .Select(p => new ProductoDto
                {
                    Id = p.ID,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock
                })
                .FirstOrDefaultAsync();

            if (producto == null)
                throw new NotFoundException("El producto no existe.");

            return producto;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ProductoDto> PostProducto(ProductoDto productoDto)
        {
            // Validaciones rápidas
            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new ValidationException("El nombre del producto es obligatorio.");

            if (productoDto.Precio < 0)
                throw new ValidationException("El precio no puede ser negativo.");

            var producto = new Productos
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            productoDto.Id = producto.ID;

            return productoDto;
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDto productoDto)
        {
            if (id != productoDto.Id)
                throw new BadRequestException("El ID del producto no coincide.");

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                throw new NotFoundException("El producto a modificar no existe.");

            producto.Nombre = productoDto.Nombre;
            producto.Descripcion = productoDto.Descripcion;
            producto.Precio = productoDto.Precio;
            producto.Stock = productoDto.Stock;

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                throw new NotFoundException("El producto que intenta eliminar no existe.");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
