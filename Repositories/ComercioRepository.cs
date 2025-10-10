using DeliveryYaBackend.Data;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace DeliveryYaBackend.Repositories
{
    public class ComercioRepository : GenericRepository<Comercio>, IComercioRepository 
    {
        public ComercioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
