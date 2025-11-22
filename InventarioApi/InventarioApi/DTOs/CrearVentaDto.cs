namespace InventarioApi.DTOs
{
    public class CrearVentaDto
    {
        public int ClienteID { get; set; }
        public List<VentaDetallaDto> Detalles { get; set; }
    }
}
