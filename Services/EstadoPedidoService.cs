using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class EstadoPedidoService : IEstadoPedidoService
    {
        private readonly IEstadoPedidoRepository _estadoPedidoRepository;
        private readonly ILogger<EstadoPedidoService> _logger;

        public EstadoPedidoService(
            IEstadoPedidoRepository estadoPedidoRepository,
            ILogger<EstadoPedidoService> logger)
        {
            _estadoPedidoRepository = estadoPedidoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<EstadoPedido>> GetAllEstadosAsync()
        {
            return await _estadoPedidoRepository.GetAllActivosAsync();
        }

        public async Task<EstadoPedido?> GetEstadoByIdAsync(int id)
        {
            return await _estadoPedidoRepository.GetByIdAsync(id);
        }

        public async Task<EstadoPedido?> GetEstadoByTipoAsync(string tipo)
        {
            return await _estadoPedidoRepository.GetByTipoAsync(tipo);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var estado = await _estadoPedidoRepository.GetByIdAsync(id);
            return estado != null && estado.deletedAt == null;
        }

        // CREATE
        public async Task<EstadoPedido> CreateEstadoAsync(EstadoPedido estado)
        {
            // Validar que el tipo no exista
            if (await _estadoPedidoRepository.TipoExistsAsync(estado.tipo))
            {
                throw new InvalidOperationException($"Ya existe un estado con el tipo '{estado.tipo}'");
            }

            estado.createdAt = DateTime.UtcNow;
            estado.updatedAt = DateTime.UtcNow;

            await _estadoPedidoRepository.AddAsync(estado);
            await _estadoPedidoRepository.SaveChangesAsync();

            return estado;
        }

        // UPDATE
        public async Task<EstadoPedido?> UpdateEstadoAsync(int id, EstadoPedido estado)
        {
            var existingEstado = await _estadoPedidoRepository.GetByIdAsync(id);
            if (existingEstado == null || existingEstado.deletedAt != null)
                return null;

            // Validar que el tipo no exista (excluyendo el actual)
            if (await _estadoPedidoRepository.TipoExistsAsync(estado.tipo, id))
            {
                throw new InvalidOperationException($"Ya existe otro estado con el tipo '{estado.tipo}'");
            }

            existingEstado.tipo = estado.tipo;
            existingEstado.updatedAt = DateTime.UtcNow;

            _estadoPedidoRepository.Update(existingEstado);
            await _estadoPedidoRepository.SaveChangesAsync();

            return existingEstado;
        }

        // DELETE (soft delete)
        public async Task<bool> DeleteEstadoAsync(int id)
        {
            var estado = await _estadoPedidoRepository.GetByIdAsync(id);
            if (estado == null) return false;

            estado.deletedAt = DateTime.UtcNow;
            estado.updatedAt = DateTime.UtcNow;

            _estadoPedidoRepository.Update(estado);
            return await _estadoPedidoRepository.SaveChangesAsync();
        }
    }
    }

}
