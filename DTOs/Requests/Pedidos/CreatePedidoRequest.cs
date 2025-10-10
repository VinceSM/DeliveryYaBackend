using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Pedidos
{
    public class CreatePedidoRequest
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int RepartidorId { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }

        [Required]
        public List<ItemPedidoRequest> Items { get; set; } = new List<ItemPedidoRequest>();

        public bool ComercioRepartidor { get; set; }
    }
}