using DeliveryYaBackend.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("repartidor")]
public class Repartidor
{
    public int idrepartidor { get; set; }
    public string? nombreCompleto { get; set; }
    public string? dni { get; set; }
    public DateOnly nacimiento { get; set; }
    public string? celular { get; set; }
    public string? ciudad { get; set; }
    public string? calle { get; set; }
    public int numero { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public int cantPedidos { get; set; }
    public decimal puntuacion { get; set; }
    public string cvu { get; set; }
    public bool libreRepartidor { get; set; }
    public int vehiculoIdVehiculo { get; set; }

    [ForeignKey("vehiculoIdVehiculo")]
    public virtual Vehiculo? Vehiculo { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
}