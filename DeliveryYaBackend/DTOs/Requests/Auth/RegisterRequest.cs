// DTOs/Requests/Auth/RegisterRequest.cs
using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Auth
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(45)]
        public string? NombreCompleto { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Dni { get; set; }

        [Required]
        public DateTime Nacimiento { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Celular { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Ciudad { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Calle { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(45)]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string? Password { get; set; }

        // Solo para repartidores
        public string? TipoVehiculo { get; set; }
        public string? Patente { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public bool Seguro { get; set; }
        public string? CompaniaSeguros { get; set; }
        public string? Cvu { get; set; }
    }
}