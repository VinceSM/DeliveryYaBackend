using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Pedidos
{
    public class UpdatePedidoRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EstadoPedidoId { get; set; }

        public bool Pagado { get; set; }
    }
}