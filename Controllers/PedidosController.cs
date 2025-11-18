using DeliveryYaBackend.DTOs.Requests.Pedidos;
using DeliveryYaBackend.DTOs.Responses.Pedidos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    // Controllers/PedidosController.cs
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<PedidosController> _logger;

        public PedidosController(
            IPedidoService pedidoService,
            ILogger<PedidosController> logger)
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
                _logger.LogError(ex, "Error al obtener todos los pedidos");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoResponse>> GetPedidoById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var pedido = await _pedidoService.GetPedidoByIdAsync(id);
                if (pedido == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                var response = await _pedidoService.ToResponseAsync(pedido);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedido por ID {PedidoId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<PedidoResponse>> GetPedidoByCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                return BadRequest(new { message = "Código inválido" });

            try
            {
                var pedido = await _pedidoService.GetPedidoByCodigoAsync(codigo);
                if (pedido == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                var response = await _pedidoService.ToResponseAsync(pedido);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedido por código {Codigo}", codigo);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetPedidosByCliente(int clienteId)
        {
            if (clienteId <= 0)
                return BadRequest(new { message = "ID de cliente inválido" });

            try
            {
                var pedidos = await _pedidoService.GetPedidosByClienteAsync(clienteId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos del cliente {ClienteId}", clienteId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("comercio/{comercioId}")]
        public async Task<IActionResult> GetPedidosByComercio(int comercioId)
        {
            if (comercioId <= 0)
                return BadRequest(new { message = "ID de comercio inválido" });

            try
            {
                var pedidos = await _pedidoService.GetPedidosByComercioAsync(comercioId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos del comercio {ComercioId}", comercioId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("estado/{estadoId}")]
        public async Task<IActionResult> GetPedidosByEstado(int estadoId)
        {
            if (estadoId <= 0)
                return BadRequest(new { message = "ID de estado inválido" });

            try
            {
                var pedidos = await _pedidoService.GetPedidosByEstadoAsync(estadoId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos por estado {EstadoId}", estadoId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> CreatePedido([FromBody] CrearPedidoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _pedidoService.CreatePedidoAsync(request);
                return CreatedAtAction(nameof(GetPedidoById), new { id = resultado.Id }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pedido para el cliente {ClienteId}", request.ClienteId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult<PedidoResponse>> UpdateEstadoPedido(int id, [FromBody] ActualizarEstadoPedidoRequest request)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _pedidoService.UpdateEstadoPedidoAsync(id, request.EstadoPedidoId);
                if (resultado == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado del pedido {PedidoId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPatch("{id}/pago")]
        public async Task<ActionResult<PedidoResponse>> UpdatePagoPedido(int id, [FromBody] bool pagado)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var resultado = await _pedidoService.UpdatePagoPedidoAsync(id, pagado);
                if (resultado == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado de pago del pedido {PedidoId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var resultado = await _pedidoService.DeletePedidoAsync(id);
                if (!resultado)
                    return NotFound(new { message = "Pedido no encontrado" });

                return Ok(new { message = "Pedido eliminado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar pedido {PedidoId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}