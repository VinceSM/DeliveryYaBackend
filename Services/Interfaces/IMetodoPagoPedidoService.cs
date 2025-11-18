using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IMetodoPagoPedidoService
    {
        Task<IEnumerable<MetodoPagoPedido>> GetAllMetodosAsync();
        Task<MetodoPagoPedido?> GetMetodoByIdAsync(int id);
        Task<MetodoPagoPedido?> GetMetodoByMetodoAsync(string metodo);
        Task<bool> ExistsAsync(int id);

        // CRUD
        Task<MetodoPagoPedido> CreateMetodoAsync(MetodoPagoPedido metodo);
        Task<MetodoPagoPedido?> UpdateMetodoAsync(int id, MetodoPagoPedido metodo);
        Task<bool> DeleteMetodoAsync(int id);
    }
}
