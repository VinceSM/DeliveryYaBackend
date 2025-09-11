namespace DeliveryYaBackend.DTOs.Responses.Admin
{
    public class LoginAdminResponse
    {
        public string? Token { get; set; }
        public int AdminId { get; set; }
        public string? Usuario { get; set; }
    }
}