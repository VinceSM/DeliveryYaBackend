using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Categorias
{
    public class CreateCategoriaRequest
    {
        [Required]
        [MaxLength(45)]
        public string? Nombre { get; set; }
    }
}