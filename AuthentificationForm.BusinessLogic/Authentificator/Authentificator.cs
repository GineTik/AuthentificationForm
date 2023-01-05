using AuthenticationForm.Core.Models;
using AuthenticationForm.Core.Models.CommandQueries;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AuthentificationForm.BusinessLogic.Authentificator
{
    public class Authentificator : IAuthentificator
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;

        public Authentificator(IUnitOfWork unitOfWork, HttpContextAccessor accessor, SignInManager<User> signInManager)
        {
            _httpContext = accessor.HttpContext;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        public User? LoginedUser => _signInManager.UserManager.GetUserAsync(_httpContext.User).GetAwaiter().GetResult();

        // ============================
        // зробити повернення якогось результату входу/реєстрації(наприклад, LoginResult або AuthentificatorResult)
        // ============================

        public void Login(User user, string password, bool rememberMe)
        {
            var loginedUser = _unitOfWork.UserRepository.GetByEmail(user.Email);
            if (loginedUser == null) 
                throw new InvalidOperationException(nameof(user) + " is not exists");

            _signInManager.PasswordSignInAsync(user, password, rememberMe, false).GetAwaiter().GetResult();
        }

        public void Registration(User user, string password, bool rememberMe)
        {
            if (_unitOfWork.UserRepository.GetByEmail(user.Email) != null)
                throw new InvalidOperationException(nameof(user) + " is exists");

            _unitOfWork.UserRepository.Add(new UserCommandQuery
            {
                User = user,
                Password = password,
            });

            _signInManager.SignInAsync(user, rememberMe).GetAwaiter().GetResult();
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}
