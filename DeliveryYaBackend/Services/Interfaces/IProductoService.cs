using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IProductoService
    {
        // Operaciones básicas de Producto
        Task<Producto> CreateProductoAsync(Producto producto, Stock stock);
        Task<Producto> GetProductoByIdAsync(int id);
        Task<Producto> GetProductoWithDetailsAsync(int id);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<IEnumerable<Producto>> GetProductosByNombreAsync(string nombre);
        Task<bool> UpdateProductoAsync(Producto producto);
        Task<bool> DeleteProductoAsync(int id);

        // Búsquedas y filtros
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        Task<IEnumerable<Producto>> GetProductosByComercioAsync(int comercioId);
        Task<IEnumerable<Producto>> GetProductosEnOfertaAsync();
        Task<IEnumerable<Producto>> GetProductosByPrecioRangeAsync(decimal minPrecio, decimal maxPrecio);

        // Gestión de stock y precios
        Task<bool> UpdateProductoStockAsync(int productoId, int cantidad);
        Task<bool> UpdateProductoPrecioAsync(int productoId, decimal nuevoPrecio);
        Task<bool> SetProductoOfertaAsync(int productoId, bool enOferta);
        Task<int> GetStockDisponibleAsync(int productoId);

        // Categorías
        Task<bool> AddCategoriaToProductoAsync(int productoId, int categoriaId);
        Task<bool> RemoveCategoriaFromProductoAsync(int productoId, int categoriaId);
        Task<IEnumerable<Categoria>> GetCategoriasByProductoAsync(int productoId);
    }
}