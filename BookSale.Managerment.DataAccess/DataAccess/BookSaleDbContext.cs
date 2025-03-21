using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class BookSaleDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public BookSaleDbContext(DbContextOptions options) : base(options)
        {
        }

        // khai báo các table
        public DbSet<Books> Books { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<BookTags> BookTags { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Tags> Tags { get; set; }

        // sửa tên các table entity tự build
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<IdentityUser>().ToTable("Users");
            // modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            // modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            // modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            // modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaim");
            // modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersToken");
            // modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
        }
    }
}
