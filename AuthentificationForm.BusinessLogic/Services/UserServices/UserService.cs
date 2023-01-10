using AuthenticationForm.Core.DTOs;
using AuthenticationForm.Core.Models;
using AuthentificationForm.BusinessLogic.Authentificator;
using AuthentificationForm.Core.Models.AuthentificationModels;
using AuthentificationForm.DataAccess.Repositories.Interfaces;

namespace AuthentificationForm.BusinessLogic.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IAuthentificator _authentificator;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IAuthentificator authentificator, IUnitOfWork unitOfWork)
        {
            _authentificator = authentificator;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User? GetLoginedUser()
        {
            return _authentificator.LoginedUser;
        }

        public User? GetUser(UserDTO dto)
        {
            return _unitOfWork.UserRepository.GetByEmail(dto.Email);
        }

        public IEnumerable<Role> GetUserRoles(UserDTO dto)
        {
            return _unitOfWork.RoleRepository.GetAllByUserEmail(dto.Email);
        }

        public AuthentificationResult Login(UserDTO dto)
        {
            return _authentificator.TryLogin(new User() { UserName = dto.Email, Email = dto.Email }, dto.Password, dto.RememberMe);
        }

        public AuthentificationResult Registration(UserDTO dto)
        {
            return _authentificator.TryRegistration(new User() { UserName = dto.Email, Email = dto.Email }, dto.Password, dto.RememberMe);
        }
    }
}
