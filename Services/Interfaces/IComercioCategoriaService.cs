using DeliveryYaBackend.DTOs.Responses.Categorias;
using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Models;

namespace DeliveryYaBackend.Services.Interfaces
{
    public interface IComercioCategoriaService
    {
        Task<bool> AddCategoriaToComercioAsync(int comercioId, int categoriaId);
        Task<bool> RemoveCategoriaFromComercioAsync(int comercioId, int categoriaId);
        Task<IEnumerable<Categoria>> GetCategoriasByComercioAsync(int comercioId);
        Task<IEnumerable<Comercio>> GetComerciosByCategoriaAsync(int categoriaId);
    }
}
