using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface ICategoriaProductoService
    {
        Task<IEnumerable<ProductoResponse>> GetProductosPorCategoriaAsync(int idCategoria);
        Task<IEnumerable<ProductoResponse>> GetProductosPorNombreAsync(string nombre);
        Task<ProductoResponse?> GetProductoPorIdAsync(int idProducto);
        Task<ProductoResponse> CrearProductoAsync(CreateProductoRequest request, int idCategoria);
        Task<ProductoResponse?> ActualizarProductoAsync(int idProducto, UpdateProductoRequest request);
        Task<bool> EliminarProductoAsync(int idProducto);
    }
}
