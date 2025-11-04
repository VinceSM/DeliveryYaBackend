using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Responses.Categorias;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponse>> GetAllAsync();
        Task<CategoriaResponse?> GetByIdAsync(int id);
        Task<CategoriaResponse> CreateAsync(CreateCategoriaRequest request);
        Task<CategoriaResponse?> UpdateAsync(int id, UpdateCategoriaRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
