using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("tarifaRepartidorLibre")]
    public class TarifaRepartidorLibre
    {
        public int idtarifa { get; set; }
        public int cantPedidos { get; set; }
        public decimal tarifaBase { get; set; }
        public decimal kmRecorridos { get; set; }

        [Required]
        [Column("repartidor_idrepartidor")]
        public int RepartidorIdRepartidor { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        [ForeignKey("RepartidorIdRepartidor")]
        public virtual Repartidor? Repartidor { get; set; }
    }
}