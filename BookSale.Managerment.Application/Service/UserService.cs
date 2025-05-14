using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Managerment.Application.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStorageService _cloundinaryService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IFileService fileService,
            IStorageServiceFactory storageServiceFactory)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileService = fileService;
            _cloundinaryService = storageServiceFactory.GetStorageService(StorageType.Cloudinary);
        }
        public async Task<UserDto?> GetUserByUserName(string username)
        {
            if (!string.IsNullOrEmpty(username))
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
                if (user != null)
                {
                    var userDto = _mapper.Map<UserDto>(user);

                    if(user.Avatar != null)
                    {
                        userDto.AvatarUrl = _cloundinaryService.GetUrlImageByPublicId(user.Avatar.Key);
                    }

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

            var total = query.Where(i => i.IsActive).Count();

            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(x => x.UserName.Contains(filter.search)
                                      || x.FullName.Contains(filter.search)
                                      || x.Email.Contains(filter.search));
            }

            var users = query.Where(i => i.IsActive).Skip(filter.Offset).Take(filter.Limit).ToList();

            return new ResponseModel<List<UserDto>>(true, "Success", _mapper.Map<List<UserDto>>(users));
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
            // Kiểm tra trùng tên đăng nhập
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new ResponseModel(false, "Tên đăng nhập đã tồn tại");

            // Kiểm tra trùng email
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseModel(false, "Email đã tồn tại");

            // Kiểm tra mật khẩu
            if (model.Password == null) return new ResponseModel(false, "Mật khẩu không hợp lệ");

            // Tạo tài khoản mới
            var user = new ApplicationUser();

            _mapper.Map(model, user);

            user.IsActive = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            // Nếu tạo tài khoản thất bại thì trả về lỗi
            if (!result.Succeeded)
                return new ResponseModel(false, string.Join("<br/>", result.Errors.Select(i => i.Description)));
            
            // Thêm role cho user
            await _userManager.AddToRoleAsync(user, model.RoleName);

            // Upload ảnh lên cloudinary và lưu thông tin ảnh vào database
            if (model.Avata != null)
            {
                DateTime now = DateTime.Now;
                var fileName = $"avatar_user_{user.FullName}_{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}";

                // Upload ảnh lên cloud storage
                var uploadResult = await _cloundinaryService.UploadImage(model.Avata, $"{CloudinaryFolder.Avatars}/{fileName}");

                if (uploadResult.Status && uploadResult.Data != null && uploadResult.Data.PublicId != null)
                {
                    var fileDto = new FIleDTO(StorageType.Cloudinary, fileName, uploadResult.Data.PublicId);

                    // Lưu thông tin file vào database
                    var fileResult = await _fileService.SaveFile(fileDto);

                    if(fileResult.Status && fileResult.Data != null)
                    {
                        // Cập nhật thông tin ảnh vào user
                        user.AvatarId = fileResult.Data.Id;

                        await _userManager.UpdateAsync(user);
                    }
                }
            }
            return new ResponseModel(true, "Tạo tài khoản thành công");
        }
        public async Task<ResponseModel> Update(UserDto model)
        {
            if (model.Id == null) return new ResponseModel(false, "Không tìm thấy id");

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null) return new ResponseModel(false, "Người dùng không tồn tại");

            _mapper.Map(model, user);

            if (model.Avata != null)
            {
                DateTime now = DateTime.Now;
                var fileName = $"avatar_user_{user.FullName}_{now.Year}_{now.Month}_{now.Day}_{now.Hour}_{now.Minute}_{now.Second}";

                // Upload ảnh lên cloud storage
                var uploadResult = await _cloundinaryService.UploadImage(model.Avata, $"{CloudinaryFolder.Avatars}/{fileName}");

                if (uploadResult.Status && uploadResult.Data != null && uploadResult.Data.PublicId != null)
                {
                    var newFile = new FIleDTO(StorageType.Cloudinary, fileName, uploadResult.Data.PublicId);

                    if (user.AvatarId != null) newFile.Id = user.AvatarId.Value;

                    // Lưu thông tin file vào database
                    var fileResult = await _fileService.SaveFile(newFile);

                    if (fileResult.Status && fileResult.Data != null)
                    {
                        // Cập nhật thông tin ảnh vào user
                        user.AvatarId = fileResult.Data.Id;
                    }
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, model.RoleName);
                if (!hasRole)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    if (role != null)
                    {
                        var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);

                        if (removeRoleResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, model.RoleName);
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, model.RoleName);
                    }
                }
                return new ResponseModel(true, "Cập nhật tài khoản thành công");
            }
            return new ResponseModel(false, "Cập nhật tài khoản không thành công");
        }
        public async Task<ResponseModel> DeleteUser(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new ResponseModel(false, "Id không được để trống");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return new ResponseModel(false, "Người dùng không tồn tại");

            user.IsActive = false;

            await _userManager.UpdateAsync(user);

            return new ResponseModel(ActionType.Delete, true, "Xóa tài khoản thành công");
        }
    }
}
