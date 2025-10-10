using DeliveryYaBackend.DTOs.Responses.Categorias;

namespace DeliveryYaBackend.DTOs.Responses.Comercios
{
    public class ComercioDetailResponse
    {
        public int Id { get; set; }
        public string? NombreComercio { get; set; }
        public string? FotoPortada { get; set; }
        public string? Celular { get; set; }
        public string? Ciudad { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? Encargado { get; set; }
        public string? CVU { get; set; }
        public string? Alias { get; set; }
        public bool Destacado { get; set; }
        public List<CategoriaResponse> Categorias { get; set; } = new List<CategoriaResponse>();
        public DateTime? CreatedAt { get; set; }
    }
}