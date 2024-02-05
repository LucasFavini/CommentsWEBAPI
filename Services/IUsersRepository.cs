using CommentsApp.Entities;
using CommentsApp.Models;

namespace CommentsApp.Services
{
    public interface IUsersRepository
    {
        Task<List<CommentDTO>> GetUserComment();
        Task<UsersDTO?> GetUserCommentsByID(int userId);
        Task AddUser(User user, int userAdminId);
        Task DeleteUser(int userId, int userAdminId);
        bool CheckUser(string? userPassword);
    }
}
