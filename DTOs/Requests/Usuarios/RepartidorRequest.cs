namespace DeliveryYaBackend.DTOs.Requests.Usuarios
{
    public class RepartidorRequest
    {
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateOnly Nacimiento { get; set; }
        public string Celular { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; } = 0;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Cvu { get; set; } = null!;
        public bool LibreRepartidor { get; set; } = true;

        // Datos de Vehículo (opcional)
        public string TipoVehiculo { get; set; } = null!;
        public string? Patente { get; set; }
        public string Modelo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public bool Seguro { get; set; }
        public string? CompaniaSeguros { get; set; }
    }
}
