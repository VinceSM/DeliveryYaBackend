using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponse>>> GetAll()
        {
            return Ok(await _clienteService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponse>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> Create([FromBody] ClienteRequest request)
        {
            var created = await _clienteService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResponse>> Update(int id, [FromBody] ClienteRequest request)
        {
            var updated = await _clienteService.UpdateAsync(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clienteService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
