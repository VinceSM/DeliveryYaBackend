using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("categoria_has_producto")]
    public class CategoriaProducto
    {
        [Column("categoria_idcategoria")]
        public int CategoriaIdCategoria { get; set; }

        [Column("producto_idproducto")]
        public int ProductoIdProducto { get; set; }

        [ForeignKey("CategoriaIdCategoria")]
        public virtual Categoria? Categoria { get; set; }

        [ForeignKey("ProductoIdProducto")]
        public virtual Producto? Producto { get; set; }
    }
}
