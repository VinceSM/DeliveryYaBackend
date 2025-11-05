using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IComercioRepository : IRepository<Comercio>
    {
        Task<IEnumerable<Comercio>> GetAllActivosAsync();
        Task<IEnumerable<Comercio>> GetDestacadosAsync();
        Task<Comercio?> GetByEmailAsync(string email);
        Task<IEnumerable<Comercio>> GetByCiudadAsync(string ciudad);
        Task<IEnumerable<Comercio>> GetByNombreAsync(string nombre);
    }
}
