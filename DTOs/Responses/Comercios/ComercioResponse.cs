namespace DeliveryYaBackend.DTOs.Responses.Comercios
{
    public class ComercioResponse
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? NombreComercio { get; set; }
        public string? FotoPortada { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public string? Encargado { get; set; }
        public string? Cvu { get; set; }
        public string? Alias { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Destacado { get; set; }
        public double? Puntuacion { get; set; }
    }
}