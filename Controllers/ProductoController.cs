using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // ✅ Crear producto
        [HttpPost("create")]
        public async Task<ActionResult<ProductoResponse>> CreateAsync([FromBody] CreateProductoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producto = await _productoService.CreateAsync(request);
            return Ok(producto);
        }

        // ✅ Actualizar producto
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ProductoResponse>> UpdateAsync(int id, [FromBody] UpdateProductoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.idproducto = id;

            var productoActualizado = await _productoService.UpdateAsync(request);
            if (productoActualizado == null)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(productoActualizado);
        }

        // ✅ Eliminar producto
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var eliminado = await _productoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(new { message = "Producto eliminado correctamente." });
        }

        // ✅ Obtener todos los productos
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetAllAsync()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        // ✅ Obtener productos por categoría
        [HttpGet("byCategoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _productoService.GetByCategoriaAsync(categoriaId);
            return Ok(productos);
        }

        // ✅ Buscar productos por nombre
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> SearchAsync([FromQuery] string nombre)
        {
            var productos = await _productoService.SearchByNameAsync(nombre);
            return Ok(productos);
        }

        // ✅ Obtener producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> GetByIdAsync(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(producto);
        }
    }
}
