using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class ItemPedidoRepository : GenericRepository<ItemPedido>, IItemPedidoRepository
    {
        private readonly AppDbContext _context;

        public ItemPedidoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemPedido>> GetByPedidoIdAsync(int pedidoId)
        {
            return await _context.ItemPedidos
                .Include(ip => ip.Producto)
                .Include(ip => ip.Comercio)
                .Where(ip => ip.PedidoIdPedido == pedidoId && ip.deletedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemPedido>> GetByComercioIdAsync(int comercioId)
        {
            return await _context.ItemPedidos
                .Include(ip => ip.Producto)
                .Include(ip => ip.Pedido)
                    .ThenInclude(p => p.Cliente)
                .Include(ip => ip.Pedido)
                    .ThenInclude(p => p.EstadoPedido)
                .Where(ip => ip.ComercioIdComercio == comercioId && ip.deletedAt == null)
                .OrderByDescending(ip => ip.Pedido.fecha)
                .ThenByDescending(ip => ip.Pedido.hora)
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemPedido>> GetByProductoIdAsync(int productoId)
        {
            return await _context.ItemPedidos
                .Include(ip => ip.Pedido)
                .Include(ip => ip.Comercio)
                .Where(ip => ip.ProductoIdProducto == productoId && ip.deletedAt == null)
                .OrderByDescending(ip => ip.Pedido.fecha)
                .ThenByDescending(ip => ip.Pedido.hora)
                .ToListAsync();
        }
    }
}
