using DeliveryYaBackend.Models;

public interface IUsuarioService
{
    // Clientes
    Task<Cliente> CreateClienteAsync(Cliente cliente);
    Task<Cliente> GetClienteByIdAsync(int id);
    Task<Cliente> GetClienteByEmailAsync(string email);
    Task<IEnumerable<Cliente>> GetAllClientesAsync();
    Task<bool> UpdateClienteAsync(Cliente cliente);
    Task<bool> DeleteClienteAsync(int id);

    // Repartidores
    Task<Repartidor> CreateRepartidorAsync(Repartidor repartidor, Vehiculo vehiculo);
    Task<Repartidor> GetRepartidorByIdAsync(int id);
    Task<Repartidor> GetRepartidorByEmailAsync(string email);
    Task<IEnumerable<Repartidor>> GetAllRepartidoresAsync();
    Task<IEnumerable<Repartidor>> GetRepartidoresLibresAsync();
    Task<bool> UpdateRepartidorAsync(Repartidor repartidor);
    Task<bool> UpdateDisponibilidadRepartidorAsync(int repartidorId, bool disponible);
    Task<bool> DeleteRepartidorAsync(int id);

    // Validaciones
    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    Task<bool> UserExistsAsync(string email);
}