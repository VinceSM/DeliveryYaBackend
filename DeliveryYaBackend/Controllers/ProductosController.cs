using DeliveryYaBackend.DTOs.Requests.Productos;
using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductoService productoService, ILogger<ProductosController> logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            try
            {
                var productos = await _productoService.GetAllProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            try
            {
                var producto = await _productoService.GetProductoByIdAsync(id);
                if (producto == null)
                    return NotFound("Producto no encontrado");

                return Ok(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetProductosByCategoria(int categoriaId)
        {
            try
            {
                var productos = await _productoService.GetProductosByCategoriaAsync(categoriaId);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos por categoría");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> GetProductosByNombre(string nombre)
        {
            try
            {
                var productos = await _productoService.GetProductosByNombreAsync(nombre);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos por nombre");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("oferta")]
        public async Task<IActionResult> GetProductosEnOferta()
        {
            try
            {
                var productos = await _productoService.GetProductosEnOfertaAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos en oferta");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] CreateProductoRequest request)
        {
            try
            {
                // Primero crear el stock
                var stock = new Stock
                {
                    stock = request.Stock,
                    stockIlimitado = request.StockIlimitado,
                    medida = request.StockMedida
                };

                // Luego crear el producto
                var producto = new Producto
                {
                    nombre = request.Nombre,
                    fotoPortada = request.FotoPortada,
                    descripcion = request.Descripcion,
                    unidadMedida = request.UnidadMedida,
                    precioUnitario = request.PrecioUnitario,
                    oferta = request.Oferta
                };

                var resultado = await _productoService.CreateProductoAsync(producto, stock);

                // Agregar categorías si se especificaron
                if (request.CategoriaIds != null && request.CategoriaIds.Any())
                {
                    foreach (var categoriaId in request.CategoriaIds)
                    {
                        await _productoService.AddCategoriaToProductoAsync(resultado.idproducto, categoriaId);
                    }
                }

                return CreatedAtAction(nameof(GetProductoById), new { id = resultado.idproducto }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] UpdateProductoRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de producto no coincide");

                var producto = new Producto
                {
                    idproducto = request.Id,
                    nombre = request.Nombre,
                    fotoPortada = request.FotoPortada,
                    descripcion = request.Descripcion,
                    unidadMedida = request.UnidadMedida,
                    precioUnitario = request.PrecioUnitario,
                    oferta = request.Oferta
                };

                var resultado = await _productoService.UpdateProductoAsync(producto);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                // Actualizar stock si se proporcionó
                if (request.Stock.HasValue || request.StockMedida != null)
                {
                    await _productoService.UpdateProductoStockAsync(id, request.Stock ?? 0);
                }

                return Ok("Producto actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var resultado = await _productoService.DeleteProductoAsync(id);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok("Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStockProducto(int id, [FromBody] int nuevaCantidad)
        {
            try
            {
                var resultado = await _productoService.UpdateProductoStockAsync(id, nuevaCantidad);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok("Stock actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar stock del producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}/precio")]
        public async Task<IActionResult> UpdatePrecioProducto(int id, [FromBody] decimal nuevoPrecio)
        {
            try
            {
                var resultado = await _productoService.UpdateProductoPrecioAsync(id, nuevoPrecio);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok("Precio actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar precio del producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}/stock")]
        public async Task<IActionResult> GetStockProducto(int id)
        {
            try
            {
                var stock = await _productoService.GetStockDisponibleAsync(id);
                return Ok(new { StockDisponible = stock });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener stock del producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}