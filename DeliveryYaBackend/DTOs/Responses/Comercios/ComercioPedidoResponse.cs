namespace DeliveryYaBackend.DTOs.Responses.Comercios
{
    public class ComercioInfoResponse
    {
        public int Id { get; set; }
        public string? NombreComercio { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}