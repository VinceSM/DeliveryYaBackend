namespace DeliveryYaBackend.Models
{
    public class MetodoPagoPedido
    {
        int idmetodo { get; set; }
        string? metodo { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }
    }
}
