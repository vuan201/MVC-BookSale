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
        // Đăng ký DbContext và cấu hình kết nối cơ sở dữ liệu
        public static void RegisterDb(this IServiceCollection service, IConfiguration confix)
        {
            // Lấy chuỗi kết nối từ file appsettings.json
            var connectionString = confix.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Khởi tạo phiên bản của MySqlServerVersion
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));

            // Đăng ký DbContext và cấu hình kết nối cơ sở dữ liệu
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            // service.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            // Cấu hình dịch vụ Identity
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

            // Cấu hình các tùy chọn về cookie cho xác thực
            service.ConfigureApplicationCookie(options => 
            {
                options.SlidingExpiration = true; // Cho phép kéo dài thời gian sống của cookie khi người dùng tiếp tục truy cập
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // Thời gian hết hạn cookie là 30 ngày
                options.Cookie.Name = "BookSaleManagermentCookie";
                options.LoginPath = "/Admin/Authentication/Login"; // Đường dẫn đến trang đăng nhập
                options.SlidingExpiration = true; // Cho phép kéo dài thời gian sống của cookie khi người dùng tiếp tục truy cập
                // options.AccessDeniedPath = "/"; // Đường dẫn đến trang không được phép truy cập
            });

            // Cấu hình các tùy chọn khác về xác thực và bảo mật
            service.Configure<IdentityOptions>(options => {
                options.Lockout.AllowedForNewUsers = true; // Cho phép khóa tài khoản cho người dùng mới tạo
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30); // Thời gian khóa mặc định là 30 giây
                options.Lockout.MaxFailedAccessAttempts = 5; // Số lần thử sai tối đa trước khi khóa tài khoản
            });
        }
        // Đăng ký các dịch vụ phụ thuộc vào ứng dụng
        public static void AddDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped<PasswordHasher<ApplicationUser>>();
            service.AddScoped<IUnitOfWork, UnitOFWork>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
