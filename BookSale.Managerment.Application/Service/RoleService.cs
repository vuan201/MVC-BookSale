using BookSale.Managerment.Application.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BookSale.Managerment.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<SelectListItem>> GetAllRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles.Select(i => new SelectListItem
            {
                Value = i.Name,
                Text = i.Name,
            });
        }
    }
}
