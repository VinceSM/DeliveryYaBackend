using DeliveryYaBackend.DTOs.Requests.Auth.Login;
using DeliveryYaBackend.DTOs.Requests.Auth.Register;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/cliente")]
    public class AuthClienteController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AuthClienteController> _logger;

        public AuthClienteController(IUsuarioService usuarioService, ILogger<AuthClienteController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginClienteRequest request)
        {
            try
            {
                var cliente = await _usuarioService.GetClienteByEmailAsync(request.Email);
                if (cliente == null)
                    return Unauthorized("Credenciales inválidas");

                if (cliente.password != request.Password)
                    return Unauthorized("Credenciales inválidas");

                var response = new LoginClienteResponse
                {
                    Token = "generar_jwt_token_here", // Implementar JWT después
                    UserId = cliente.idcliente,
                    ClienteId = cliente.idcliente,
                    NombreCompleto = cliente.nombreCompleto,
                    Email = cliente.email,
                    Celular = cliente.celular,
                    Direccion = $"{cliente.calle} {cliente.numero}, {cliente.ciudad}"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login de cliente");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterClienteRequest request)
        {
            try
            {
                var existe = await _usuarioService.UserExistsAsync(request.Email);
                if (existe)
                    return Conflict("El email ya está registrado");

                var cliente = new Cliente
                {
                    nombreCompleto = request.NombreCompleto,
                    dni = request.Dni,
                    nacimiento = request.Nacimiento,
                    celular = request.Celular,
                    ciudad = request.Ciudad,
                    calle = request.Calle,
                    numero = request.Numero,
                    email = request.Email,
                    password = request.Password // Hashear después
                };

                var resultado = await _usuarioService.CreateClienteAsync(cliente);
                return Ok(new { Message = "Cliente registrado exitosamente", ClienteId = resultado.idcliente });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en registro de cliente");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}