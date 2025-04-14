using BookSale.Managerment.Domain.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DotNetEnv;
using dotenv.net;
namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Load file .env
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

            // Lấy biến môi trường xác định môi trường hiện tại
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine($"Current Environment: {environment}");

            // Lấy đường dẫn thư mục gốc của dự án
            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current directory: {basePath}");

            // Tìm file appsettings.json trong các dự án
            string configPath = Path.Combine(basePath, "..\\BookSale.Managerment.Ui", $"appsettings.{environment}json");

            // Nếu không tìm thấy, sử dụng connection string mặc định trong file .env
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            // Nếu có file appsettings, đọc connection string từ đó
            if (File.Exists(configPath))
            {
                try
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(configPath))
                        .AddJsonFile(Path.GetFileName(configPath))
                        .Build();

                    var configConnectionString = configuration.GetConnectionString("DefaultConnection");
                    if (!string.IsNullOrEmpty(configConnectionString))
                    {
                        connectionString = configConnectionString;
                        Console.WriteLine($"Using connection string from config: {connectionString}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading configuration: {ex.Message}");
                }
            }

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}