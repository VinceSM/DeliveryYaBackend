using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryYaBackend.Models
{
    [Table("repartidor")]
    public class Repartidor
    {
        public int IdRepartidor { get; set; }
        public int cantPedidos { get; set; }
        public decimal puntuacion { get; set; }
        public string? cvu { get; set; }
        public bool libreRepartidor { get; set; }
        public int IUserTypeIdIUserType { get; set; }
        public int VehiculoIdVehiculo { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        [ForeignKey("IUserTypeIdIUserType")]
        public virtual IUserType? IUserType { get; set; }

        [ForeignKey("VehiculoIdVehiculo")]
        public virtual Vehiculo? Vehiculo { get; set; }
    }
}