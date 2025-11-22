namespace InventarioApi.Models
{
    public class Users
    {
        public int ID { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Para login (si usarás autenticación)
        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        // Rol del usuario (admin, empleado, etc.)
        public string Rol { get; set; } = "user";

        // Fecha de creación del usuario
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
