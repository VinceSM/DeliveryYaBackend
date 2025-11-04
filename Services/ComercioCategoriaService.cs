using DeliveryYaBackend.Data;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories;
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

        public ComercioCategoriaService
        (
            AppDbContext context,
            IRepository<Categoria> categoriaRepository,
            IRepository<Comercio> comercioRepository,
            IRepository<ComercioCategoria> comercioCategoriaRepository
        )
        {
            _context = context;
            _comercioRepository = comercioRepository;
            _categoriaRepository = categoriaRepository;
            _comercioCategoriaRepository = comercioCategoriaRepository;
        }

        // CATEGORÍAS DE COMERCIOS
        public async Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId)
        {
            // Verificar si ya existe la relación
            var existingRelation = await _comercioCategoriaRepository.FindAsync(cc =>
                cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            if (existingRelation.Any()) return true;

            var comercioCategoria = new ComercioCategoria
            {
                ComercioIdComercio = comercioId,
                CategoriaIdCategoria = categoriaId
            };

            await _comercioCategoriaRepository.AddAsync(comercioCategoria);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId)
        {
            var relaciones = await _comercioCategoriaRepository.FindAsync(cc =>
                cc.ComercioIdComercio == comercioId && cc.CategoriaIdCategoria == categoriaId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _comercioCategoriaRepository.Remove(relacion);
            return await _comercioCategoriaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId)
        {
            var relaciones = await _comercioCategoriaRepository.FindAsync(cc => cc.ComercioIdComercio == comercioId);
            var categorias = new List<Categoria>();

            foreach (var relacion in relaciones)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(relacion.CategoriaIdCategoria);
                if (categoria != null)
                {
                    categorias.Add(categoria);
                }
            }

            return categorias;
        }

        public async Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId)
        {
            var relaciones = await _comercioCategoriaRepository.FindAsync(cc => cc.CategoriaIdCategoria == categoriaId);
            var comercios = new List<Comercio>();

            foreach (var relacion in relaciones)
            {
                var comercio = await _comercioRepository.GetByIdAsync(relacion.ComercioIdComercio);
                if (comercio != null)
                {
                    comercios.Add(comercio);
                }
            }

            return comercios;
        }
    }
}
