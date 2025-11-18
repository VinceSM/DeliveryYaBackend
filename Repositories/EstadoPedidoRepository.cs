using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class EstadoPedidoRepository : GenericRepository<EstadoPedido>, IEstadoPedidoRepository
    {
        private readonly AppDbContext _context;

        public EstadoPedidoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EstadoPedido?> GetByTipoAsync(string tipo)
        {
            return await _context.EstadoPedidos
                .FirstOrDefaultAsync(e => e.tipo.ToLower() == tipo.ToLower() && e.deletedAt == null);
        }

        public async Task<IEnumerable<EstadoPedido>> GetAllActivosAsync()
        {
            return await _context.EstadoPedidos
                .Where(e => e.deletedAt == null)
                .OrderBy(e => e.tipo)
                .ToListAsync();
        }

        public async Task<bool> TipoExistsAsync(string tipo, int? excludeId = null)
        {
            var query = _context.EstadoPedidos
                .Where(e => e.tipo.ToLower() == tipo.ToLower() && e.deletedAt == null);

            if (excludeId.HasValue)
            {
                query = query.Where(e => e.idestado != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
