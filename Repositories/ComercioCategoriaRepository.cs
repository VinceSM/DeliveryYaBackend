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

        public async Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId)
        {
            var existe = await _context.ComercioCategorias
                .AnyAsync(cc => cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (existe)
                return false;

            var comercioExiste = await _context.Comercios.AnyAsync(c => c.idcomercio == comercioId && c.deletedAt == null);
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.idcategoria == categoriaId && c.deletedAt == null);

            if (!comercioExiste || !categoriaExiste)
                return false;

            var nueva = new ComercioCategoria
            {
                ComercioIdComercio = comercioId,
                CategoriaIdCategoria = categoriaId
            };

            await _context.ComercioCategorias.AddAsync(nueva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId)
        {
            var relacion = await _context.ComercioCategorias
                .FirstOrDefaultAsync(cc => cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (relacion == null)
                return false;

            _context.ComercioCategorias.Remove(relacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId)
        {
            return await _context.ComercioCategorias
                .Where(cc => cc.ComercioIdComercio == comercioId)
                .Include(cc => cc.Categoria)
                .Select(cc => cc.Categoria!)
                .Where(c => c.deletedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId)
        {
            return await _context.ComercioCategorias
                .Where(cc => cc.CategoriaIdCategoria == categoriaId)
                .Include(cc => cc.Comercio)
                .Select(cc => cc.Comercio!)
                .Where(c => c.deletedAt == null)
                .ToListAsync();
        }
    }

}
