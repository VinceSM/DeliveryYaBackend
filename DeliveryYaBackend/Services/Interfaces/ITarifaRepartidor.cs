using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface ITarifaRepartidorService
    {
        // Operaciones básicas de Tarifas
        Task<TarifaRepartidorLibre> CreateTarifaAsync(TarifaRepartidorLibre tarifa);
        Task<TarifaRepartidorLibre> GetTarifaByIdAsync(int id);
        Task<IEnumerable<TarifaRepartidorLibre>> GetAllTarifasAsync();
        Task<IEnumerable<TarifaRepartidorLibre>> GetTarifasByRepartidorAsync(int repartidorId);
        Task<bool> UpdateTarifaAsync(TarifaRepartidorLibre tarifa);
        Task<bool> DeleteTarifaAsync(int id);

        // Cálculos de tarifas
        Task<decimal> CalcularTarifaEnvioAsync(int repartidorId, decimal kmRecorridos, int cantPedidos);
        Task<decimal> CalcularTarifaBaseAsync(int repartidorId);
        Task<decimal> CalcularTarifaPorKmAsync(int repartidorId, decimal kmRecorridos);

        // Gestión de tarifas por repartidor
        Task<TarifaRepartidorLibre> GetTarifaActualByRepartidorAsync(int repartidorId);
        Task<bool> SetTarifaRepartidorAsync(int repartidorId, decimal tarifaBase, decimal tarifaKm);
        Task<bool> UpdateTarifaRepartidorAsync(int repartidorId, decimal nuevaTarifaBase, decimal nuevaTarifaKm);

        // Estadísticas y reportes
        Task<decimal> GetGananciasTotalesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate);
        Task<int> GetCantidadViajesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate);
        Task<decimal> GetKmTotalesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate);
    }
}