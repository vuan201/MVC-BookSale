using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public interface IUserService
    {
        Task<(bool Success, string Message)> CheckLogin(string username, string password);
    }
}