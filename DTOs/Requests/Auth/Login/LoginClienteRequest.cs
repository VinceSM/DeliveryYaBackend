using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Auth.Login
{
    public class LoginClienteRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}