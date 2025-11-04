using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IComercioService
    {
        // Operaciones básicas de Comercio
        Task<ComercioResponse> CreateComercioAsync(ComercioRequest comercio);
        Task<Comercio> GetComercioByIdAsync(int id);
        Task<IEnumerable<Comercio>> GetAllComerciosAsync();
        Task<IEnumerable<Comercio>> GetComerciosByNombreAsync(string nombre);
        Task<IEnumerable<Comercio>> GetComerciosByCiudadAsync(string ciudad);
        Task<Comercio> GetComercioByEmailAsync(string email);
        Task<bool> ComercioExistsAsync(string email);
        Task<ComercioResponse> UpdateComercioAsync(int id, UpdateComercioRequest comercio);
        Task<bool> DeleteComercioAsync(int id);

        // Gestión de estado y destacados
        Task<IEnumerable<Comercio>> GetComerciosDestacadosAsync();

        // Productos de comercios
        Task<IEnumerable<Producto>> GetProductosByComercioAsync(int comercioId);
        Task<int> GetCantidadProductosByComercioAsync(int comercioId);

        // Estadísticas y reportes
        Task<decimal> GetVentasTotalesByComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate);
        Task<int> GetPedidosComercioAsync(int comercioId, DateTime? startDate, DateTime? endDate);
        Task<ComercioPanelResponse?> GetComercioPanelDetalleAsync(int id);
    }
}