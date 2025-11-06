using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class UpdateProductoRequest
    {
        [Required(ErrorMessage = "Debe especificar el ID del producto.")]
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool? Oferta { get; set; }
        public bool? Stock { get; set; }
        public string? FotoPortada { get; set; }
    }
}
