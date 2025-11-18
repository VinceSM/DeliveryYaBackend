using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IEstadoPedidoRepository : IRepository<EstadoPedido>
    {
        Task<EstadoPedido?> GetByTipoAsync(string tipo);
        Task<IEnumerable<EstadoPedido>> GetAllActivosAsync();
        Task<bool> TipoExistsAsync(string tipo, int? excludeId = null);
    }
}
