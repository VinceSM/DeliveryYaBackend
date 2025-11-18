using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.DTOs.Responses.Comercios;

namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class ItemPedidoResponse
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string? ProductoNombre { get; set; }
        public string? ProductoDescripcion { get; set; }
        public string? ComercioNombre { get; set; }
        public int ProductoId { get; set; }
        public int ComercioId { get; set; }
    }
}