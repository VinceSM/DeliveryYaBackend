namespace DeliveryYaBackend.DTOs.Responses.Comercios
{
    public class ComercioResponse
    {
        public int Id { get; set; }
        public string? NombreComercio { get; set; }
        public string? FotoPortada { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Destacado { get; set; }
        public double? Puntuacion { get; set; }
    }
}