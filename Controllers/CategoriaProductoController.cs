using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaProductoController : ControllerBase
    {
        private readonly ICategoriaProductoService _categoriaProductoService;
        private readonly ICategoriaService _categoriaService; // 🔹 validación de categoría existente

        public CategoriaProductoController(
            ICategoriaProductoService categoriaProductoService,
            ICategoriaService categoriaService)
        {
            _categoriaProductoService = categoriaProductoService;
            _categoriaService = categoriaService;
        }

        // 🔹 Crear producto dentro de una categoría
        [HttpPost("{idCategoria}/productos")]
        public async Task<IActionResult> CrearProducto(int idCategoria, [FromBody] CreateProductoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Datos inválidos.", errores = ModelState });

            var categoriaExiste = await _categoriaService.ExistsAsync(idCategoria);
            if (!categoriaExiste)
                return NotFound(new { mensaje = "La categoría especificada no existe." });

            var producto = await _categoriaProductoService.CrearProductoAsync(request, idCategoria);
            return Ok(new { mensaje = "Producto creado correctamente.", data = producto });
        }

        // 🔹 Listar productos por categoría
        [HttpGet("{idCategoria}/productos")]
        public async Task<IActionResult> GetProductosPorCategoria(int idCategoria)
        {
            var productos = await _categoriaProductoService.GetProductosPorCategoriaAsync(idCategoria);
            if (!productos.Any())
                return NotFound(new { mensaje = "No se encontraron productos para esta categoría." });

            return Ok(new { data = productos });
        }

        // 🔹 Buscar productos por nombre
        [HttpGet("productos/buscar")]
        public async Task<IActionResult> GetProductosPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { mensaje = "Debe especificar un nombre de producto para la búsqueda." });

            var productos = await _categoriaProductoService.GetProductosPorNombreAsync(nombre);
            if (!productos.Any())
                return NotFound(new { mensaje = $"No se encontraron productos con el nombre '{nombre}'." });

            return Ok(new { data = productos });
        }

        // 🔹 Obtener producto por ID
        [HttpGet("productos/{idProducto}")]
        public async Task<IActionResult> GetProductoPorId(int idProducto)
        {
            var producto = await _categoriaProductoService.GetProductoPorIdAsync(idProducto);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(new { data = producto });
        }

        // 🔹 Actualizar producto
        [HttpPut("productos/{idProducto}")]
        public async Task<IActionResult> ActualizarProducto(int idProducto, [FromBody] UpdateProductoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Datos inválidos.", errores = ModelState });

            var actualizado = await _categoriaProductoService.ActualizarProductoAsync(idProducto, request);
            if (actualizado == null)
                return NotFound(new { mensaje = "Producto no encontrado para actualizar." });

            return Ok(new { mensaje = "Producto actualizado correctamente.", data = actualizado });
        }

        // 🔹 Eliminar producto
        [HttpDelete("productos/{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            var eliminado = await _categoriaProductoService.EliminarProductoAsync(idProducto);
            if (!eliminado)
                return NotFound(new { mensaje = "Producto no encontrado o ya eliminado." });

            return Ok(new { mensaje = "Producto eliminado correctamente." });
        }
    }
}
