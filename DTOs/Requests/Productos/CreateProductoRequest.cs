using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class CreateProductoRequest
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string? nombre { get; set; } = string.Empty;

        public string? descripcion { get; set; }
        public string? unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }

        public bool? oferta { get; set; } = false;

        public string? fotoPortada { get; set; }

        [Required(ErrorMessage = "Debe especificar el ID de stock.")]
        public int StockIdStock { get; set; }

        public int CategoriaId { get; set; }
    }
}
