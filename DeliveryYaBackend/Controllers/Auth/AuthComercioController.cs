using DeliveryYaBackend.DTOs.Requests.Auth.Login;
using DeliveryYaBackend.DTOs.Requests.Auth.Register;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/comercio")]
    public class AuthComercioController : ControllerBase
    {
        private readonly IComercioService _comercioService;
        private readonly ILogger<AuthComercioController> _logger;

        public AuthComercioController(IComercioService comercioService, ILogger<AuthComercioController> logger)
        {
            _comercioService = comercioService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginComercioRequest request)
        {
            try
            {
                var comercio = await _comercioService.GetComercioByEmailAsync(request.Email);
                if (comercio == null)
                    return Unauthorized("Credenciales inválidas");

                if (comercio.password != request.Password)
                    return Unauthorized("Credenciales inválidas");

                var response = new LoginComercioResponse
                {
                    Token = "generar_jwt_token_here",
                    ComercioId = comercio.idcomercio,
                    NombreComercio = comercio.nombreComercio,
                    Email = comercio.email,
                    Encargado = comercio.encargado,
                    Celular = comercio.celular,
                    Direccion = $"{comercio.calle} {comercio.numero}, {comercio.ciudad}",
                    Latitud = comercio.latitud,
                    Longitud = comercio.longitud,
                    CVU = comercio.cvu,
                    Alias = comercio.alias,
                    Destacado = comercio.destacado
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login de comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterComercioRequest request)
        {
            try
            {
                var existe = await _comercioService.ComercioExistsAsync(request.Email);
                if (existe)
                    return Conflict("El email ya está registrado");

                var comercio = new Comercio
                {
                    nombreComercio = request.NombreComercio,
                    email = request.Email,
                    password = request.Password,
                    fotoPortada = request.FotoPortada,
                    celular = request.Celular,
                    ciudad = request.Ciudad,
                    calle = request.Calle,
                    numero = request.Numero,
                    latitud = request.Latitud,
                    longitud = request.Longitud,
                    encargado = request.Encargado,
                    cvu = request.Cvu,
                    alias = request.Alias,
                    destacado = request.Destacado
                };

                var resultado = await _comercioService.CreateComercioAsync(comercio);
                return Ok(new { Message = "Comercio registrado exitosamente", ComercioId = resultado.idcomercio });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en registro de comercio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}