using DeliveryYaBackend.Data;
using DeliveryYaBackend.DTOs.Requests.Login;
using DeliveryYaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliveryYaBackend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // LOGIN CLIENTE
        public async Task<string?> LoginClienteAsync(LoginClienteRequest request)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.email == request.Email && c.password == request.Password);

            if (cliente == null)
                return null;

            return GenerateJwtToken(cliente.email!, "Cliente");
        }

        // LOGIN REPARTIDOR
        public async Task<string?> LoginRepartidorAsync(LoginRepartidorRequest request)
        {
            var repartidor = await _context.Repartidores
                .FirstOrDefaultAsync(r => r.email == request.Email && r.password == request.Password);

            if (repartidor == null)
                return null;

            return GenerateJwtToken(repartidor.email!, "Repartidor");
        }

        // LOGIN COMERCIO
        public async Task<string?> LoginComercioAsync(LoginComercioRequest request)
        {
            var comercio = await _context.Comercios
                .FirstOrDefaultAsync(c => c.email == request.Email && c.password == request.Password);

            if (comercio == null)
                return null;

            return GenerateJwtToken(comercio.email!, "Comercio");
        }

        // LOGIN ADMIN
        public async Task<string?> LoginAdminAsync(LoginAdminRequest request)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.usuario == request.Usuario && a.password == request.Password);

            if (admin == null)
                return null;

            return GenerateJwtToken(admin.usuario!, "Admin");
        }

        // GENERADOR DE TOKEN
        private string GenerateJwtToken(string identifier, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, identifier),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
