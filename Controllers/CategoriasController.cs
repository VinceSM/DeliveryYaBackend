using DeliveryYaBackend.DTOs.Requests.Categorias;
using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaService categoriaService, ILogger<CategoriasController> logger)
        {
            _categoriaService = categoriaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            try
            {
                var categorias = await _categoriaService.GetAllCategoriasAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorías");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            try
            {
                var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
                if (categoria == null)
                    return NotFound("Categoría no encontrada");

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categoría por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CreateCategoriaRequest request)
        {
            try
            {
                var categoria = new Categoria { nombre = request.Nombre };
                var resultado = await _categoriaService.CreateCategoriaAsync(categoria);
                return CreatedAtAction(nameof(GetCategoriaById), new { id = resultado.idcategoria }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear categoría");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] UpdateCategoriaRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de categoría no coincide");

                var categoria = new Categoria
                {
                    idcategoria = request.Id,
                    nombre = request.Nombre
                };

                var resultado = await _categoriaService.UpdateCategoriaAsync(categoria);
                if (!resultado)
                    return NotFound("Categoría no encontrada");

                return Ok("Categoría actualizada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar categoría");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var resultado = await _categoriaService.DeleteCategoriaAsync(id);
                if (!resultado)
                    return NotFound("Categoría no encontrada");

                return Ok("Categoría eliminada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar categoría");
                return StatusCode(500, ex.Message); // Retornar el mensaje de error específico
            }
        }

        [HttpGet("{id}/productos")]
        public async Task<IActionResult> GetProductosByCategoria(int id)
        {
            try
            {
                var productos = await _categoriaService.GetProductosByCategoriaAsync(id);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos por categoría");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}/comercios")]
        public async Task<IActionResult> GetComerciosByCategoria(int id)
        {
            try
            {
                var comercios = await _categoriaService.GetComerciosByCategoriaAsync(id);
                return Ok(comercios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercios por categoría");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}