using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComercioHorariosController : ControllerBase
    {
        private readonly IComercioHorariosService _horariosService;

        public ComercioHorariosController(IComercioHorariosService horariosService)
        {
            _horariosService = horariosService;
        }

        [HttpGet("{comercioId}/horarios")]
        public async Task<IActionResult> GetHorarios(int comercioId)
        {
            var horarios = await _horariosService.GetHorariosPorComercioAsync(comercioId);
            if (!horarios.Any())
                return NotFound(new { mensaje = "El comercio no tiene horarios configurados." });

            return Ok(horarios);
        }

        [HttpPost("{comercioId}/horarios/crear")]
        public async Task<IActionResult> CrearHorario(int comercioId, [FromBody] Horarios horario)
        {
            var nuevo = await _horariosService.CrearHorarioParaComercioAsync(comercioId, horario);
            return Ok(new { mensaje = "Horario creado correctamente.", data = nuevo });
        }

        [HttpPut("{comercioId}/horarios/{horarioId}/editar")]
        public async Task<IActionResult> EditarHorario(int comercioId, int horarioId, [FromBody] Horarios horario)
        {
            var actualizado = await _horariosService.ActualizarHorarioAsync(comercioId, horarioId, horario.apertura ?? TimeSpan.Zero, horario.cierre ?? TimeSpan.Zero, horario.abierto);
            if (!actualizado)
                return NotFound(new { mensaje = "No se pudo actualizar el horario." });

            return Ok(new { mensaje = "Horario actualizado correctamente." });
        }

        [HttpDelete("{comercioId}/horarios/{horarioId}/eliminar")]
        public async Task<IActionResult> EliminarHorario(int comercioId, int horarioId)
        {
            var eliminado = await _horariosService.EliminarHorarioAsync(comercioId, horarioId);
            if (!eliminado)
                return NotFound(new { mensaje = "No se encontró el horario." });

            return Ok(new { mensaje = "Horario eliminado correctamente." });
        }
    }
}
