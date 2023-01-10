using AuthenticationForm.Core.Models;
using AuthentificationForm.DataAccess.EF;
using AuthentificationForm.DataAccess.Repositories.EFImplemetations;
using AuthentificationForm.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationForm.Host.ServicesExtensions
{
    public static class AddDataServicesExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<DataContext>();
        }
    }
}
