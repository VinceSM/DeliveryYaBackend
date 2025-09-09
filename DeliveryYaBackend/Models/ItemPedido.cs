namespace DeliveryYaBackend.Models
{
    public class ItemPedido
    {
        public int iditemPedido { get; set; }
        public int cantProducto { get; set; }
        public decimal precioFinal { get; set; }
        public decimal total { get; set; }
        public int ProductoIdProducto { get; set; }
        public int PedidoIdPedido { get; set; }
        public int ComercioIdComercio { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public DateTime? updatedAt { get; set; }

        // Navigation properties
        public Producto? Producto { get; set; }
        public Pedido? Pedido { get; set; }
        public Comercio? Comercio { get; set; }
    }
}
