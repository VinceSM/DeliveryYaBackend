using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Login
{
    public class LoginAdminRequest
    {
        [Required]
        [EmailAddress]
        public string? Usuario { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
