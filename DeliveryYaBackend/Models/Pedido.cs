namespace DeliveryYaBackend.Models
{
    public class Pedido
    {
        int idpedido { get; set; }
        DateOnly fecha { get; set; }
        TimeOnly hora { get; set; }
        string? codigo { get; set; }
        Boolean pagado { get; set; }
        Boolean comercioRepartidor { get; set; }
        decimal subtotalPedido { get; set; }
        Cliente? cliente { get; set; }
        Repartidor? repartidor { get; set; }
        EstadoPedido? estadoPedido { get; set; }
        MetodoPagoPedido? metodoPagoPedido { get; set; }

        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }
    }
}
