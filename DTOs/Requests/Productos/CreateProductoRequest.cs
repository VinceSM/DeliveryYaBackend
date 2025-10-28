namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class CreateProductoRequest
    {
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public bool? oferta { get; set; }
        public string? fotoPortada { get; set; }
        public bool? stock { get; set; }

        // Relación con categoría
        public int CategoriaId { get; set; }
    }
}
