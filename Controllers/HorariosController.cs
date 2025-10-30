using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorariosController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        // ================================================
        // 🔹 CRUD DE HORARIOS
        // ================================================

        [HttpPost]
        public async Task<ActionResult<HorarioResponse>> CreateHorario([FromBody] CreateHorarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var horario = await _horarioService.CreateHorarioAsync(request);
            return CreatedAtAction(nameof(GetHorarioById), new { id = horario.IdHorario }, horario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioResponse>> GetHorarioById(int id)
        {
            var horario = await _horarioService.GetHorarioByIdAsync(id);
            if (horario == null)
                return NotFound(new { message = $"No se encontró el horario con ID {id}" });

            return Ok(horario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioResponse>>> GetAllHorarios()
        {
            var horarios = await _horarioService.GetAllHorariosAsync();
            return Ok(horarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario(int id, [FromBody] UpdateHorarioRequest request)
        {
            if (id != request.IdHorario)
                return BadRequest(new { message = "El ID del horario no coincide con el parámetro." });

            var result = await _horarioService.UpdateHorarioAsync(request);
            if (!result)
                return NotFound(new { message = $"No se pudo actualizar el horario con ID {id}" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var result = await _horarioService.DeleteHorarioAsync(id);
            if (!result)
                return NotFound(new { message = $"No se encontró el horario con ID {id}" });

            return NoContent();
        }

        // ================================================
        // 🔹 GESTIÓN DE HORARIOS POR COMERCIO
        // ================================================

        [HttpPost("comercio/{comercioId}")]
        public async Task<ActionResult<HorarioResponse>> CreateAndAssignHorario(
            int comercioId,
            [FromBody] CreateHorarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var horario = await _horarioService.CreateAndAssignHorarioAsync(comercioId, request);
            return CreatedAtAction(nameof(GetHorarioById), new { id = horario.IdHorario }, horario);
        }

        [HttpGet("comercio/{comercioId}")]
        public async Task<ActionResult<IEnumerable<HorarioResponse>>> GetHorariosByComercio(int comercioId)
        {
            var horarios = await _horarioService.GetHorariosByComercioAsync(comercioId);
            return Ok(horarios);
        }

        [HttpDelete("comercio/{comercioId}/horario/{horarioId}")]
        public async Task<IActionResult> RemoveHorarioFromComercio(int comercioId, int horarioId)
        {
            var result = await _horarioService.RemoveHorarioFromComercioAsync(comercioId, horarioId);
            if (!result)
                return NotFound(new { message = "La relación entre comercio y horario no existe." });

            return NoContent();
        }

        [HttpPut("comercio/{comercioId}/horario/{horarioId}")]
        public async Task<IActionResult> UpdateHorarioComercio(
            int comercioId,
            int horarioId,
            [FromBody] UpdateHorarioRequest request)
        {
            // Removed the incorrect HasValue checks since TimeSpan is a non-nullable value type.
            if (request.Apertura == default || request.Cierre == default)
                return BadRequest(new { message = "Los campos apertura y cierre son obligatorios." });

            var result = await _horarioService.UpdateHorarioComercioAsync(
                comercioId,
                horarioId,
                request.Apertura,
                request.Cierre
            );

            if (!result)
                return NotFound(new { message = $"No se pudo actualizar el horario {horarioId} para el comercio {comercioId}." });

            return NoContent();
        }
    }
}
