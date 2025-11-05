using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/comercios")]
    public class ComerciosController : ControllerBase
    {
        private readonly IComercioService _comercioService;
        private readonly ILogger<ComerciosController> _logger;

        public ComerciosController(IComercioService comercioService, ILogger<ComerciosController> logger)
        {
            _comercioService = comercioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComercios()
        {
            try
            {
                var comercios = await _comercioService.GetAllComerciosAsync();
                return Ok(comercios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercios");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComercioById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var comercio = await _comercioService.GetComercioByIdAsync(id);
                if (comercio == null)
                    return NotFound(new { message = "Comercio no encontrado" });

                return Ok(comercio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercio por ID");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("destacados")]
        public async Task<IActionResult> GetComerciosDestacados()
        {
            var comercios = await _comercioService.GetComerciosDestacadosAsync();
            return Ok(comercios);
        }

        [HttpGet("ciudad/{ciudad}")]
        public async Task<IActionResult> GetComerciosByCiudad(string ciudad)
        {
            var comercios = await _comercioService.GetComerciosByCiudadAsync(ciudad);
            return Ok(comercios);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComercio([FromBody] ComercioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _comercioService.CreateComercioAsync(request);
                return CreatedAtAction(nameof(GetComercioById), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear comercio");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComercio(int id, [FromBody] UpdateComercioRequest request)
        {
            if (id != request.Id)
                return BadRequest(new { message = "El ID del comercio no coincide con el de la URL" });

            try
            {
                var resultado = await _comercioService.UpdateComercioAsync(id, request);
                if (resultado == null)
                    return NotFound(new { message = "Comercio no encontrado" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar comercio");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComercio(int id)
        {
            try
            {
                var resultado = await _comercioService.DeleteComercioAsync(id);
                if (!resultado)
                    return NotFound(new { message = "Comercio no encontrado" });

                return Ok(new { message = "Comercio eliminado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar comercio");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}/productos")]
        public async Task<IActionResult> GetProductosByComercio(int id)
        {
            var productos = await _comercioService.GetProductosByComercioAsync(id);
            return Ok(productos);
        }

        [HttpGet("panel/{id}")]
        public async Task<ActionResult<ComercioPanelResponse>> GetComercioPanelDetalle(int id)
        {
            var detalle = await _comercioService.GetComercioPanelDetalleAsync(id);
            if (detalle == null)
                return NotFound(new { message = "Comercio no encontrado" });

            return Ok(detalle);
        }
    }
}
