using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class ComercioHorariosRepository : GenericRepository<ComercioHorario>, IComercioHorariosRepository
    {
        public ComercioHorariosRepository(AppDbContext context) : base(context)
        {
        }
    }
}
