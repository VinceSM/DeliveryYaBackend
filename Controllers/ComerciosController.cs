using DeliveryYaBackend.DTOs.Requests.Comercios;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComerciosController : ControllerBase
    {
        private readonly IComercioService _comercioService;
        private readonly ILogger<ComerciosController> _logger;

        public ComerciosController(IComercioService comercioService, ILogger<ComerciosController> logger)
        {
            _comercioService = comercioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComercios()
        {
            try
            {
                var comercios = await _comercioService.GetAllComerciosAsync();
                return Ok(comercios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercios");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComercioById(int id)
        {
            try
            {
                var comercio = await _comercioService.GetComercioByIdAsync(id);
                if (comercio == null)
                    return NotFound("Comercio no encontrado");

                return Ok(comercio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercio por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("destacados")]
        public async Task<IActionResult> GetComerciosDestacados()
        {
            try
            {
                var comercios = await _comercioService.GetComerciosDestacadosAsync();
                return Ok(comercios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercios destacados");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("ciudad/{ciudad}")]
        public async Task<IActionResult> GetComerciosByCiudad(string ciudad)
        {
            try
            {
                var comercios = await _comercioService.GetComerciosByCiudadAsync(ciudad);
                return Ok(comercios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comercios por ciudad");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComercio([FromBody] ComercioRequest requestC)
        {
            try
            {
                var comercio = new Comercio
                {
                    nombreComercio = requestC.NombreComercio,
                    email = requestC.Email,
                    password = requestC.Password,
                    tipoComercio = requestC.TipoComercio,
                    fotoPortada = requestC.FotoPortada,
                    envio = requestC.Envio,
                    deliveryPropio = requestC.DeliveryPropio,
                    celular = requestC.Celular,
                    ciudad = requestC.Ciudad,
                    calle = requestC.Calle,
                    numero = requestC.Numero,
                    sucursales = requestC.Sucursales,
                    latitud = requestC.Latitud,
                    longitud = requestC.Longitud,
                    encargado = requestC.Encargado,
                    cvu = requestC.Cvu,
                    alias = requestC.Alias,
                    destacado = requestC.Destacado
                };

                var resultado = await _comercioService.CreateComercioAsync(requestC);
                return CreatedAtAction(nameof(GetComercioById), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComercio(int id, [FromBody] UpdateComercioRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de comercio no coincide");

                var comercio = new Comercio
                {
                    idcomercio = request.Id,
                    nombreComercio = request.NombreComercio,
                    tipoComercio = request.TipoComercio,
                    email = request.Email,
                    fotoPortada = request.FotoPortada,
                    envio = request.Envio,
                    deliveryPropio = request.DeliveryPropio,
                    celular = request.Celular,
                    ciudad = request.Ciudad,
                    calle = request.Calle,
                    numero = request.Numero,
                    sucursales = request.Sucursales,
                    latitud = request.Latitud,
                    longitud = request.Longitud,
                    encargado = request.Encargado,
                    cvu = request.Cvu,
                    alias = request.Alias,
                    destacado = request.Destacado
                };

                var resultado = await _comercioService.UpdateComercioAsync(id, request);
                if (resultado == null)
                    return NotFound("Comercio no encontrado");

                return Ok("Comercio actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComercio(int id)
        {
            try
            {
                var resultado = await _comercioService.DeleteComercioAsync(id);
                if (!resultado)
                    return NotFound("Comercio no encontrado");

                return Ok("Comercio eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}/productos")]
        public async Task<IActionResult> GetProductosByComercio(int id)
        {
            try
            {
                var productos = await _comercioService.GetProductosByComercioAsync(id);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos del comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}/categorias")]
        public async Task<IActionResult> GetCategoriasByComercio(int id)
        {
            try
            {
                var categorias = await _comercioService.GetCategoriasByComercioAsync(id);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorías del comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/categorias/{categoriaId}")]
        public async Task<IActionResult> AddCategoriaToComercio(int id, int categoriaId)
        {
            try
            {
                var resultado = await _comercioService.AddCategoriaToComercioAsync(id, categoriaId);
                if (!resultado)
                    return NotFound("Comercio o categoría no encontrados");

                return Ok("Categoría agregada al comercio exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar categoría al comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}