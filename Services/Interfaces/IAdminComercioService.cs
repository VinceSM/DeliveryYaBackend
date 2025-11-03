using DeliveryYaBackend.DTOs.Responses.Comercios;
using System.Threading.Tasks;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IAdminComercioService
    {
        Task<IEnumerable<ComercioResponse>> GetPendientesAsync();
        Task<IEnumerable<ComercioResponse>> GetActivosAsync();
        Task<ComercioDetalleResponse?> GetDetalleAsync(int id);
        Task<ComercioResponse?> AprobarComercioAsync(int id);
        Task<ComercioResponse?> DestacarComercioAsync(int id, bool destacado);
    }
}
