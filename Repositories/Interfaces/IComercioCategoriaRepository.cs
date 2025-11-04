using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Repositories.Interfaces
{
    public interface IComercioCategoriaRepository
    {
        Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId);
        Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId);
        Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId);
        Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId);
    }
}
