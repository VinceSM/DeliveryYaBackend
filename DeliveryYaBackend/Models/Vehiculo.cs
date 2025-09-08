namespace DeliveryYaBackend.Models
{
    public class Vehiculo
    {
        int idvehiculo { get; set; }
        enum tipo {Moto, Auto, Bici, Camioneta, AutoCamioneta }
        string? marca { get; set; }
        string? modelo { get; set; }
        string? patente { get; set; }
        Boolean seguro { get; set; }
        string? companiaSeguros { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }

    }
}
