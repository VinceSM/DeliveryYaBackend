using System.ComponentModel.DataAnnotations;

namespace DeliveryYaBackend.DTOs.Requests.Admin
{
    public class LoginAdminRequest
    {
        [Required]
        [MaxLength(50)]
        public string? Usuario { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string? Password { get; set; }
    }
}