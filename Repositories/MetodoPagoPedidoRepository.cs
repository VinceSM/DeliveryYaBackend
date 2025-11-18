using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class MetodoPagoPedidoRepository : GenericRepository<MetodoPagoPedido>, IMetodoPagoPedidoRepository
    {
        private readonly AppDbContext _context;

        public MetodoPagoPedidoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MetodoPagoPedido?> GetByMetodoAsync(string metodo)
        {
            return await _context.MetodoPagoPedidos
                .FirstOrDefaultAsync(m => m.metodo.ToLower() == metodo.ToLower() && m.deletedAt == null);
        }

        public async Task<IEnumerable<MetodoPagoPedido>> GetAllActivosAsync()
        {
            return await _context.MetodoPagoPedidos
                .Where(m => m.deletedAt == null)
                .OrderBy(m => m.metodo)
                .ToListAsync();
        }

        public async Task<bool> MetodoExistsAsync(string metodo, int? excludeId = null)
        {
            var query = _context.MetodoPagoPedidos
                .Where(m => m.metodo.ToLower() == metodo.ToLower() && m.deletedAt == null);

            if (excludeId.HasValue)
            {
                query = query.Where(m => m.idmetodo != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
