using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/admin/comercios")]
    public class AdminComerciosController : ControllerBase
    {
        private readonly IAdminComercioService _acservice;
        private readonly ILogger<AdminComerciosController> _logger;

        public AdminComerciosController(IAdminComercioService service, ILogger<AdminComerciosController> logger)
        {
            _acservice = service;
            _logger = logger;
        }

        [HttpGet("pendientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ComercioResponse>>> GetPendientes()
        {
            var result = await _acservice.GetPendientesAsync();
            return Ok(result);
        }

        [HttpGet("activos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ComercioResponse>>> GetActivos()
        {
            var result = await _acservice.GetActivosAsync();
            return Ok(result);
        }

        [HttpPut("{id}/aprobar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComercioResponse>> Aprobar(int id)
        {
            if (id <= 0) return BadRequest(new { message = "ID inválido." });

            try
            {
                var result = await _acservice.AprobarComercioAsync(id);
                if (result == null) return NotFound(new { message = "Comercio no encontrado." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar comercio");
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }

        [HttpPut("{id}/destacar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComercioResponse>> Destacar(int id, [FromQuery] bool destacado)
        {
            if (id <= 0) return BadRequest(new { message = "ID inválido." });

            try
            {
                var result = await _acservice.DestacarComercioAsync(id, destacado);
                if (result == null) return NotFound(new { message = "Comercio no encontrado." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al destacar comercio");
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }

        [HttpGet("{id}/detalle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComercioDetalleResponse>> GetDetalle(int id)
        {
            if (id <= 0) return BadRequest(new { message = "ID inválido." });

            var comercio = await _acservice.GetDetalleAsync(id);
            if (comercio == null)
                return NotFound(new { message = "Comercio no encontrado." });

            return Ok(comercio);
        }
    }
}
