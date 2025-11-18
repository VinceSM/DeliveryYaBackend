using DeliveryYaBackend.DTOs.Requests.Login;
using DeliveryYaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login-cliente")]
        public async Task<IActionResult> LoginCliente([FromBody] LoginClienteRequest request)
        {
            var token = await _authService.LoginClienteAsync(request);
            if (token == null)
                return Unauthorized("Email o contraseña incorrectos.");
            return Ok(new { token });
        }

        //[HttpPost("login-repartidor")]
        //public async Task<IActionResult> LoginRepartidor([FromBody] LoginRepartidorRequest request)
        //{
        //    var token = await _authService.LoginRepartidorAsync(request);
        //    if (token == null)
        //        return Unauthorized("Email o contraseña incorrectos.");
        //    return Ok(new { token });
        //}

        [HttpPost("login-comercio")]
        public async Task<IActionResult> LoginComercio([FromBody] LoginComercioRequest request)
        {
            var token = await _authService.LoginComercioAsync(request);
            if (token == null)
                return Unauthorized("Email o contraseña incorrectos.");
            return Ok(new { token });
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminRequest request)
        {
            var token = await _authService.LoginAdminAsync(request);
            if (token == null)
                return Unauthorized("Usuario o contraseña incorrectos.");
            return Ok(new { token });
        }
    }
}
