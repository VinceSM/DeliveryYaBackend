using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class TarifaRepartidorRepository : GenericRepository<TarifaRepartidorLibre>, ITarifaRepartidorRepository
    {
        private readonly AppDbContext _context;

        public TarifaRepartidorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TarifaRepartidorLibre>> GetTarifasPorRepartidorAsync(int repartidorId)
        {
            return await _context.TarifasRepartidorLibre
                                 .Where(t => t.RepartidorIdRepartidor == repartidorId)
                                 .ToListAsync();
        }

        public async Task<TarifaRepartidorLibre> GetUltimaTarifaByRepartidorAsync(int repartidorId)
        {
            return await _context.TarifasRepartidorLibre
                                 .Where(t => t.RepartidorIdRepartidor == repartidorId)
                                 .OrderByDescending(t => t.createdAt)
                                 .FirstOrDefaultAsync();
        }
    }
}
