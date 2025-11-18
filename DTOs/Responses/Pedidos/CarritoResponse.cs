namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class CarritoResponse
    {
        public int ComercioId { get; set; }
        public string? ComercioNombre { get; set; }
        public decimal Envio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public List<ItemCarritoResponse>? Items { get; set; }
    }
}
