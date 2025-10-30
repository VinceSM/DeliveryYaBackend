namespace DeliveryYaBackend.DTOs.Requests.Horarios
{
    public class CreateHorarioRequest
    {
        public TimeSpan Apertura { get; set; }
        public TimeSpan Cierre { get; set; }
        public string? Dias { get; set; }
        public bool Abierto { get; set; }
    }
}
