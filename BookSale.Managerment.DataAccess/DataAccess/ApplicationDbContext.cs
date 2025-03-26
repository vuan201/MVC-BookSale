using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // khai báo các table
        public DbSet<Books> Books { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<BookTags> BookTags { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Tags> Tags { get; set; }

        // sửa tên các table entity tự build
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.Entity<IdentityUser>().ToTable("Users");
            // builder.Entity<IdentityRole>().ToTable("Roles");
            // builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            // builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            // builder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaim");
            // builder.Entity<IdentityUserToken<string>>().ToTable("UsersToken");
            // builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");

            // Có thể tạo tài khoản khởi đầu cho server
            // builder.Entity<ApplicationUser>().HasData(
            //     new ApplicationUser(),
            //     new ApplicationUser(),
            //     new ApplicationUser(),
            //     new ApplicationUser()
            // );
        }
    }
}
