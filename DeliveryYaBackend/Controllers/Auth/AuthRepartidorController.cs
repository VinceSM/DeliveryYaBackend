using DeliveryYaBackend.DTOs.Requests.Auth.Login;
using DeliveryYaBackend.DTOs.Requests.Auth.Register;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/repartidor")]
    public class AuthRepartidorController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AuthRepartidorController> _logger;

        public AuthRepartidorController(IUsuarioService usuarioService, ILogger<AuthRepartidorController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRepartidorRequest request)
        {
            try
            {
                var repartidor = await _usuarioService.GetRepartidorByEmailAsync(request.Email);
                if (repartidor == null)
                    return Unauthorized("Credenciales inválidas");

                if (repartidor.password != request.Password)
                    return Unauthorized("Credenciales inválidas");

                var repartidorCompleto = await _usuarioService.GetRepartidorByIdAsync(repartidor.idrepartidor);

                var response = new LoginRepartidorResponse
                {
                    Token = "generar_jwt_token_here",
                    UserId = repartidor.idrepartidor,
                    RepartidorId = repartidor.idrepartidor,
                    NombreCompleto = repartidor.nombreCompleto,
                    Email = repartidor.email,
                    Celular = repartidor.celular,
                    CVU = repartidor.cvu,
                    Libre = repartidor.libreRepartidor,
                    Puntuacion = repartidor.puntuacion,
                    TipoVehiculo = repartidorCompleto?.Vehiculo?.tipo,
                    Patente = repartidorCompleto?.Vehiculo?.patente
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login de repartidor");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRepartidorRequest request)
        {
            try
            {
                var existe = await _usuarioService.UserExistsAsync(request.Email);
                if (existe)
                    return Conflict("El email ya está registrado");

                var vehiculo = new Vehiculo
                {
                    tipo = request.TipoVehiculo,
                    patente = request.Patente,
                    modelo = request.Modelo,
                    marca = request.Marca,
                    seguro = request.Seguro,
                    companiaSeguros = request.CompaniaSeguros
                };

                var repartidor = new Repartidor
                {
                    nombreCompleto = request.NombreCompleto,
                    dni = request.Dni,
                    nacimiento = request.Nacimiento,
                    celular = request.Celular,
                    ciudad = request.Ciudad,
                    calle = request.Calle,
                    numero = request.Numero,
                    email = request.Email,
                    password = request.Password,
                    cvu = request.Cvu,
                    libreRepartidor = true // Por defecto libre al registrarse
                };

                var resultado = await _usuarioService.CreateRepartidorAsync(repartidor, vehiculo);
                return Ok(new { Message = "Repartidor registrado exitosamente", RepartidorId = resultado.idrepartidor });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en registro de repartidor");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}