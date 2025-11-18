using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Pedidos
{
    public class ActualizarEstadoPedidoRequest
    {
        [Required]
        public int EstadoPedidoId { get; set; }
    }
}
