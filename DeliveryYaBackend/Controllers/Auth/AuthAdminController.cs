using DeliveryYaBackend.DTOs.Requests.Admin;
using DeliveryYaBackend.DTOs.Responses.Admin;
using DeliveryYaBackend.DTOs.Responses.Auth;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/admin")]
    public class AuthAdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AuthAdminController> _logger;

        public AuthAdminController(IAdminService adminService, ILogger<AuthAdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAdminRequest request)
        {
            try
            {
                var admin = await _adminService.LoginAsync(request.Usuario, request.Password);
                if (admin == null)
                    return Unauthorized("Credenciales inválidas");

                var response = new LoginAdminResponse
                {
                    Token = "generar_jwt_token_here",
                    AdminId = admin.idadmin,
                    Usuario = admin.usuario
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login de admin");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Opcional: Endpoint para cambiar password (si lo necesitas)
        [HttpPut("cambiar-password")]
        public async Task<IActionResult> CambiarPassword(int adminId, string nuevaPassword)
        {
            try
            {
                var resultado = await _adminService.ChangePasswordAsync(adminId, nuevaPassword);
                if (!resultado)
                    return NotFound("Admin no encontrado");

                return Ok("Password actualizada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar password de admin");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}