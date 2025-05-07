using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookSale.Managerment.Domain.Extension;
using DotNetEnv;
using dotenv.net;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain;
using CloudinaryDotNet.Actions;
namespace BookSale.Managerment.DataAccess
{
    public static class ConfigurationService
    {
        public static async Task AutoMigrations(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                /*
                    - Kiểm tra xem cơ sở dữ liệu đã tồn tại chưa, nếu chưa, nó sẽ tạo cơ sở dữ liệu và các bảng tương ứng.
                    - Không hỗ trợ EF Core Migrations, nghĩa là nếu bạn thay đổi mô hình dữ liệu (thêm/sửa cột), bạn phải xóa và tạo lại cơ sở dữ liệu.
                    - Thích hợp cho các dự án thử nghiệm hoặc các ứng dụng nhỏ, không dùng khi làm việc với cơ sở dữ liệu đã triển khai thực tế. 
                    - câu lệnh appContext.Database.EnsureCreated()
                */

                /*
                // generate database
                    - Hỗ trợ thay đổi dữ liệu mà không cần xóa toàn bộ cơ sở dữ liệu.
                    - Khi bạn thay đổi mô hình dữ liệu (DbContext và các DbSet<>), bạn chỉ cần tạo migration và cập nhật CSDL.
                    - Dùng trong các ứng dụng thực tế để tránh mất dữ liệu. 
                */
                await appContext.Database.MigrateAsync();
            }
        }
        public static async Task SeedData(this WebApplication webApplication, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            var baseRoleList = Roles.GetRoles();

            // Load file .env
            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));

            // Lấy các biến môi trường từ file .env
            string userName = Environment.GetEnvironmentVariable("SUPPER_ADMIN_USERNAME") ?? Setup.UserName;
            string password = Environment.GetEnvironmentVariable("SUPPER_ADMIN_PASSWORD") ?? Setup.password;
            string fullName = Environment.GetEnvironmentVariable("SUPPER_ADMIN_FULLNAME") ?? Setup.FullName;
            string email = Environment.GetEnvironmentVariable("SUPPER_ADMIN_EMAIL") ?? Setup.Email;

            using (var scope = webApplication.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                try
                {
                    // Thêm tất cả các role trong code vào db nếu chưa có (Tự động hóa đồng bộ khi chỉ cần thêm role vào code)
                    if(baseRoleList != null)
                    {
                        foreach( var role in baseRoleList)
                        {
                            if (!await roleManager.RoleExistsAsync(role))
                            {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }
                        }
                    }

                    // Kiểm tra xem người dùng có tồn tại hay không, nếu không thì tạo mới
                    var existUser = await userManager.FindByNameAsync(userName);
                    if (existUser == null)
                    {
                        // Tạo người dùng mặc định
                        var baseUser = new ApplicationUser
                        {
                            UserName = userName,
                            FullName = fullName,
                            Email = email,
                            NormalizedEmail = email,
                            Address = "Việt Nam",
                            IsActive = true,
                            AccessFailedCount = 0,
                            PhoneNumber = Setup.PhoneNumber,
                        };

                        // thiết lập user mạc định
                        var identityUser = await userManager.CreateAsync(baseUser, password);

                        if (identityUser.Succeeded)
                        {
                            // Thêm role cho người dùng mặc định
                            await userManager.AddToRoleAsync(baseUser, Roles.SupperAdmin);
                        }

                        // Xác nhận email của người dùng mặc định
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(baseUser);
                        await userManager.ConfirmEmailAsync(baseUser, token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
