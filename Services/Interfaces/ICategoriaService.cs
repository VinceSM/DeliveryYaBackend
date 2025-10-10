using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface ICategoriaService
    {
        // Operaciones básicas de Categoría
        Task<Categoria> CreateCategoriaAsync(Categoria categoria);
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> GetCategoriaByNombreAsync(string nombre);
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync();
        Task<bool> UpdateCategoriaAsync(Categoria categoria);
        Task<bool> DeleteCategoriaAsync(int id);

        // Relación con Productos
        Task<bool> AddProductoToCategoriaAsync(int categoriaId, int productoId);
        Task<bool> RemoveProductoFromCategoriaAsync(int categoriaId, int productoId);
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        Task<int> GetCantidadProductosByCategoriaAsync(int categoriaId);

        // Relación con Comercios
        Task<bool> AddComercioToCategoriaAsync(int categoriaId, int comercioId);
        Task<bool> RemoveComercioFromCategoriaAsync(int categoriaId, int comercioId);
        Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId);
        Task<int> GetCantidadComerciosByCategoriaAsync(int categoriaId);

        // Búsquedas y filtros
        Task<IEnumerable<Categoria>> GetCategoriasWithProductosAsync();
        Task<IEnumerable<Categoria>> GetCategoriasWithComerciosAsync();
        Task<IEnumerable<Categoria>> SearchCategoriasAsync(string searchTerm);

        // Validaciones
        Task<bool> CategoriaExistsAsync(string nombre);
        Task<bool> CategoriaHasProductosAsync(int categoriaId);
        Task<bool> CategoriaHasComerciosAsync(int categoriaId);
    }
}