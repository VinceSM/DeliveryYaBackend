using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> CreatePedidoAsync(Pedido pedido, List<ItemPedido> items);
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId);
        Task<IEnumerable<Pedido>> GetPedidosByRepartidorAsync(int repartidorId);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado);
        Task<bool> UpdatePedidoAsync(Pedido pedido);
        Task<bool> UpdateEstadoPedidoAsync(int pedidoId, string nuevoEstado);
        Task<decimal> CalcularTotalPedidoAsync(int pedidoId);
        Task<bool> DeletePedidoAsync(int pedidoId);
    }
}