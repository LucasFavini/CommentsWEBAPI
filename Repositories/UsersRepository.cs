using AutoMapper;
using CommentsApp.context;
using CommentsApp.Entities;
using CommentsApp.Models;
using CommentsApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace CommentsApp.Repositories
{
    public class UsersRepository: IUsersRepository
    {
        private readonly ApplicationDbContext _repository;
        private readonly IMapper _mapper;

        public UsersRepository(ApplicationDbContext repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsersDTO?> GetUserCommentsByID(int userId)
        {
            var userExists = await _repository.User.AnyAsync(x => x.Id == userId);
            if (userExists)
            {
                var userAndComments = await _repository.User.Join(
                    _repository.Comment,
                    comment => comment.Id,
                    user => user.UserId,
                    (user, comment) =>
                    new UsersDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        UserCreation = user.UserCreation,
                        Comments = _repository.Comment.Where(x => x.UserId == userId).ToList(),
                    }).Where(x => x.Id == userId).FirstAsync();

                return _mapper.Map<UsersDTO>(userAndComments);
            }
            return null;
        }

        public async Task<List<CommentDTO>> GetUserComment()
        {
            var userAndComments = await _repository.User.Join(
                _repository.Comment,
                comment => comment.Id,
                user => user.UserId,
                (user, comment) =>
                new CommentDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CommentId = comment.Id,
                    CommentDateTime = comment.CommentDateTime,
                    CommentValue = comment.CommentValue,
                    SubComments = _repository.SubComment
                        .Where(subComment => subComment.CommentId == comment.Id)
                        .ToList()
                })
                .OrderBy(x => x.CommentDateTime)
                .ToListAsync();

            return userAndComments;
        }

        public async Task AddUser(User userToAdd, int userAdminId)
        {
            var user = await _repository.User.Where(x => x.Id == userAdminId).FirstAsync();
            var test = _repository.User.Where(x => x.UserName == userToAdd.UserName).AnyAsync().Result;
            if (user.isAdminUser && !test)
            {
                userToAdd.Password = Crypto.HashPassword(userToAdd.Password);
                await _repository.User.AddAsync(userToAdd);
                await _repository.SaveChangesAsync();
            }
            //TODO: Agregar notificacion para que sepa que ya existe ese userName
        }

        public bool CheckUser(string? userPassword)
        {
            var hash = Crypto.HashPassword(userPassword);
            var verified = Crypto.VerifyHashedPassword(hash, userPassword);

            return verified;
        }

        public async Task DeleteUser(int userId, int userAdminId)
        {
            var user = await _repository.User.Where(x => x.Id == userAdminId).FirstAsync();
            var userToDelete = await _repository.User.Where(x => x.Id == userId).FirstAsync();

            if (user.isAdminUser && userToDelete != null)
            {
                _repository.User.Remove(userToDelete);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
