using DeliveryYaBackend.DTOs.Responses.Categorias;

namespace DeliveryYaBackend.DTOs.Responses.Productos
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? FotoPortada { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Oferta { get; set; }
        public int StockDisponible { get; set; }
        public bool StockIlimitado { get; set; }
        public List<CategoriaResponse> Categorias { get; set; } = new List<CategoriaResponse>();
        public DateTime? CreatedAt { get; set; }
    }
}