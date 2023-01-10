using AuthenticationForm.Core.Models;
using AuthenticationForm.Core.Models.CommandQueries;

namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, UserCommandQuery>
    {
        User? GetByEmail(string email);
        bool AttachRole(long userId, RoleList role);
    }
}
