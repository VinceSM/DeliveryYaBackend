using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("producto")]
    public class Producto
    {
        public int idproducto { get; set; }
        public string? nombre { get; set; }
        public string? fotoPortada { get; set; }
        public string? descripcion { get; set; }
        public string? unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public bool? oferta { get; set; }
        public bool? stock { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

    }
}