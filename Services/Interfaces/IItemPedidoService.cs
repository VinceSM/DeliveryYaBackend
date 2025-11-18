using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IItemPedidoService
    {
        // Consultas
        Task<IEnumerable<ItemPedido>> GetItemsByPedidoIdAsync(int pedidoId);
        Task<IEnumerable<ItemPedido>> GetItemsByComercioIdAsync(int comercioId);
        Task<IEnumerable<ItemPedido>> GetItemsByProductoIdAsync(int productoId);
        Task<ItemPedido?> GetItemByIdAsync(int id);

        // Métodos auxiliares
        Task<bool> ExistsAsync(int id);
    }
}