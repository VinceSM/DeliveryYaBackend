using DeliveryYaBackend.DTOs.Requests.Usuarios;
using DeliveryYaBackend.DTOs.Responses.Usuarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace DeliveryYaBackend.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteResponse>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => ToResponse(c));
        }

        public async Task<ClienteResponse?> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            return cliente == null ? null : ToResponse(cliente);
        }

        public async Task<ClienteResponse> CreateAsync(ClienteRequest request)
        {
            var cliente = new Cliente
            {
                nombreCompleto = request.NombreCompleto,
                dni = request.Dni,
                nacimiento = request.Nacimiento,
                celular = request.Celular,
                ciudad = request.Ciudad,
                calle = request.Calle,
                numero = request.Numero,
                email = request.Email,
                password = request.Password
            };

            cliente.password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            return ToResponse(cliente);
        }

        public async Task<ClienteResponse?> UpdateAsync(int id, ClienteRequest request)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return null;

            cliente.nombreCompleto = request.NombreCompleto;
            cliente.dni = request.Dni;
            cliente.nacimiento = request.Nacimiento;
            cliente.celular = request.Celular;
            cliente.ciudad = request.Ciudad;
            cliente.calle = request.Calle;
            cliente.numero = request.Numero;
            cliente.email = request.Email;
            // cliente.password se puede actualizar en un flujo aparte

            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();

            return ToResponse(cliente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return false;

            _clienteRepository.Remove(cliente);
            await _clienteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClienteExistsAsync(string email)
        {
            return await _clienteRepository.ExistsAsync(c => c.email == email);
        }

        private ClienteResponse ToResponse(Cliente cliente)
        {
            return new ClienteResponse
            {
                Id = cliente.idcliente,
                NombreCompleto = cliente.nombreCompleto,
                Dni = cliente.dni,
                Nacimiento = cliente.nacimiento,
                Celular = cliente.celular,
                Ciudad = cliente.ciudad,
                Calle = cliente.calle,
                Numero = cliente.numero,
                Email = cliente.email
            };
        }
    }
}
