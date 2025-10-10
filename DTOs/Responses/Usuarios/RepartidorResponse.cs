namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class RepartidorResponse
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateOnly Nacimiento { get; set; }
        public string Celular { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; } = 0;
        public string Email { get; set; } = null!;
        public decimal Puntuacion { get; set; }
        public string Cvu { get; set; } = null!;
        public bool LibreRepartidor { get; set; }

        // Info del vehículo
        public int VehiculoId { get; set; }
        public string TipoVehiculo { get; set; } = null!;
        public string? Patente { get; set; }
        public string Modelo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public bool Seguro { get; set; }
        public string? CompaniaSeguros { get; set; }
    }
}
