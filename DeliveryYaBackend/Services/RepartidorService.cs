using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class RepartidorService : IRepartidorService
    {
        private readonly IRepartidorRepository _repartidorRepository;

        public RepartidorService(IRepartidorRepository repartidorRepository)
        {
            _repartidorRepository = repartidorRepository;
        }

        public async Task<IEnumerable<RepartidorResponse>> GetAllAsync()
        {
            var repartidores = await _repartidorRepository.GetAllAsync();
            return repartidores.Select(r => ToResponse(r));
        }

        public async Task<RepartidorResponse?> GetByIdAsync(int id)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(id);
            return repartidor == null ? null : ToResponse(repartidor);
        }

        public async Task<IEnumerable<RepartidorResponse>> GetLibresAsync()
        {
            var libres = await _repartidorRepository.GetRepartidoresLibresAsync();
            return libres.Select(r => ToResponse(r));
        }

        public async Task<RepartidorResponse> CreateAsync(RepartidorRequest request)
        {
            var vehiculo = new Vehiculo
            {
                tipo = request.TipoVehiculo,
                patente = request.Patente,
                modelo = request.Modelo,
                marca = request.Marca,
                seguro = request.Seguro,
                companiaSeguros = request.CompaniaSeguros
            };

            var repartidor = new Repartidor
            {
                nombreCompleto = request.NombreCompleto,
                dni = request.Dni,
                nacimiento = request.Nacimiento,
                celular = request.Celular,
                ciudad = request.Ciudad,
                calle = request.Calle,
                numero = request.Numero,
                email = request.Email,
                password = request.Password,
                cvu = request.Cvu,
                libreRepartidor = request.LibreRepartidor,
                puntuacion = 0,
                Vehiculo = vehiculo
            };

            await _repartidorRepository.AddAsync(repartidor);
            await _repartidorRepository.SaveChangesAsync();

            return ToResponse(repartidor);
        }

        public async Task<RepartidorResponse?> UpdateAsync(int id, RepartidorRequest request)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(id);
            if (repartidor == null) return null;

            repartidor.nombreCompleto = request.NombreCompleto;
            repartidor.dni = request.Dni;
            repartidor.nacimiento = request.Nacimiento;
            repartidor.celular = request.Celular;
            repartidor.ciudad = request.Ciudad;
            repartidor.calle = request.Calle;
            repartidor.numero = request.Numero;
            repartidor.email = request.Email;
            repartidor.cvu = request.Cvu;
            repartidor.libreRepartidor = request.LibreRepartidor;

            if (repartidor.Vehiculo != null)
            {
                repartidor.Vehiculo.tipo = request.TipoVehiculo;
                repartidor.Vehiculo.patente = request.Patente;
                repartidor.Vehiculo.modelo = request.Modelo;
                repartidor.Vehiculo.marca = request.Marca;
                repartidor.Vehiculo.seguro = request.Seguro;
                repartidor.Vehiculo.companiaSeguros = request.CompaniaSeguros;
            }

            _repartidorRepository.Update(repartidor);
            await _repartidorRepository.SaveChangesAsync();

            return ToResponse(repartidor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(id);
            if (repartidor == null) return false;

            _repartidorRepository.Delete(repartidor);
            await _repartidorRepository.SaveChangesAsync();
            return true;
        }

        private RepartidorResponse ToResponse(Repartidor repartidor)
        {
            return new RepartidorResponse
            {
                Id = repartidor.idrepartidor,
                NombreCompleto = repartidor.nombreCompleto,
                Dni = repartidor.dni,
                Nacimiento = repartidor.nacimiento,
                Celular = repartidor.celular,
                Ciudad = repartidor.ciudad,
                Calle = repartidor.calle,
                Numero = repartidor.numero,
                Email = repartidor.email,
                Puntuacion = repartidor.puntuacion,
                Cvu = repartidor.cvu,
                LibreRepartidor = repartidor.libreRepartidor,
                VehiculoId = repartidor.vehiculoIdVehiculo,
                TipoVehiculo = repartidor.Vehiculo?.tipo ?? "",
                Patente = repartidor.Vehiculo?.patente,
                Modelo = repartidor.Vehiculo?.modelo ?? "",
                Marca = repartidor.Vehiculo?.marca ?? "",
                Seguro = repartidor.Vehiculo?.seguro ?? false,
                CompaniaSeguros = repartidor.Vehiculo?.companiaSeguros
            };
        }
    }
}
