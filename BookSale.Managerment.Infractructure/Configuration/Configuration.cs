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
using BookSale.Managerment.Application.Mappings;
using System.Reflection;
using BookSale.Managerment.Application.Abstracts;
using Microsoft.AspNetCore.Authorization;
using BookSale.Managerment.Domain.constants;
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
            service.AddDbContext<ApplicationDbContext>(options => {

                // Sử dụng MySQL với chuỗi kết nối và phiên bản của MySqlServerVersion
                options.UseMySql(connectionString, serverVersion);

                // Cho phép sử dụng lazy loading proxies để tải các đối tượng liên quan 
                options.UseLazyLoadingProxies(); 
            });

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

                // Yêu cầu có số
                options.Password.RequireDigit = true;

                // Yêu cầu có chữ thường
                options.Password.RequireLowercase = true;

                // Yêu cầu có chữ hoa
                options.Password.RequireUppercase = true;

                // Yêu cầu có ký tự đặc biệt
                options.Password.RequireNonAlphanumeric = true;

                // Độ dài tối thiểu
                options.Password.RequiredLength = 6; 
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Cấu hình các tùy chọn về cookie cho xác thực
            service.ConfigureApplicationCookie(options =>
            {
                // Cho phép kéo dài thời gian sống của cookie khi người dùng tiếp tục truy cập
                options.SlidingExpiration = true;

                // Thời gian hết hạn cookie là 30 ngày
                options.ExpireTimeSpan = TimeSpan.FromDays(3); 

                // Tên Cookie
                options.Cookie.Name = "BookSaleManagermentCookie";

                // Đường dẫn đến trang đăng nhập
                options.LoginPath = "/Admin/Authentication/Login";

                // Cho phép kéo dài thời gian sống của cookie khi người dùng tiếp tục truy cập
                options.SlidingExpiration = true;

                // Đường dẫn đến trang không được phép truy cập
                // options.AccessDeniedPath = "/"; 
            });

            // Cấu hình các tùy chọn khác về xác thực và bảo mật
            service.Configure<IdentityOptions>(options =>
            {
                // Cho phép khóa tài khoản cho người dùng mới tạo
                options.Lockout.AllowedForNewUsers = true;

                // Thời gian khóa mặc định là 30 giây
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

                // Số lần thử sai tối đa trước khi khóa tài khoản
                options.Lockout.MaxFailedAccessAttempts = 5; 
            
                // Các ký tự hợp lệ trong tên người dùng
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Email phải duy nhất
                options.User.RequireUniqueEmail = true;
            });
        }
        // Thêm Dependency Injection 
        public static void AddDependencyInjection(this IServiceCollection service)
        {
            // Inject các dịch vụ Identity
            service.AddScoped<PasswordHasher<ApplicationUser>>();
            service.AddScoped<IUnitOfWork, UnitOFWork>();

            // Inject các dịch vụ khác
            service.AddScoped<IAuthenticationService, AuthenticationService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<ICloudStorageService, CloudStorageService>();
            service.AddScoped<IFileService, FileService>();
            service.AddScoped<IGenreService, GenreService>();
            service.AddScoped<ITagService, TagService>();
            // ===================================================
            // Inject các dịch vụ Storage

            // Inject Storage Factory
            service.AddScoped<IStorageServiceFactory, StorageServiceFactory>();
            
            // Inject dịch vụ Cloudinary
            service.AddScoped<CloudinaryService>();

            // Inject AutoMapper
            service.AddAutoMapper(typeof(MappingProfile));
        }

        // Đăng ký AutoMapper
        public static void AddAutoMapperConfiguration(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        }
        // Cấu hình Localization
        public static void LocalizationConfiguration(this IServiceCollection services)
        {
            var supportedCultures = new[] { "vi-VN" };

            // Cấu hình localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Cấu hình các culture được hỗ trợ
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });
        }
        // Cấu hình Authorization
        public static void SetAuthorization(this IServiceCollection services)
        {
            var authorizedAdmin = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Setup.AuthorizedAdminPolicy, authorizedAdmin);
            });
        }

        // Thêm Anti Forgery Header
        public static void AddAntiForgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "__RequestVerificationToken"; // Đặt tên header mong muốn
            });
        }
    }
}