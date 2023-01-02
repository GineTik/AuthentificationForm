using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.Models
{
    public class Logins
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string LoginProvider { get; set; }
        [Required]
        public string ProviderKey { get; set; }
    }
}
