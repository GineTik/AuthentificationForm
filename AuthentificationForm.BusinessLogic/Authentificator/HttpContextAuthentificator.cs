using AuthenticationForm.Core.Models;
using AuthenticationForm.Core.Models.CommandQueries;
using AuthentificationForm.BusinessLogic.Authentificator.AuthentificationResults;
using AuthentificationForm.BusinessLogic.Authentificator.ClaimPrincipalFactories;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AuthentificationForm.BusinessLogic.Authentificator
{
    public class HttpContextAuthentificator : IAuthentificator
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IClaimPrincipalFactory _claimsFactory;

        public HttpContextAuthentificator(IUnitOfWork unitOfWork, HttpContextAccessor accessor, UserManager<User> userManager, IClaimPrincipalFactory claimsFactory)
        {
            _httpContext = accessor.HttpContext;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public User? LoginedUser => _userManager.GetUserAsync(_httpContext.User).GetAwaiter().GetResult();

        // ============================
        // зробити повернення якогось результату входу/реєстрації(наприклад, LoginResult або AuthentificatorResult)
        // ============================

        public AuthentificationResult Login(User user, string password, bool rememberMe)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(password);

            if (_unitOfWork.UserRepository.GetByEmail(user.Email) == null)
                return AuthentificationResult.Failure(AuthentificationErrors.UserNotFound);

            return SignInWithPassword(user, password, rememberMe);
        }

        public AuthentificationResult Registration(User user, string password, bool rememberMe)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(password);

            if (_unitOfWork.UserRepository.GetByEmail(user.Email) != null)
                return AuthentificationResult.Failure(AuthentificationErrors.UserNotFound);

            var addedUser = _unitOfWork.UserRepository.Add(new UserCommandQuery
            {
                User = user,
                Password = password,
            });

            return SignIn(addedUser, rememberMe);
        }

        public void Logout()
        {
            _httpContext.SignOutAsync(); // by default used DefaultScheme, that have value is CookieAuthenticationDefaults.AuthenticationScheme
        }

        private AuthentificationResult SignInWithPassword(User user, string password, bool rememberMe)
        {
            if (_userManager.CheckPasswordAsync(user, password).GetAwaiter().GetResult() == true)
                return SignIn(user, rememberMe);
            return AuthentificationResult.Failure(AuthentificationErrors.UserNotFound);
        }

        private AuthentificationResult SignIn(User user, bool rememberMe)
        {
            _httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                _claimsFactory.CreateClaimsPrincipal(user, _unitOfWork.RoleRepository),
                new AuthenticationProperties()
                {
                    IsPersistent = rememberMe,
                })
                .GetAwaiter().GetResult();

            return AuthentificationResult.Success(user);
        }
    }
}
