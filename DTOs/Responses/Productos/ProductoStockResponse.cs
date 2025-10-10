namespace DeliveryYaBackend.DTOs.Responses.Productos
{
    public class ProductoStockResponse
    {
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public int StockActual { get; set; }
        public bool StockIlimitado { get; set; }
        public string? UnidadMedida { get; set; }
        public DateTime? UltimaActualizacion { get; set; }
    }
}