using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class MetodoPagoPedidoService : IMetodoPagoPedidoService
    {
        private readonly IMetodoPagoPedidoRepository _metodoPagoPedidoRepository;
        private readonly ILogger<MetodoPagoPedidoService> _logger;

        public MetodoPagoPedidoService(
            IMetodoPagoPedidoRepository metodoPagoPedidoRepository,
            ILogger<MetodoPagoPedidoService> logger)
        {
            _metodoPagoPedidoRepository = metodoPagoPedidoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MetodoPagoPedido>> GetAllMetodosAsync()
        {
            return await _metodoPagoPedidoRepository.GetAllActivosAsync();
        }

        public async Task<MetodoPagoPedido?> GetMetodoByIdAsync(int id)
        {
            return await _metodoPagoPedidoRepository.GetByIdAsync(id);
        }

        public async Task<MetodoPagoPedido?> GetMetodoByMetodoAsync(string metodo)
        {
            return await _metodoPagoPedidoRepository.GetByMetodoAsync(metodo);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var metodo = await _metodoPagoPedidoRepository.GetByIdAsync(id);
            return metodo != null && metodo.deletedAt == null;
        }

        // CREATE
        public async Task<MetodoPagoPedido> CreateMetodoAsync(MetodoPagoPedido metodo)
        {
            // Validar que el método no exista
            if (await _metodoPagoPedidoRepository.MetodoExistsAsync(metodo.metodo))
            {
                throw new InvalidOperationException($"Ya existe un método de pago con el nombre '{metodo.metodo}'");
            }

            metodo.createdAt = DateTime.UtcNow;
            metodo.updatedAt = DateTime.UtcNow;

            await _metodoPagoPedidoRepository.AddAsync(metodo);
            await _metodoPagoPedidoRepository.SaveChangesAsync();

            return metodo;
        }

        // UPDATE
        public async Task<MetodoPagoPedido?> UpdateMetodoAsync(int id, MetodoPagoPedido metodo)
        {
            var existingMetodo = await _metodoPagoPedidoRepository.GetByIdAsync(id);
            if (existingMetodo == null || existingMetodo.deletedAt != null)
                return null;

            // Validar que el método no exista (excluyendo el actual)
            if (await _metodoPagoPedidoRepository.MetodoExistsAsync(metodo.metodo, id))
            {
                throw new InvalidOperationException($"Ya existe otro método de pago con el nombre '{metodo.metodo}'");
            }

            existingMetodo.metodo = metodo.metodo;
            existingMetodo.updatedAt = DateTime.UtcNow;

            _metodoPagoPedidoRepository.Update(existingMetodo);
            await _metodoPagoPedidoRepository.SaveChangesAsync();

            return existingMetodo;
        }

        // DELETE (soft delete)
        public async Task<bool> DeleteMetodoAsync(int id)
        {
            var metodo = await _metodoPagoPedidoRepository.GetByIdAsync(id);
            if (metodo == null) return false;

            metodo.deletedAt = DateTime.UtcNow;
            metodo.updatedAt = DateTime.UtcNow;

            _metodoPagoPedidoRepository.Update(metodo);
            return await _metodoPagoPedidoRepository.SaveChangesAsync();
        }
    }
}
