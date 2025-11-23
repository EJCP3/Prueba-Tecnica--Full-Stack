namespace InventarioApi.DTOs
{
    public class NewUsuarioDTO
    {

         public int ID { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Para login (si usarás autenticación)
        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;


        // Fecha de creación del usuario
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }
}