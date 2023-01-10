using AuthenticationForm.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthentificationForm.DataAccess.EF
{
    public class DataContext : IdentityDbContext<User, Role, long>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug); // LogLevel.Debug - default value
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(b => b.DateOfRegistration)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
