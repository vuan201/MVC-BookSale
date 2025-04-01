using System.Threading.Tasks;
using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Service
{
    public interface IUserService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool rememberMex);
        Task Logout();
        // Task<bool> Register(string username, string password);
        Task<UserDTO?> GetUserByUsername(string username);
        ResponseModel<List<UserDTO>> GetAllUsers(RequestFilterModel filter);
    }
}