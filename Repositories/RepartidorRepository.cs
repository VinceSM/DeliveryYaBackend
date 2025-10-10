using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace DeliveryYaBackend.Repositories
{
    public class RepartidorRepository : GenericRepository<Repartidor>, IRepartidorRepository
    {
        private readonly AppDbContext _context;

        public RepartidorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Repartidor?> GetByEmailAsync(string email)
        {
            return await _context.Repartidores
                                 .Include(r => r.Vehiculo)
                                 .FirstOrDefaultAsync(r => r.email == email);
        }

        public async Task<IEnumerable<Repartidor>> GetRepartidoresLibresAsync()
        {
            return await _context.Repartidores
                                 .Include(r => r.Vehiculo)
                                 .Where(r => r.libreRepartidor)
                                 .ToListAsync();
        }
    }
}
