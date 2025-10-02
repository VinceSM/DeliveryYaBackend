namespace DeliveryYaBackend.DTOs.Requests.Horarios
{
    public class HorarioUpdateRequest
    {
        public TimeSpan Apertura { get; set; }
        public TimeSpan Cierre { get; set; }
    }
}
