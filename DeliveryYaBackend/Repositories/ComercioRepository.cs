using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class ComercioRepository : GenericRepository<Comercio>, IComercioRepository 
    {
        public ComercioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
