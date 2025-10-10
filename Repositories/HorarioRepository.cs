using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;

namespace DeliveryYaBackend.Repositories
{
    public class HorarioRepository : GenericRepository<Horarios>, IHorarioRepository 
    {
        public HorarioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
