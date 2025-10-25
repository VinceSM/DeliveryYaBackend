using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaProductoController : ControllerBase
    {
        private readonly ICategoriaProductoService _categoriaProductoService;

        public CategoriaProductoController(ICategoriaProductoService categoriaProductoService)
        {
            _categoriaProductoService = categoriaProductoService;
        }

        // ✅ Crear producto dentro de una categoría
        [HttpPost("{idCategoria}/crear")]
        public async Task<IActionResult> CrearProducto([FromRoute] int idCategoria, [FromBody] CreateProductoRequest request)
        {
            try
            {
                var producto = await _categoriaProductoService.CrearProductoAsync(request, idCategoria);
                return Ok(new { mensaje = "Producto creado correctamente", data = producto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Error al crear producto: {ex.Message}" });
            }
        }

        // ✅ Listar productos por categoría
        [HttpGet("{idCategoria}/productos")]
        public async Task<IActionResult> GetProductosPorCategoria([FromRoute] int idCategoria)
        {
            var productos = await _categoriaProductoService.GetProductosPorCategoriaAsync(idCategoria);
            if (!productos.Any())
                return NotFound(new { mensaje = "No se encontraron productos para esta categoría." });

            return Ok(productos);
        }

        // ✅ Buscar productos por nombre
        [HttpGet("buscar")]
        public async Task<IActionResult> GetProductosPorNombre([FromQuery] string nombre)
        {
            var productos = await _categoriaProductoService.GetProductosPorNombreAsync(nombre);
            if (!productos.Any())
                return NotFound(new { mensaje = $"No se encontraron productos con el nombre '{nombre}'." });

            return Ok(productos);
        }

        // ✅ Obtener un producto por su ID
        [HttpGet("producto/{idProducto}")]
        public async Task<IActionResult> GetProductoPorId([FromRoute] int idProducto)
        {
            var producto = await _categoriaProductoService.GetProductoPorIdAsync(idProducto);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(producto);
        }

        // ✅ Actualizar producto
        [HttpPut("producto/{idProducto}/editar")]
        public async Task<IActionResult> ActualizarProducto([FromRoute] int idProducto, [FromBody] UpdateProductoRequest request)
        {
            try
            {
                var actualizado = await _categoriaProductoService.ActualizarProductoAsync(idProducto, request);
                if (actualizado == null)
                    return NotFound(new { mensaje = "Producto no encontrado para actualizar." });

                return Ok(new { mensaje = "Producto actualizado correctamente.", data = actualizado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Error al actualizar producto: {ex.Message}" });
            }
        }

        // ✅ Eliminar producto
        [HttpDelete("producto/{idProducto}/eliminar")]
        public async Task<IActionResult> EliminarProducto([FromRoute] int idProducto)
        {
            var eliminado = await _categoriaProductoService.EliminarProductoAsync(idProducto);
            if (!eliminado)
                return NotFound(new { mensaje = "Producto no encontrado o ya eliminado." });

            return Ok(new { mensaje = "Producto eliminado correctamente." });
        }
    }
}
