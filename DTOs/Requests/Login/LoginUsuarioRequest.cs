using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Login
{
    public class LoginUsuarioRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}