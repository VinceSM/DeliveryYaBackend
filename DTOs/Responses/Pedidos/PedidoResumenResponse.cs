namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class PedidoResumenResponse
    {
        public int Id { get; set; }
        public string? Codigo { get; set; } // ← Para listados
        public DateTime Fecha { get; set; }
        public decimal TotalPedido { get; set; }
        public string? Estado { get; set; }
        public string? ComercioNombre { get; set; }
        public int CantidadItems { get; set; }
    }
}
