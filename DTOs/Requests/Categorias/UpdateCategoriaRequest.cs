using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Categorias
{
    public class UpdateCategoriaRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Nombre { get; set; }
    }
}