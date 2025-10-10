namespace DeliveryYaBackend.DTOs.Responses.Auth
{
    public class LoginRepartidorResponse
    {
        public string? Token { get; set; }
        public int idRepartidor { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? CVU { get; set; }
        public bool Libre { get; set; }
        public decimal Puntuacion { get; set; }
        public string? TipoVehiculo { get; set; }
        public string? Patente { get; set; }
    }
}