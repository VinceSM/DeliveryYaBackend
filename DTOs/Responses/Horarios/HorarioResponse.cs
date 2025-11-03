using System;

namespace DeliveryYaBackend.DTOs.Responses.Horarios
{
    public class HorarioResponse
    {
        public int IdHorario { get; set; }
        public TimeSpan? Apertura { get; set; }
        public TimeSpan? Cierre { get; set; }
        public string? Dias { get; set; }
        public bool Abierto { get; set; }
    }
}
