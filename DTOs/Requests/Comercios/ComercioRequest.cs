namespace DeliveryYaBackend.DTOs.Requests.Comercios
{
    public class ComercioRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoComercio { get; set; }
        public string? Eslogan { get; set; }
        public bool? DeliveryPropio { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public int Sucursales { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Encargado { get; set; }
        public string? Cvu { get; set; }
        public string? Alias { get; set; }
    }
}
