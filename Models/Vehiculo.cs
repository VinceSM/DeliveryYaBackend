namespace DeliveryYaBackend.Models
{
    public class Vehiculo
    {
        public int idvehiculo { get; set; }
        public string tipo { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public string? patente { get; set; }
        public Boolean seguro { get; set; }
        public string? companiaSeguros { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }

    }
}
