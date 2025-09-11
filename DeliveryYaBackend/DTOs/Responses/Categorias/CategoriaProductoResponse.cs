using DeliveryYaBackend.DTOs.Responses.Productos;

namespace DeliveryYaBackend.DTOs.Responses.Categorias
{
    public class CategoriaProductoResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public List<ProductoResponse> Productos { get; set; } = new List<ProductoResponse>();
        public DateTime? CreatedAt { get; set; }
    }
}