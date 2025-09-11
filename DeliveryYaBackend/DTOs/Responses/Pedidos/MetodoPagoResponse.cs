namespace DeliveryYaBackend.DTOs.Responses.Pedidos
{
    public class MetodoPagoResponse
    {
        public int Id { get; set; }
        public string? Metodo { get; set; } // "Tarjeta", "Transferencia", "Efectivo"
        public DateTime? CreatedAt { get; set; }
    }
}