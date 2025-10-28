using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class ItemPedidoService : IItemPedidoService
    {
        private readonly IRepository<ItemPedido> _itemPedidoRepository;
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Comercio> _comercioRepository;

        public ItemPedidoService(
            IRepository<ItemPedido> itemPedidoRepository,
            IRepository<Producto> productoRepository,
            IRepository<Pedido> pedidoRepository,
            IRepository<Comercio> comercioRepository
            )
        {
            _itemPedidoRepository = itemPedidoRepository;
            _productoRepository = productoRepository;
            _pedidoRepository = pedidoRepository;
            _comercioRepository = comercioRepository;
        }

        public async Task<ItemPedido> CreateItemPedidoAsync(ItemPedido itemPedido)
        {

            throw new NotImplementedException();
        }

        public async Task<ItemPedido> GetItemPedidoByIdAsync(int id)
        {
            return await _itemPedidoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByPedidoIdAsync(int pedidoId)
        {
            return await _itemPedidoRepository.FindAsync(ip => ip.PedidoIdPedido == pedidoId);
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByComercioIdAsync(int comercioId)
        {
            return await _itemPedidoRepository.FindAsync(ip => ip.ComercioIdComercio == comercioId);
        }

        public async Task<IEnumerable<ItemPedido>> GetItemsByProductoIdAsync(int productoId)
        {
            return await _itemPedidoRepository.FindAsync(ip => ip.ProductoIdProducto == productoId);
        }

        public async Task<bool> UpdateItemPedidoAsync(ItemPedido itemPedido)
        {
            var existingItem = await _itemPedidoRepository.GetByIdAsync(itemPedido.iditemPedido);
            if (existingItem == null) return false;

            _itemPedidoRepository.Update(itemPedido);
            return await _itemPedidoRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemPedidoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddItemToPedidoAsync(int pedidoId, int productoId, int comercioId, int cantidad, decimal precio)
        {
            var item = new ItemPedido
            {
                PedidoIdPedido = pedidoId,
                ProductoIdProducto = productoId,
                ComercioIdComercio = comercioId,
                cantProducto = cantidad,
                precioFinal = precio,
                total = cantidad * precio
            };

            await CreateItemPedidoAsync(item);
            return await _itemPedidoRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateCantidadItemAsync(int itemId, int nuevaCantidad)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrecioItemAsync(int itemId, decimal nuevoPrecio)
        {
            var item = await _itemPedidoRepository.GetByIdAsync(itemId);
            if (item == null) return false;

            item.precioFinal = nuevoPrecio;
            item.total = item.cantProducto * nuevoPrecio;

            _itemPedidoRepository.Update(item);
            return await _itemPedidoRepository.SaveChangesAsync();
        }

        public async Task<decimal> CalcularTotalItemAsync(int itemId)
        {
            var item = await _itemPedidoRepository.GetByIdAsync(itemId);
            if (item == null) return 0m;

            return item.cantProducto * item.precioFinal;
        }

        public async Task<decimal> CalcularSubtotalPedidoAsync(int pedidoId)
        {
            var items = await GetItemsByPedidoIdAsync(pedidoId);
            return items.Sum(ip => ip.total != 0 ? ip.total : ip.cantProducto * ip.precioFinal);
        }

        public async Task<int> GetCantidadTotalItemsPedidoAsync(int pedidoId)
        {
            var items = await GetItemsByPedidoIdAsync(pedidoId);
            return items.Sum(ip => ip.cantProducto);
        }

        public async Task<decimal> GetTotalVentasComercioAsync(int comercioId, DateTime startDate, DateTime endDate)
        {
            // Primero obtener todos los items del comercio
            var items = await _itemPedidoRepository.FindAsync(ip =>
                ip.ComercioIdComercio == comercioId);

            // Luego filtrar por fecha usando Include o cargando los pedidos
            var itemsConPedidos = new List<ItemPedido>();

            foreach (var item in items)
            {
                // Cargar el pedido para cada item
                item.Pedido = await _pedidoRepository.GetByIdAsync(item.PedidoIdPedido);

                if (item.Pedido != null &&
                    item.Pedido.fecha >= startDate &&
                    item.Pedido.fecha <= endDate)
                {
                    itemsConPedidos.Add(item);
                }
            }

            return itemsConPedidos.Sum(ip => ip.total);
        }

        public async Task<bool> VerificarStockParaItemAsync(int productoId, int cantidad)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ItemExistsInPedidoAsync(int pedidoId, int productoId)
        {
            var items = await _itemPedidoRepository.FindAsync(ip =>
                ip.PedidoIdPedido == pedidoId && ip.ProductoIdProducto == productoId);

            return items.Any();
        }
    }
}