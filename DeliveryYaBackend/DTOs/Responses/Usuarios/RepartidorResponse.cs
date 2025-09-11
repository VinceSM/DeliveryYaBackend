namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class RepartidorResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; } // idusertype
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public decimal Puntuacion { get; set; }
        public bool Libre { get; set; }
        public string? TipoVehiculo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}