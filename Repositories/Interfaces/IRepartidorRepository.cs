using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IRepartidorRepository : IRepository<Repartidor>
    {
        Task<Repartidor?> GetByEmailAsync(string email);
        Task<IEnumerable<Repartidor>> GetRepartidoresLibresAsync();
    }
}
