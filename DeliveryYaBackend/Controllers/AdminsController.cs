using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminResponse>>> GetAll()
        {
            return Ok(await _adminService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminResponse>> GetById(int id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null) return NotFound();
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult<AdminResponse>> Create([FromBody] AdminRequest request)
        {
            var created = await _adminService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminResponse>> Update(int id, [FromBody] AdminRequest request)
        {
            var updated = await _adminService.UpdateAsync(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _adminService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
