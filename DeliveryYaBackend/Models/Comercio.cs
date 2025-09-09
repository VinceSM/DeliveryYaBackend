using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("comercio")]
    public class Comercio
    {
        public int idcomercio { get; set; }
        public string? nombreComercio { get; set; }
        public string? fotoPortada { get; set; }
        public string? celular { get; set; }
        public string? ciudad { get; set; }
        public string? calle { get; set; }
        public int numero { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public string? encargado { get; set; }
        public string? cvu { get; set; }
        public string? alias { get; set; }
        public bool destacado { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<ItemPedido>? ItemsPedido { get; set; }
        public virtual ICollection<ComercioCategoria>? ComercioCategorias { get; set; }
        public virtual ICollection<ComercioHorario>? ComercioHorarios { get; set; }
    }
}