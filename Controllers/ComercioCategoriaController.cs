using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComercioCategoriaController : ControllerBase
    {
        private readonly IComercioCategoriaService _service;

        public ComercioCategoriaController(IComercioCategoriaService service)
        {
            _service = service;
        }

        // ✅ Asignar categoría a comercio
        [HttpPost("{comercioId}/categorias/{categoriaId}/asignar")]
        public async Task<IActionResult> AsignarCategoria(int comercioId, int categoriaId)
        {
            var resultado = await _service.AddCategoriaToComercioAsync(comercioId, categoriaId);
            if (!resultado)
                return BadRequest(new { mensaje = "No se pudo asignar la categoría al comercio." });

            return Ok(new { mensaje = "Categoría asignada correctamente al comercio." });
        }

        // ✅ Quitar categoría de comercio
        [HttpDelete("{comercioId}/categorias/{categoriaId}/eliminar")]
        public async Task<IActionResult> QuitarCategoria(int comercioId, int categoriaId)
        {
            var resultado = await _service.RemoveCategoriaFromComercioAsync(comercioId, categoriaId);
            if (!resultado)
                return NotFound(new { mensaje = "Relación comercio-categoría no encontrada." });

            return Ok(new { mensaje = "Categoría eliminada correctamente del comercio." });
        }

        // ✅ Obtener todas las categorías de un comercio
        [HttpGet("{comercioId}/categorias")]
        public async Task<IActionResult> GetCategoriasPorComercio(int comercioId)
        {
            var categorias = await _service.GetCategoriasByComercioAsync(comercioId);

            if (!categorias.Any())
                return NotFound(new { mensaje = "El comercio no tiene categorías asignadas." });

            return Ok(categorias.Select(c => new
            {
                c.idcategoria,
                c.nombre,
            }));
        }

        // ✅ Obtener todos los comercios asociados a una categoría
        [HttpGet("categorias/{categoriaId}/comercios")]
        public async Task<IActionResult> GetComerciosPorCategoria(int categoriaId)
        {
            var comercios = await _service.GetComerciosByCategoriaAsync(categoriaId);

            if (!comercios.Any())
                return NotFound(new { mensaje = "No se encontraron comercios asociados a esta categoría." });

            return Ok(comercios.Select(c => new
            {
                c.idcomercio,
                c.nombreComercio,
                c.calle,
                c.numero,
                c.envio,
                c.destacado
            }));
        }
    }
}
