namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class EstadoPedidoResponse
    {
        public int Id { get; set; }
        public string? Tipo { get; set; } // "En Preparacion", "Listo", "A Retirar", etc.
        public DateTime? CreatedAt { get; set; }
    }
}