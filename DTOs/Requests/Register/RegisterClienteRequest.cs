using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Register
{
    public class RegisterClienteRequest
    {
        // Datos de IUserType
        [Required]
        [MaxLength(45)]
        public string NombreCompleto { get; set; }

        [Required]
        [MaxLength(25)]
        public string Dni { get; set; }

        [Required]
        public DateOnly Nacimiento { get; set; }

        [Required]
        [MaxLength(25)]
        public string Celular { get; set; }

        [Required]
        [MaxLength(45)]
        public string Ciudad { get; set; }

        [Required]
        [MaxLength(45)]
        public string Calle { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(45)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}