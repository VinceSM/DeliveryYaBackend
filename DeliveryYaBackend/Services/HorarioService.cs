using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly IRepository<Horarios> _horarioRepository;
        private readonly IRepository<Comercio> _comercioRepository;
        private readonly IRepository<ComercioHorario> _comercioHorarioRepository;

        public HorarioService(
            IRepository<Horarios> horarioRepository,
            IRepository<Comercio> comercioRepository,
            IRepository<ComercioHorario> comercioHorarioRepository)
        {
            _horarioRepository = horarioRepository;
            _comercioRepository = comercioRepository;
            _comercioHorarioRepository = comercioHorarioRepository;
        }

        public async Task<Horarios> CreateHorarioAsync(Horarios horario)
        {
            await _horarioRepository.AddAsync(horario);
            await _horarioRepository.SaveChangesAsync();
            return horario;
        }

        public async Task<Horarios> GetHorarioByIdAsync(int id)
        {
            return await _horarioRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Horarios>> GetAllHorariosAsync()
        {
            return await _horarioRepository.GetAllAsync();
        }

        public async Task<bool> UpdateHorarioAsync(Horarios horario)
        {
            var existingHorario = await _horarioRepository.GetByIdAsync(horario.idhorarios);
            if (existingHorario == null) return false;

            _horarioRepository.Update(horario);
            return await _horarioRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteHorarioAsync(int id)
        {
            var horario = await _horarioRepository.GetByIdAsync(id);
            if (horario == null) return false;

            // Verificar si el horario está asociado a algún comercio
            var relaciones = await _comercioHorarioRepository.FindAsync(ch => ch.HorariosIdHorarios == id);
            if (relaciones.Any())
            {
                throw new Exception("No se puede eliminar el horario porque está asociado a comercios");
            }

            _horarioRepository.Remove(horario);
            return await _horarioRepository.SaveChangesAsync();
        }

        public async Task<bool> AddHorarioToComercioAsync(int comercioId, int horarioId)
        {
            var existingRelation = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            if (existingRelation.Any()) return true;

            var comercioHorario = new ComercioHorario
            {
                ComercioIdComercio = comercioId,
                HorariosIdHorarios = horarioId
            };

            await _comercioHorarioRepository.AddAsync(comercioHorario);
            return await _comercioHorarioRepository.SaveChangesAsync();
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

        public async Task<IEnumerable<Horarios>> GetHorariosByComercioAsync(int comercioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            var horarios = new List<Horarios>();

            foreach (var relacion in relaciones)
            {
                var horario = await _horarioRepository.GetByIdAsync(relacion.HorariosIdHorarios);
                if (horario != null)
                {
                    horarios.Add(horario);
                }
            }

            return horarios;
        }

        public async Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre)
        {
            var horario = await _horarioRepository.GetByIdAsync(horarioId);
            if (horario == null) return false;

            horario.apertura = apertura;
            horario.cierre = cierre;

            _horarioRepository.Update(horario);
            return await _horarioRepository.SaveChangesAsync();
        }

        public async Task<bool> CheckComercioAbiertoAsync(int comercioId)
        {
            var horarios = await GetHorariosByComercioAsync(comercioId);
            var ahora = DateTime.Now.TimeOfDay;
            var diaActual = DateTime.Now.DayOfWeek.ToString();

            return horarios.Any(h =>
                h.dias != null && h.dias.Contains(diaActual) &&
                h.apertura.HasValue && h.cierre.HasValue &&
                ahora >= h.apertura.Value && ahora <= h.cierre.Value &&
                h.abierto);
        }

        public async Task<bool> CheckHorarioValidoAsync(TimeSpan apertura, TimeSpan cierre)
        {
            return apertura < cierre;
        }

        public async Task<IEnumerable<Horarios>> GetHorariosByDiaAsync(string dia)
        {
            return await _horarioRepository.FindAsync(h =>
                h.dias != null && h.dias.Contains(dia, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> SetHorarioAbiertoAsync(int horarioId, bool abierto)
        {
            var horario = await _horarioRepository.GetByIdAsync(horarioId);
            if (horario == null) return false;

            horario.abierto = abierto;
            _horarioRepository.Update(horario);
            return await _horarioRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateDiasHorarioAsync(int horarioId, string dias)
        {
            var horario = await _horarioRepository.GetByIdAsync(horarioId);
            if (horario == null) return false;

            horario.dias = dias;
            _horarioRepository.Update(horario);
            return await _horarioRepository.SaveChangesAsync();
        }
    }
}