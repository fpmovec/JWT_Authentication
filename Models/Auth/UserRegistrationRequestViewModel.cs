using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthorization.Models.Auth
{
    public class UserRegistrationRequestViewModel
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(5)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
