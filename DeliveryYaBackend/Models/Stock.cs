namespace DeliveryYaBackend.Models
{
    public class Stock
    {
        public int idstock { get; set; }
        public int stock { get; set; }
        public bool stockIlimitado { get; set; }
        public string? medida { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }

    }
}
