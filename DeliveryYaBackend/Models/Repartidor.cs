namespace DeliveryYaBackend.Models
{
    public class Repartidor
    {
        int idrepartidor { get; set; }
        int cantPedidos { get; set; }
        decimal puntuacion { get; set; }
        string? cvu { get; set; }
        Boolean libreRepartidor { get; set; }
        Vehiculo? vehiculo { get; set; }
        IUserType? user { get; set; }

    }
}
