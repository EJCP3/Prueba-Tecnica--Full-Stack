namespace InventarioApi.DTOs
{
    public class VentaCreateDto
    {
        public int ClienteID { get; set; }
        public List<VentaDetalleCreateDto> Detalles { get; set; }
    }
}
