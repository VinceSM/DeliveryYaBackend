using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IStockService
    {
        // Operaciones básicas de Stock
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock> GetStockByIdAsync(int id);
        Task<Stock> GetStockByProductoIdAsync(int productoId);
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<bool> UpdateStockAsync(Stock stock);
        Task<bool> DeleteStockAsync(int id);

        // Gestión de cantidades de stock
        Task<bool> UpdateCantidadStockAsync(int stockId, int nuevaCantidad);
        Task<bool> IncrementarStockAsync(int stockId, int cantidad);
        Task<bool> DecrementarStockAsync(int stockId, int cantidad);
        Task<bool> SetStockIlimitadoAsync(int stockId, bool ilimitado);

        // Validaciones y verificaciones
        Task<bool> VerificarStockSuficienteAsync(int stockId, int cantidadRequerida);
        Task<int> GetStockDisponibleAsync(int stockId);
        Task<bool> StockExistsForProductoAsync(int productoId);

        // Operaciones por producto
        Task<bool> ActualizarStockPorProductoAsync(int productoId, int nuevaCantidad);
        Task<bool> IncrementarStockPorProductoAsync(int productoId, int cantidad);
        Task<bool> DecrementarStockPorProductoAsync(int productoId, int cantidad);
        Task<int> GetStockDisponiblePorProductoAsync(int productoId);

        // Reportes y estadísticas
        Task<IEnumerable<Stock>> GetStocksBajosAsync(int nivelMinimo = 10);
        Task<IEnumerable<Stock>> GetStocksAgotadosAsync();
        Task<int> GetTotalProductosConStockAsync();
        Task<int> GetTotalProductosSinStockAsync();
    }
}