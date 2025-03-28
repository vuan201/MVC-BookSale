using BookSale.Managerment.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool Success, string Message)> CheckLogin(string username, string password)
        {
            // Kiểm tra người dùng tồn tại
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return (false, "Tài khoản không tồn tại");
            }

            // Kiểm tra tài khoản có bị khóa không
            if (await _userManager.IsLockedOutAsync(user))
            {
                return (false, "Tài khoản đã bị khóa");
            }

            // Kiểm tra email đã được xác thực chưa (nếu yêu cầu xác thực email)
            if (await _userManager.IsEmailConfirmedAsync(user) == false && _userManager.Options.SignIn.RequireConfirmedEmail)
            {
                return (false, "Email chưa được xác thực");
            }

            // Kiểm tra trạng thái IsActive của tài khoản
            if (!user.IsActive)
            {
                return (false, "Tài khoản đã bị vô hiệu hóa");
            }

            // Kiểm tra mật khẩu có khớp không
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded)
            {
                return (true, "Đăng nhập thành công");
            }
            // Kiểm tra trạng thái IsActive của tài khoản
            else if (!user.IsActive)
            {
                return (false, "Tài khoản đã bị vô hiệu hóa");
            }
            else if (result.IsLockedOut)
            {
                return (false, "Tài khoản đã bị khóa");
            }
            else if (result.IsNotAllowed)
            {
                return (false, "Tài khoản không được phép đăng nhập");
            }
            else
            {
                // Kiểm tra trực tiếp mật khẩu để xác định rõ hơn
                var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordCheck)
                {
                    return (false, "Mật khẩu không chính xác");
                }

                return (false, "Đăng nhập thất bại: " + (result.ToString() ?? "Lỗi không xác định"));
            }
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
