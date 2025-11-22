namespace InventarioApi.Models
{
    public class Clientes
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }
        public string Telefono { get; set; }

        
        public ICollection<Ventas> Ventas { get; set; } = new List<Ventas>();
    }
}
