using BookSale.Managerment.Application.DTOs;
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

        public async Task<ResponseModel> CheckLogin(string username, string password, bool rememberMe)
        {
            // Kiểm tra người dùng tồn tại
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return new ResponseModel(false, "Tài khoản không tồn tại");
            }
            // Kiểm tra email đã được xác thực chưa (nếu yêu cầu xác thực email)
            if (await _userManager.IsEmailConfirmedAsync(user) == false && _userManager.Options.SignIn.RequireConfirmedEmail)
            {
                return new ResponseModel(false, "Email chưa được xác thực");
            }

            // Kiểm tra trạng thái IsActive của tài khoản
            if (!user.IsActive)
            {
                return new ResponseModel(false, "Tài khoản đã bị vô hiệu hóa");
            }

            // Kiểm tra mật khẩu có khớp không
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: rememberMe,lockoutOnFailure: true);
            if(result.IsLockedOut)
            {
                // Nếu tài khoản bị khóa, trả về thông báo và thời gian còn lại để mở khóa
                if( user.LockoutEnd != null)
                {
                    var remainingLockot = user.LockoutEnd.Value - DateTimeOffset.UtcNow;
                    return new ResponseModel(false, $"Tài khoản đã bị khóa, vui lòng thử lại sau {Math.Round(remainingLockot.TotalSeconds)}");
                }
            }
            if (result.Succeeded)
            {
                // Xóa AccessFailedCount khi đăng nhập thành công
                if(user.AccessFailedCount > 0)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                }

                return new ResponseModel(true, "Đăng nhập thành công");
            }
            // Kiểm tra trạng thái IsActive của tài khoản
            else if (!user.IsActive)
            {
                return new ResponseModel(false, "Tài khoản đã bị vô hiệu hóa");
            }
            else if (result.IsLockedOut)
            {
                return new ResponseModel(false, "Tài khoản đã bị khóa");
            }
            else if (result.IsNotAllowed)
            {
                return new ResponseModel(false, "Tài khoản không được phép đăng nhập");
            }
            else
            {
                return new ResponseModel(false, "Tài khoản hoặc mật khẩu không chính xác");

                // return new ResponseModel(false, "Đăng nhập thất bại: " + (result.ToString() ?? "Lỗi không xác định"));
            }
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
