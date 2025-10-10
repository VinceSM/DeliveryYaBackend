using DeliveryYaBackend.DTOs.Requests.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<PedidosController> _logger;

        public PedidosController(IPedidoService pedidoService, ILogger<PedidosController> logger)
        {
            _pedidoService = pedidoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.GetAllPedidosAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoByIdAsync(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedido por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetPedidosByCliente(int clienteId)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByClienteAsync(clienteId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos por cliente");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("repartidor/{repartidorId}")]
        public async Task<IActionResult> GetPedidosByRepartidor(int repartidorId)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByRepartidorAsync(repartidorId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos por repartidor");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetPedidosByEstado(string estado)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByEstadoAsync(estado);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos por estado");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] CreatePedidoRequest request)
        {
            try
            {
                var pedido = new Pedido
                {
                    fecha = DateTime.Now,
                    hora = TimeOnly.FromDateTime(DateTime.Now).ToTimeSpan(), // Convertir a TimeSpan
                    codigo = GenerateCodigoPedido(),
                    pagado = false,
                    comercioRepartidor = request.ComercioRepartidor,
                    subtotalPedido = 0,
                    ClienteIdCliente = request.ClienteId, // ← Usar el nombre exacto de la propiedad
                    RepartidorIdRepartidor = request.RepartidorId, // ← Usar el nombre exacto
                    EstadoPedidoIdEstado = 1,
                    MetodoPagoPedidoIdMetodo = request.MetodoPagoId // ← Usar el nombre exacto
                };

                // Convertir items del request a modelos
                var items = request.Items.Select(item => new ItemPedido
                {
                    ProductoIdProducto = item.ProductoId, // ← Usar el nombre exacto
                    ComercioIdComercio = item.ComercioId, // ← Usar el nombre exacto
                    cantProducto = item.Cantidad,
                    precioFinal = item.PrecioUnitario,
                    total = item.Cantidad * item.PrecioUnitario
                }).ToList();

                var resultado = await _pedidoService.CreatePedidoAsync(pedido, items);
                return CreatedAtAction(nameof(GetPedidoById), new { id = resultado.idpedido }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pedido");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstadoPedido(int id, [FromBody] string nuevoEstado)
        {
            try
            {
                var resultado = await _pedidoService.UpdateEstadoPedidoAsync(id, nuevoEstado);
                if (!resultado)
                    return NotFound("Pedido no encontrado");

                return Ok("Estado del pedido actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado del pedido");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}/pago")]
        public async Task<IActionResult> UpdatePagoPedido(int id, [FromBody] bool pagado)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoByIdAsync(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                pedido.pagado = pagado;
                var resultado = await _pedidoService.UpdatePedidoAsync(pedido);

                if (!resultado)
                    return StatusCode(500, "Error al actualizar pago del pedido");

                return Ok("Estado de pago actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar pago del pedido");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            try
            {
                var resultado = await _pedidoService.DeletePedidoAsync(id);
                if (!resultado)
                    return NotFound("Pedido no encontrado");

                return Ok("Pedido eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar pedido");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/total")]
        public async Task<IActionResult> GetTotalPedido(int id)
        {
            try
            {
                var total = await _pedidoService.CalcularTotalPedidoAsync(id);
                return Ok(new { Total = total });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular total del pedido");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private string GenerateCodigoPedido()
        {
            return $"PED-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}