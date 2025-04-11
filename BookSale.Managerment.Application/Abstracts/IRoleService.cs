using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IRoleService
    {
        Task<IEnumerable<SelectListItem>> GetAllRole();
    }
}