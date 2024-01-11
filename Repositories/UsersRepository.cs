using AutoMapper;
using CommentsApp.context;
using CommentsApp.Entities;
using CommentsApp.Models;
using CommentsApp.Services;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UsersDTO?> GetUserComments(int userId)
        {
            var userExists = await _repository.User.AnyAsync(x => x.Id == userId);
            if (userExists)
            {
                var user = await _repository.User.Join(
                    _repository.Comment,
                    comment => comment.Id,
                    user => user.UserId,
                    (user, comment) =>
                    new UsersDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        UserCreation = user.UserCreation,
                        Comments = _repository.Comment.Where(x => x.UserId == userId).ToList(),
                    }).Where(x => x.Id == userId).FirstAsync();

                return _mapper.Map<UsersDTO>(user);
            }
            return null;
        }

        public async Task AddUser(User userToAdd, int userAdminId)
        {
            var user = await _repository.User.Where(x => x.Id == userAdminId).FirstAsync();
            if (user.isAdminUser)
            {
                await _repository.User.AddAsync(userToAdd);
                await _repository.SaveChangesAsync();
            }
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
