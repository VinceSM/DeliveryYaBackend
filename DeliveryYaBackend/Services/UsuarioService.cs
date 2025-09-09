using DeliveryYaBackend.Data.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Repartidor> _repartidorRepository;
        private readonly IRepository<IUserType> _userTypeRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;

        public UsuarioService(
            IRepository<Cliente> clienteRepository,
            IRepository<Repartidor> repartidorRepository,
            IRepository<IUserType> userTypeRepository,
            IRepository<Vehiculo> vehiculoRepository)
        {
            _clienteRepository = clienteRepository;
            _repartidorRepository = repartidorRepository;
            _userTypeRepository = userTypeRepository;
            _vehiculoRepository = vehiculoRepository;
        }

        // CLIENTES
        public async Task<Cliente> CreateClienteAsync(Cliente cliente, IUserType userType)
        {
            // Primero crear el UserType
            await _userTypeRepository.AddAsync(userType);
            await _userTypeRepository.SaveChangesAsync();

            // Luego crear el Cliente
            cliente.IUserTypeIdIUserType = userType.idusertype;
            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente != null)
            {
                cliente.IUserType = await _userTypeRepository.GetByIdAsync(cliente.IUserTypeIdIUserType);
            }
            return cliente;
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            var userTypes = await _userTypeRepository.FindAsync(u => u.email == email);
            var userType = userTypes.FirstOrDefault();

            if (userType == null) return null;

            var clientes = await _clienteRepository.FindAsync(c => c.IUserTypeIdIUserType == userType.idusertype);
            var cliente = clientes.FirstOrDefault();

            if (cliente != null)
            {
                cliente.IUserType = userType;
            }

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            foreach (var cliente in clientes)
            {
                cliente.IUserType = await _userTypeRepository.GetByIdAsync(cliente.IUserTypeIdIUserType);
            }
            return clientes;
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente)
        {
            var existingCliente = await _clienteRepository.GetByIdAsync(cliente.idcliente);
            if (existingCliente == null) return false;

            _clienteRepository.Update(cliente);
            return await _clienteRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return false;

            _clienteRepository.Remove(cliente);

            // Opcional: también eliminar el UserType asociado
            var userType = await _userTypeRepository.GetByIdAsync(cliente.IUserTypeIdIUserType);
            if (userType != null)
            {
                _userTypeRepository.Remove(userType);
            }

            return await _clienteRepository.SaveChangesAsync();
        }

        // REPARTIDORES
        public async Task<Repartidor> CreateRepartidorAsync(Repartidor repartidor, IUserType userType, Vehiculo vehiculo)
        {
            // Crear Vehiculo primero
            await _vehiculoRepository.AddAsync(vehiculo);
            await _vehiculoRepository.SaveChangesAsync();

            // Crear UserType
            await _userTypeRepository.AddAsync(userType);
            await _userTypeRepository.SaveChangesAsync();

            // Crear Repartidor
            repartidor.IUserTypeIdIUserType = userType.idusertype;
            repartidor.VehiculoIdVehiculo = vehiculo.idvehiculo;
            await _repartidorRepository.AddAsync(repartidor);
            await _repartidorRepository.SaveChangesAsync();

            return repartidor;
        }

        public async Task<Repartidor> GetRepartidorByIdAsync(int id)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(id);
            if (repartidor != null)
            {
                repartidor.IUserType = await _userTypeRepository.GetByIdAsync(repartidor.IUserTypeIdIUserType);
                repartidor.Vehiculo = await _vehiculoRepository.GetByIdAsync(repartidor.VehiculoIdVehiculo);
            }
            return repartidor;
        }

        public async Task<Repartidor> GetRepartidorByEmailAsync(string email)
        {
            var userTypes = await _userTypeRepository.FindAsync(u => u.email == email);
            var userType = userTypes.FirstOrDefault();

            if (userType == null) return null;

            var repartidores = await _repartidorRepository.FindAsync(r => r.IUserTypeIdIUserType == userType.idusertype);
            var repartidor = repartidores.FirstOrDefault();

            if (repartidor != null)
            {
                repartidor.IUserType = userType;
                repartidor.Vehiculo = await _vehiculoRepository.GetByIdAsync(repartidor.VehiculoIdVehiculo);
            }

            return repartidor;
        }

        public async Task<IEnumerable<Repartidor>> GetAllRepartidoresAsync()
        {
            var repartidores = await _repartidorRepository.GetAllAsync();
            foreach (var repartidor in repartidores)
            {
                repartidor.IUserType = await _userTypeRepository.GetByIdAsync(repartidor.IUserTypeIdIUserType);
                repartidor.Vehiculo = await _vehiculoRepository.GetByIdAsync(repartidor.VehiculoIdVehiculo);
            }
            return repartidores;
        }

        public async Task<IEnumerable<Repartidor>> GetRepartidoresLibresAsync()
        {
            var repartidores = await _repartidorRepository.FindAsync(r => r.libreRepartidor == true);
            foreach (var repartidor in repartidores)
            {
                repartidor.IUserType = await _userTypeRepository.GetByIdAsync(repartidor.IUserTypeIdIUserType);
                repartidor.Vehiculo = await _vehiculoRepository.GetByIdAsync(repartidor.VehiculoIdVehiculo);
            }
            return repartidores;
        }

        public async Task<bool> UpdateRepartidorAsync(Repartidor repartidor)
        {
            var existingRepartidor = await _repartidorRepository.GetByIdAsync(repartidor.IdRepartidor);
            if (existingRepartidor == null) return false;

            _repartidorRepository.Update(repartidor);
            return await _repartidorRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateDisponibilidadRepartidorAsync(int repartidorId, bool disponible)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(repartidorId);
            if (repartidor == null) return false;

            repartidor.libreRepartidor = disponible;
            _repartidorRepository.Update(repartidor);
            return await _repartidorRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteRepartidorAsync(int id)
        {
            var repartidor = await _repartidorRepository.GetByIdAsync(id);
            if (repartidor == null) return false;

            _repartidorRepository.Remove(repartidor);
            return await _repartidorRepository.SaveChangesAsync();
        }

        // AUTENTICACIÓN Y VALIDACIÓN
        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var userTypes = await _userTypeRepository.FindAsync(u => u.email == email && u.password == password);
            return userTypes.Any();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var userTypes = await _userTypeRepository.FindAsync(u => u.email == email);
            return userTypes.Any();
        }

        public async Task<bool> UpdateUserPasswordAsync(int userId, string newPassword)
        {
            var userType = await _userTypeRepository.GetByIdAsync(userId);
            if (userType == null) return false;

            userType.password = newPassword;
            _userTypeRepository.Update(userType);
            return await _userTypeRepository.SaveChangesAsync();
        }

        // USERTYPE
        public async Task<IUserType> GetUserTypeByIdAsync(int id)
        {
            return await _userTypeRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUserTypeAsync(IUserType userType)
        {
            var existingUserType = await _userTypeRepository.GetByIdAsync(userType.idusertype);
            if (existingUserType == null) return false;

            _userTypeRepository.Update(userType);
            return await _userTypeRepository.SaveChangesAsync();
        }
    }
}