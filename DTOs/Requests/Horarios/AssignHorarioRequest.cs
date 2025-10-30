namespace DeliveryYaBackend.DTOs.Requests.Horarios
{
    public class AssignHorarioRequest
    {
        public int comercioId { get; set; }
        public TimeSpan apertura { get; set; }
        public TimeSpan cierre { get; set; }
        public string? dias { get; set; }
        public bool abierto { get; set; }
    }
}
