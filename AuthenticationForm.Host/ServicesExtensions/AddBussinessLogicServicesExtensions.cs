using AuthentificationForm.BusinessLogic.Authentificator.ClaimPrincipalFactories;
using AuthentificationForm.BusinessLogic.Authentificator;
using AuthentificationForm.BusinessLogic.Services.RoleServices;
using AuthentificationForm.BusinessLogic.Services.UserServices;

namespace AuthenticationForm.Host.ServicesExtensions
{
    public static class AddBussinessLogicServicesExtensions
    {
        public static void AddBussinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthentificator, HttpContextAuthentificator>();
            services.AddScoped<IClaimPrincipalFactory, CookieClaimsFactory>();
        }
    }
}
