using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        // Métodos específicos de Categoría si los necesitás más adelante
        Task<IEnumerable<Categoria>> GetAllActiveAsync();
        Task<Categoria?> GetByNameAsync(string nombre);

    }
}
