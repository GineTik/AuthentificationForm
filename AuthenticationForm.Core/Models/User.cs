using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.Models
{
    public class User : IdentityUser<long>
    {
        [Required]
        public DateTime DateOfRegistration { get; set; }
    }
}
