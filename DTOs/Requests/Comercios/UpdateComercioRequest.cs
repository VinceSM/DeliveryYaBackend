using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Comercios
{
    public class UpdateComercioRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string? NombreComercio { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(45)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Required]
        public decimal Envio { get; set; }

        [Required]
        public bool DeliveryPropio { get; set; }

        [MaxLength(45)]
        public string? FotoPortada { get; set; }

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
        public decimal Latitud { get; set; }

        [Required]
        public decimal Longitud { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Encargado { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Cvu { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Alias { get; set; }

        public bool Destacado { get; set; }

        public string? Password { get; set; }
    }
}