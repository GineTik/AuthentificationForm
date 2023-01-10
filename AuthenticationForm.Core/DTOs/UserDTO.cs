using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.DTOs
{
    public class UserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
