using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminResponse>> GetAllAsync();
        Task<AdminResponse?> GetByIdAsync(int id);
        Task<AdminResponse> CreateAsync(AdminRequest request);
        Task<AdminResponse?> UpdateAsync(int id, AdminRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
