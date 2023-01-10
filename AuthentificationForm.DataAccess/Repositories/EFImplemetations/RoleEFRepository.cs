using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.EF;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AuthentificationForm.DataAccess.Repositories.EFImplemetations
{
    public class RoleEFRepository : IRoleRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleEFRepository(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Role Add(Role entity)
        {
            var role = GetByName(entity.Name);
            if (role != null)
                return role;

            _roleManager.CreateAsync(entity).GetAwaiter().GetResult();
            return entity;
        }

        public Role? Get(long id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles;
        }

        public IEnumerable<Role> GetAllByUserEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            ArgumentNullException.ThrowIfNull(user);

            return _userManager.GetRolesAsync(user).GetAwaiter().GetResult().Select(x => new Role(x));
        }

        public IEnumerable<Role> GetAllByUserId(long id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            ArgumentNullException.ThrowIfNull(user);

            return _userManager.GetRolesAsync(user).GetAwaiter().GetResult().Select(x => new Role(x));
        }

        public Role? GetByName(RoleList role)
        {
            return GetByName(role.ToString());
        }

        public Role Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Role Update(Role entity)
        {
            _roleManager.UpdateAsync(entity).GetAwaiter().GetResult();
            return entity;
        }

        private Role? GetByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == roleName);
        }
    }
}
