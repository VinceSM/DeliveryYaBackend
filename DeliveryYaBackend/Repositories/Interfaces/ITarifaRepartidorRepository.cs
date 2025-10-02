using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface ITarifaRepartidorRepository : IRepository<TarifaRepartidorLibre>
    {
        Task<IEnumerable<TarifaRepartidorLibre>> GetTarifasPorRepartidorAsync(int repartidorId);
        Task<TarifaRepartidorLibre> GetUltimaTarifaByRepartidorAsync(int repartidorId);
    }
}
