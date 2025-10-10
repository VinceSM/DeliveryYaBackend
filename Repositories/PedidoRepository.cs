using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository 
    {
        public PedidoRepository(AppDbContext context) : base(context) { }
    }
}
