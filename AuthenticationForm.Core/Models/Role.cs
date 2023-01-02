using System.ComponentModel.DataAnnotations;

namespace AuthenticationForm.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
