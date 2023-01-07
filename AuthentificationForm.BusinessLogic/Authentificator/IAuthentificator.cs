using AuthenticationForm.Core.Models;
using AuthentificationForm.BusinessLogic.Authentificator.AuthentificationResults;

namespace AuthentificationForm.BusinessLogic.Authentificator
{
    public interface IAuthentificator
    {
        User? LoginedUser { get; }
        AuthentificationResult Login(User user, string password, bool rememberMe);
        AuthentificationResult Registration(User user, string password, bool rememberMe);
    }
}
