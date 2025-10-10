using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IItemPedidoService
    {
        // Operaciones básicas de Items de Pedido
        Task<ItemPedido> CreateItemPedidoAsync(ItemPedido itemPedido);
        Task<ItemPedido> GetItemPedidoByIdAsync(int id);
        Task<IEnumerable<ItemPedido>> GetItemsByPedidoIdAsync(int pedidoId);
        Task<IEnumerable<ItemPedido>> GetItemsByComercioIdAsync(int comercioId);
        Task<IEnumerable<ItemPedido>> GetItemsByProductoIdAsync(int productoId);
        Task<bool> UpdateItemPedidoAsync(ItemPedido itemPedido);
        Task<bool> DeleteItemPedidoAsync(int id);

        // Gestión de items
        Task<bool> AddItemToPedidoAsync(int pedidoId, int productoId, int comercioId, int cantidad, decimal precio);
        Task<bool> UpdateCantidadItemAsync(int itemId, int nuevaCantidad);
        Task<bool> UpdatePrecioItemAsync(int itemId, decimal nuevoPrecio);
        Task<decimal> CalcularTotalItemAsync(int itemId);

        // Cálculos y totales
        Task<decimal> CalcularSubtotalPedidoAsync(int pedidoId);
        Task<int> GetCantidadTotalItemsPedidoAsync(int pedidoId);
        Task<decimal> GetTotalVentasComercioAsync(int comercioId, DateTime startDate, DateTime endDate);

        // Validaciones
        Task<bool> VerificarStockParaItemAsync(int productoId, int cantidad);
        Task<bool> ItemExistsInPedidoAsync(int pedidoId, int productoId);
    }
}