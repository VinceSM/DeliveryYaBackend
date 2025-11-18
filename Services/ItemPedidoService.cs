using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    // Services/ItemPedidoService.cs
    public class ItemPedidoService : IItemPedidoService
    {
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly ILogger<ItemPedidoService> _logger;

        public ItemPedidoService(
            IItemPedidoRepository itemPedidoRepository,
            ILogger<ItemPedidoService> logger)
        {
            _itemPedidoRepository = itemPedidoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByPedidoIdAsync(int pedidoId)
        {
            return await _itemPedidoRepository.GetByPedidoIdAsync(pedidoId);
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByComercioIdAsync(int comercioId)
        {
            return await _itemPedidoRepository.GetByComercioIdAsync(comercioId);
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByProductoIdAsync(int productoId)
        {
            return await _itemPedidoRepository.GetByProductoIdAsync(productoId);
        }

        public async Task<ItemPedido?> GetItemByIdAsync(int id)
        {
            return await _itemPedidoRepository.GetByIdAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var item = await _itemPedidoRepository.GetByIdAsync(id);
            return item != null && item.deletedAt == null;
        }
    }
}