namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class ItemCarritoResponse
    {
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public string? ProductoDescripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public bool StockDisponible { get; set; }
    }
}
