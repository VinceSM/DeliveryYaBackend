using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class TarifaRepartidorService : ITarifaRepartidorService
    {
        private readonly IRepository<TarifaRepartidorLibre> _tarifaRepository;
        private readonly IRepository<Repartidor> _repartidorRepository;
        private readonly IRepository<Pedido> _pedidoRepository;

        public TarifaRepartidorService(
            IRepository<TarifaRepartidorLibre> tarifaRepository,
            IRepository<Repartidor> repartidorRepository,
            IRepository<Pedido> pedidoRepository)
        {
            _tarifaRepository = tarifaRepository;
            _repartidorRepository = repartidorRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<TarifaRepartidorLibre> CreateTarifaAsync(TarifaRepartidorLibre tarifa)
        {
            await _tarifaRepository.AddAsync(tarifa);
            await _tarifaRepository.SaveChangesAsync();
            return tarifa;
        }

        public async Task<TarifaRepartidorLibre> GetTarifaByIdAsync(int id)
        {
            return await _tarifaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TarifaRepartidorLibre>> GetAllTarifasAsync()
        {
            return await _tarifaRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TarifaRepartidorLibre>> GetTarifasByRepartidorAsync(int repartidorId)
        {
            return await _tarifaRepository.FindAsync(t => t.RepartidorIdRepartidor == repartidorId);
        }

        public async Task<bool> UpdateTarifaAsync(TarifaRepartidorLibre tarifa)
        {
            var existingTarifa = await _tarifaRepository.GetByIdAsync(tarifa.idtarifa);
            if (existingTarifa == null) return false;

            _tarifaRepository.Update(tarifa);
            return await _tarifaRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTarifaAsync(int id)
        {
            var tarifa = await _tarifaRepository.GetByIdAsync(id);
            if (tarifa == null) return false;

            _tarifaRepository.Remove(tarifa);
            return await _tarifaRepository.SaveChangesAsync();
        }

        public async Task<decimal> CalcularTarifaEnvioAsync(int repartidorId, decimal kmRecorridos, int cantPedidos)
        {
            var tarifa = await GetTarifaActualByRepartidorAsync(repartidorId);
            if (tarifa == null) return 0m;

            return tarifa.tarifaBase + (tarifa.kmRecorridos * kmRecorridos);
        }

        public async Task<decimal> CalcularTarifaBaseAsync(int repartidorId)
        {
            var tarifa = await GetTarifaActualByRepartidorAsync(repartidorId);
            return tarifa?.tarifaBase ?? 0m;
        }

        public async Task<decimal> CalcularTarifaPorKmAsync(int repartidorId, decimal kmRecorridos)
        {
            var tarifa = await GetTarifaActualByRepartidorAsync(repartidorId);
            if (tarifa == null) return 0m;

            return tarifa.kmRecorridos * kmRecorridos;
        }

        public async Task<TarifaRepartidorLibre> GetTarifaActualByRepartidorAsync(int repartidorId)
        {
            var tarifas = await _tarifaRepository.FindAsync(t => t.RepartidorIdRepartidor == repartidorId);
            return tarifas.OrderByDescending(t => t.createdAt).FirstOrDefault();
        }

        public async Task<bool> SetTarifaRepartidorAsync(int repartidorId, decimal tarifaBase, decimal tarifaKm)
        {
            var tarifa = new TarifaRepartidorLibre
            {
                RepartidorIdRepartidor = repartidorId,
                tarifaBase = tarifaBase,
                kmRecorridos = tarifaKm,
                cantPedidos = 0,
                createdAt = DateTime.UtcNow
            };

            await _tarifaRepository.AddAsync(tarifa);
            return await _tarifaRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateTarifaRepartidorAsync(int repartidorId, decimal nuevaTarifaBase, decimal nuevaTarifaKm)
        {
            var tarifaActual = await GetTarifaActualByRepartidorAsync(repartidorId);
            if (tarifaActual == null) return false;

            // Crear nueva tarifa en lugar de modificar la existente (para histórico)
            var nuevaTarifa = new TarifaRepartidorLibre
            {
                RepartidorIdRepartidor = repartidorId,
                tarifaBase = nuevaTarifaBase,
                kmRecorridos = nuevaTarifaKm,
                cantPedidos = tarifaActual.cantPedidos,
                createdAt = DateTime.UtcNow
            };

            await _tarifaRepository.AddAsync(nuevaTarifa);
            return await _tarifaRepository.SaveChangesAsync();
        }

        public async Task<decimal> GetGananciasTotalesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var pedidos = await _pedidoRepository.FindAsync(p =>
                p.RepartidorIdRepartidor == repartidorId &&
                p.fecha >= startDate && p.fecha <= endDate);

            // Aquí deberías implementar la lógica específica de cálculo de ganancias
            return pedidos.Sum(p => p.subtotalPedido) * 0.8m; // Ejemplo: 80% para el repartidor
        }

        public async Task<int> GetCantidadViajesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var pedidos = await _pedidoRepository.FindAsync(p =>
                p.RepartidorIdRepartidor == repartidorId &&
                p.fecha >= startDate && p.fecha <= endDate);

            return pedidos.Count();
        }

        public async Task<decimal> GetKmTotalesRepartidorAsync(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var tarifas = await _tarifaRepository.FindAsync(t =>
                t.RepartidorIdRepartidor == repartidorId &&
                t.createdAt >= startDate && t.createdAt <= endDate);

            // Lógica de cálculo de km (esto es un ejemplo, deberías adaptarlo)
            return tarifas.Sum(t => t.kmRecorridos);
        }
    }
}