using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class ItemPedidoRepository : GenericRepository<ItemPedido>, IItemPedidoRepository 
    {
        public ItemPedidoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
