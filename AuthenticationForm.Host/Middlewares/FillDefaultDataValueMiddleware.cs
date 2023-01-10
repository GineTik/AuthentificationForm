using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.Repositories.Interfaces;

namespace AuthenticationForm.Host.Middlewares
{
    public class FillDefaultDataValueMiddleware
    {
        private readonly RequestDelegate _next;

        public FillDefaultDataValueMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            await AddRoles(unitOfWork.RoleRepository);
            await _next.Invoke(context);
        }

        public async Task AddRoles(IRoleRepository repository)
        {
            foreach (var role in Enum.GetValues(typeof(RoleList)))
                repository.Add(new Role { Name = role.ToString() });
        }
    }
}
