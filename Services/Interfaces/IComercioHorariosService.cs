using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IComercioHorariosService
    {
        // Horarios de comercios
        Task<bool> AddHorarioToComercioAsync(int comercioId, int horarioId);
        Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId);
        Task<IEnumerable<Horarios>> GetHorariosByComercioAsync(int comercioId);
        Task<bool> CheckComercioAbiertoAsync(int comercioId);
        Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre);
    }
}
