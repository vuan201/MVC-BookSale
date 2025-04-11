using AutoMapper;
using Bogus;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
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
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDTO?> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            // Sử dụng AutoMapper để chuyển đổi từ ApplicationUser sang UserDTO
            return _mapper.Map<UserDTO>(user);
        }

        public ResponseModel<List<UserDTO>> GetUsers(RequestFilterModel filter)
        {
            var query = _userManager.Users.AsQueryable();

            var total = query.Count();

            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(x => x.UserName.Contains(filter.search)
                                      || x.FullName.Contains(filter.search)
                                      || x.Email.Contains(filter.search));
            }

            var users = query.Skip(filter.Offset).Take(filter.Limit).ToList();

            // var fakeUsers = FakeUser(filter.Limit);

            // return new ResponseModel<List<UserDTO>>(true, "Success", filter.Limit * 10, fakeUsers);
            return new ResponseModel<List<UserDTO>>(true, "Success", total, _mapper.Map<List<UserDTO>>(users));
        }
        public async Task<ResponseModel> Save(UserRequestModel model)
        {
            var actionType = string.IsNullOrEmpty(model.Id) ? ActionType.Insert : ActionType.Update;

            // Tạo tài khoản mới
            if (actionType == ActionType.Insert)
            {
                var result = await Create(model);
                // Nếu có lỗi, lấy thông báo lỗi từ Result

                if (result.Status)
                {
                    return new ResponseModel(actionType, true, result.Message);
                }

                return new ResponseModel(actionType, false, $"{actionType.ToString()} : {result.Message}");
            }
            else
            {
                var result = await Update(model);

                if (result.Status)
                {
                    return new ResponseModel(actionType, true, result.Message);
                }

                return new ResponseModel(actionType, false, $"{actionType.ToString()} : {result.Message}");
            }

        }
        public async Task<ResponseModel> Create(UserRequestModel model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ResponseModel(false, "Tên đăng nhập đã tồn tại");

            var user = new ApplicationUser();

            _mapper.Map(model, user);

            user.IsActive = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);

                return new ResponseModel(true, "Tạo tài khoản thành công");
            }
            return new ResponseModel(false, string.Join("<br/>", result.Errors.Select(i => i.Description)));
        }
        public async Task<ResponseModel> Update(UserRequestModel model)
        {
            if (model.Id == null) return new ResponseModel(false, "Không tìm thấy id");

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return new ResponseModel(true, "Cập nhật tài khoản thành công");

            return new ResponseModel(false, "Cập nhật tài khoản không thành công");
        }
        private List<UserDTO> FakeUser(int count)
        {
            var ids = 0;
            var fakeUsers = new Faker<UserDTO>()
                .StrictMode(true)//OrderId is deterministic
                .RuleFor(u => u.Id, f => f.Random.String2(10))
                .RuleFor(u => u.UserName, f => f.Name.FindName())
                .RuleFor(u => u.FullName, f => f.Name.FindName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.IsAdmin, f => true);

            return fakeUsers.Generate(count);
        }
    }
}
