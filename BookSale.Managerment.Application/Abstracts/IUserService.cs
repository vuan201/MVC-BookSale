using System.Threading.Tasks;
using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByUserName(string username);
        Task<UserDto?> GetUserById(string userId);
        ResponseModel<List<UserDto>> GetUsers(RequestFilterModel filter);
        Task<ResponseModel> Save(UserDto model);
        Task<ResponseModel> Create(UserDto model);
        Task<ResponseModel> Update(UserDto model);
    }
}