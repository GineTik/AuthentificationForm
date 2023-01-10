using AuthenticationForm.Core.Models;
using AuthentificationForm.Core.Models.AuthentificationModels;

namespace AuthentificationForm.BusinessLogic.Authentificator
{
    public interface IAuthentificator
    {
        User? LoginedUser { get; }
        AuthentificationResult TryLogin(User user, string password, bool rememberMe);
        AuthentificationResult TryRegistration(User user, string password, bool rememberMe);
    }
}
