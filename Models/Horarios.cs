using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("horarios")]
    public class Horarios
    {
        public int idhorarios { get; set; }
        public TimeSpan? apertura { get; set; }
        public TimeSpan? cierre { get; set; }
        public string? dias { get; set; }
        public bool abierto { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

    }
}