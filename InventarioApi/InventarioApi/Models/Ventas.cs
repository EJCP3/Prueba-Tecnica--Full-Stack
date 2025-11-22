
namespace InventarioApi.Models
{
    public class Ventas
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        // Foreign key
        public int ClienteID { get; set; }

        // Navigation property
        public Clientes Cliente { get; set; }

        // Relación con VentaDetalle (1 - N)
        public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
