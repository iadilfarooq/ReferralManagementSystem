using ReferralManagementSystem.Models;

namespace ReferralManagementSystem.Repository.IRepository
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetUsersDetailsAsync();
        Task CreateUserAsync(Users users, IFormFile picture);
        Task<Users> GetUserByIdAsync(int id);
        Task UpdateUserAsync(Users users,IFormFile picture);
        Task DeleteUserAsync(int id);
        Task<Users> AuthenticateAsync(string username, string password);
    }
}