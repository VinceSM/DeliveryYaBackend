using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class ComercioCategoriaRepository : IComercioCategoriaRepository
    {
        private readonly AppDbContext _context;

        public ComercioCategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Asignar categoría a comercio
        public async Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId)
        {
            var existe = await _context.ComercioCategorias
                .AnyAsync(cc => cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (existe)
                return false;

            var nueva = new ComercioCategoria
            {
                ComercioIdComercio = comercioId,
                CategoriaIdCategoria = categoriaId
            };

            _context.ComercioCategorias.Add(nueva);
            await _context.SaveChangesAsync();
            return true;
        }


        // ✅ Quitar categoría de comercio
        public async Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId)
        {
            var relacion = await _context.ComercioCategorias
                .FirstOrDefaultAsync(cc => cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (relacion == null) return false;

            _context.ComercioCategorias.Remove(relacion);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ Categorías por comercio
        public async Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId)
        {
            return await _context.ComercioCategorias
                .Where(cc => cc.ComercioIdComercio == comercioId)
                .Include(cc => cc.Categoria)
                .Select(cc => cc.Categoria)
                .ToListAsync();
        }

        // ✅ Comercios por categoría
        public async Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId)
        {
            return await _context.ComercioCategorias
                .Where(cc => cc.CategoriaIdCategoria == categoriaId)
                .Include(cc => cc.Comercio)
                .Select(cc => cc.Comercio)
                .ToListAsync();
        }
    }
}
