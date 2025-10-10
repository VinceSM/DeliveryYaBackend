using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("comercio_has_horarios")]
    public class ComercioHorario
    {
        [Column("comercio_idcomercio")]
        public int ComercioIdComercio { get; set; }

        [Column("horarios_idhorarios")]
        public int HorariosIdHorarios { get; set; }

        [ForeignKey("ComercioIdComercio")]
        public virtual Comercio? Comercio { get; set; }

        [ForeignKey("HorariosIdHorarios")]
        public virtual Horarios? Horarios { get; set; }
    }
}
