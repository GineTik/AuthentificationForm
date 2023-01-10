using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AuthentificationForm.BusinessLogic.Authentificator.ClaimPrincipalFactories
{
    public class CookieClaimsFactory : IClaimPrincipalFactory
    {
        public ClaimsPrincipal CreateClaimsPrincipal(User user, IRoleRepository repository)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ClaimTypes.NameIdentifier for UserManager.GetUserAsync
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, repository.GetAllByUserId(user.Id).Last().Name),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
