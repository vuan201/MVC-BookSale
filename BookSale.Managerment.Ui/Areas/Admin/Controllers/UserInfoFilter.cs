using System.Security.Claims;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Domain.constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BookSale.Managerment.Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserInfoFilter : IActionFilter
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UserInfoFilter(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
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
                    var avatarUrl = _cloudinaryService.GetUrlImageByPublicId($"{CloudinaryFolder.Avatars}/{userId}");
                    controller.ViewBag.AvatarUrl = avatarUrl;
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing
        }
    }
}