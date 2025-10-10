using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IRepartidorService
    {
        Task<IEnumerable<RepartidorResponse>> GetAllAsync();
        Task<RepartidorResponse?> GetByIdAsync(int id);
        Task<IEnumerable<RepartidorResponse>> GetLibresAsync();
        Task<RepartidorResponse> CreateAsync(RepartidorRequest request);
        Task<RepartidorResponse?> UpdateAsync(int id, RepartidorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
