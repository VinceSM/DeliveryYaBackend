using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.DTOs.Responses.Horarios;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IHorarioService
    {
        // CRUD básico de Horarios
        Task<HorarioResponse> CreateHorarioAsync(CreateHorarioRequest request);
        Task<HorarioResponse?> GetHorarioByIdAsync(int id);
        Task<IEnumerable<HorarioResponse>> GetAllHorariosAsync();
        Task<bool> UpdateHorarioAsync(UpdateHorarioRequest request);
        Task<bool> DeleteHorarioAsync(int id);

        // Gestión de horarios por comercio
        Task<HorarioResponse> CreateAndAssignHorarioAsync(int comercioId, CreateHorarioRequest request);
        Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId);
        Task<IEnumerable<HorarioResponse>> GetHorariosByComercioAsync(int comercioId);
        Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre);
    }
}
