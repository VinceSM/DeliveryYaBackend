using DeliveryYaBackend.DTOs.Requests.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IClienteService _clienteService;
        private readonly IComercioService _comercioService;
        private readonly IProductoService _productoService;
        private readonly IEstadoPedidoService _estadoPedidoService;
        private readonly IMetodoPagoPedidoService _metodoPagoPedidoService;
        private readonly ILogger<PedidoService> _logger;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository,
            IClienteService clienteService,
            IComercioService comercioService,
            IProductoService productoService,
            IEstadoPedidoService estadoPedidoService,
            IMetodoPagoPedidoService metodoPagoPedidoService,
            ILogger<PedidoService> logger)
        {
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _clienteService = clienteService;
            _comercioService = comercioService;
            _productoService = productoService;
            _estadoPedidoService = estadoPedidoService;
            _metodoPagoPedidoService = metodoPagoPedidoService;
            _logger = logger;
        }

        // Consultas
        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _pedidoRepository.GetAllWithDetailsAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<Pedido?> GetPedidoByCodigoAsync(string codigo)
        {
            return await _pedidoRepository.GetByCodigoAsync(codigo);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByClienteAsync(int clienteId)
        {
            return await _pedidoRepository.GetByClienteIdAsync(clienteId);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByComercioAsync(int comercioId)
        {
            return await _pedidoRepository.GetByComercioIdAsync(comercioId);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByEstadoAsync(int estadoId)
        {
            return await _pedidoRepository.GetByEstadoIdAsync(estadoId);
        }

        // CREATE - Método principal para crear pedido
        public async Task<PedidoResponse> CreatePedidoAsync(CrearPedidoRequest request)
        {
            try
            {
                // Validaciones
                await ValidarCreacionPedidoAsync(request);

                // Generar código único
                var codigo = await GenerarCodigoPedidoAsync();

                // Crear pedido
                var pedido = new Pedido
                {
                    codigo = codigo,
                    fecha = DateTime.Today,
                    hora = DateTime.Now.TimeOfDay,
                    pagado = false,
                    comercioRepartidor = false, // Por defecto, el sistema maneja el reparto
                    subtotalPedido = 0, // Se calculará abajo
                    direccionEnvio = request.DireccionEnvio,
                    ClienteIdCliente = request.ClienteId,
                    EstadoPedidoIdEstado = 1, // Estado inicial: "Pendiente"
                    MetodoPagoPedidoIdMetodo = request.MetodoPagoId,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                };

                // Crear items del pedido y calcular subtotal
                decimal subtotal = 0;
                var itemsPedido = new List<ItemPedido>();

                foreach (var itemRequest in request.Items)
                {
                    var item = await CrearItemPedidoAsync(itemRequest, pedido.idpedido);
                    itemsPedido.Add(item);
                    subtotal += item.total;
                }

                pedido.subtotalPedido = subtotal;
                pedido.ItemsPedido = itemsPedido;

                // Guardar pedido
                await _pedidoRepository.AddAsync(pedido);
                await _pedidoRepository.SaveChangesAsync();

                // Retornar response
                return await ToResponseAsync(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pedido para el cliente {ClienteId}", request.ClienteId);
                throw;
            }
        }

        // UPDATE Estado
        public async Task<PedidoResponse?> UpdateEstadoPedidoAsync(int pedidoId, int estadoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null || pedido.deletedAt != null)
                return null;

            // Validar que el estado existe
            if (!await _estadoPedidoService.ExistsAsync(estadoId))
                throw new InvalidOperationException("El estado de pedido no existe");

            pedido.EstadoPedidoIdEstado = estadoId;
            pedido.updatedAt = DateTime.UtcNow;

            _pedidoRepository.Update(pedido);
            await _pedidoRepository.SaveChangesAsync();

            return await ToResponseAsync(pedido);
        }

        // UPDATE Pago
        public async Task<PedidoResponse?> UpdatePagoPedidoAsync(int pedidoId, bool pagado)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null || pedido.deletedAt != null)
                return null;

            pedido.pagado = pagado;
            pedido.updatedAt = DateTime.UtcNow;

            _pedidoRepository.Update(pedido);
            await _pedidoRepository.SaveChangesAsync();

            return await ToResponseAsync(pedido);
        }

        // DELETE
        public async Task<bool> DeletePedidoAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null) return false;

            pedido.deletedAt = DateTime.UtcNow;
            pedido.updatedAt = DateTime.UtcNow;

            _pedidoRepository.Update(pedido);
            return await _pedidoRepository.SaveChangesAsync();
        }

        // Métodos auxiliares
        public async Task<bool> ExistsAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            return pedido != null && pedido.deletedAt == null;
        }

        public async Task<decimal> CalcularTotalPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdWithDetailsAsync(pedidoId);
            if (pedido == null) return 0;

            // Aquí podríamos agregar envío, comisiones, etc.
            return pedido.subtotalPedido;
        }

        // Métodos privados
        private async Task ValidarCreacionPedidoAsync(CrearPedidoRequest request)
        {
            // Validar cliente
            var cliente = await _clienteService.GetByIdAsync(request.ClienteId);
            if (cliente == null)
                throw new InvalidOperationException("El cliente no existe");

            // Validar método de pago
            if (!await _metodoPagoPedidoService.ExistsAsync(request.MetodoPagoId))
                throw new InvalidOperationException("El método de pago no existe");

            // Validar items
            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("El pedido debe contener al menos un item");

            // Validar cada item
            foreach (var item in request.Items)
            {
                await ValidarItemPedidoAsync(item);
            }
        }

        private async Task ValidarItemPedidoAsync(ItemPedidoRequest itemRequest)
        {
            // Validar producto
            var producto = await _productoService.GetByIdAsync(itemRequest.ProductoId);
            if (producto == null)
                throw new InvalidOperationException($"El producto con ID {itemRequest.ProductoId} no existe");

            // Validar stock
            if (producto.Stock == false)
                throw new InvalidOperationException($"El producto '{producto.Nombre}' no tiene stock disponible");

            // Validar comercio
            if (!await _comercioService.ExistsAsync(itemRequest.ComercioId))
                throw new InvalidOperationException($"El comercio con ID {itemRequest.ComercioId} no existe");

            // Validar que el producto pertenece al comercio
            // (Aquí necesitaríamos una relación Producto-Comercio o validar por categorías)
        }

        private async Task<ItemPedido> CrearItemPedidoAsync(ItemPedidoRequest itemRequest, int pedidoId)
        {
            var producto = await _productoService.GetByIdAsync(itemRequest.ProductoId);

            var item = new ItemPedido
            {
                cantProducto = itemRequest.Cantidad,
                precioFinal = producto.PrecioUnitario, // Usar precio actual de la BD
                total = producto.PrecioUnitario * itemRequest.Cantidad,
                ProductoIdProducto = itemRequest.ProductoId,
                PedidoIdPedido = pedidoId,
                ComercioIdComercio = itemRequest.ComercioId,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };

            return item;
        }

        private async Task<string> GenerarCodigoPedidoAsync()
        {
            var ultimoPedido = await _pedidoRepository.GetUltimoPedidoAsync();

            if (ultimoPedido == null || string.IsNullOrEmpty(ultimoPedido.codigo))
            {
                return "AAA-0001";
            }

            var partes = ultimoPedido.codigo.Split('-');
            if (partes.Length != 2)
                return "AAA-0001";

            var letras = partes[0];
            var numeros = partes[1];

            if (int.TryParse(numeros, out int numero))
            {
                numero++;
                if (numero > 9999)
                {
                    numero = 1;
                    letras = IncrementarLetras(letras);
                }

                return $"{letras}-{numero.ToString("D4")}";
            }

            return "AAA-0001";
        }

        private string IncrementarLetras(string letras)
        {
            char[] chars = letras.ToCharArray();

            for (int i = chars.Length - 1; i >= 0; i--)
            {
                if (chars[i] < 'Z')
                {
                    chars[i]++;
                    return new string(chars);
                }
                else
                {
                    chars[i] = 'A';
                }
            }

            return "AAA";
        }

        public async Task<PedidoResponse> ToResponseAsync(Pedido pedido)
        {
            var pedidoCompleto = await _pedidoRepository.GetByIdWithDetailsAsync(pedido.idpedido);

            return new PedidoResponse
            {
                Id = pedidoCompleto.idpedido,
                Codigo = pedidoCompleto.codigo,
                Fecha = pedidoCompleto.fecha,
                Hora = pedidoCompleto.hora,
                Pagado = pedidoCompleto.pagado,
                ComercioRepartidor = pedidoCompleto.comercioRepartidor,
                SubtotalPedido = pedidoCompleto.subtotalPedido,
                TotalPedido = pedidoCompleto.subtotalPedido, // Por ahora igual al subtotal
                DireccionEnvio = pedidoCompleto.direccionEnvio,
                Estado = pedidoCompleto.EstadoPedido?.tipo,
                MetodoPago = pedidoCompleto.MetodoPagoPedido?.metodo,
                ClienteNombre = pedidoCompleto.Cliente?.nombreCompleto,
                Items = pedidoCompleto.ItemsPedido?.Select(ip => new ItemPedidoResponse
                {
                    Id = ip.iditemPedido,
                    Cantidad = ip.cantProducto,
                    PrecioUnitario = ip.precioFinal,
                    Total = ip.total,
                    ProductoNombre = ip.Producto?.nombre,
                    ProductoDescripcion = ip.Producto?.descripcion,
                    ComercioNombre = ip.Comercio?.nombreComercio,
                    ProductoId = ip.ProductoIdProducto,
                    ComercioId = ip.ComercioIdComercio
                }).ToList() ?? new List<ItemPedidoResponse>()
            };
        }
    }
}