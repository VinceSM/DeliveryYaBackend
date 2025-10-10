using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("categoria")]
    public class Categoria
    {
        public int idcategoria { get; set; }
        public string? nombre { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<CategoriaProducto>? CategoriaProductos { get; set; }
        public virtual ICollection<ComercioCategoria>? ComercioCategorias { get; set; }
    }
}