using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(3)]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public bool IsEmailConfirmed { get; set; }

        [Required]
        public bool IsBlocked { get; set; }
        public DateTime? BlockedEnd { get; set; }

        [Required]
        public DateTime DateOfRegistration { get; set; }

        public IList<Role> Roles { get; set; }
    }
}
