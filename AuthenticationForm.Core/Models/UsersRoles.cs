using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.Models
{
    public class UsersRoles
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
