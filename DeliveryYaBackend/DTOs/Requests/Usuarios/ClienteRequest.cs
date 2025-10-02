namespace DeliveryYaBackend.DTOs.Requests.Usuarios
{
    public class ClienteRequest
    {
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateOnly Nacimiento { get; set; }
        public string Celular { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; } = 0;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;  // Se usará solo en creación/login
    }
}
