namespace DeliveryYaBackend.DTOs.Responses.Horarios
{
    public class ComercioHorarioResponse
    {
        public int comercioId { get; set; }
        public int horarioId { get; set; }
        public TimeSpan? apertura { get; set; }
        public TimeSpan? cierre { get; set; }
        public string? dias { get; set; }
        public bool abierto { get; set; }
    }
}
