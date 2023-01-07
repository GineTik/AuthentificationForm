using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using System.Security.Claims;

namespace AuthentificationForm.BusinessLogic.Authentificator.ClaimPrincipalFactories
{
    public interface IClaimPrincipalFactory
    {
        ClaimsPrincipal CreateClaimsPrincipal(User user, IRoleRepository repository);
    }
}
