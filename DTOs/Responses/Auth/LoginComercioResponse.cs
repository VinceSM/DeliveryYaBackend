namespace DeliveryYaBackend.DTOs.Responses.Auth
{
    public class LoginComercioResponse
    {
        public string? Token { get; set; }
        public int ComercioId { get; set; }      // idcomercio
        public string? NombreComercio { get; set; }
        public string? Email { get; set; }
        public string? Encargado { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? CVU { get; set; }
        public string? Alias { get; set; }
        public bool Destacado { get; set; }
    }
}