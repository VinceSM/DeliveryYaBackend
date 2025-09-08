namespace DeliveryYaBackend.Models
{
    public class TarifaRepartidorLibre
    {
        int idtarifa { get; set; }
        int cantPedidos { get; set; }
        decimal tarifaBase { get; set; }
        decimal kmrecorridos { get; set; }
        Repartidor? repartidor { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }
    }
}
