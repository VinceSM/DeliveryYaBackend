using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Horarios
{
    public class CreateHorarioRequest
    {
        public TimeOnly? Apertura { get; set; }

        public TimeOnly? Cierre { get; set; }

        [MaxLength(25)]
        public string? Dias { get; set; }

        [Required]
        public bool Abierto { get; set; }
    }
}