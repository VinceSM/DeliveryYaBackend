using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository 
    {
        public StockRepository(AppDbContext context) : base(context)
        {
        }
    }
}
