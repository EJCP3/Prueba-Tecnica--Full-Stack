
namespace InventarioApi.DTOs
{
    public class VentaDetallaDto
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }   // ← NUEVO
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
