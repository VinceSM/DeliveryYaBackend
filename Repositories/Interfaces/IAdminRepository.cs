using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin?> GetByUsuarioAsync(string usuario);
    }
}
