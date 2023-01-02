using AuthenticationForm.Core.Models;

namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
