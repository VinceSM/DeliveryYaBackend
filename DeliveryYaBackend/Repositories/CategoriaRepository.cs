using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository 
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
