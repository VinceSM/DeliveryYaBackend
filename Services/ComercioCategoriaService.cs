using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Services
{
    public class ComercioCategoriaService : IComercioCategoriaService
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Comercio> _comercioRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<ComercioCategoria> _comercioCategoriaRepository;

        public ComercioCategoriaService(
            AppDbContext context,
            IRepository<Categoria> categoriaRepository,
            IRepository<Comercio> comercioRepository,
            IRepository<ComercioCategoria> comercioCategoriaRepository)
        {
            _context = context;
            _comercioRepository = comercioRepository;
            _categoriaRepository = categoriaRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
        }

        public async Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId)
        {
            // 1️⃣ Validar existencia y estado
            var comercio = await _comercioRepository.GetByIdAsync(comercioId);
            var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);

            if (comercio == null || comercio.deletedAt != null ||
                categoria == null || categoria.deletedAt != null)
                return false;

            // 2️⃣ Verificar si ya existe la relación
            var existe = await _comercioCategoriaRepository.FindAsync(cc =>
                cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (existe.Any())
                return false;

            // 3️⃣ Crear relación
            var nuevaRelacion = new ComercioCategoria
            {
                ComercioIdComercio = comercioId,
                CategoriaIdCategoria = categoriaId
            };

            await _comercioCategoriaRepository.AddAsync(nuevaRelacion);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId)
        {
            var relacion = (await _comercioCategoriaRepository.FindAsync(cc =>
                cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId)).FirstOrDefault();

            if (relacion == null)
                return false;

            _comercioCategoriaRepository.Remove(relacion);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId)
        {
            // 🔹 Más eficiente que varios awaits
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
