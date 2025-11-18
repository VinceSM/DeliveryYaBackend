// Controllers/EstadoPedidoController.cs
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/estados-pedido")]
public class EstadoPedidoController : ControllerBase
{
    private readonly IEstadoPedidoService _estadoPedidoService;
    private readonly ILogger<EstadoPedidoController> _logger;

    public EstadoPedidoController(
        IEstadoPedidoService estadoPedidoService,
        ILogger<EstadoPedidoController> logger)
    {
        _estadoPedidoService = estadoPedidoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEstados()
    {
        try
        {
            var estados = await _estadoPedidoService.GetAllEstadosAsync();
            return Ok(estados);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estados de pedido");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoById(int id)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID inválido" });

        try
        {
            var estado = await _estadoPedidoService.GetEstadoByIdAsync(id);
            if (estado == null)
                return NotFound(new { message = "Estado de pedido no encontrado" });

            return Ok(estado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado de pedido por ID");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpGet("tipo/{tipo}")]
    public async Task<IActionResult> GetEstadoByTipo(string tipo)
    {
        if (string.IsNullOrEmpty(tipo))
            return BadRequest(new { message = "Tipo inválido" });

        try
        {
            var estado = await _estadoPedidoService.GetEstadoByTipoAsync(tipo);
            if (estado == null)
                return NotFound(new { message = "Estado de pedido no encontrado" });

            return Ok(estado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado de pedido por tipo");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEstado([FromBody] EstadoPedido estado)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var resultado = await _estadoPedidoService.CreateEstadoAsync(estado);
            return CreatedAtAction(nameof(GetEstadoById), new { id = resultado.idestado }, resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear estado de pedido");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] EstadoPedido estado)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID inválido" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var resultado = await _estadoPedidoService.UpdateEstadoAsync(id, estado);
            if (resultado == null)
                return NotFound(new { message = "Estado de pedido no encontrado" });

            return Ok(resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar estado de pedido");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstado(int id)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID inválido" });

        try
        {
            var resultado = await _estadoPedidoService.DeleteEstadoAsync(id);
            if (!resultado)
                return NotFound(new { message = "Estado de pedido no encontrado" });

            return Ok(new { message = "Estado de pedido eliminado correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar estado de pedido");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}