using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IItemPedidoRepository : IRepository<ItemPedido>
    {
        Task<IEnumerable<ItemPedido>> GetByPedidoIdAsync(int pedidoId);
        Task<IEnumerable<ItemPedido>> GetByComercioIdAsync(int comercioId);
        Task<IEnumerable<ItemPedido>> GetByProductoIdAsync(int productoId);
    }
}
