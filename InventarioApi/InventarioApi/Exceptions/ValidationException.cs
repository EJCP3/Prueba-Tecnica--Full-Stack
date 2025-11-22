namespace InventarioApi.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(Dictionary<string, string[]> errors)
            : base("Errores de validación")
        {
            Errors = errors;
        }

        public ValidationException(string? message) : base(message)
        {
        }
    }
}
