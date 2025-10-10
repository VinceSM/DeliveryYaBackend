using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace DeliveryYaBackend.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<AdminResponse>> GetAllAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            return admins.Select(a => ToResponse(a));
        }

        public async Task<AdminResponse?> GetByIdAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            return admin == null ? null : ToResponse(admin);
        }

        public async Task<AdminResponse> CreateAsync(AdminRequest request)
        {
            var admin = new Admin
            {
                usuario = request.Usuario,
                password = request.Password // ⚠️ luego conviene hashear
            };

            admin.password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _adminRepository.AddAsync(admin);
            await _adminRepository.SaveChangesAsync();

            return ToResponse(admin);
        }

        public async Task<AdminResponse?> UpdateAsync(int id, AdminRequest request)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if (admin == null) return null;

            admin.usuario = request.Usuario;
            admin.password = request.Password;

            _adminRepository.Update(admin);
            await _adminRepository.SaveChangesAsync();

            return ToResponse(admin);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            if (admin == null) return false;

            _adminRepository.Remove(admin);
            await _adminRepository.SaveChangesAsync();
            return true;
        }

        private AdminResponse ToResponse(Admin admin)
        {
            return new AdminResponse
            {
                Id = admin.idadmin,
                Usuario = admin.usuario
            };
        }
    }
}
