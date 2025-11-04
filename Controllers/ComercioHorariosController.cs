using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComercioHorariosController : ControllerBase
    {
        private readonly IComercioHorariosService _comercioHorariosService;

        public ComercioHorariosController(IComercioHorariosService comercioHorariosService)
        {
            _comercioHorariosService = comercioHorariosService;
        }

        // ✅ Asignar un horario existente a un comercio
        [HttpPost("{comercioId}/add/{horarioId}")]
        public async Task<IActionResult> AddHorarioToComercio(int comercioId, int horarioId)
        {
            var agregado = await _comercioHorariosService.AddHorarioToComercioAsync(comercioId, horarioId);
            if (!agregado)
                return BadRequest(new { mensaje = "No se pudo asignar el horario al comercio." });

            return Ok(new { mensaje = "Horario asignado correctamente al comercio." });
        }

        // ✅ Quitar un horario de un comercio
        [HttpDelete("{comercioId}/remove/{horarioId}")]
        public async Task<IActionResult> RemoveHorarioFromComercio(int comercioId, int horarioId)
        {
            var eliminado = await _comercioHorariosService.RemoveHorarioFromComercioAsync(comercioId, horarioId);
            if (!eliminado)
                return NotFound(new { mensaje = "No se encontró la relación entre comercio y horario." });

            return Ok(new { mensaje = "Horario eliminado del comercio correctamente." });
        }

        // ✅ Obtener todos los horarios de un comercio
        [HttpGet("{comercioId}/list")]
        public async Task<IActionResult> GetHorariosByComercio(int comercioId)
        {
            var horarios = await _comercioHorariosService.GetHorariosByComercioAsync(comercioId);
            if (!horarios.Any())
                return NotFound(new { mensaje = "El comercio no tiene horarios asignados." });

            return Ok(horarios);
        }

        // ✅ Verificar si un comercio está abierto
        [HttpGet("{comercioId}/abierto")]
        public async Task<IActionResult> CheckComercioAbierto(int comercioId)
        {
            var abierto = await _comercioHorariosService.CheckComercioAbiertoAsync(comercioId);
            return Ok(new
            {
                comercioId,
                abierto,
                mensaje = abierto ? "El comercio está abierto actualmente." : "El comercio está cerrado actualmente."
            });
        }

        // ✅ Actualizar un horario de un comercio
        [HttpPut("{comercioId}/update/{horarioId}")]
        public async Task<IActionResult> UpdateHorarioComercio(
            int comercioId,
            int horarioId,
            [FromBody] UpdateHorarioRequest request)
        {
            if (request == null)
                return BadRequest(new { mensaje = "Datos inválidos para la actualización." });

            var actualizado = await _comercioHorariosService.UpdateHorarioComercioAsync(
                comercioId, horarioId, request.Apertura, request.Cierre);

            if (!actualizado)
                return NotFound(new { mensaje = "No se encontró el horario o comercio especificado." });

            return Ok(new { mensaje = "Horario actualizado correctamente." });
        }
    }
}
