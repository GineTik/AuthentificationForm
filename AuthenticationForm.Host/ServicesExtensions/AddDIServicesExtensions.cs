using AuthentificationForm.BusinessLogic.Services.RoleServices;
using AuthentificationForm.BusinessLogic.Services.UserServices;

namespace AuthenticationForm.Host.ServicesExtensions
{
    public static class AddDIServicesExtensions
    {
        public static void AddDIServices(this IServiceCollection services) 
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            //services.AddTransient<IUnitOfWork>();
        }
    }
}