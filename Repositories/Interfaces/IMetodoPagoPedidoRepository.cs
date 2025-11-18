using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IMetodoPagoPedidoRepository : IRepository<MetodoPagoPedido>
    {
        Task<MetodoPagoPedido?> GetByMetodoAsync(string metodo);
        Task<IEnumerable<MetodoPagoPedido>> GetAllActivosAsync();
        Task<bool> MetodoExistsAsync(string metodo, int? excludeId = null);
    }
}
