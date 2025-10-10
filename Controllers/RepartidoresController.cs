using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepartidoresController : ControllerBase
    {
        private readonly IRepartidorService _repartidorService;

        public RepartidoresController(IRepartidorService repartidorService)
        {
            _repartidorService = repartidorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepartidorResponse>>> GetAll()
        {
            return Ok(await _repartidorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RepartidorResponse>> GetById(int id)
        {
            var repartidor = await _repartidorService.GetByIdAsync(id);
            if (repartidor == null) return NotFound();
            return Ok(repartidor);
        }

        [HttpGet("libres")]
        public async Task<ActionResult<IEnumerable<RepartidorResponse>>> GetLibres()
        {
            return Ok(await _repartidorService.GetLibresAsync());
        }

        [HttpPost]
        public async Task<ActionResult<RepartidorResponse>> Create([FromBody] RepartidorRequest request)
        {
            var created = await _repartidorService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RepartidorResponse>> Update(int id, [FromBody] RepartidorRequest request)
        {
            var updated = await _repartidorService.UpdateAsync(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repartidorService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
