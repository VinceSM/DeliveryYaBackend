using DeliveryYaBackend.DTOs.Responses.Usuarios;

namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class PedidoDetailResponse
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Total { get; set; }
        public bool Pagado { get; set; }
        public EstadoPedidoResponse? Estado { get; set; } // ← Objeto completo
        public MetodoPagoResponse? MetodoPago { get; set; } // ← Objeto completo
        public bool ComercioRepartidor { get; set; }

        // Información de usuarios
        public ClienteDetalleResponse? ClienteDetail { get; set; }
        public RepartidorDetalleResponse? Repartidor { get; set; }

        // Items del pedido
        public List<ItemPedidoResponse> Items { get; set; } = new List<ItemPedidoResponse>();
        public DateTime? CreatedAt { get; set; }
    }
}