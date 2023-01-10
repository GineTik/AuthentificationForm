using AuthenticationForm.Core.Models;

namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role? GetByName(RoleList role);
        IEnumerable<Role> GetAllByUserId(long id);
        IEnumerable<Role> GetAllByUserEmail(string email);
    }
}
