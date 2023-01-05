using AuthenticationForm.Core.Models;

namespace AuthentificationForm.BusinessLogic.Authentificator
{
    public interface IAuthentificator
    {
        User? LoginedUser { get; }
        void Login(User user, string password, bool rememberMe);
        void Registration(User user, string password, bool rememberMe);
    }
}
