namespace DeliveryYaBackend.DTOs.Responses.TarifaRepartidor
{
    public class TarifaRepartidorResponse
    {
        public int IdTarifa { get; set; }
        public int RepartidorIdRepartidor { get; set; }
        public int CantPedidos { get; set; }
        public decimal TarifaBase { get; set; }
        public decimal KmRecorridos { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
