namespace DeliveryYaBackend.Models
{
    public class Producto
    {
        int idproducto { get; set; }
        string? nombre { get; set; }
        string? fotoPortada { get; set; }
        string? descripcion { get; set; }
        enum unidadMedida { kg, g, l, ml, u }
        decimal precioUnitario { get; set; }
        Boolean oferta { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime deletedAt { get; set; }
        Stock? stock { get; set; }
    }
}
