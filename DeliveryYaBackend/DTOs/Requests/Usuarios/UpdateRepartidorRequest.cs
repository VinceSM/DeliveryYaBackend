using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Usuarios
{
    public class UpdateRepartidorRequest
    {
        [Required]
        public int Id { get; set; }

        // Datos de IUserType
        [Required]
        [MaxLength(45)]
        public string? NombreCompleto { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Dni { get; set; }

        [Required]
        public DateOnly Nacimiento { get; set; }

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

        // Datos específicos de Repartidor
        [Required]
        [MaxLength(25)]
        public string? Cvu { get; set; }

        public bool LibreRepartidor { get; set; }

        // Datos del Vehículo
        [Required]
        [MaxLength(45)]
        public string? TipoVehiculo { get; set; }

        [MaxLength(45)]
        public string? Patente { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Modelo { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Marca { get; set; }

        [Required]
        public bool Seguro { get; set; }

        [MaxLength(45)]
        public string? CompaniaSeguros { get; set; }
    }
}