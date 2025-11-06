using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/comercios")]
    public class ComercioCategoriaController : ControllerBase
    {
        private readonly IComercioCategoriaService _service;
        private readonly IComercioService _comercioService; // 🔹 validaciones
        private readonly ICategoriaService _categoriaService;

        public ComercioCategoriaController(
            IComercioCategoriaService service,
            IComercioService comercioService,
            ICategoriaService categoriaService)
        {
            _service = service;
            _comercioService = comercioService;
            _categoriaService = categoriaService;
        }

        // ✅ Asignar categoría a comercio
        [HttpPost("{comercioId}/categorias/{categoriaId}")]
        public async Task<IActionResult> AsignarCategoria(int comercioId, int categoriaId)
        {
            if (!await _comercioService.ExistsAsync(comercioId))
                return NotFound(new { mensaje = "El comercio especificado no existe." });

            if (!await _categoriaService.ExistsAsync(categoriaId))
                return NotFound(new { mensaje = "La categoría especificada no existe." });

            var resultado = await _service.AddCategoriaToComercioAsync(comercioId, categoriaId);
            if (!resultado)
                return BadRequest(new { mensaje = "La categoría ya está asignada a este comercio." });

            return Ok(new { mensaje = "Categoría asignada correctamente al comercio." });
        }

        // ✅ Quitar categoría de comercio
        [HttpDelete("{comercioId}/categorias/{categoriaId}")]
        public async Task<IActionResult> QuitarCategoria(int comercioId, int categoriaId)
        {
            var resultado = await _service.RemoveCategoriaFromComercioAsync(comercioId, categoriaId);
            if (!resultado)
                return NotFound(new { mensaje = "No existe una relación entre este comercio y la categoría." });

            return Ok(new { mensaje = "Categoría eliminada correctamente del comercio." });
        }

        // ✅ Obtener todas las categorías de un comercio
        [HttpGet("{comercioId}/categorias")]
        public async Task<IActionResult> GetCategoriasPorComercio(int comercioId)
        {
            var categorias = await _service.GetCategoriasByComercioAsync(comercioId);

            if (!categorias.Any())
                return NotFound(new { mensaje = "El comercio no tiene categorías asignadas." });

            var data = categorias.Select(c => new CategoriaResponse
            {
                Id = c.idcategoria,
                Nombre = c.nombre
            });

            return Ok(new { data });
        }

        // ✅ Obtener todos los comercios asociados a una categoría
        [HttpGet("/api/categorias/{categoriaId}/comercios")]
        public async Task<IActionResult> GetComerciosPorCategoria(int categoriaId)
        {
            var comercios = await _service.GetComerciosByCategoriaAsync(categoriaId);

            if (!comercios.Any())
                return NotFound(new { mensaje = "No se encontraron comercios asociados a esta categoría." });

            var data = comercios.Select(c => new ComercioResponse
            {
                Id = c.idcomercio,
                NombreComercio = c.nombreComercio,
                Calle = c.calle,
                Numero = c.numero,
                Envio = c.envio,
                Destacado = c.destacado
            });

            return Ok(new { data });
        }
    }
}
