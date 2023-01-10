using Microsoft.AspNetCore.Identity;

namespace AuthenticationForm.Core.Models
{
    public class Role : IdentityRole<long>
    {
        public Role() { }
        public Role(string roleName) : base(roleName) { }
    }
}
