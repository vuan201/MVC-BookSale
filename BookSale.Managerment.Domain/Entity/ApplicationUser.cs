using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(500)]
        public string FullName { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public bool IsActive { get; set; }

    }
}
