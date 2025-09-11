namespace DeliveryYaBackend.DTOs.Responses.Categorias
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CantidadProductos { get; set; }
    }
}