using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class ComercioRepository : GenericRepository<Comercio>, IComercioRepository
    {
        private readonly AppDbContext _context;

        public ComercioRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comercio>> GetAllActivosAsync()
        {
            return await _context.Comercios
                .Where(c => c.deletedAt == null)
                .OrderBy(c => c.nombreComercio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comercio>> GetDestacadosAsync()
        {
            return await _context.Comercios
                .Where(c => c.destacado && c.deletedAt == null)
                .OrderByDescending(c => c.updatedAt)
                .ToListAsync();
        }

        public async Task<Comercio?> GetByEmailAsync(string email)
        {
            return await _context.Comercios
                .FirstOrDefaultAsync(c => c.email == email && c.deletedAt == null);
        }

        public async Task<IEnumerable<Comercio>> GetByCiudadAsync(string ciudad)
        {
            return await _context.Comercios
                .Where(c => c.ciudad.ToLower() == ciudad.ToLower() && c.deletedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comercio>> GetByNombreAsync(string nombre)
        {
            return await _context.Comercios
                .Where(c => c.nombreComercio.ToLower().Contains(nombre.ToLower()) && c.deletedAt == null)
                .ToListAsync();
        }
    }
}
