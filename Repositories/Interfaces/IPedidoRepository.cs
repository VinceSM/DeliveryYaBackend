using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Pedido>> GetAllWithDetailsAsync();
        Task<IEnumerable<Pedido>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<Pedido>> GetByComercioIdAsync(int comercioId);
        Task<IEnumerable<Pedido>> GetByEstadoIdAsync(int estadoId);
        Task<Pedido?> GetByCodigoAsync(string codigo);
        Task<Pedido?> GetUltimoPedidoAsync();
        Task<bool> CodigoExistsAsync(string codigo);
    }
}
