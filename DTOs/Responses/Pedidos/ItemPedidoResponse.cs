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
        public ProductoResponse? Producto { get; set; }
        public ComercioInfoResponse? Comercio { get; set; }
    }
}