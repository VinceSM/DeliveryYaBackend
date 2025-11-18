using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pedido?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Producto)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Comercio)
                .FirstOrDefaultAsync(p => p.idpedido == id && p.deletedAt == null);
        }

        public async Task<IEnumerable<Pedido>> GetAllWithDetailsAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                .Where(p => p.deletedAt == null)
                .OrderByDescending(p => p.fecha)
                .ThenByDescending(p => p.hora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Producto)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Comercio)
                .Where(p => p.ClienteIdCliente == clienteId && p.deletedAt == null)
                .OrderByDescending(p => p.fecha)
                .ThenByDescending(p => p.hora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetByComercioIdAsync(int comercioId)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Producto)
                .Where(p => p.ItemsPedido.Any(ip => ip.ComercioIdComercio == comercioId) && p.deletedAt == null)
                .OrderByDescending(p => p.fecha)
                .ThenByDescending(p => p.hora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetByEstadoIdAsync(int estadoId)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                .Where(p => p.EstadoPedidoIdEstado == estadoId && p.deletedAt == null)
                .OrderByDescending(p => p.fecha)
                .ThenByDescending(p => p.hora)
                .ToListAsync();
        }

        public async Task<Pedido?> GetByCodigoAsync(string codigo)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoPagoPedido)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Producto)
                .Include(p => p.ItemsPedido)
                    .ThenInclude(ip => ip.Comercio)
                .FirstOrDefaultAsync(p => p.codigo == codigo && p.deletedAt == null);
        }

        public async Task<Pedido?> GetUltimoPedidoAsync()
        {
            return await _context.Pedidos
                .Where(p => p.deletedAt == null)
                .OrderByDescending(p => p.idpedido)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CodigoExistsAsync(string codigo)
        {
            return await _context.Pedidos
                .AnyAsync(p => p.codigo == codigo && p.deletedAt == null);
        }
    }
}
