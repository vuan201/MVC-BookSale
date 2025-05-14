using System.Security.Claims;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserInfoFilter : IActionFilter
    {
        private readonly IStorageService _cloudinaryService;
        private readonly IUserService _userService;

        public UserInfoFilter(IStorageServiceFactory storageServiceFactory, IUserService userService)
        {
            _cloudinaryService = storageServiceFactory.GetStorageService(StorageType.Cloudinary);
            _userService = userService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var user = context.HttpContext.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    // Sử dụng phương thức đồng bộ hoặc .Result để tránh lỗi DbContext
                    var userIncomation = _userService.GetUserById(userId).GetAwaiter().GetResult();
                    if (userIncomation != null && userIncomation.AvatarUrl != null)
                    {
                        var avatarUrl = userIncomation.AvatarUrl;
                        controller.ViewBag.AvatarUrl = avatarUrl;
                    }
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}