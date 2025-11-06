using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IComercioHorariosService
    {
        Task<IEnumerable<Horarios>> GetHorariosPorComercioAsync(int comercioId);
        Task<Horarios?> GetHorarioPorIdAsync(int comercioId, int horarioId);
        Task<Horarios> CrearHorarioParaComercioAsync(int comercioId, Horarios nuevoHorario);
        Task<bool> ActualizarHorarioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre, bool abierto);
        Task<bool> EliminarHorarioAsync(int comercioId, int horarioId);
        Task<bool> CheckComercioAbiertoAsync(int comercioId);
    }

}
