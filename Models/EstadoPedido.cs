namespace DeliveryYaBackend.Models
{
    public class EstadoPedido
    {
        public int idestado { get; set; }
        public string? tipo { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
