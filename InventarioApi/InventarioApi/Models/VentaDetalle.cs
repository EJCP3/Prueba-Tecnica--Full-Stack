
namespace InventarioApi.Models

{
    public class VentaDetalle
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public Productos Productos { get; set; }

        public int VentaID { get; set; }
        public Ventas Ventas { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public string NombreProducto { get; set; } // ✔ agrega esto
    }
}
