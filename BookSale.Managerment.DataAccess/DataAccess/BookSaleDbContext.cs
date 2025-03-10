using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.DataAccess.DataAccess
{
    public class BookSaleDbContext : IdentityDbContext
    {
        public BookSaleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
