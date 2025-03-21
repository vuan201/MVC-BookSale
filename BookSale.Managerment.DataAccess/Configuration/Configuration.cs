using BookSale.Managerment.DataAccess.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using BookSale.Managerment.Domain.Entity;
namespace BookSale.Managerment.DataAccess.Configuration
{
    public static class Configuration 
    {
        public static void RegisterDb(this IServiceCollection service, IConfiguration confix)
        {
            var connectionString = confix.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));

            service.AddDbContext<BookSaleDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            service.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BookSaleDbContext>();
        }

    }
}
