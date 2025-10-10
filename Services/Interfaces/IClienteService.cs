using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponse>> GetAllAsync();
        Task<ClienteResponse?> GetByIdAsync(int id);
        Task<ClienteResponse> CreateAsync(ClienteRequest request);
        Task<ClienteResponse?> UpdateAsync(int id, ClienteRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
