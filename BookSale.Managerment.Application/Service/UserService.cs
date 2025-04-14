using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
using Microsoft.AspNetCore.Identity;

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
        public async Task<UserDto?> GetUserByUserName(string username)
        {
            if(!string.IsNullOrEmpty(username))
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                    return _mapper.Map<UserDto>(user);
            }
            return null;
        }
        public async Task<UserDto?> GetUserById(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null) {
                    var userDto = _mapper.Map<UserDto>(user);

                    var roles = await _userManager.GetRolesAsync(user);
                    userDto.RoleName = roles.FirstOrDefault();
                    return userDto;
                }
            }

            return null;
        }
        public ResponseModel<List<UserDto>> GetUsers(RequestFilterModel filter)
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

            return new ResponseModel<List<UserDto>>(true, "Success", total, _mapper.Map<List<UserDto>>(users));
        }
        public async Task<ResponseModel> Save(UserDto model)
        {
            var actionType = string.IsNullOrEmpty(model.Id) ? ActionType.Insert : ActionType.Update;

            // Tạo tài khoản mới
            if (actionType == ActionType.Insert)
            {
                var result = await Create(model);

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
        public async Task<ResponseModel> Create(UserDto model)
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
        public async Task<ResponseModel> Update(UserDto model)
        {
            if (model.Id == null) return new ResponseModel(false, "Không tìm thấy id");

            var user = await _userManager.FindByIdAsync(model.Id);

            if(user == null) return new ResponseModel(false, "Không tìm thấy tài khoản");

            _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, model.RoleName);
                if (!hasRole)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    if(role != null){
                        var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);

                        if (removeRoleResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, model.RoleName);
                        }
                    }
                    else {
                        await _userManager.AddToRoleAsync(user, model.RoleName);
                    }
                }
                return new ResponseModel(true, "Cập nhật tài khoản thành công");
            }

            return new ResponseModel(false, "Cập nhật tài khoản không thành công");
        }
    }
}
