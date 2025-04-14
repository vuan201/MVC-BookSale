using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookSale.Managerment.Domain.Extension;
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
            // Định nghĩa đường dẫn tới file .env (nằm ngoài thư mục BE)
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, ".env");

            // Đọc file .env
            var envVars = LoadEnvFile.Load(filePath);

            // Lấy các biến môi trường từ file .env
            string baseRole = envVars["ROLE"] ?? "SupperAdmin";
            string userName = envVars["SUPPER_ADMIN_USERNAME"] ?? "SupperAdmin";
            string password = envVars["SUPPER_ADMIN_PASSWORD"] ?? "Sa@12345!";
            string fullName = envVars["SUPPER_ADMIN_FULLNAME"] ?? "Supper Admin";
            string email = envVars["SUPPER_ADMIN_EMAIL"] ?? "supperadmin@gmail.com";

            using (var scope = webApplication.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                try
                {
                    // Kiểm tra xem role có tồn tại hay không
                    if (!await roleManager.RoleExistsAsync(baseRole))
                    {
                        await roleManager.CreateAsync(new IdentityRole(baseRole));
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
                            PhoneNumber = "0989771234",
                        };

                        // thiết lập user mạc định
                        var identityUser = await userManager.CreateAsync(baseUser, password);

                        if (identityUser.Succeeded)
                        {
                            // Thêm role cho người dùng mặc định
                            await userManager.AddToRoleAsync(baseUser, baseRole);
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
