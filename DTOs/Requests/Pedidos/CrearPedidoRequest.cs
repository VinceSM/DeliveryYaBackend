using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Pedidos
{
    public class CrearPedidoRequest
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }

        [Required]
        public string? DireccionEnvio { get; set; }

        [Required]
        public List<ItemPedidoRequest> ? Items { get; set; }
    }
}