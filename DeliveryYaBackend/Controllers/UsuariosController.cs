using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioService usuarioService, ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetAllClientes()
        {
            try
            {
                var clientes = await _usuarioService.GetAllClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("clientes/{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            try
            {
                var cliente = await _usuarioService.GetClienteByIdAsync(id);
                if (cliente == null)
                    return NotFound("Cliente no encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("repartidores")]
        public async Task<IActionResult> GetAllRepartidores()
        {
            try
            {
                var repartidores = await _usuarioService.GetAllRepartidoresAsync();
                return Ok(repartidores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener repartidores");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("repartidores/{id}")]
        public async Task<IActionResult> GetRepartidorById(int id)
        {
            try
            {
                var repartidor = await _usuarioService.GetRepartidorByIdAsync(id);
                if (repartidor == null)
                    return NotFound("Repartidor no encontrado");

                return Ok(repartidor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener repartidor por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("repartidores/libres")]
        public async Task<IActionResult> GetRepartidoresLibres()
        {
            try
            {
                var repartidores = await _usuarioService.GetRepartidoresLibresAsync();
                return Ok(repartidores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener repartidores libres");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("clientes/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] UpdateClienteRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de cliente no coincide");

                var cliente = new Cliente
                {
                    idcliente = request.Id,
                    nombreCompleto = request.NombreCompleto,
                    dni = request.Dni,
                    nacimiento = request.Nacimiento,
                    celular = request.Celular,
                    ciudad = request.Ciudad,
                    calle = request.Calle,
                    numero = request.Numero,
                    email = request.Email
                };

                var resultado = await _usuarioService.UpdateClienteAsync(cliente);
                if (!resultado)
                    return NotFound("Cliente no encontrado");

                return Ok("Cliente actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cliente");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("repartidores/{id}")]
        [HttpPut("repartidores/{id}")]
        public async Task<IActionResult> UpdateRepartidor(int id, [FromBody] UpdateRepartidorRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest("ID de repartidor no coincide");

                // Primero obtener el repartidor existente
                var repartidorExistente = await _usuarioService.GetRepartidorByIdAsync(id);
                if (repartidorExistente == null)
                    return NotFound("Repartidor no encontrado");

                // Actualizar solo los campos básicos del repartidor
                repartidorExistente.nombreCompleto = request.NombreCompleto;
                repartidorExistente.dni = request.Dni;
                repartidorExistente.nacimiento = request.Nacimiento;
                repartidorExistente.celular = request.Celular;
                repartidorExistente.ciudad = request.Ciudad;
                repartidorExistente.calle = request.Calle;
                repartidorExistente.numero = request.Numero;
                repartidorExistente.email = request.Email;
                repartidorExistente.cvu = request.Cvu;
                repartidorExistente.libreRepartidor = request.LibreRepartidor;

                // Actualizar el vehículo (si existe)
                if (repartidorExistente.Vehiculo != null)
                {
                    repartidorExistente.Vehiculo.tipo = request.TipoVehiculo;
                    repartidorExistente.Vehiculo.patente = request.Patente;
                    repartidorExistente.Vehiculo.modelo = request.Modelo;
                    repartidorExistente.Vehiculo.marca = request.Marca;
                    repartidorExistente.Vehiculo.seguro = request.Seguro;
                    repartidorExistente.Vehiculo.companiaSeguros = request.CompaniaSeguros;
                }

                var resultado = await _usuarioService.UpdateRepartidorAsync(repartidorExistente);
                if (!resultado)
                    return NotFound("Repartidor no encontrado");

                return Ok("Repartidor actualizado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar repartidor");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("repartidores/{id}/disponibilidad")]
        public async Task<IActionResult> UpdateDisponibilidadRepartidor(int id, [FromBody] bool disponible)
        {
            try
            {
                var resultado = await _usuarioService.UpdateDisponibilidadRepartidorAsync(id, disponible);
                if (!resultado)
                    return NotFound("Repartidor no encontrado");

                return Ok(disponible ? "Repartidor marcado como disponible" : "Repartidor marcado como no disponible");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar disponibilidad de repartidor");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("clientes/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var resultado = await _usuarioService.DeleteClienteAsync(id);
                if (!resultado)
                    return NotFound("Cliente no encontrado");

                return Ok("Cliente eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar cliente");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("repartidores/{id}")]
        public async Task<IActionResult> DeleteRepartidor(int id)
        {
            try
            {
                var resultado = await _usuarioService.DeleteRepartidorAsync(id);
                if (!resultado)
                    return NotFound("Repartidor no encontrado");

                return Ok("Repartidor eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar repartidor");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("clientes/email/{email}")]
        public async Task<IActionResult> GetClienteByEmail(string email)
        {
            try
            {
                var cliente = await _usuarioService.GetClienteByEmailAsync(email);
                if (cliente == null)
                    return NotFound("Cliente no encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente por email");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("repartidores/email/{email}")]
        public async Task<IActionResult> GetRepartidorByEmail(string email)
        {
            try
            {
                var repartidor = await _usuarioService.GetRepartidorByEmailAsync(email);
                if (repartidor == null)
                    return NotFound("Repartidor no encontrado");

                return Ok(repartidor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener repartidor por email");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}