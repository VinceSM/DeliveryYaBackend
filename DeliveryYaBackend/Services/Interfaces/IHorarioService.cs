using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IHorarioService
    {
        // Operaciones básicas de Horarios
        Task<Horarios> CreateHorarioAsync(Horarios horario);
        Task<Horarios> GetHorarioByIdAsync(int id);
        Task<IEnumerable<Horarios>> GetAllHorariosAsync();
        Task<bool> UpdateHorarioAsync(Horarios horario);
        Task<bool> DeleteHorarioAsync(int id);

        // Gestión de horarios por comercio
        Task<bool> AddHorarioToComercioAsync(int comercioId, int horarioId);
        Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId);
        Task<IEnumerable<Horarios>> GetHorariosByComercioAsync(int comercioId);
        Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre);

        // Validaciones de horarios
        Task<bool> CheckComercioAbiertoAsync(int comercioId);
        Task<bool> CheckHorarioValidoAsync(TimeSpan apertura, TimeSpan cierre);
        Task<IEnumerable<Horarios>> GetHorariosByDiaAsync(string dia);

        // Configuración de horarios
        Task<bool> SetHorarioAbiertoAsync(int horarioId, bool abierto);
        Task<bool> UpdateDiasHorarioAsync(int horarioId, string dias);
    }
}