using CommentsApp.Entities;
using CommentsApp.Models;

namespace CommentsApp.Services
{
    public interface IUsersRepository
    {
        Task<UsersDTO?> GetUserComments(int userId);
        Task AddUser(User user, int userAdminId);
        Task DeleteUser(int userId, int userAdminId);

    }
}
