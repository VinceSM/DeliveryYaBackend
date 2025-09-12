using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Horarios
{
    public class CreateHorarioRequest
    {
        public TimeSpan? Apertura { get; set; }

        public TimeSpan? Cierre { get; set; }

        [MaxLength(25)]
        public string? Dias { get; set; }

        [Required]
        public bool Abierto { get; set; }
    }
}