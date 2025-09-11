using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> LoginAsync(string usuario, string password);
        Task<Admin> GetAdminByIdAsync(int id);
        Task<bool> ChangePasswordAsync(int adminId, string newPassword);
    }
}