using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface ICategoriaProductoRepository
    {
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int idCategoria);
        Task<IEnumerable<Producto>> GetProductosByNombreAsync(string nombre);
        Task<Producto?> GetProductoByIdAsync(int idProducto);
        Task<Producto> CreateProductoAsync(Producto producto, int idCategoria);
        Task<Producto> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(int idProducto);
    }
}
