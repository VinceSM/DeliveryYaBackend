using DeliveryYaBackend.Data.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _adminRepository;

        public AdminService(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> LoginAsync(string usuario, string password)
        {
            var admins = await _adminRepository.FindAsync(a =>
                a.usuario == usuario && a.password == password);
            return admins.FirstOrDefault();
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _adminRepository.GetByIdAsync(id);
        }

        public async Task<bool> ChangePasswordAsync(int adminId, string newPassword)
        {
            var admin = await _adminRepository.GetByIdAsync(adminId);
            if (admin == null) return false;

            admin.password = newPassword;
            _adminRepository.Update(admin);
            return await _adminRepository.SaveChangesAsync();
        }
    }
}