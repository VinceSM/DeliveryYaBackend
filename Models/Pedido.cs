using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("pedido")]
    public class Pedido
    {
        public int idpedido { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public string? codigo { get; set; }
        public bool pagado { get; set; }
        public bool comercioRepartidor { get; set; }
        public decimal subtotalPedido { get; set; }

        [Required]
        [Column("cliente_idcliente")]
        public int ClienteIdCliente { get; set; }

        [Required]
        [Column("estadopedido_idestado")]
        public int EstadoPedidoIdEstado { get; set; }

        [Required]
        [Column("metodoPagoPedido_idmetodo")]
        public int MetodoPagoPedidoIdMetodo { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ClienteIdCliente")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("EstadoPedidoIdEstado")]
        public virtual EstadoPedido? EstadoPedido { get; set; }

        [ForeignKey("MetodoPagoPedidoIdMetodo")]
        public virtual MetodoPagoPedido? MetodoPagoPedido { get; set; }

        public virtual ICollection<ItemPedido>? ItemsPedido { get; set; }
    }
}