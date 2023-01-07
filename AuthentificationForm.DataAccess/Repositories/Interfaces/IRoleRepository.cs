using AuthenticationForm.Core.Models;

namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
        IList<Role> GetAllByUserId(long id);
    }
}
