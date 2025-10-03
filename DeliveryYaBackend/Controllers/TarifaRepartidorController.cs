using DeliveryYaBackend.Models;
using DeliveryYaBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryYaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaRepartidorController : ControllerBase
    {
        private readonly ITarifaRepartidorService _tarifaService;
        private readonly ILogger<TarifaRepartidorController> _logger;

        public TarifaRepartidorController(
            ITarifaRepartidorService tarifaService,
            ILogger<TarifaRepartidorController> logger)
        {
            _tarifaService = tarifaService;
            _logger = logger;
        }

        // ======================
        // CRUD BÁSICO
        // ======================

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarifaRepartidorLibre tarifa)
        {
            var created = await _tarifaService.CreateTarifaAsync(tarifa);
            return CreatedAtAction(nameof(GetById), new { id = created.idtarifa }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarifa = await _tarifaService.GetTarifaByIdAsync(id);
            return tarifa == null ? NotFound() : Ok(tarifa);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarifas = await _tarifaService.GetAllTarifasAsync();
            return Ok(tarifas);
        }

        [HttpGet("repartidor/{repartidorId}")]
        public async Task<IActionResult> GetByRepartidor(int repartidorId)
        {
            var tarifas = await _tarifaService.GetTarifasByRepartidorAsync(repartidorId);
            return Ok(tarifas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TarifaRepartidorLibre tarifa)
        {
            if (id != tarifa.idtarifa)
                return BadRequest("El ID de la tarifa no coincide");

            var updated = await _tarifaService.UpdateTarifaAsync(tarifa);
            return updated ? Ok("Tarifa actualizada correctamente") : NotFound("Tarifa no encontrada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tarifaService.DeleteTarifaAsync(id);
            return deleted ? Ok("Tarifa eliminada correctamente") : NotFound("Tarifa no encontrada");
        }

        // ======================
        // CÁLCULOS
        // ======================

        [HttpGet("calcular/envio")]
        public async Task<IActionResult> CalcularEnvio(int repartidorId, decimal kmRecorridos, int cantPedidos)
        {
            var total = await _tarifaService.CalcularTarifaEnvioAsync(repartidorId, kmRecorridos, cantPedidos);
            return Ok(total);
        }

        [HttpGet("calcular/base/{repartidorId}")]
        public async Task<IActionResult> CalcularBase(int repartidorId)
        {
            var baseTarifa = await _tarifaService.CalcularTarifaBaseAsync(repartidorId);
            return Ok(baseTarifa);
        }

        [HttpGet("calcular/km")]
        public async Task<IActionResult> CalcularPorKm(int repartidorId, decimal kmRecorridos)
        {
            var tarifaKm = await _tarifaService.CalcularTarifaPorKmAsync(repartidorId, kmRecorridos);
            return Ok(tarifaKm);
        }

        // ======================
        // GESTIÓN DE TARIFAS POR REPARTIDOR
        // ======================

        [HttpGet("repartidor/{repartidorId}/actual")]
        public async Task<IActionResult> GetTarifaActual(int repartidorId)
        {
            var tarifa = await _tarifaService.GetTarifaActualByRepartidorAsync(repartidorId);
            return tarifa == null ? NotFound("No hay tarifa actual para este repartidor") : Ok(tarifa);
        }

        [HttpPost("repartidor/{repartidorId}/set")]
        public async Task<IActionResult> SetTarifa(int repartidorId, decimal tarifaBase, decimal tarifaKm)
        {
            var ok = await _tarifaService.SetTarifaRepartidorAsync(repartidorId, tarifaBase, tarifaKm);
            return ok ? Ok("Tarifa asignada al repartidor") : BadRequest("Error al asignar la tarifa");
        }

        [HttpPut("repartidor/{repartidorId}/update")]
        public async Task<IActionResult> UpdateTarifaRepartidor(int repartidorId, decimal nuevaTarifaBase, decimal nuevaTarifaKm)
        {
            var ok = await _tarifaService.UpdateTarifaRepartidorAsync(repartidorId, nuevaTarifaBase, nuevaTarifaKm);
            return ok ? Ok("Tarifa del repartidor actualizada") : NotFound("Repartidor no encontrado o tarifa inválida");
        }

        // ======================
        // REPORTES / ESTADÍSTICAS
        // ======================

        [HttpGet("repartidor/{repartidorId}/ganancias")]
        public async Task<IActionResult> Ganancias(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var total = await _tarifaService.GetGananciasTotalesRepartidorAsync(repartidorId, startDate, endDate);
            return Ok(total);
        }

        [HttpGet("repartidor/{repartidorId}/viajes")]
        public async Task<IActionResult> CantidadViajes(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var cantidad = await _tarifaService.GetCantidadViajesRepartidorAsync(repartidorId, startDate, endDate);
            return Ok(cantidad);
        }

        [HttpGet("repartidor/{repartidorId}/km")]
        public async Task<IActionResult> KmTotales(int repartidorId, DateTime startDate, DateTime endDate)
        {
            var km = await _tarifaService.GetKmTotalesRepartidorAsync(repartidorId, startDate, endDate);
            return Ok(km);
        }
    }
}
