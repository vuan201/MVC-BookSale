using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class BookSaleDbContextFactory : IDesignTimeDbContextFactory<BookSaleDbContext>
    {
        public BookSaleDbContext CreateDbContext(string[] args)
        {
            // Lấy đường dẫn thư mục gốc của dự án
            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current directory: {basePath}");

            // Tìm file appsettings.json trong các dự án
            string[] possiblePaths = {
                Path.Combine(basePath, "appsettings.json"),
                Path.Combine(basePath, "..\\BookSale.Managerment", "appsettings.json"),
                Path.Combine(basePath, "..\\BookSale.Managerment.Ui", "appsettings.json")
            };

            // Nếu không tìm thấy, sử dụng connection string mặc định
            var connectionString = "Server=localhost;Port=3306;Database=BookStore;User=root;Password=123456;";

            // Tìm file cấu hình đầu tiên tồn tại
            string configPath = null;
            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    configPath = path;
                    Console.WriteLine($"Found configuration file at: {configPath}");
                    break;
                }
            }

            // Nếu tìm thấy file cấu hình, đọc connection string từ đó
            if (configPath != null)
            {
                try
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(connectionString))
                        .AddJsonFile(Path.GetFileName(connectionString))
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
            
            var optionsBuilder = new DbContextOptionsBuilder<BookSaleDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            return new BookSaleDbContext(optionsBuilder.Options);
        }
    }
}