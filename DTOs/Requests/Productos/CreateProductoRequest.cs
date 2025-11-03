namespace DeliveryYaBackend.DTOs.Requests.Productos
{
    public class CreateProductoRequest
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool? Oferta { get; set; }
        public string? FotoPortada { get; set; }
        public bool? Stock { get; set; }

        // Relación con categoría
        public int CategoriaId { get; set; }
    }
}
