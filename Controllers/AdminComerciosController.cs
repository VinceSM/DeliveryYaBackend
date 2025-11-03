using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/admin/comercios")]
    public class AdminComerciosController : ControllerBase
    {
        private readonly IAdminComercioService _acservice;

        public AdminComerciosController(IAdminComercioService service)
        {
            _acservice = service;
        }

        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<ComercioResponse>>> GetPendientes()
        {
            return Ok(await _acservice.GetPendientesAsync());
        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<ComercioResponse>>> GetActivos()
        {
            return Ok(await _acservice.GetActivosAsync());
        }

        [HttpPut("{id}/aprobar")]
        public async Task<ActionResult<ComercioResponse>> Aprobar(int id)
        {
            var result = await _acservice.AprobarComercioAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}/destacar")]
        public async Task<ActionResult<ComercioResponse>> Destacar(int id, [FromQuery] bool destacado)
        {
            var result = await _acservice.DestacarComercioAsync(id, destacado);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}/detalle")]
        public async Task<ActionResult<ComercioDetalleResponse>> GetDetalle(int id)
        {
            var comercio = await _acservice.GetDetalleAsync(id);
            if (comercio == null) return NotFound();
            return Ok(comercio);
        }

    }
}
