using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("comercio_has_categoria")]
    public class ComercioCategoria
    {
        [Column("comercio_idcomercio")]
        public int ComercioIdComercio { get; set; }

        [Column("categoria_idcategoria")]
        public int CategoriaIdCategoria { get; set; }

        [ForeignKey("ComercioIdComercio")]
        public virtual Comercio? Comercio { get; set; }

        [ForeignKey("CategoriaIdCategoria")]
        public virtual Categoria? Categoria { get; set; }
    }
}
