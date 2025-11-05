using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // ✅ Crear categoría
        public async Task<CategoriaResponse> CreateAsync(CreateCategoriaRequest request)
        {
            // Evitar duplicados
            var existentes = await _categoriaRepository.GetAllActiveAsync();
            if (existentes.Any(c => c.nombre.ToLower() == request.Nombre.ToLower()))
                throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

            var categoria = new Categoria
            {
                nombre = request.Nombre,
                createdAt = DateTime.UtcNow
            };

            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveChangesAsync();

            return ToResponse(categoria);
        }

        // ✏️ Actualizar
        public async Task<CategoriaResponse?> UpdateAsync(int id, UpdateCategoriaRequest request)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null || categoria.deletedAt != null)
                return null;

            categoria.nombre = request.Nombre;
            categoria.updatedAt = DateTime.UtcNow;

            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveChangesAsync();

            return ToResponse(categoria);
        }

        // 🗑️ Borrado lógico
        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null || categoria.deletedAt != null)
                return false;

            categoria.deletedAt = DateTime.UtcNow;

            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveChangesAsync();
            return true;
        }

        // 📜 Todas
        public async Task<IEnumerable<CategoriaResponse>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(ToResponse);
        }

        // 📜 Solo activas
        public async Task<IEnumerable<CategoriaResponse>> GetAllActiveAsync()
        {
            var categorias = await _categoriaRepository.GetAllActiveAsync();
            return categorias.Select(ToResponse);
        }

        // 📄 Por ID
        public async Task<CategoriaResponse?> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null || categoria.deletedAt != null)
                return null;

            return ToResponse(categoria);
        }

        private static CategoriaResponse ToResponse(Categoria categoria) => new CategoriaResponse
        {
            Id = categoria.idcategoria,
            Nombre = categoria.nombre
        };
    }
}
