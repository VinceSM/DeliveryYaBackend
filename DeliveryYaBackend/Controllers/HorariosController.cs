using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;
        private readonly ILogger<HorariosController> _logger;

        public HorariosController(IHorarioService horarioService, ILogger<HorariosController> logger)
        {
            _horarioService = horarioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHorarios()
        {
            try
            {
                var horarios = await _horarioService.GetAllHorariosAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horarios");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHorarioById(int id)
        {
            try
            {
                var horario = await _horarioService.GetHorarioByIdAsync(id);
                if (horario == null)
                    return NotFound("Horario no encontrado");

                return Ok(horario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horario por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("comercio/{comercioId}")]
        public async Task<IActionResult> GetHorariosByComercio(int comercioId)
        {
            try
            {
                var horarios = await _horarioService.GetHorariosByComercioAsync(comercioId);
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener horarios por comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("comercio/{comercioId}/abierto")]
        public async Task<IActionResult> CheckComercioAbierto(int comercioId)
        {
            try
            {
                var abierto = await _horarioService.CheckComercioAbiertoAsync(comercioId);
                return Ok(new { Abierto = abierto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si comercio está abierto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateHorario([FromBody] CreateHorarioRequest request)
        {
            try
            {
                var horario = new Horarios
                {
                    apertura = request.Apertura,
                    cierre = request.Cierre,
                    dias = request.Dias,
                    abierto = request.Abierto
                };

                var resultado = await _horarioService.CreateHorarioAsync(horario);
                return CreatedAtAction(nameof(GetHorarioById), new { id = resultado.idhorarios }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear horario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario(int id, [FromBody] UpdateHorarioRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de horario no coincide");

                var horario = new Horarios
                {
                    idhorarios = request.Id,
                    apertura = request.Apertura,
                    cierre = request.Cierre,
                    dias = request.Dias,
                    abierto = request.Abierto
                };

                var resultado = await _horarioService.UpdateHorarioAsync(horario);
                if (!resultado)
                    return NotFound("Horario no encontrado");

                return Ok("Horario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar horario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            try
            {
                var resultado = await _horarioService.DeleteHorarioAsync(id);
                if (!resultado)
                    return NotFound("Horario no encontrado");

                return Ok("Horario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar horario");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("comercio/{comercioId}/horario/{horarioId}")]
        public async Task<IActionResult> AddHorarioToComercio(int comercioId, int horarioId)
        {
            try
            {
                var resultado = await _horarioService.AddHorarioToComercioAsync(comercioId, horarioId);
                if (!resultado)
                    return NotFound("Comercio o horario no encontrados");

                return Ok("Horario agregado al comercio exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar horario al comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("comercio/{comercioId}/horario/{horarioId}")]
        public async Task<IActionResult> UpdateHorarioComercio(int comercioId, int horarioId,
            [FromBody] TimeSpan apertura, [FromBody] TimeSpan cierre)
        {
            try
            {
                var resultado = await _horarioService.UpdateHorarioComercioAsync(comercioId, horarioId, apertura, cierre);
                if (!resultado)
                    return NotFound("Comercio o horario no encontrados");

                return Ok("Horario de comercio actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar horario de comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}