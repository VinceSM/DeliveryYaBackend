namespace DeliveryYaBackend.DTOs.Requests.TarifaRepartidor
{
    public class TarifaRepartidorRequest
    {
        public int RepartidorIdRepartidor { get; set; }
        public int CantPedidos { get; set; }
        public decimal TarifaBase { get; set; }
        public decimal KmRecorridos { get; set; }
    }
}
