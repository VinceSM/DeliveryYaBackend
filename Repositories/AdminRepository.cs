using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace DeliveryYaBackend.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<object> _passwordHasher;

        public AdminRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<object>();
        }

        public async Task<Admin?> GetByUsuarioAsync(string usuario)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.usuario == usuario);
        }

        public async Task<Admin> CrearAdminAsync(Admin admin)
        {
            // Hash de la contraseña antes de guardar
            admin.password = PasswordHelper.Hash(admin.password);

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin?> LoginAsync(string usuario, string password)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.usuario == usuario);

            if (admin == null) return null;

            // Verificar contraseña
            bool passwordOk = PasswordHelper.Verify(admin.password, password);
            if (!passwordOk) return null;

            return admin;
        }
    }
}
