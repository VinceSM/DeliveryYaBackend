using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class ComercioHorariosService : IComercioHorariosService
    {
        private readonly IRepository<ComercioHorario> _comercioHorarioRepository;
        private readonly IRepository<Horarios> _horariosRepository;

        public ComercioHorariosService(
            IRepository<ComercioHorario> comercioHorarioRepository,
            IRepository<Horarios> horariosRepository)
        {
            _horariosRepository = horariosRepository;
            _comercioHorarioRepository = comercioHorarioRepository;
        }

        // ✅ Asigna un horario a un comercio (evita duplicados)
        public async Task<bool> AddHorarioToComercioAsync(int comercioId, int horarioId)
        {
            var existe = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            if (existe.Any()) return true;

            var nuevaRelacion = new ComercioHorario
            {
                ComercioIdComercio = comercioId,
                HorariosIdHorarios = horarioId
            };

            await _comercioHorarioRepository.AddAsync(nuevaRelacion);
            return await _comercioHorarioRepository.SaveChangesAsync();
        }

        // ✅ Elimina un horario asignado a un comercio
        public async Task<bool> RemoveHorarioFromComercioAsync(int comercioId, int horarioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId);

            var relacion = relaciones.FirstOrDefault();
            if (relacion == null) return false;

            _comercioHorarioRepository.Remove(relacion);
            return await _comercioHorarioRepository.SaveChangesAsync();
        }

        // ✅ Obtiene todos los horarios asignados a un comercio
        public async Task<IEnumerable<Horarios>> GetHorariosByComercioAsync(int comercioId)
        {
            var relaciones = await _comercioHorarioRepository.FindAsync(ch => ch.ComercioIdComercio == comercioId);

            var horarios = new List<Horarios>();
            foreach (var relacion in relaciones)
            {
                var horario = await _horariosRepository.GetByIdAsync(relacion.HorariosIdHorarios);
                if (horario != null)
                    horarios.Add(horario);
            }

            return horarios;
        }

        // ✅ Verifica si el comercio está abierto en este momento
        public async Task<bool> CheckComercioAbiertoAsync(int comercioId)
        {
            var horarios = await GetHorariosByComercioAsync(comercioId);
            var ahora = DateTime.Now.TimeOfDay;
            var diaActual = DateTime.Now.DayOfWeek.ToString();

            return horarios.Any(h =>
                h.dias != null &&
                h.dias.Contains(diaActual, StringComparison.OrdinalIgnoreCase) && // mejora: case-insensitive
                h.apertura.HasValue && h.cierre.HasValue &&
                ahora >= h.apertura.Value &&
                ahora <= h.cierre.Value &&
                h.abierto);
        }

        // ✅ Actualiza el horario de un comercio (sin recrear la relación)
        public async Task<bool> UpdateHorarioComercioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre)
        {
            var relacion = (await _comercioHorarioRepository.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId)).FirstOrDefault();

            if (relacion == null) return false;

            var horario = await _horariosRepository.GetByIdAsync(horarioId);
            if (horario == null) return false;

            horario.apertura = apertura;
            horario.cierre = cierre;
            horario.updatedAt = DateTime.UtcNow;

            _horariosRepository.Update(horario);
            return await _horariosRepository.SaveChangesAsync();
        }
    }
}
