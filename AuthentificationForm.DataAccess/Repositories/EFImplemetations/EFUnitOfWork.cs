using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.EF;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthentificationForm.DataAccess.Repositories.EFImplemetations
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public EFUnitOfWork(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public IUserRepository UserRepository => _userRepository ??= new UserEFRepository(_userManager, _context);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleEFRepository(_context, _userManager, _roleManager);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
