using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class UpdateProductoRequest
    {
        [Required(ErrorMessage = "Debe especificar el ID del producto.")]
        public int idproducto { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public bool? oferta { get; set; }
        public bool? stock { get; set; }
        public string? fotoPortada { get; set; }
        public int StockIdStock { get; set; }
    }
}
