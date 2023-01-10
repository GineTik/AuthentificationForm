using AuthenticationForm.Core.Models;
using AuthenticationForm.Core.Models.CommandQueries;
using AuthentificationForm.Core.Models.AuthentificationModels;
using AuthentificationForm.BusinessLogic.Authentificator.ClaimPrincipalFactories;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
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
        private readonly SignInManager<User> _signInManager;

        public HttpContextAuthentificator(IUnitOfWork unitOfWork, IHttpContextAccessor accessor, UserManager<User> userManager, IClaimPrincipalFactory claimsFactory, SignInManager<User> signInManager)
        {
            _httpContext = accessor.HttpContext;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _signInManager = signInManager;
        }

        public User? LoginedUser => _userManager.GetUserAsync(_httpContext.User).GetAwaiter().GetResult();

        public AuthentificationResult TryLogin(User user, string password, bool rememberMe)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(password);

            var loginedUser = _unitOfWork.UserRepository.GetByEmail(user.Email);
            if (loginedUser == null)
                return AuthentificationResult.Failure(AuthentificationResponses.UserNotFound);

            return SignInWithPassword(loginedUser, password, rememberMe);
        }

        public AuthentificationResult TryRegistration(User user, string password, bool rememberMe)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(password);

            if (_unitOfWork.UserRepository.GetByEmail(user.Email) != null)
                return AuthentificationResult.Failure(AuthentificationResponses.UserIsExists);

            var addedUser = _unitOfWork.UserRepository.Add(new UserCommandQuery
            {
                User = user,
                Password = password,
            });
            bool result = _unitOfWork.UserRepository.AttachRole(addedUser.Id, RoleList.User);

            if (result == false)
                throw new InvalidOperationException("failed attach role to user");

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
            return AuthentificationResult.Failure(AuthentificationResponses.UserNotFound);
        }

        private AuthentificationResult SignIn(User user, bool rememberMe)
        {
            var claimsPrincipal = _claimsFactory.CreateClaimsPrincipal(user, _unitOfWork.RoleRepository);

            _httpContext.SignInAsync(
                claimsPrincipal,
                new AuthenticationProperties()
                {
                    IsPersistent = rememberMe,
                })
                .GetAwaiter().GetResult();

            return AuthentificationResult.Success(user);
        }
    }
}
