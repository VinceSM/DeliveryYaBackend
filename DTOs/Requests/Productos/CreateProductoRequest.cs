using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class CreateProductoRequest
    {
        [Required]
        [MaxLength(45)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(45)]
        public string? FotoPortada { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Required]
        [MaxLength(25)]
        public string? UnidadMedida { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }

        public bool Oferta { get; set; }

        // Datos del stock
        public int Stock { get; set; }
        public bool StockIlimitado { get; set; }
        public string? StockMedida { get; set; }

        // Categorías del producto
        public List<int> CategoriaIds { get; set; } = new List<int>();
    }
}