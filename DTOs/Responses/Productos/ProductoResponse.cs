namespace DeliveryYaBackend.DTOs.Responses.Productos
{
    public class ProductoResponse
    {
        public int idproducto { get; set; }
        public string? nombre { get; set; }
        public string? fotoPortada { get; set; }
        public string? descripcion { get; set; }
        public string? unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public bool? oferta { get; set; }
        public bool? stock { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
