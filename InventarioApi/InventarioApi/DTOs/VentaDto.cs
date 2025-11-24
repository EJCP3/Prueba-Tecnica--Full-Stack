using InventarioApi.DTOs;

namespace InventarioApi.DTOs
{
    public class VentaDto
    {

        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public ClienteDto Cliente { get; set; }
        public List<VentaDetallaDto> Detalles { get; set; }
    }
}
