using DeliveryYaBackend.DTOs.Requests.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IPedidoService
    {
        // Consultas
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task<Pedido?> GetPedidoByCodigoAsync(string codigo);
        Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId);
        Task<IEnumerable<Pedido>> GetPedidosByComercioAsync(int comercioId);
        Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(int estadoId);

        // CRUD
        Task<PedidoResponse> CreatePedidoAsync(CrearPedidoRequest request);
        Task<PedidoResponse?> UpdateEstadoPedidoAsync(int pedidoId, int estadoId);
        Task<PedidoResponse?> UpdatePagoPedidoAsync(int pedidoId, bool pagado);
        Task<bool> DeletePedidoAsync(int id);

        // Métodos auxiliares
        Task<bool> ExistsAsync(int id);
        Task<decimal> CalcularTotalPedidoAsync(int pedidoId);
        Task<PedidoResponse> ToResponseAsync(Pedido pedido);
    }
}