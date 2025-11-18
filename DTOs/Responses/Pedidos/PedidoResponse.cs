namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public bool Pagado { get; set; }
        public bool ComercioRepartidor { get; set; }
        public decimal SubtotalPedido { get; set; }
        public decimal TotalPedido { get; set; }
        public string? DireccionEnvio { get; set; }
        public string? Estado { get; set; }
        public string? MetodoPago { get; set; }
        public string? ClienteNombre { get; set; }
        public List<ItemPedidoResponse>? Items { get; set; }
    }
}