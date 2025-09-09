namespace DeliveryYaBackend.Models
{
    public interface IUserType
    {
        public int idusertype { get; set; }
        public string nombreCompleto { get; set; }
        public string dni { get; set; }
        public DateOnly nacimiento { get; set; }
        public string celular { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
