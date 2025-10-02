using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository 
    {
        public ProductoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
