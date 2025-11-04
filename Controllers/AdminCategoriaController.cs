using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/categorias")]
    public class AdminCategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IProductoService _productoService;

        public AdminCategoriaController(
            ICategoriaService categoriaService,
            IProductoService productoService
        )
        {
            _categoriaService = categoriaService;
            _productoService = productoService;
        }

        // ✅ Crear una nueva categoría
        [HttpPost]
        public async Task<ActionResult<CategoriaResponse>> CreateAsync([FromBody] CreateCategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaCategoria = await _categoriaService.CreateAsync(request);
            // Devuelve 201 Created con header Location correcto
            return CreatedAtAction(nameof(GetByIdAsync), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        // ✏️ Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaResponse>> UpdateAsync(int id, [FromBody] UpdateCategoriaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaActualizada = await _categoriaService.UpdateAsync(id, request);
            if (categoriaActualizada == null)
                return NotFound(new { message = "Categoría no encontrada." });

            return Ok(categoriaActualizada);
        }

        // 🗑️ Eliminar (borrado lógico)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var eliminado = await _categoriaService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Categoría no encontrada o ya eliminada." });

            return Ok(new { message = "Categoría eliminada correctamente." });
        }

        // 📜 Listar todas las categorías activas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetAllAsync()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        // 📄 Obtener una categoría por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponse>> GetByIdAsync(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            return Ok(categoria);
        }

        // 🛍️ Obtener todos los productos de una categoría
        [HttpGet("{id}/productos")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductosPorCategoriaAsync(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            var productos = await _productoService.GetByCategoriaAsync(id);
            return Ok(productos);
        }
    }
}
