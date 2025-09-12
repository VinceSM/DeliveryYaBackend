using DeliveryYaBackend.DTOs.Responses.Productos;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockService stockService, ILogger<StockController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet("producto/{productoId}")]
        public async Task<IActionResult> GetStockByProductoId(int productoId)
        {
            try
            {
                var stock = await _stockService.GetStockByProductoIdAsync(productoId);
                if (stock == null)
                    return NotFound("Stock no encontrado para el producto");

                return Ok(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener stock por producto ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("bajos")]
        public async Task<IActionResult> GetStocksBajos([FromQuery] int nivelMinimo = 10)
        {
            try
            {
                var stocks = await _stockService.GetStocksBajosAsync(nivelMinimo);
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener stocks bajos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("agotados")]
        public async Task<IActionResult> GetStocksAgotados()
        {
            try
            {
                var stocks = await _stockService.GetStocksAgotadosAsync();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener stocks agotados");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("producto/{productoId}")]
        public async Task<IActionResult> UpdateStockProducto(int productoId, [FromBody] int nuevaCantidad)
        {
            try
            {
                var resultado = await _stockService.ActualizarStockPorProductoAsync(productoId, nuevaCantidad);
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

        [HttpPut("producto/{productoId}/incrementar")]
        public async Task<IActionResult> IncrementarStockProducto(int productoId, [FromBody] int cantidad)
        {
            try
            {
                var resultado = await _stockService.IncrementarStockPorProductoAsync(productoId, cantidad);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok("Stock incrementado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al incrementar stock del producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("producto/{productoId}/decrementar")]
        public async Task<IActionResult> DecrementarStockProducto(int productoId, [FromBody] int cantidad)
        {
            try
            {
                var resultado = await _stockService.DecrementarStockPorProductoAsync(productoId, cantidad);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok("Stock decrementado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al decrementar stock del producto");
                return StatusCode(500, ex.Message); // Retornar mensaje de error específico
            }
        }

        [HttpPut("producto/{productoId}/ilimitado")]
        public async Task<IActionResult> SetStockIlimitado(int productoId, [FromBody] bool ilimitado)
        {
            try
            {
                var resultado = await _stockService.SetStockIlimitadoAsync(productoId, ilimitado);
                if (!resultado)
                    return NotFound("Producto no encontrado");

                return Ok(ilimitado ? "Stock configurado como ilimitado" : "Stock configurado como limitado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al configurar stock ilimitado");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("estadisticas")]
        public async Task<IActionResult> GetEstadisticasStock()
        {
            try
            {
                var totalConStock = await _stockService.GetTotalProductosConStockAsync();
                var totalSinStock = await _stockService.GetTotalProductosSinStockAsync();

                return Ok(new
                {
                    TotalProductosConStock = totalConStock,
                    TotalProductosSinStock = totalSinStock,
                    TotalProductos = totalConStock + totalSinStock
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas de stock");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("producto/{productoId}/suficiente")]
        public async Task<IActionResult> VerificarStockSuficiente(int productoId, [FromQuery] int cantidadRequerida)
        {
            try
            {
                var suficiente = await _stockService.VerificarStockSuficienteAsync(productoId, cantidadRequerida);
                return Ok(new { Suficiente = suficiente });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar stock suficiente");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}