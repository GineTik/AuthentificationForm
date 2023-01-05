using AuthenticationForm.Core.Models;
using AuthenticationForm.Core.Models.CommandQueries;
using AuthentificationForm.DataAccess.EF;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthentificationForm.DataAccess.Repositories.EFImplemetations
{
    public class UserEFRepository : IUserRepository
    {
        private readonly UserManager<User> _manager;
        private readonly DataContext _context;

        public UserEFRepository(UserManager<User> manager, DataContext context)
        {
            _manager = manager;
            _context = context;
        }

        public User Add(UserCommandQuery entity)
        {
            var result = _manager.CreateAsync(entity.User, entity.Password).GetAwaiter().GetResult();
            
            if (result.Succeeded == false)
                throw new InvalidOperationException(String.Join(", ", result.Errors.Select(e => e.Description)));
            
            return entity.User;
        }

        public User? Get(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IList<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(UserCommandQuery entity)
        {
            var result = _manager.UpdateAsync(entity.User).GetAwaiter().GetResult();

            if (result.Succeeded == false)
                throw new InvalidOperationException(String.Join(", ", result.Errors.Select(e => e.Description)));

            return entity.User;
        }
    }
}
