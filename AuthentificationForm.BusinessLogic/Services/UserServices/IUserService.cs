using AuthenticationForm.Core.DTOs;
using AuthenticationForm.Core.Models;
using AuthentificationForm.Core.Models.AuthentificationModels;

namespace AuthentificationForm.BusinessLogic.Services.UserServices
{
    public interface IUserService
    {
        AuthentificationResult Login(UserDTO dto);
        AuthentificationResult Registration(UserDTO dto);
        User? GetLoginedUser();
        User? GetUser(UserDTO dto);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Role> GetUserRoles(UserDTO dto);
    }
}
