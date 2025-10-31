using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<ItemPedido> _itemPedidoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<EstadoPedido> _estadoPedidoRepository;

        public PedidoService(
            IRepository<Pedido> pedidoRepository,
            IRepository<ItemPedido> itemPedidoRepository,
            IRepository<Cliente> clienteRepository,
            IRepository<EstadoPedido> estadoPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _clienteRepository = clienteRepository;
            _estadoPedidoRepository = estadoPedidoRepository;
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido, List<ItemPedido> items)
        {
            // Validaciones y lógica de negocio
            var clienteExists = await _clienteRepository.ExistsAsync(c => c.idcliente == pedido.ClienteIdCliente);
            if (!clienteExists)
                throw new Exception("Cliente no encontrado");

            await _pedidoRepository.AddAsync(pedido);

            foreach (var item in items)
            {
                item.PedidoIdPedido = pedido.idpedido;
                await _itemPedidoRepository.AddAsync(item);
            }

            await _pedidoRepository.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId)
        {
            return await _pedidoRepository.FindAsync(p => p.ClienteIdCliente == clienteId);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByRepartidorAsync(int repartidorId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(string estado)
        {
            // Primero obtener el ID del estado
            var estados = await _estadoPedidoRepository.FindAsync(e => e.tipo == estado);
            var estadoPedido = estados.FirstOrDefault();

            if (estadoPedido == null)
                return new List<Pedido>();

            // Luego obtener los pedidos con ese estado
            return await _pedidoRepository.FindAsync(p => p.EstadoPedidoIdEstado == estadoPedido.idestado);
        }

        public async Task<bool> UpdatePedidoAsync(Pedido pedido)
        {
            var existingPedido = await _pedidoRepository.GetByIdAsync(pedido.idpedido);
            if (existingPedido == null) return false;

            _pedidoRepository.Update(pedido);
            return await _pedidoRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateEstadoPedidoAsync(int pedidoId, string nuevoEstado)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null) return false;

            var estado = await _estadoPedidoRepository.FindAsync(e => e.tipo == nuevoEstado);
            if (estado.FirstOrDefault() == null) return false;

            pedido.EstadoPedidoIdEstado = estado.First().idestado;
            _pedidoRepository.Update(pedido);

            return await _pedidoRepository.SaveChangesAsync();
        }

        public async Task<decimal> CalcularTotalPedidoAsync(int pedidoId)
        {
            var items = await _itemPedidoRepository.FindAsync(ip => ip.PedidoIdPedido == pedidoId);
            return items.Sum(ip => ip.precioFinal * ip.cantProducto);
        }

        public async Task<bool> DeletePedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null) return false;

            _pedidoRepository.Remove(pedido);
            return await _pedidoRepository.SaveChangesAsync();
        }
    }
}