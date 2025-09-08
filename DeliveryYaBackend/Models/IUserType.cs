namespace DeliveryYaBackend.Models
{
    public interface IUserType
    {
        int idusertype { get; set; }
        string nombreCompleto { get; set; }
        string dni { get; set; }
        DateOnly nacimiento { get; set; }
        string celular { get; set; }
        string ciudad { get; set; }
        string calle { get; set; }
        int numero { get; set; }
        string email { get; set; }
        string password { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
        DateTime? deletedAt { get; set; }
    }
}
