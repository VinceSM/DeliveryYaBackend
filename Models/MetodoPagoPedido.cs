namespace DeliveryYaBackend.Models
{
    public class MetodoPagoPedido
    {
        public int idmetodo { get; set; }
        public string? metodo { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
