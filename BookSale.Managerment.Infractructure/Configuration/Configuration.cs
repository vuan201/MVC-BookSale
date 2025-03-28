using BookSale.Managerment.DataAccess.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Builder;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.DataAccess.Repository;
using BookSale.Managerment.Application.Service;
namespace BookSale.Managerment.DataAccess.Configuration
{
    public static class Configuration 
    {
        public static void RegisterDb(this IServiceCollection service, IConfiguration confix)
        {
            var connectionString = confix.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));

            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            // service.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            service.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Không yêu cầu xác thực email
                options.SignIn.RequireConfirmedEmail = false;

                // Không yêu cầu xác thực số điện thoại
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Không yêu cầu xác thực tài khoản
                options.SignIn.RequireConfirmedAccount = false;

                // Cấu hình chính sách mật khẩu (tùy chọn)
                options.Password.RequireDigit = true; // Yêu cầu có số
                options.Password.RequireLowercase = true; // Yêu cầu có chữ thường
                options.Password.RequireUppercase = true; // Yêu cầu có chữ hoa
                options.Password.RequireNonAlphanumeric = true; // Yêu cầu có ký tự đặc biệt
                options.Password.RequiredLength = 6; // Độ dài tối thiểu
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            service.ConfigureApplicationCookie(options => 
            {
                options.Cookie.Name = "BookSaleManagermentCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(30); // Thời gian hết hạn cookie là 30 ngày
                options.SlidingExpiration = true; // Cho phép kéo dài thời gian sống của cookie khi người dùng tiếp tục truy cập
                options.LoginPath = "/Admin/Authentication/Login"; // Đường dẫn đến trang đăng nhập
                // options.AccessDeniedPath = "/"; // Đường dẫn đến trang không được phép truy cập
            });
        }
        public static void AddDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped<PasswordHasher<ApplicationUser>>();
            service.AddScoped<IUnitOfWork, UnitOFWork>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
