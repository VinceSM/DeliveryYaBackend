using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IProductoService
    {
        Task<ProductoResponse> CreateAsync(CreateProductoRequest request);
        Task<ProductoResponse?> UpdateAsync(UpdateProductoRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductoResponse>> GetAllAsync();
        Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int categoriaId);
        Task<IEnumerable<ProductoResponse>> SearchByNameAsync(string nombre);
        Task<ProductoResponse?> GetByIdAsync(int id);
    }
}
