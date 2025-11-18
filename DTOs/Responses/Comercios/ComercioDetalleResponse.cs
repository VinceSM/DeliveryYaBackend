using DeliveryYaBackend.DTOs.Responses.Categorias;

namespace DeliveryYaBackend.DTOs.Responses.Comercios
{
    public class ComercioDetalleResponse
    {
        public int IdComercio { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoComercio { get; set; }
        public string? Eslogan { get; set; }
        public string? FotoPortada { get; set; }
        public decimal? Envio { get; set; }
        public bool? DeliveryPropio { get; set; }
        public bool? PagoEfectivo { get; set; }
        public bool? PagoTarjeta { get; set; }
        public bool? PagoTransferencia { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public int Sucursales { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Encargado { get; set; }
        public string? CVU { get; set; }
        public string? Alias { get; set; }
        public bool Destacado { get; set; }
        public decimal Comision { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}