namespace DeliveryYaBackend.DTOs.Responses.Usuarios
{
    public class VehiculoResponse
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Patente { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public bool Seguro { get; set; }
        public string? CompaniaSeguros { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}