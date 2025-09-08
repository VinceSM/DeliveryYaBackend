namespace DeliveryYaBackend.Models
{
    public class Stock
    {
        int idstock { get; set; }
        int stock { get; set; }
        Boolean stockIlimitado { get; set; }
        enum stockMedida { kg, g, l, ml, u }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }

    }
}
