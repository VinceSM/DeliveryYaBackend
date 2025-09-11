using DeliveryYaBackend.DTOs.Responses.Comercios;

namespace DeliveryYaBackend.DTOs.Responses.Horarios
{
    public class HorarioComercioResponse
    {
        public int Id { get; set; }
        public TimeOnly? Apertura { get; set; }
        public TimeOnly? Cierre { get; set; }
        public string? Dias { get; set; }
        public bool Abierto { get; set; }
        public ComercioInfoResponse? Comercio { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}