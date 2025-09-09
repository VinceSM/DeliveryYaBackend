using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("cliente")]
    public class Cliente
    {
        public int idcliente { get; set; }
        public int IUserTypeIdIUserType { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation property
        [ForeignKey("IUserTypeIdIUserType")]
        public virtual IUserType? IUserType { get; set; }
    }
}