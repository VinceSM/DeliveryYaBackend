using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    // Controllers/MetodoPagoPedidoController.cs
    [ApiController]
    [Route("api/metodos-pago-pedido")]
    public class MetodoPagoPedidoController : ControllerBase
    {
        private readonly IMetodoPagoPedidoService _metodoPagoPedidoService;
        private readonly ILogger<MetodoPagoPedidoController> _logger;

        public MetodoPagoPedidoController(
            IMetodoPagoPedidoService metodoPagoPedidoService,
            ILogger<MetodoPagoPedidoController> logger)
        {
            _metodoPagoPedidoService = metodoPagoPedidoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMetodos()
        {
            try
            {
                var metodos = await _metodoPagoPedidoService.GetAllMetodosAsync();
                return Ok(metodos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener métodos de pago");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMetodoById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var metodo = await _metodoPagoPedidoService.GetMetodoByIdAsync(id);
                if (metodo == null)
                    return NotFound(new { message = "Método de pago no encontrado" });

                return Ok(metodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener método de pago por ID");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("metodo/{metodo}")]
        public async Task<IActionResult> GetMetodoByMetodo(string metodo)
        {
            if (string.IsNullOrEmpty(metodo))
                return BadRequest(new { message = "Método inválido" });

            try
            {
                var metodoPago = await _metodoPagoPedidoService.GetMetodoByMetodoAsync(metodo);
                if (metodoPago == null)
                    return NotFound(new { message = "Método de pago no encontrado" });

                return Ok(metodoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener método de pago por nombre");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMetodo([FromBody] MetodoPagoPedido metodo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _metodoPagoPedidoService.CreateMetodoAsync(metodo);
                return CreatedAtAction(nameof(GetMetodoById), new { id = resultado.idmetodo }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear método de pago");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMetodo(int id, [FromBody] MetodoPagoPedido metodo)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _metodoPagoPedidoService.UpdateMetodoAsync(id, metodo);
                if (resultado == null)
                    return NotFound(new { message = "Método de pago no encontrado" });

                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar método de pago");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodo(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var resultado = await _metodoPagoPedidoService.DeleteMetodoAsync(id);
                if (!resultado)
                    return NotFound(new { message = "Método de pago no encontrado" });

                return Ok(new { message = "Método de pago eliminado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar método de pago");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
