using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Devuelve solo categorías activas (no eliminadas)
        public async Task<IEnumerable<Categoria>> GetAllActiveAsync()
        {
            return await _context.Categorias
                .Where(c => c.deletedAt == null)
                .OrderBy(c => c.nombre)
                .ToListAsync();
        }

        public async Task<Categoria?> GetByNameAsync(string nombre)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.nombre.ToLower() == nombre.ToLower() && c.deletedAt == null);
        }

    }
}
