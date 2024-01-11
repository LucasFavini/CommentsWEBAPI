﻿using CommentsApp.Entities;

namespace CommentsApp.Services
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetComments();
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int userId, int commentId);
    }
}
