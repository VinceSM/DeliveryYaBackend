namespace DeliveryYaBackend.DTOs.Responses.Productos
{
    public class ProductoResponse
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? FotoPortada { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool? Oferta { get; set; }
        public bool? Stock { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
