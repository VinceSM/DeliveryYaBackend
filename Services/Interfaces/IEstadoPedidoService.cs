using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IEstadoPedidoService
    {
        Task<IEnumerable<EstadoPedido>> GetAllEstadosAsync();
        Task<EstadoPedido?> GetEstadoByIdAsync(int id);
        Task<EstadoPedido?> GetEstadoByTipoAsync(string tipo);
        Task<bool> ExistsAsync(int id);

        // CRUD
        Task<EstadoPedido> CreateEstadoAsync(EstadoPedido estado);
        Task<EstadoPedido?> UpdateEstadoAsync(int id, EstadoPedido estado);
        Task<bool> DeleteEstadoAsync(int id);
    }
}
