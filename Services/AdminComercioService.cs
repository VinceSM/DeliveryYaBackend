using DeliveryYaBackend.DTOs.Responses.Comercios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class AdminComercioService : IAdminComercioService
    {
        private readonly IComercioRepository _comercioRepository;

        public AdminComercioService(IComercioRepository comercioRepository)
        {
            _comercioRepository = comercioRepository;
        }

        public async Task<IEnumerable<ComercioResponse>> GetPendientesAsync()
        {
            var comercios = await _comercioRepository.GetAllAsync();
            return comercios
                .Where(c => c.createdAt == null && c.deletedAt == null)
                .Select(ToResponse)
                .ToList();
        }

        public async Task<IEnumerable<ComercioResponse>> GetActivosAsync()
        {
            var comercios = await _comercioRepository.GetAllAsync();
            return comercios
                .Where(c => c.createdAt != null && c.deletedAt == null)
                .Select(ToResponse)
                .ToList();
        }

        public async Task<ComercioResponse?> AprobarComercioAsync(int id)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null || comercio.deletedAt != null)
                return null;

            comercio.createdAt = DateTime.UtcNow;
            _comercioRepository.Update(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        public async Task<ComercioResponse?> DestacarComercioAsync(int id, bool destacado)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null) return null;

            comercio.destacado = destacado;
            comercio.updatedAt = DateTime.UtcNow;

            _comercioRepository.Update(comercio);
            await _comercioRepository.SaveChangesAsync();

            return ToResponse(comercio);
        }

        private ComercioResponse ToResponse(Comercio comercio)
        {
            return new ComercioResponse
            {
                Id = comercio.idcomercio,
                NombreComercio = comercio.nombreComercio,
                TipoComercio = comercio.tipoComercio,
                Ciudad = comercio.ciudad,
                Destacado = comercio.destacado,
                CreatedAt = comercio.createdAt
            };
        }

        public async Task<ComercioDetalleResponse?> GetDetalleAsync(int id)
        {
            var comercio = await _comercioRepository.GetByIdAsync(id);
            if (comercio == null || comercio.deletedAt != null)
                return null;

            return new ComercioDetalleResponse
            {
                IdComercio = comercio.idcomercio,
                NombreComercio = comercio.nombreComercio,
                TipoComercio = comercio.tipoComercio,
                Eslogan = comercio.eslogan,
                FotoPortada = comercio.fotoPortada,
                Envio = comercio.envio,
                DeliveryPropio = comercio.deliveryPropio,
                Celular = comercio.celular,
                Ciudad = comercio.ciudad,
                Calle = comercio.calle,
                Numero = comercio.numero,
                Sucursales = comercio.sucursales,
                Latitud = comercio.latitud,
                Longitud = comercio.longitud,
                Encargado = comercio.encargado,
                CVU = comercio.cvu,
                Alias = comercio.alias,
                Destacado = comercio.destacado,
                CreatedAt = comercio.createdAt,
                UpdatedAt = comercio.updatedAt
            };
        }
    }
}
