using DeliveryYaBackend.DTOs.Requests;
using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaRequest request)
        {
            var nueva = await _categoriaService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriaRequest request)
        {
            var actualizada = await _categoriaService.UpdateAsync(id, request);
            if (actualizada == null) return NotFound();
            return Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _categoriaService.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
