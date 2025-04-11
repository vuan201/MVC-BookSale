using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool rememberMe);
        Task Logout();
    }
}