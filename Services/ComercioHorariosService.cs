using DeliveryYaBackend.Models;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Services.Interfaces;

namespace DeliveryYaBackend.Services
{
    public class ComercioHorariosService : IComercioHorariosService
    {
        private readonly IRepository<ComercioHorario> _comercioHorarioRepo;
        private readonly IRepository<Horarios> _horariosRepo;
        private readonly IRepository<Comercio> _comercioRepo;

        public ComercioHorariosService(
            IRepository<ComercioHorario> comercioHorarioRepo,
            IRepository<Horarios> horariosRepo,
            IRepository<Comercio> comercioRepo)
        {
            _comercioHorarioRepo = comercioHorarioRepo;
            _horariosRepo = horariosRepo;
            _comercioRepo = comercioRepo;
        }

        // ✅ Obtener todos los horarios de un comercio
        public async Task<IEnumerable<Horarios>> GetHorariosPorComercioAsync(int comercioId)
        {
            var relaciones = await _comercioHorarioRepo.FindAsync(ch => ch.ComercioIdComercio == comercioId);
            var horarios = new List<Horarios>();

            foreach (var rel in relaciones)
            {
                var horario = await _horariosRepo.GetByIdAsync(rel.HorariosIdHorarios);
                if (horario != null && horario.deletedAt == null)
                    horarios.Add(horario);
            }

            return horarios;
        }

        // ✅ Obtener un horario específico del comercio
        public async Task<Horarios?> GetHorarioPorIdAsync(int comercioId, int horarioId)
        {
            var relacion = (await _comercioHorarioRepo.FindAsync(ch =>
                ch.ComercioIdComercio == comercioId && ch.HorariosIdHorarios == horarioId)).FirstOrDefault();

            if (relacion == null) return null;

            return await _horariosRepo.GetByIdAsync(horarioId);
        }

        // ✅ Crear un horario nuevo para un comercio
        public async Task<Horarios> CrearHorarioParaComercioAsync(int comercioId, Horarios nuevoHorario)
        {
            // Validar que el comercio exista
            var comercio = await _comercioRepo.GetByIdAsync(comercioId);
            if (comercio == null)
                throw new ArgumentException("El comercio especificado no existe.");

            // Asignar datos
            nuevoHorario.createdAt = DateTime.UtcNow;
            nuevoHorario.abierto = true;

            await _horariosRepo.AddAsync(nuevoHorario);
            await _horariosRepo.SaveChangesAsync();

            // Crear relación
            var relacion = new ComercioHorario
            {
                ComercioIdComercio = comercioId,
                HorariosIdHorarios = nuevoHorario.idhorarios
            };

            await _comercioHorarioRepo.AddAsync(relacion);
            await _comercioHorarioRepo.SaveChangesAsync();

            return nuevoHorario;
        }

        // ✅ Actualizar horario (solo si pertenece al comercio)
        public async Task<bool> ActualizarHorarioAsync(int comercioId, int horarioId, TimeSpan apertura, TimeSpan cierre, bool abierto)
        {
            var horario = await GetHorarioPorIdAsync(comercioId, horarioId);
            if (horario == null) return false;

            horario.apertura = apertura;
            horario.cierre = cierre;
            horario.abierto = abierto;
            horario.updatedAt = DateTime.UtcNow;

            _horariosRepo.Update(horario);
            return await _horariosRepo.SaveChangesAsync();
        }

        // ✅ Eliminar horario (lógico)
        public async Task<bool> EliminarHorarioAsync(int comercioId, int horarioId)
        {
            var horario = await GetHorarioPorIdAsync(comercioId, horarioId);
            if (horario == null) return false;

            horario.deletedAt = DateTime.UtcNow;
            _horariosRepo.Update(horario);
            return await _horariosRepo.SaveChangesAsync();
        }

        // ✅ Verificar si el comercio está abierto actualmente
        public async Task<bool> CheckComercioAbiertoAsync(int comercioId)
        {
            var horarios = await GetHorariosPorComercioAsync(comercioId);
            var ahora = DateTime.Now.TimeOfDay;
            var diaActual = DateTime.Now.DayOfWeek.ToString();

            return horarios.Any(h =>
                h.abierto &&
                h.dias != null &&
                h.dias.Contains(diaActual, StringComparison.OrdinalIgnoreCase) &&
                h.apertura <= ahora &&
                h.cierre >= ahora);
        }
    }
}
