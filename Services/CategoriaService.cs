using DeliveryYaBackend.DTOs.Requests;
using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Responses;
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
            var categoria = new Categoria
            {
                nombre = request.Nombre,
                createdAt = DateTime.UtcNow
            };

            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveChangesAsync();

            return ToResponse(categoria);
        }

        // ✏️ Actualizar nombre
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

        // 🗑️ Eliminar categoría (borrado lógico)
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

        // 📜 Listar todas las categorías activas
        public async Task<IEnumerable<CategoriaResponse>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias
                .Where(c => c.deletedAt == null)
                .Select(ToResponse);
        }

        // 📄 Obtener una categoría por ID
        public async Task<CategoriaResponse?> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null || categoria.deletedAt != null)
                return null;

            return ToResponse(categoria);
        }

        private CategoriaResponse ToResponse(Categoria categoria)
        {
            return new CategoriaResponse
            {
                Id = categoria.idcategoria,
                Nombre = categoria.nombre
            };
        }
    }
}
