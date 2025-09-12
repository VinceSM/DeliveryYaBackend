using DeliveryYaBackend.Data.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

public class UsuarioService : IUsuarioService
{
    private readonly IRepository<Cliente> _clienteRepository;
    private readonly IRepository<Repartidor> _repartidorRepository;
    private readonly IRepository<Vehiculo> _vehiculoRepository;

    public UsuarioService(
        IRepository<Cliente> clienteRepository,
        IRepository<Repartidor> repartidorRepository,
        IRepository<Vehiculo> vehiculoRepository)
    {
        _clienteRepository = clienteRepository;
        _repartidorRepository = repartidorRepository;
        _vehiculoRepository = vehiculoRepository;
    }

    // CLIENTES
    public async Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        // Validar que el email no exista
        var existe = await _clienteRepository.FindAsync(c => c.email == cliente.email);
        if (existe.Any())
            throw new Exception("Ya existe un cliente con ese email");

        await _clienteRepository.AddAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {
        return await _clienteRepository.GetByIdAsync(id);
    }

    public async Task<Cliente> GetClienteByEmailAsync(string email)
    {
        var clientes = await _clienteRepository.FindAsync(c => c.email == email);
        return clientes.FirstOrDefault();
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        return await _clienteRepository.GetAllAsync();
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
        return await _clienteRepository.SaveChangesAsync();
    }

    // REPARTIDORES
    public async Task<Repartidor> CreateRepartidorAsync(Repartidor repartidor, Vehiculo vehiculo)
    {
        // Validar email único
        var existe = await _repartidorRepository.FindAsync(r => r.email == repartidor.email);
        if (existe.Any())
            throw new Exception("Ya existe un repartidor con ese email");

        // Crear vehículo
        await _vehiculoRepository.AddAsync(vehiculo);
        await _vehiculoRepository.SaveChangesAsync();

        // Asignar vehiculo al repartidor
        repartidor.vehiculoIdVehiculo = vehiculo.idvehiculo;

        await _repartidorRepository.AddAsync(repartidor);
        await _repartidorRepository.SaveChangesAsync();
        return repartidor;
    }

    public async Task<Repartidor> GetRepartidorByEmailAsync(string email)
    {
        var repartidores = await _repartidorRepository.FindAsync(r => r.email == email);
        return repartidores.FirstOrDefault();
    }

    public async Task<Repartidor> GetRepartidorByIdAsync(int id)
    {
        var repartidor = await _repartidorRepository.GetByIdAsync(id);
        if (repartidor != null)
        {
            repartidor.Vehiculo = await _vehiculoRepository.GetByIdAsync(repartidor.vehiculoIdVehiculo);
        }
        return repartidor;
    }

    public async Task<IEnumerable<Repartidor>> GetAllRepartidoresAsync()
    {
        return await _repartidorRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Repartidor>> GetRepartidoresLibresAsync()
    {
        return await _repartidorRepository.FindAsync(r => r.libreRepartidor == true);
    }

    public async Task<bool> UpdateRepartidorAsync(Repartidor repartidor)
    {
        var existingRepartidor = await _repartidorRepository.GetByIdAsync(repartidor.idrepartidor);
        if (existingRepartidor == null) return false;

        // Actualizar propiedades del repartidor
        existingRepartidor.nombreCompleto = repartidor.nombreCompleto;
        existingRepartidor.dni = repartidor.dni;
        existingRepartidor.nacimiento = repartidor.nacimiento;
        existingRepartidor.celular = repartidor.celular;
        existingRepartidor.ciudad = repartidor.ciudad;
        existingRepartidor.calle = repartidor.calle;
        existingRepartidor.numero = repartidor.numero;
        existingRepartidor.email = repartidor.email;
        existingRepartidor.cvu = repartidor.cvu;
        existingRepartidor.libreRepartidor = repartidor.libreRepartidor;

        // Si el repartidor trae el vehículo, actualizarlo también
        if (repartidor.Vehiculo != null && existingRepartidor.Vehiculo != null)
        {
            existingRepartidor.Vehiculo.tipo = repartidor.Vehiculo.tipo;
            existingRepartidor.Vehiculo.patente = repartidor.Vehiculo.patente;
            existingRepartidor.Vehiculo.modelo = repartidor.Vehiculo.modelo;
            existingRepartidor.Vehiculo.marca = repartidor.Vehiculo.marca;
            existingRepartidor.Vehiculo.seguro = repartidor.Vehiculo.seguro;
            existingRepartidor.Vehiculo.companiaSeguros = repartidor.Vehiculo.companiaSeguros;

            _vehiculoRepository.Update(existingRepartidor.Vehiculo);
        }

        _repartidorRepository.Update(existingRepartidor);

        // Guardar cambios de ambas entidades
        var savedRepartidor = await _repartidorRepository.SaveChangesAsync();
        var savedVehiculo = true;

        if (repartidor.Vehiculo != null)
        {
            savedVehiculo = await _vehiculoRepository.SaveChangesAsync();
        }

        return savedRepartidor && savedVehiculo;
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

    // VALIDACIONES
    public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
    {
        // Buscar en clientes
        var clientes = await _clienteRepository.FindAsync(c => c.email == email && c.password == password);
        if (clientes.Any()) return true;

        // Buscar en repartidores
        var repartidores = await _repartidorRepository.FindAsync(r => r.email == email && r.password == password);
        return repartidores.Any();
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        var clientes = await _clienteRepository.FindAsync(c => c.email == email);
        var repartidores = await _repartidorRepository.FindAsync(r => r.email == email);
        return clientes.Any() || repartidores.Any();
    }
}