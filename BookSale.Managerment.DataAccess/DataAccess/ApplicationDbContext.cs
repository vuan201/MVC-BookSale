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
        public DbSet<CloudStorages> CloudStorages { get; set; }
        public DbSet<Files> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
