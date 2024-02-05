using AutoMapper;
using CommentsApp.context;
using CommentsApp.Entities;
using CommentsApp.Models;
using CommentsApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CommentsApp.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _repository;
        private readonly IMapper _mapper;
        public CommentsRepository(ApplicationDbContext repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _repository.Comment.ToListAsync();
        }

        public async Task AddComment(Comment comment)
        {
            await _repository.Comment.AddAsync(comment);
            await _repository.SaveChangesAsync();
        }

        public async Task AddSubComment(SubComment subComment)
        {
            await _repository.SubComment.AddAsync(subComment);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            var user = _repository.User.Where(user => user.Id == comment.UserId).Select(x => x.Id).FirstOrDefaultAsync().Result;
            if (user > 0)
            {
                var comments = await GetUserAndComment();
                var commentToEdit = comments.Where(x => x.Id == comment.Id).FirstOrDefault();
                if (commentToEdit != null)
                {
                    commentToEdit.CommentValue = comment.CommentValue;
                    _repository.Comment.Update(commentToEdit);
                    await _repository.SaveChangesAsync();
                }
            }

        }

        public async Task DeleteComment(int userId, int commentId)
        {

            var user = _repository.User.Where(user => user.Id == userId).FirstOrDefaultAsync().Result;
            if (user != null)
            {
                {
                    var comments = await GetUserAndComment();
                    var commentToDelete = comments.Where(x => x.Id == commentId).FirstOrDefault();
                    if (commentToDelete != null && (user.Id == commentToDelete.UserId || user.isAdminUser))
                    {
                        _repository.Comment.Remove(commentToDelete);
                        await _repository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task DeleteSubComment(int commentId, string userName)
        {
            var subComment = _repository.SubComment.Where(x => x.CommentId == commentId && x.UserName == userName).FirstOrDefault();
            _repository.SubComment.Remove(subComment);
            await _repository.SaveChangesAsync();
        }

        private async Task<List<Comment>> GetUserAndComment()
        {
            return await _repository.Comment.Join(
                    _repository.User,
                    user => user.UserId,
                    comment => comment.Id,
                    (comment, user) =>
                    new Comment
                    {
                        Id = comment.Id,
                        UserId = comment.UserId,
                        CommentDateTime = comment.CommentDateTime,
                        CommentValue = comment.CommentValue
                    }).ToListAsync();
        }
    }
}
