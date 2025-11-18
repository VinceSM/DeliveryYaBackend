using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    // Controllers/ItemPedidoController.cs
    [ApiController]
    [Route("api/item-pedidos")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItemPedidoService _itemPedidoService;
        private readonly ILogger<ItemPedidoController> _logger;

        public ItemPedidoController(
            IItemPedidoService itemPedidoService,
            ILogger<ItemPedidoController> logger)
        {
            _itemPedidoService = itemPedidoService;
            _logger = logger;
        }

        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> GetItemsByPedido(int pedidoId)
        {
            if (pedidoId <= 0)
                return BadRequest(new { message = "ID de pedido inválido" });

            try
            {
                var items = await _itemPedidoService.GetItemsByPedidoIdAsync(pedidoId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener items del pedido {PedidoId}", pedidoId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("comercio/{comercioId}")]
        public async Task<IActionResult> GetItemsByComercio(int comercioId)
        {
            if (comercioId <= 0)
                return BadRequest(new { message = "ID de comercio inválido" });

            try
            {
                var items = await _itemPedidoService.GetItemsByComercioIdAsync(comercioId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener items del comercio {ComercioId}", comercioId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("producto/{productoId}")]
        public async Task<IActionResult> GetItemsByProducto(int productoId)
        {
            if (productoId <= 0)
                return BadRequest(new { message = "ID de producto inválido" });

            try
            {
                var items = await _itemPedidoService.GetItemsByProductoIdAsync(productoId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener items del producto {ProductoId}", productoId);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID inválido" });

            try
            {
                var item = await _itemPedidoService.GetItemByIdAsync(id);
                if (item == null)
                    return NotFound(new { message = "Item de pedido no encontrado" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener item de pedido por ID {ItemId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
