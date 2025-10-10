using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryYaBackend.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByUsuarioAsync(string usuario)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.usuario == usuario);
        }
    }
}
