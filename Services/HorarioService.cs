using DeliveryYaBackend.DTOs.Requests.Horarios;
using DeliveryYaBackend.DTOs.Responses.Horarios;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly IRepository<Horarios> _horariosRepository;
        private readonly IRepository<Comercio> _comercioRepository;
        private readonly IRepository<ComercioHorario> _comercioHorarioRepository;

        public HorarioService(
            IRepository<Horarios> horariosRepository,
            IRepository<Comercio> comercioRepository,
            IRepository<ComercioHorario> comercioHorarioRepository)
        {
            _horariosRepository = horariosRepository;
            _comercioRepository = comercioRepository;
            _comercioHorarioRepository = comercioHorarioRepository;
        }

        // ================================================
        // CRUD BÁSICO DE HORARIOS
        // ================================================

        public async Task<HorarioResponse> CreateHorarioAsync(CreateHorarioRequest request)
        {
            var horario = new Horarios
            {
                apertura = request.Apertura,
                cierre = request.Cierre,
                dias = request.Dias,
                abierto = request.Abierto,
                createdAt = DateTime.UtcNow
            };

            await _horariosRepository.AddAsync(horario);
            await _horariosRepository.SaveChangesAsync();

            return ToResponse(horario);
        }

        public async Task<HorarioResponse?> GetHorarioByIdAsync(int id)
        {
            var horario = await _horariosRepository.GetByIdAsync(id);
            return horario == null ? null : ToResponse(horario);
        }

        public async Task<IEnumerable<HorarioResponse>> GetAllHorariosAsync()
        {
            var horarios = await _horariosRepository.GetAllAsync();
            return horarios.Select(ToResponse);
        }

        public async Task<bool> UpdateHorarioAsync(UpdateHorarioRequest request)
        {
            var horario = await _horariosRepository.GetByIdAsync(request.IdHorario);
            if (horario == null) return false;

            horario.apertura = request.Apertura;
            horario.cierre = request.Cierre;
            horario.dias = request.Dias;
            horario.abierto = request.Abierto;
            horario.updatedAt = DateTime.UtcNow;

            _horariosRepository.Update(horario);
            return await _horariosRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteHorarioAsync(int id)
        {
            var horario = await _horariosRepository.GetByIdAsync(id);
            if (horario == null) return false;

            _horariosRepository.Remove(horario);
            return await _horariosRepository.SaveChangesAsync();
        }

        // ================================================
        // GESTIÓN DE HORARIOS POR COMERCIO
        // ================================================

        public async Task<HorarioResponse> CreateAndAssignHorarioAsync(int comercioId, CreateHorarioRequest request)
        {
            // Validar comercio
            var comercio = await _comercioRepository.GetByIdAsync(comercioId);
            if (comercio == null)
                throw new Exception($"No se encontró el comercio con ID {comercioId}");

            // Crear el horario
            var horario = new Horarios
            {
                apertura = request.Apertura,
                cierre = request.Cierre,
                dias = request.Dias,
                abierto = request.Abierto,
                createdAt = DateTime.UtcNow
            };

            await _horariosRepository.AddAsync(horario);
            await _horariosRepository.SaveChangesAsync();

            // Asociar al comercio
            var comercioHorario = new ComercioHorario
            {
                ComercioIdComercio = comercioId,
                HorariosIdHorarios = horario.idhorarios
            };

            await _comercioHorarioRepository.AddAsync(comercioHorario);
            await _comercioHorarioRepository.SaveChangesAsync();

            return ToResponse(horario);
        }

        public async Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _comercioHorarioRepository.Remove(relacion);
            return await _comercioHorarioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<HorarioResponse>> GetHorariosByComercioAsync(int comercioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            var horarios = new List<Horarios>();

            foreach (var relacion in relaciones)
            {
                var horario = await _horariosRepository.GetByIdAsync(relacion.HorariosIdHorarios);
                if (horario != null)
                    horarios.Add(horario);
            }

            return horarios.Select(ToResponse);
        }

        public async Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            var horario = await _horariosRepository.GetByIdAsync(horarioId);
            if (horario == null) return false;

            horario.apertura = apertura;
            horario.cierre = cierre;
            horario.updatedAt = DateTime.UtcNow;

            _horariosRepository.Update(horario);
            return await _horariosRepository.SaveChangesAsync();
        }

        // ================================================
        // MAPPER
        // ================================================

        private static HorarioResponse ToResponse(Horarios horario)
        {
            return new HorarioResponse
            {
                IdHorario = horario.idhorarios,
                Apertura = horario.apertura,
                Cierre = horario.cierre,
                Dias = horario.dias,
                Abierto = horario.abierto
            };
        }
    }
}
