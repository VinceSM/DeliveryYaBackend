using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Pedidos
{
    public class ItemPedidoRequest
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        public int ComercioId { get; set; }
        // NOTA: El precio se validará contra la BD al crear el pedido
    }
}