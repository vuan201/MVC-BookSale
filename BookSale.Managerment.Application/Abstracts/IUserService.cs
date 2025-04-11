using System.Threading.Tasks;
using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserByUsername(string username);
        ResponseModel<List<UserDTO>> GetUsers(RequestFilterModel filter);
        Task<ResponseModel> Save(UserRequestModel model);
        Task<ResponseModel> Create(UserRequestModel model);
    }
}