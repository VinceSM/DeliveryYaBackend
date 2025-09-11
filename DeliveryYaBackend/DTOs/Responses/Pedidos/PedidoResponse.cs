namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Total { get; set; }
        public EstadoPedidoResponse? Estado { get; set; } // ← Cambiado a objeto
        public bool Pagado { get; set; }
        public MetodoPagoResponse? MetodoPago { get; set; } // ← Cambiado a objeto
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}